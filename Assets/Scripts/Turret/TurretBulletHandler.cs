using System;
using System.Collections;
using Bullet;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Turret
{
    public class TurretBulletHandler : MonoBehaviour
    {
        public Animator animator;
        [FormerlySerializedAs("damageSound")] public AudioSource shootSound;
        
        public Enemy.Enemy Target = null;
        
        public Transform rightSpawn;
        public Transform leftSpawn;
        public float reloadTime;

        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Idle = Animator.StringToHash("Idle");

        private float _rightReload;
        private float _leftReload;
        private bool _isBattle;
        private bool _rotateFlag = true;
        private static readonly int Damage = Animator.StringToHash("TakeDamage");

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
            
            if (animator == true)
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
            
            if (animator == true)
                animator.SetTrigger(Idle);
        }

        public void TakeDamage()
        {
            if (animator == true)
                animator.SetTrigger(Damage);
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

                if (_rotateFlag == true &&
                    Target != null && Target.gameObject.activeSelf)
                {
                    _rotateFlag = false;
                    
                    Vector2 pos = Target.transform.position - transform.position;
                    pos.Normalize();
                    
                    float angle = Mathf.Rad2Deg * Mathf.Atan2(pos.x, pos.y);

                    transform.DORotate(new Vector3(0,0, -angle), 0.25f).
                        OnComplete(() => _rotateFlag = true);
                }
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
            
            shootSound.Play();
        }
    }
}
