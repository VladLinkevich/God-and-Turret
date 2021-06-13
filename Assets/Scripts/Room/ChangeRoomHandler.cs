using System;
using System.Collections;
using Enemy;
using Game;
using Player;
using UnityEngine;

namespace Room
{
    public class ChangeRoomHandler : MonoBehaviour
    {
        private Camera _camera;

        private Vector2Int _lastPosition;
        private Vector2Int _currentPosition;
        private Direction _changeDirection;

        private EnemySpawner _enemySpawner;
        private Room[,] _spawnedRooms;
        private Room _currentRoom;
        
        private void Start()
        {
            _camera = Camera.main;
            
            _lastPosition = new Vector2Int(5, 5);
            _currentPosition = new Vector2Int(5, 5);
            _changeDirection = Direction.None;
            
            _spawnedRooms = GetComponent<RoomsSpawner>().SpawnedRooms;
            _enemySpawner = GetComponent<EnemySpawner>();
        }

        private void OnEnable()
        {
            Messenger<Direction>.AddListener(GameEvent.STARTCHANGEROOM, StartChangePlayRoom);
            Messenger<Direction>.AddListener(GameEvent.STOPCHANGEROOM, EndChangePlayRoom);
        }
        
        private void OnDisable()
        {
            Messenger<Direction>.RemoveListener(GameEvent.STARTCHANGEROOM, StartChangePlayRoom);
            Messenger<Direction>.RemoveListener(GameEvent.STOPCHANGEROOM, EndChangePlayRoom);
        }
        
        private void StartChangePlayRoom(Direction direction)
        {
            if (_changeDirection != Direction.None) return;

            _changeDirection = direction;
            
            SelectedDirection();

            PrepareRoom();
        }

        private void PrepareRoom()
        {
            _currentRoom = _spawnedRooms[_currentPosition.x, _currentPosition.y];
            _currentRoom.gameObject.SetActive(true);

            if (_currentRoom.isAttacked == true)
            {
                _enemySpawner.StartSpawn(_currentRoom);
                _currentRoom.CloseDoor();
                StartCoroutine(OpenDoor());
            }
        }

        IEnumerator OpenDoor()
        {
            bool flag = true;
            
            yield return new WaitForSeconds(0.5f);
            
            while (flag)
            {
                if (GameHandler.Instance.IsAttack == false)
                {
                    _currentRoom.OpenDoor();
                    flag = false;
                }

                yield return null;
            }
        }

        private void SelectedDirection()
        {
            Vector3 p = _camera.transform.position;
            
            if (_changeDirection == Direction.Up)
            {
                _camera.transform.position = new Vector3(p.x, p.y + 10f, p.z);
                _currentPosition += Vector2Int.up;
            }

            if (_changeDirection == Direction.Down)
            {
                _camera.transform.position = new Vector3(p.x, p.y - 10f, p.z);
                _currentPosition += Vector2Int.down;
            }

            if (_changeDirection == Direction.Right)
            {
                _camera.transform.position = new Vector3(p.x + 18f, p.y, p.z);
                _currentPosition += Vector2Int.right;
            }

            if (_changeDirection == Direction.Left)
            {
                _camera.transform.position = new Vector3(p.x - 18f, p.y, p.z);
                _currentPosition += Vector2Int.left;
            }
        }

        private void EndChangePlayRoom(Direction direction)
        {
            if (_changeDirection == direction)
            {
                _spawnedRooms[_lastPosition.x, _lastPosition.y].gameObject.SetActive(false);
                _lastPosition = _currentPosition;
                _changeDirection = Direction.None;
            }
        }
    }
    


    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        None
    }
}
