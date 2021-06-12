using UnityEngine;

namespace Enemy.EnemyState
{
    public class EnemyAttackState : EnemyState
    {
        private Enemy _enemy;
        
        public override void EnterState()
        {
            Debug.Log("Enter Attack");

            _enemy = GetComponent<Enemy>();
        }

        public override void ExitState()
        {        
            Debug.Log("Enter Attack");
        }

        public override void FixedUpdate()
        {         
            Debug.Log("Enter Attack");
        }
    }
}