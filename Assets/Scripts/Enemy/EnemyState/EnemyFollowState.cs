using UnityEngine;
using UnityEngine.AI;

namespace Enemy.EnemyState
{
    public class EnemyFollowState : EnemyState
    {
        public NavMeshAgent MeshAgent;
        
        public override void EnterState()
        {
            MeshAgent.updateRotation = false;
            MeshAgent.updateUpAxis = false;
            
            Debug.Log("Enter Follow");
        }

        public override void ExitState()
        {
            Debug.Log("Exit Follow");
        }

        public override void FixedUpdate()
        {
            MeshAgent.SetDestination(_enemy.Target.position);
        }
    }
}