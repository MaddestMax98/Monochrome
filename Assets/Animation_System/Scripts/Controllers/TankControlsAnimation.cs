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
                    _animator.SetBool("isMoving", true);

                    _horizontalMove = Input.GetAxis("Horizontal") * Time.deltaTime * 120;                   
                    _verticalMove = Input.GetAxis("Vertical") * Time.deltaTime * 1.5f;

                    _controller.Move((transform.forward * _verticalMove));
                    transform.Rotate(0, _horizontalMove, 0);
                }
                else
                {
                    _animator.SetBool("isMoving", false);
                }

                if(!Input.GetButton("Horizontal")) _horizontalMove = 0;
                
                if(!Input.GetButton("Vertical"))
                {
                    _verticalMove = 0;
                    _animator.SetFloat("Xaxis", _horizontalMove * 120);
                }else _animator.SetFloat("Xaxis", 0);

                _animator.SetFloat("Yaxis", _verticalMove * 125);
            }
        }
        private void OnCollisionStay(Collision collision)
        {
            //Debug.Log(collision.gameObject.layer + " PlayerController: " + _controller.radius);

            if (collision.gameObject.layer == 11) _controller.radius = 0.012f;
            else _controller.radius = 0.5f;
        }
    }
}
