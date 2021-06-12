using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody2D rb;
    
        public float Velocity = 1f;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Vector2 movement = GetMoveInput();
            Vector2 movementNormalized = movement.normalized;

            rb.velocity = movement * Velocity;
        }

        private Vector2 GetMoveInput()
        {
            Vector2 movement = Vector2.zero;
            
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                movement += Vector2.up;
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                movement += Vector2.down;
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                movement += Vector2.left;
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                movement += Vector2.right;
            }

            return movement;
        }
    }
}
