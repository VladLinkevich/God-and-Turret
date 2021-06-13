using System;
using UnityEngine;

namespace Enemy.EnemyType
{
    public class Ghost : Enemy
    {
        public override EnemyType Type => EnemyType.Ghost;
        public override void Attack(Vector3 target)
        {
            
        }

        private void OnEnable()
        {
            _target = TurretTransform;
            
            base.OnEnable();
        }
    }
}