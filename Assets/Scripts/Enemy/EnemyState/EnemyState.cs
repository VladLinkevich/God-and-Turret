using UnityEngine;

namespace Enemy.EnemyState
{
    public abstract class EnemyState : MonoBehaviour
    {
        public abstract void EnterState();

        public abstract void ExitState();

        public abstract void Update();

    }
}