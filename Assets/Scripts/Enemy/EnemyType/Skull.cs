﻿

using UnityEngine;

namespace Enemy.EnemyType
{
    public class Skull : Enemy
    {
        public override EnemyType Type => EnemyType.Skull;
        public override void Attack(Vector3 target)
        {
            
        }

        public void OnEnable()
        {
            int rand = Random.Range(0, 2);
            _target = rand == 0 ? TurretTransform : PlayerTransform;
        }
    }
}