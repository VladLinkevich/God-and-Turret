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
    }
}