using System;
using Game;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    public abstract class Enemy : MonoBehaviour
    {
        public abstract EnemyType.EnemyType Type { get; }

        [FormerlySerializedAs("HitEnemy")] public AudioSource HitEnemySound;
        [FormerlySerializedAs("DestroyEnemy")] public AudioSource DestroyEnemySound;
        
        public GameObject Cursor;
        
        public Transform PlayerTransform;
        public Transform TurretTransform;

        public float defaultDistance;
        public int maxHeath = 4;

        protected Transform _target;

        public Transform Target => _target;

        private int _currentHealth;
        
        private bool _firstStart = true;
        private bool _isSubscribe = false;
        private void Start()
        {
            Subscribe();
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            if (_isSubscribe == false)
            {
                _isSubscribe = true;
                
                Messenger.AddListener(GameEvent.SETNEWCURSOR, RemoveCursor);
            }
        }
        
        private void Unsubscribe()
        {
            if (_isSubscribe == true)
            {
                _isSubscribe = false;
                
                Messenger.RemoveListener(GameEvent.SETNEWCURSOR, RemoveCursor);
            }
        }

        public void OnEnable()
        {
            Subscribe();

            _currentHealth = maxHeath;
            
            if (_firstStart == false)
                GameHandler.Instance.AddActiveEnemy(this);
        }

        private void OnDisable()
        {
            if (_firstStart == false)
                GameHandler.Instance.RemoveActiveEnemy(this);
            else
                _firstStart = false;
        }

        public void SetCursor()
        {
               Messenger.Broadcast(GameEvent.SETNEWCURSOR);
               
               Cursor.SetActive(true);
        }

        private void RemoveCursor()
        {
            Cursor.SetActive(false);
        }

        public void RemoveHealth()
        {
            _currentHealth--;

            if (_currentHealth == 0)
            {
                DestroyEnemySound.Play();
                gameObject.SetActive(false);
            }
            else
            {
                HitEnemySound.Play();
            }
        }

        public abstract void Attack(Vector3 target);
    }
}