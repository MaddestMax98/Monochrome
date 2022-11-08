using Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerCharacter
{
    public class Player : MonoBehaviour
    {
        private Phone phone;

        private int sanity = 10;


        private Transform playerTransform = null;
        private float playerInteractionDistance = 35f;

        public int Sanity { get => sanity; set => sanity = value; }


        private void Awake()
        {
            playerTransform = GetComponent<Transform>();
            phone = GetComponent<Phone>();
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
                if (hit.collider.TryGetComponent<PickupItem>(out PickupItem item))
                {
                    item.Pickup();
                }
                if (hit.collider.TryGetComponent<TaskMachine>(out TaskMachine taskMachine))
                {
                    phone.CurrentTask = taskMachine.GetTask();//Handle UI
                }
                if (hit.collider.TryGetComponent<LoadNextLevel>(out LoadNextLevel loadLevel))
                {
                    loadLevel.LoadNextScene();
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

