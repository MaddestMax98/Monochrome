using Item;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

namespace PlayerCharacter
{
    public class Player : MonoBehaviour
    {
        private Phone phone;

        private int sanity = 10;

        private bool canMove = true;

        private float playerInteractionDistance = 1f;

        [SerializeField]
        private LayerMask interactionMask;
        [SerializeField]
        private Animator _animator;
        
        private GameObject Target;

        private Vector3 rayCastHeight = new Vector3(0, 1.5f, 0);

        [Header("Post-Processing Settings")]
        [SerializeField]
        private VolumeProfile _volumeProfile;
        [SerializeField]
        private Vignette _vignette;
        [SerializeField]
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
            phone = GetComponent<Phone>();
           

            // get the vignette effect
            for (int i = 0; i < _volumeProfile.components.Count; i++)
            {
                if (_volumeProfile.components[i].name == "Vignette")
                {
                    _vignette = (Vignette)_volumeProfile.components[i];
                }

                if (_volumeProfile.components[i].name == "ChromaticAberration")
                {
                    _chromaticAberration = (ChromaticAberration)_volumeProfile.components[i];
                }
            }

        }

        private void Update()
        {
            UpdateAnimator();

            if (Input.GetKeyDown(KeyCode.P))
            {
                Sanity = -10;
                UpdateAnimator();
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (canMove)
                {
                    Interact();
                }
            }
            //TODO: Optimize this code. Proposition: event system to call thing once?
            /*System to outline object when looking at them*/
            if (!Physics.Raycast(this.transform.position + rayCastHeight, this.transform.forward, out RaycastHit nothit, playerInteractionDistance, interactionMask))
            {
                if (Target != null)
                {
                    Target.GetComponent<Outline>().OutlineWidth = 0f;
                }
            }
            if (Physics.Raycast(this.transform.position + rayCastHeight, this.transform.forward, out RaycastHit hit, playerInteractionDistance, interactionMask))
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
                _vignette.intensity.value = 0.50f;
                _chromaticAberration.intensity.value = 1f;
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
            if (Physics.Raycast(this.transform.position + rayCastHeight, this.transform.forward, out RaycastHit hit, playerInteractionDistance, interactionMask))
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
            Gizmos.DrawRay(this.transform.position + rayCastHeight, this.transform.forward);
        }

        public void ReturnToNormalMovement()
        {
            _animator.SetBool("isInteracting", false);
            canMove = true;
        }
    }
}
