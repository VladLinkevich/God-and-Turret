using System;
using Turret;
using UnityEngine;


namespace Player
{
    public class PlayerDamageHandler : MonoBehaviour
    {
        public Animator animator;
        public TurretBulletHandler turretHandler;
        private static readonly int Damage = Animator.StringToHash("Damage");

        public void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                //transform.localPosition = Vector3.zero;

                Vector3 mousePosition = ScreenToWorld(Camera.main, Input.mousePosition);

                Vector2 pos = mousePosition - transform.parent.position;
                pos.Normalize();
                
                transform.position = (Vector3)pos + transform.parent.position;

                float angle = Mathf.Rad2Deg * Mathf.Atan2(pos.x, pos.y);

                transform.eulerAngles = new Vector3(0, 0, - angle + 90);
                
                // Debug.Log($"{angle}");
                
                //transform.position = new Vector2(- Mathf.Sin(Mathf.Deg2Rad * angle),
                //        Mathf.Cos(Mathf.Deg2Rad * angle));
                
                animator.SetTrigger(Damage);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"Damage: {other.tag}" );
            if (other.CompareTag("Enemy") == true)
            {
                Enemy.Enemy enemy = other.GetComponentInParent<Enemy.Enemy>();
                enemy.SetCursor();

                turretHandler.Target = enemy;
            }
        }
        
        public Vector3 ScreenToWorld(Camera camera, Vector3 position)
        {
            position.z = camera.nearClipPlane;
            return camera.ScreenToWorldPoint(position);
        }
    }
}