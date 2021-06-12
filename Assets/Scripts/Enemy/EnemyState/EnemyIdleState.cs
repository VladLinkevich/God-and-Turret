using UnityEngine;

namespace Enemy.EnemyState
{
    public class EnemyIdleState : EnemyState
    {
        public override void EnterState()
        {
            Debug.Log("Enter Idle");
        }

        public override void ExitState()
        {
            Debug.Log("Exit Idle");
        }

        public override void Update()
        {
            Debug.Log("Update Idle");

        }
    }
}