using System;
using System.Collections;
using UnityEngine;

namespace Room
{
    public class ChangeRoomHandler : MonoBehaviour
    {
        private Camera _camera;

        private Vector2Int _lastPostion;
        private Vector2Int _currentPosition;
        private Direction _changeDirection;
        
        private Room[,] _spawnedRooms;
        private Room _currentRoom;
        
        private void Start()
        {
            _camera = Camera.main;
            
            _lastPostion = new Vector2Int(5, 5);
            _currentPosition = new Vector2Int(5, 5);
            _changeDirection = Direction.None;
            
            _spawnedRooms = GetComponent<RoomsSpawner>().SpawnedRooms;
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
                _currentRoom.CloseDoor();
                StartCoroutine(OpenDoor());
            }
        }

        IEnumerator OpenDoor()
        {
            yield return new WaitForSeconds(5f);
            _currentRoom.OpenDoor();
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
                _spawnedRooms[_lastPostion.x, _lastPostion.y].gameObject.SetActive(false);
                _lastPostion = _currentPosition;
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
