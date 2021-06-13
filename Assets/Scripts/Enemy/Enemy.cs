using System;
using Game;
using UnityEngine;

namespace Enemy
{
    public abstract class Enemy : MonoBehaviour
    {
        public abstract EnemyType.EnemyType Type { get; }

        public Transform PlayerTransform;
        public Transform TurretTransform;

        public float defaultDistance;

        protected Transform _target;

        public Transform Target => _target;
        
        private bool _firstStart = true;

        public void OnEnable()
        {
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

        public abstract void Attack(Vector3 target);
    }
}