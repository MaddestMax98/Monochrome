using UnityEngine;

namespace PlayerCharacter
{
    public class TankControls : MonoBehaviour
    {
        private float horizontalMove;
        private float verticalMove;

        void Update()
        {
            //TODO: Code it properly
            if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            {
                horizontalMove = Input.GetAxis("Horizontal") * Time.deltaTime * 150;
                verticalMove = Input.GetAxis("Vertical") * Time.deltaTime * 4;
                transform.Rotate(0, horizontalMove, 0);
                transform.Translate(0, 0, verticalMove);
            }
        }
    }
}
