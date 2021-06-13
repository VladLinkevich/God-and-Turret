using System;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy.EnemyState
{
    public class EnemyFollowState : EnemyState
    {
        public float Velocity;

        private CharacterController _controller;

        public override void EnterState()
        {
            Debug.Log("Enter Follow");
        }

        public override void ExitState()
        {
            Debug.Log("Exit Follow");
        }

        public override void FixedUpdate()
        {
            float distance =
                Vector3.Distance(transform.position, _enemy.Target.position);

            if (distance > _enemy.defaultDistance)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position, _enemy.Target.position, Velocity);
            }
            else
            {
                _stateHandler.ChangeState(EnemyStateEnum.Attack);
            }
        }
    }
}