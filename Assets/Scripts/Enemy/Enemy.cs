using System;
using Game;
using UnityEngine;

namespace Enemy
{
    public abstract class Enemy : MonoBehaviour
    {
        public abstract EnemyType.EnemyType Type { get; }

        public GameObject Cursor;
        
        public Transform PlayerTransform;
        public Transform TurretTransform;

        public float defaultDistance;

        protected Transform _target;

        public Transform Target => _target;
        
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

        public abstract void Attack(Vector3 target);
    }
}