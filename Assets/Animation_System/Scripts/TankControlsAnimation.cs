using UnityEngine;

namespace PlayerCharacter
{
    public class TankControlsAnimation : MonoBehaviour
    {
        private float horizontalMove;
        private float verticalMove;

        private CharacterController controller;
        private Animator animator;


        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {

            if (GetComponent<Player>().CanMove)
            {
                if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
                {
                    horizontalMove = Input.GetAxis("Horizontal") * Time.deltaTime * 120;
                    verticalMove = Input.GetAxis("Vertical") * Time.deltaTime * 1.5f;

                    controller.Move((transform.forward * verticalMove));
                    transform.Rotate(0, horizontalMove, 0);
                }

                if (Input.GetKey(KeyCode.W) && Input.GetButton("Vertical")) animator.SetBool("isWalkingForward", true);
                else animator.SetBool("isWalkingForward", false);

                if (Input.GetKey(KeyCode.S) && Input.GetButton("Vertical")) animator.SetBool("isWalkingBackwards", true);
                else animator.SetBool("isWalkingBackwards", false);

                if (Input.GetKey(KeyCode.D) && !Input.GetButton("Vertical")) animator.SetBool("isTurningRight", true);
                else animator.SetBool("isTurningRight", false);

                if (Input.GetKey(KeyCode.A) && !Input.GetButton("Vertical")) animator.SetBool("isTurningLeft", true);
                else animator.SetBool("isTurningLeft", false);
            }
        }
        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.layer == 11) controller.radius = 0.012f;
            else controller.radius = 0.5f;
        }
    }
}
