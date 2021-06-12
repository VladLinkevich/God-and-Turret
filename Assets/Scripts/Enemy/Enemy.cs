using System;
using UnityEngine;

namespace Enemy
{
    public abstract class Enemy : MonoBehaviour
    {
        public abstract EnemyType Type { get; }

        public Transform PlayerTransform;
        public Transform TurretTransform;

        public void Update()
        {
            Debug.Log($"{PlayerTransform.position}");
        }
    }
}