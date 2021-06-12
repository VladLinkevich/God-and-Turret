using System;

namespace Enemy.EnemyType
{
    public class Ghost : Enemy
    {
        public override EnemyType Type => EnemyType.Ghost;

        private void OnEnable()
        {
            _target = TurretTransform;
        }
    }
}