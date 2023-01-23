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

        private bool canMove = true;

        private GameObject Target;

        private Transform playerTransform = null;
        private float playerInteractionDistance = 35f;

        [SerializeField]
        private LayerMask interactionMask;

        public int Sanity { get => sanity; set => sanity = value; }
        public bool CanMove { get => canMove; set => canMove = value; }
        public Phone Phone { get => phone; set => phone = value; }

        private void Awake()
        {
            playerTransform = GetComponent<Transform>();
            Phone = GetComponent<Phone>();
        }

        private void Update()
        {
         
            if (Input.GetMouseButtonDown(0))
            {
                if (canMove)
                {
                    Interact();
                }
            }

            //TODO: Optimize this code. Proposition: event system to call thing once?
            /*System to outline object when looking at them*/
            if (!Physics.Raycast(playerTransform.position, playerTransform.forward, out RaycastHit nothit, playerInteractionDistance, interactionMask))
            {
                if (Target != null)
                {
                    Target.GetComponent<Outline>().OutlineWidth = 0f;
                }
            }
            if (Physics.Raycast(playerTransform.position, playerTransform.forward, out RaycastHit hit, playerInteractionDistance, interactionMask))
            {

                if (hit.collider.GetComponent<Outline>() != null)
                {
                    Target = hit.collider.gameObject;
                }

                if (hit.collider.TryGetComponent<Outline>(out Outline interactable))
                {
                    Target.GetComponent<Outline>().OutlineWidth = 5f;
                }
            }
        }

        private void Interact()
        {
            if (Physics.Raycast(playerTransform.position, playerTransform.forward, out RaycastHit hit, playerInteractionDistance, interactionMask))
            {

                if (hit.collider.TryGetComponent<Interactable>(out Interactable interactable))
                {
                    interactable.Interact();
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

