using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Room
{
    public class RoomsSpawner : MonoBehaviour
    {
        public Transform Parent;
        
        public Room[] RoomPrefabs;
        public Room StartingRoom;
        
        private Room[,] spawnedRooms;

        public Room[,] SpawnedRooms => spawnedRooms;

        private void Awake()
        {
            spawnedRooms = new Room[11, 11];
            spawnedRooms[5, 5] = Instantiate(StartingRoom, Parent);

            for (int i = 0; i < 12; i++)
            {
                PlaceOneRoom();
            }
        }

        private void PlaceOneRoom()
        {
            HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>();
            for (int x = 0; x < spawnedRooms.GetLength(0); x++)
            {
                for (int y = 0; y < spawnedRooms.GetLength(1); y++)
                {
                    if (spawnedRooms[x, y] == null) continue;

                    int maxX = spawnedRooms.GetLength(0) - 1;
                    int maxY = spawnedRooms.GetLength(1) - 1;

                    if (x > 0 && spawnedRooms[x - 1, y] == null) vacantPlaces.Add(new Vector2Int(x - 1, y));
                    if (y > 0 && spawnedRooms[x, y - 1] == null) vacantPlaces.Add(new Vector2Int(x, y - 1));
                    if (x < maxX && spawnedRooms[x + 1, y] == null) vacantPlaces.Add(new Vector2Int(x + 1, y));
                    if (y < maxY && spawnedRooms[x, y + 1] == null) vacantPlaces.Add(new Vector2Int(x, y + 1));
                }
            }
            
            Room newRoom = Instantiate(RoomPrefabs[Random.Range(0, RoomPrefabs.Length)], Parent);

            Vector2Int position = vacantPlaces.ElementAt(Random.Range(0, vacantPlaces.Count));
            newRoom.transform.position = new Vector3((position.x - 5) * 18, (position.y - 5) * 10, 0);

            ConnectToSomething(newRoom, position);
            
            spawnedRooms[position.x, position.y] = newRoom;
            newRoom.gameObject.SetActive(false);
        }

        private static void ActivateDoor(Room newRoom)
        {
            newRoom.DoorUp.SetActive(true);
            newRoom.DoorDown.SetActive(true);
            newRoom.DoorLeft.SetActive(true);
            newRoom.DoorRight.SetActive(true);
        }

        private bool ConnectToSomething(Room room, Vector2Int p)
        {
            int maxX = spawnedRooms.GetLength(0) - 1;
            int maxY = spawnedRooms.GetLength(1) - 1;

            List<Vector2Int> neighbours = new List<Vector2Int>();

            if (room.DoorUp != null &&
                p.y < maxY &&
                spawnedRooms[p.x, p.y + 1]?.DoorDown != null)
            {
                neighbours.Add(Vector2Int.up);
            }
            
            if (room.DoorDown != null &&
                p.y > 0 &&
                spawnedRooms[p.x, p.y - 1]?.DoorUp != null)
            {
                neighbours.Add(Vector2Int.down);
            }
            
            if (room.DoorLeft != null &&
                p.x > 0 &&
                spawnedRooms[p.x - 1, p.y]?.DoorRight != null)
            {
                neighbours.Add(Vector2Int.left);
            }
            
            if (room.DoorRight != null &&
                p.x < maxX &&
                spawnedRooms[p.x + 1, p.y]?.DoorLeft != null)
            {
                neighbours.Add(Vector2Int.right);
            }

            if (neighbours.Count == 0) return false;
            
            Vector2Int selectedDirection = neighbours[Random.Range(0, neighbours.Count)];
            Room selectedRoom = spawnedRooms[p.x + selectedDirection.x, p.y + selectedDirection.y];
            
            if(selectedDirection == Vector2Int.up)
            {
                room.DoorUp.SetActive(false);
                selectedRoom.DoorDown.SetActive(false);
            }
            else if (selectedDirection == Vector2Int.down)
            {
                room.DoorDown.SetActive(false);
                selectedRoom.DoorUp.SetActive(false);
            }
            else if (selectedDirection == Vector2Int.right)
            {
                room.DoorRight.SetActive(false);
                selectedRoom.DoorLeft.SetActive(false);
            }
            else if (selectedDirection == Vector2Int.left)
            {
                room.DoorLeft.SetActive(false);
                selectedRoom.DoorRight.SetActive(false);
            }

            return true;
        }
    }
}
