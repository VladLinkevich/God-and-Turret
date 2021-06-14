using System;
using DG.Tweening;
using Game;
using UnityEngine;
using UnityEngine.Serialization;

namespace Bullet
{
    public class Bullet : MonoBehaviour
    {
        public float MaxDistance;
        public float Velocity;

        public bool _isEnemyBullet;

        private Tween _tween;
        
        public void OnEnable()
        { 
            //float angle = transform.rotation.z;

            _tween.Kill();

            _tween = transform.DOMove(transform.up * MaxDistance,
                MaxDistance / Velocity);

            //_controller.velocity = transform.up * Velocity;

            //_rb.velocity = new Vector2(- Mathf.Sin(Mathf.Deg2Rad * angle),
            //    Mathf.Cos(Mathf.Deg2Rad * angle)) * Velocity;
        }
        

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Hit");
            
            if ((other.CompareTag("Turret") == true && _isEnemyBullet == false) ||
                (other.CompareTag("Enemy") == true && _isEnemyBullet == true))
            {
                return;
            }

            if (other.CompareTag("Enemy") == true && _isEnemyBullet == false)
            {
                Debug.Log("kill enemy");
                
                Enemy.Enemy enemy = other.GetComponentInParent<Enemy.Enemy>();

                if (enemy == true)
                {
                    enemy.RemoveHealth();
                }
            }
            
            if (other.CompareTag("Turret") == true && _isEnemyBullet == true)
            {
                Debug.Log("turret hit");
            }
            
            _tween.Kill();
            gameObject.SetActive(false);
        }
        
    }
}