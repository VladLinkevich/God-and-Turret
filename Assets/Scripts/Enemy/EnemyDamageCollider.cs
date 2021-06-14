using System;
using UI;
using UnityEngine;

namespace Enemy
{
    public class EnemyDamageCollider : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Enter Damage");

            if (other.CompareTag("Turret"))
            {
                HealthHandler.Instance.RemoveHealth();
            }
        }
    }
}