using Item;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

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

        [SerializeField]
        private PostProcessVolume _volume; 
        private Vignette _vignette;
        private ChromaticAberration _chromaticAberration;

        public int Sanity { get => sanity; set => sanity = value; }
        public bool CanMove { get => canMove; set => canMove = value; }

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
        { //Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask)
            if (Physics.Raycast(playerTransform.position, playerTransform.forward, out RaycastHit hit, playerInteractionDistance, interactionMask))
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
                if (hit.collider.TryGetComponent<LoadNextLevel>(out LoadNextLevel level))
                {
                    level.LoadNextScene();
                }
                if (hit.collider.TryGetComponent<PickupItem>(out PickupItem item))
                {
                    item.Pickup();
                }
                if (hit.collider.TryGetComponent<TaskMachine>(out TaskMachine taskMachine))
                {
                    phone.CurrentTask = taskMachine.GetTask();//Handle UI
                }
                if (hit.collider.TryGetComponent<MannequinSystem>(out MannequinSystem system))
                {
                    system.EnterMannequinMode(this);
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
