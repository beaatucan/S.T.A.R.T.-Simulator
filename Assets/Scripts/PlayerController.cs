using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour, PlayerControls.IMovementActions
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float interactionRadius = 2f; // Detection radius for interactables
    [SerializeField] private float minInteractionDistance = 1.5f; // Minimum distance required to interact

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;
    private IInteractable currentInteractable;

    private const float movementThreshold = 0.1f;

    private void Awake() {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        
        // Subscribe to input actions
        playerControls.Movement.SetCallbacks(this);
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void OnDisable() {
        playerControls.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // Only process movement input if game is not paused
        if (GameManager.Instance != null && GameManager.Instance.IsPaused())
        {
            movement = Vector2.zero;
            return;
        }

        movement = context.ReadValue<Vector2>();
        
        // Update Animator parameters
        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
        myAnimator.SetFloat("speed", movement.sqrMagnitude);

        // Handle animation triggers
        HandleMovementAnimation();
    }

    private void HandleMovementAnimation()
    {
        bool isVertical = Mathf.Abs(movement.y) > Mathf.Abs(movement.x);
        myAnimator.SetBool("isVertical", isVertical);

        if (movement.sqrMagnitude > movementThreshold)
        {
            if (isVertical)
            {
                if (movement.y > movementThreshold)
                {
                    myAnimator.SetTrigger("MoveUp");
                    myAnimator.ResetTrigger("MoveDown");
                }
                else if (movement.y < -movementThreshold)
                {
                    myAnimator.SetTrigger("MoveDown");
                    myAnimator.ResetTrigger("MoveUp");
                }
            }
            else
            {
                myAnimator.ResetTrigger("MoveUp");
                myAnimator.ResetTrigger("MoveDown");
            }
        }
        else
        {
            myAnimator.ResetTrigger("MoveUp");
            myAnimator.ResetTrigger("MoveDown");
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        // Always allow interaction input, even when paused
        if (context.performed && currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    private void FixedUpdate()
    {
        // Only process movement if game is not paused
        if (GameManager.Instance == null || !GameManager.Instance.IsPaused())
        {
            Move();
            AdjustPlayerFacingDirection();
        }
    }

    private void Update()
    {
        // Always check for interactables, even when paused
        CheckForInteractables();
    }

    private void Move()
    {
        if (movement != Vector2.zero)
        {
            rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
        }
    }
    
    private void AdjustPlayerFacingDirection()
    {
        Vector3 MousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        mySpriteRenderer.flipX = MousePos.x < playerScreenPoint.x;
    }

    private void CheckForInteractables()
    {
        IInteractable closest = null;
        float closestDistance = float.MaxValue;

        // Find all nearby colliders
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactionRadius);

        // First pass: find the closest interactable
        foreach (Collider2D collider in colliders)
        {
            IInteractable interactable = collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = interactable;
                }
            }
        }

        // Handle interaction state changes
        if (closest != null && closestDistance <= minInteractionDistance)
        {
            // If we found a new interactable that's close enough
            if (currentInteractable != closest)
            {
                // Exit the old interactable if we had one
                if (currentInteractable != null)
                {
                    currentInteractable.OnPlayerExit();
                }

                // Set and enter the new interactable
                currentInteractable = closest;
                currentInteractable.OnPlayerApproach();
            }
        }
        else
        {
            // If we either found nothing, or the closest thing was too far
            if (currentInteractable != null)
            {
                currentInteractable.OnPlayerExit();
                currentInteractable = null;
            }
        }
    }
}
