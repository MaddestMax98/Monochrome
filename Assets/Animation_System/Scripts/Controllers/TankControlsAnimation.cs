using UnityEngine;

namespace PlayerCharacter
{
    public class TankControlsAnimation : MonoBehaviour
    {
        private float _horizontalMove;
        private float _verticalMove;

        private CharacterController _controller;
        private Animator _animator;


        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
        }

        void Update()
        {

            if (GetComponent<Player>().CanMove)
            {
                if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
                {
                    _horizontalMove = Input.GetAxis("Horizontal") * Time.deltaTime * 120;                   
                    _verticalMove = Input.GetAxis("Vertical") * Time.deltaTime * 1.5f;

                    _controller.Move((transform.forward * _verticalMove));
                    transform.Rotate(0, _horizontalMove, 0);
                }

                if (Input.GetKey(KeyCode.W) && Input.GetButton("Vertical")) _animator.SetBool("isWalkingForward", true);
                else _animator.SetBool("isWalkingForward", false);

                if (Input.GetKey(KeyCode.S) && Input.GetButton("Vertical")) _animator.SetBool("isWalkingBackwards", true);
                else _animator.SetBool("isWalkingBackwards", false);

                if (Input.GetKey(KeyCode.D) && !Input.GetButton("Vertical")) _animator.SetBool("isTurningRight", true);
                else _animator.SetBool("isTurningRight", false);

                if (Input.GetKey(KeyCode.A) && !Input.GetButton("Vertical")) _animator.SetBool("isTurningLeft", true);
                else _animator.SetBool("isTurningLeft", false);
            }
        }
        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.layer == 11) _controller.radius = 0.012f;
            else _controller.radius = 0.5f;
        }
    }
}
