

using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy.EnemyType
{
    public class Skull : Enemy
    {
        public float AttackVelocity;
        public override EnemyType Type => EnemyType.Skull;

        private Rigidbody2D _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void OnEnable()
        {
            int rand = Random.Range(0, 2);
            _target = rand == 0 ? TurretTransform : PlayerTransform;
            base.OnEnable();
        }
        
        public override void Attack(Vector3 target)
        {
            Vector3 direction = target - transform.position;
            direction.Normalize();

            _rb.AddForce(direction * AttackVelocity, ForceMode2D.Impulse);
            
            //StartCoroutine(AttackAnimation(direction));
        }

        private void AttackMover(Vector3 direction)
        {
            transform.Translate(direction * (AttackVelocity * Time.deltaTime));
        }

        private IEnumerator AttackAnimation(Vector3 direction)
        {
            bool flag = true;
            float startTime = Time.time;
            
            while (flag)
            {
                //Debug.Log("Attack");
                AttackMover(direction);
                
                if (startTime + 1.0f < Time.time)
                {
                    flag = false;
                }
                yield return null;
            }
        }
    }
}