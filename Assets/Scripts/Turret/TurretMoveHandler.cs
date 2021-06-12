using System;
using UnityEngine;

namespace Turret
{
    public class TurretMoveHandler : MonoBehaviour
    {
        public BoxCollider2D Collider;
        
        public void SetColliderIsTrigger(bool flag)
        {
            Collider.isTrigger = flag;
        }
    }
}
