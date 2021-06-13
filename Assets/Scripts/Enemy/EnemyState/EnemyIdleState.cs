using UnityEngine;

namespace Enemy.EnemyState
{
    public class EnemyIdleState : EnemyState
    {
        public float IdleDuration;

        private float _startTime;
        
        public override void EnterState()
        {
            //Debug.Log("Enter Idle");

            _startTime = Time.time;
        }

        public override void ExitState()
        {
            //Debug.Log("Exit Idle");
            
        }

        public override void FixedUpdate()
        {
            if (_startTime + IdleDuration < Time.time)
            {
                float distance =
                    Vector3.Distance(transform.position, _enemy.Target.position);
                
                if (distance > _enemy.defaultDistance)
                {
                    _stateHandler.ChangeState(EnemyStateEnum.Follow);
                }
                else
                {
                    _stateHandler.ChangeState(EnemyStateEnum.Attack);
                }
            }
        }
    }
}