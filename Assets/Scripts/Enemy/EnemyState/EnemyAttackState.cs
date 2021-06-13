using DG.Tweening;
using UnityEngine;

namespace Enemy.EnemyState
{
    public class EnemyAttackState : EnemyState
    {
        public float PreparationAttackDuration;
        public float ReloadTime;
        
        private float _startAttackTime;
        public override void EnterState()
        {
            _startAttackTime = Time.time;
            Vector3 target = _enemy.Target.position;
            
            transform.DOShakePosition(PreparationAttackDuration, 0.1f, 10)
                .OnComplete(() =>
                {
                    _enemy.Attack(target);
                });
        }

        public override void ExitState()
        {        
            Debug.Log("Exit Attack");
        }

        public override void FixedUpdate()
        {
            if (_startAttackTime + ReloadTime < Time.time)
            {
                _stateHandler.ChangeState(EnemyStateEnum.Idle);
            }
        }
    }
}