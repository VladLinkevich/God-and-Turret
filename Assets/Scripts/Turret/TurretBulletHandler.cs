using System;
using System.Collections;
using Bullet;
using UnityEngine;
using UnityEngine.Serialization;

namespace Turret
{
    public class TurretBulletHandler : MonoBehaviour
    {
        public Animator animator;

        public Transform rightSpawn;
        public Transform leftSpawn;
        public float reloadTime;

        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Idle = Animator.StringToHash("Idle");

        private float _rightReload;
        private float _leftReload;
        private bool _isBattle;
        private void OnEnable()
        {
            Messenger.AddListener(GameEvent.STARTBATTLE, StartBattle);
            Messenger.AddListener(GameEvent.STOPBATTLE, StopBattle);

            if (_isBattle == true)
            {
                StartBattle();
            }
        }

        private void OnDisable()
        {
            Messenger.AddListener(GameEvent.STARTBATTLE, StartBattle);
            Messenger.AddListener(GameEvent.STOPBATTLE, StopBattle);
        }
        
        private void StartBattle()
        {
            _isBattle = true;
            animator.SetTrigger(Attack);

            _rightReload = Time.time;
            _leftReload = Time.time + reloadTime / 2;

            // press F метрвым куратинам
            //StartCoroutine(BulletSpawn(rightSpawn, 0));
            //StartCoroutine(BulletSpawn(leftSpawn, reloadTime/2));
        }
        
        private void StopBattle()
        {
            _isBattle = false;
            animator.SetTrigger(Idle);
        }

        private IEnumerator BulletSpawn(Transform spawnTransform, float startDelay)
        {
            yield return new WaitForSeconds(startDelay);

            while (_isBattle)
            {
                Bullet.Bullet bullet = BulletFactory.Instance.GetFreeBullet();
                bullet.gameObject.SetActive(true);

                var bulletTransform = bullet.transform;
                bulletTransform.rotation = spawnTransform.rotation;
                bulletTransform.position = spawnTransform.position;

                yield return new WaitForSeconds(reloadTime);
            }
        }

        private void Update()
        {
            if (_isBattle)
            {
                if (_rightReload + reloadTime < Time.time)
                {
                    SpawnBullet(rightSpawn);
                    _rightReload = Time.time;
                    _leftReload = Time.time + reloadTime / 2;
                }
                
                if (_leftReload < Time.time)
                {
                    SpawnBullet(leftSpawn);
                    _leftReload = Time.time + 100f;
                }
                
                transform.Rotate(new Vector3(0,0,1));
            }
        }

        private void SpawnBullet(Transform spawnTransform)
        {
            Bullet.Bullet bullet = BulletFactory.Instance.GetFreeBullet();
            bullet._isEnemyBullet = false;

            var bulletTransform = bullet.transform;
            bulletTransform.rotation = spawnTransform.rotation;
            bulletTransform.position = spawnTransform.position;
            
            bullet.gameObject.SetActive(true);
        }
    }
}
