using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Room
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class ChangeRoomTrigger : MonoBehaviour
    {
        public Direction direction;
        
        private BoxCollider2D _collider;
        
        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
        }

        private void OnEnable()
        {
            Messenger.AddListener(GameEvent.PLAYERTOUCHTURRET, OnTouchTurret);
            Messenger.AddListener(GameEvent.PLAYERSTOPTOUCHTURRET, OnStopTouchTurret);
        }
        
        private void OnDisable()
        {
            Messenger.RemoveListener(GameEvent.PLAYERTOUCHTURRET, OnTouchTurret);
            Messenger.RemoveListener(GameEvent.PLAYERSTOPTOUCHTURRET, OnStopTouchTurret);
        }

        private void OnTouchTurret()
        {
            _collider.isTrigger = true;
        }

        private void OnStopTouchTurret()
        {
            _collider.isTrigger = false;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Messenger<Direction>.Broadcast(GameEvent.STARTCHANGEROOM, direction);
            }
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Messenger.Broadcast(GameEvent.HELPSHOWTURRET);
                Debug.Log("CollisionEnter");
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Messenger<Direction>.Broadcast(GameEvent.STOPCHANGEROOM, direction);
            }
        }
    }
}
