using System.Collections.Generic;
using Enemy.EnemyState;
using UnityEngine;

namespace Enemy
{
    public class EnemyStateHandler : MonoBehaviour
    {
        public List<EnemyState.EnemyState> EnemyStates;
        public Enemy Enemy;

        private EnemyStateEnum _currentState = EnemyStateEnum.None;
        private EnemyState.EnemyState _enemyState;
        
        public EnemyStateEnum CurrentState => _currentState;

        private void OnEnable()
        {
            ChangeState(EnemyStateEnum.Idle);
        }

        private void OnDisable()
        {
            ChangeState(EnemyStateEnum.None);
        }

        public void ChangeState(EnemyStateEnum state)
        {
            if (_currentState == state)
                return;

            if (state == EnemyStateEnum.None)
            {
                _currentState = EnemyStateEnum.None;
                _enemyState.ExitState();
                _enemyState.enabled = false;
                _enemyState = null;
                return;
            }
            
            if (_enemyState == true)
            {
                _enemyState.ExitState();
                _enemyState.enabled = false;
                _enemyState = null;
            }

            _currentState = state;
            _enemyState = EnemyStates[(int) state];
            _enemyState.enabled = true;
            _enemyState.EnterState();

        }
    }
}