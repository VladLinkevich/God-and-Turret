using System;
using UnityEngine;

namespace Turret
{
    public class TurretTouchTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Enter");
                Messenger.Broadcast(GameEvent.READYTOTOUCHTURRET);
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Messenger.Broadcast(GameEvent.STOPTOTOUCHTURRET);
            }
        }
    }
}
