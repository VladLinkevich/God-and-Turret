using UnityEngine;

namespace Enemy.EnemyState
{
    public abstract class EnemyState : MonoBehaviour
    {
        public Enemy _enemy;
        public EnemyStateHandler _stateHandler;
        public abstract void EnterState();

        public abstract void ExitState();

        public abstract void FixedUpdate();

    }
}