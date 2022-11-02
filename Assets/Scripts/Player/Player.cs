using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerCharacter
{
    public class Player : MonoBehaviour
    {
        private Transform playerTransform = null;
        private float playerInteractionDistance = 35f;

        private void Awake()
        {
            playerTransform = GetComponent<Transform>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Interact();
            }
        }

        private void Interact()
        {
            if (Physics.Raycast(playerTransform.position, playerTransform.forward, out RaycastHit hit, playerInteractionDistance))
            {
                if (hit.collider.TryGetComponent<Door>(out Door door))
                {
                    if (door.IsOpen)
                    {
                        door.Close();
                    }
                    else
                    {
                        door.Open(transform.position);
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            if (playerTransform != null)
            {
                Gizmos.DrawRay(playerTransform.position, playerTransform.forward);
            }
         
        }

    }
}

