using UnityEngine;

namespace Enemy.EnemyState
{
    public class EnemyFollowState : EnemyState
    {
        public override void EnterState()
        {
            Debug.Log("Enter Idle");

        }

        public override void ExitState()
        {            Debug.Log("Enter Idle");

        }

        public override void Update()
        {
            Debug.Log("Enter Idle");
      

        }
    }
}