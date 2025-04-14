using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;

    private const float movementThreshold = 0.1f;

    private void Awake() {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void Update() {
        PlayerInput();
    }

    private void FixedUpdate() {
        AdjustPlayerFacingDirection();
        Move();
    }

    private void PlayerInput() {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        // Calculate speed (magnitude of movement vector)
        float speed = movement.sqrMagnitude;

        // Update Animator parameters
        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
        myAnimator.SetFloat("speed", speed);

        // Determine if vertical movement is dominant
        bool isVertical = Mathf.Abs(movement.y) > Mathf.Abs(movement.x);
        myAnimator.SetBool("isVertical", isVertical);

        // Prioritize vertical movement animations
        if (speed > movementThreshold) {
            if (Mathf.Abs(movement.y) > Mathf.Abs(movement.x)) {
                if (movement.y > 0.1f) {
                    myAnimator.SetTrigger("MoveUp");
                    myAnimator.ResetTrigger("MoveDown");
                } else if (movement.y < -0.1f) {
                    myAnimator.SetTrigger("MoveDown");
                    myAnimator.ResetTrigger("MoveUp");
                }
            } else {
                // Horizontal movement
                myAnimator.SetFloat("moveX", movement.x);
                myAnimator.ResetTrigger("MoveUp");
                myAnimator.ResetTrigger("MoveDown");
            }
        } else {
            // Reset triggers when idle
            myAnimator.ResetTrigger("MoveUp");
            myAnimator.ResetTrigger("MoveDown");
        }
    }

    private void Move(){
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }
    
    private void AdjustPlayerFacingDirection()
    {

    Vector3 MousePos = Input.mousePosition;
    Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

    if (MousePos.x < playerScreenPoint.x)
    {
        mySpriteRenderer.flipX = true;
    }
    else if (MousePos.x > playerScreenPoint.x)
    {
        mySpriteRenderer.flipX = false;
    }
    }
}
