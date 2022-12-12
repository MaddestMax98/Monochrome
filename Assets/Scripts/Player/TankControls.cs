using UnityEngine;

namespace PlayerCharacter
{
    public class TankControls : MonoBehaviour
    {
        private float horizontalMove;
        private float verticalMove;

        private float velocity;
        private const float GRAVITY = -9.81f;
        private float gravityMultiplier = 1.0f;

        private CharacterController controller;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
        }

        void Update()
        {
            ApplyGravity();

            if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            {
                horizontalMove = Input.GetAxis("Horizontal") * Time.deltaTime * 150;
                verticalMove = Input.GetAxis("Vertical") * Time.deltaTime * 4;

                Vector3 grav = new Vector3(0, velocity, 0);
                controller.Move((transform.forward * verticalMove) + grav);
                transform.Rotate(0, horizontalMove, 0);
            }
        }

        private void ApplyGravity()
        {
            if (controller.isGrounded && velocity < 0.0f)
            {
                velocity = -1.0f;
            }
            else
            {
                velocity += GRAVITY * gravityMultiplier * Time.deltaTime;
            }

        }
    }
}
