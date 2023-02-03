using Item;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

namespace PlayerCharacter
{
    public class Player : MonoBehaviour
    {
        private Phone phone;

        private int sanity = 10;

        private bool canMove = true;

        private Transform playerTransform = null;
        private float playerInteractionDistance = 35f;

        [SerializeField]
        private LayerMask interactionMask;
        [SerializeField]
        private Animator _animator;
        
        private GameObject Target;
        
        [Header("Post-Processing Settings")]
        [SerializeField]
        private PostProcessVolume _volume; 
        private Vignette _vignette;
        private ChromaticAberration _chromaticAberration;

        public int Sanity { get => sanity; set => sanity = value; }
        public bool CanMove { get => canMove; set => canMove = value; }
        public Phone Phone { get => phone; set => phone = value; }


        private void OnEnable()
        {
            SanityTaker.onSanityTaken += UpdateAnimator;
            SanityGiver.onSanityGiven += UpdateAnimator;
        }

        private void OnDisable()
        {
            SanityTaker.onSanityTaken -= UpdateAnimator;
            SanityGiver.onSanityGiven -= UpdateAnimator;
        }

        private void Awake()
        {
            playerTransform = GetComponent<Transform>();
            phone = GetComponent<Phone>();
            
            _volume.profile.TryGetSettings(out _vignette);
            _volume.profile.TryGetSettings(out _chromaticAberration);
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

        public void UpdateAnimator()
        {
            if (sanity <= 4) 
            {
                _animator.SetBool("hasLowSanity", true);
                _vignette.intensity.value = 0.5f;
                _chromaticAberration.intensity.value = 0.50f;
            }
            else
            {
                _animator.SetBool("hasLowSanity", false);
                _vignette.intensity.value = 0.35f;
                _chromaticAberration.intensity.value = 0;
            }
            
        }

        private void Interact()
        {
            if (Physics.Raycast(playerTransform.position, playerTransform.forward, out RaycastHit hit, playerInteractionDistance, interactionMask))
            {

                if (hit.collider.TryGetComponent<Interactable>(out Interactable interactable))
                {
                    interactable.Interact();

                    if(hit.collider.gameObject.transform.position.y < 0.5f) _animator.SetFloat("InteractionHeight", -1);
                    else if (hit.collider.gameObject.transform.position.y > 0.5 && hit.collider.gameObject.transform.position.y < 1.5f) _animator.SetFloat("InteractionHeight", 0);
                    else _animator.SetFloat("InteractionHeight", 1);

                    _animator.SetBool("isInteracting", true);
                    canMove = false;
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

        public void ReturnToNormalMovement()
        {
            _animator.SetBool("isInteracting", false);
            canMove = true;
        }
    }
}
