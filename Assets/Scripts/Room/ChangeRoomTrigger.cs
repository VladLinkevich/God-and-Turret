using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Room
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class ChangeRoomTrigger : MonoBehaviour
    {
        public Direction direction;
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Enter");
            Messenger<Direction>.Broadcast(GameEvent.STARTCHANGEROOM, direction);
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            Debug.Log("Exit");
            Messenger<Direction>.Broadcast(GameEvent.STOPCHANGEROOM, direction);
        }
    }
}
