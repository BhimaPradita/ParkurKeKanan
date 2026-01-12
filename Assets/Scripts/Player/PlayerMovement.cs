using System.Runtime.InteropServices;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float climbSpeed = 2f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    private Vector2 moveInput;

    private float jumpInput;
    private bool jumpPressed;
    private bool isGrounded;
    private bool isClimbing;
    private bool isFacingRight = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
    }

    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        jumpInput = jumpAction.ReadValue<float>();
        jumpPressed = jumpInput > 0.5f;

        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (isFacingRight && moveInput.x < 0)
        {
            Flip();
        }
        else if (!isFacingRight && moveInput.x > 0)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        Move();

        if (jumpPressed && isGrounded)
        {
            Jump();
            jumpPressed = false;
        }

        if (isClimbing)
        {
            Climb();
        }
    }

    private void Move()
    {
        Vector2 velocity = rb.linearVelocity;
        velocity.x = moveInput.x * moveSpeed;
        rb.linearVelocity = velocity;
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("Ladder"))
        {
            Debug.Log("On Ladder");
            isClimbing = true;
        }
    }

    void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("Ladder"))
        {
            isClimbing = false;
        }
    }

    void Climb()
    {
        Vector2 velocity = rb.linearVelocity;
        velocity.y = moveInput.y * climbSpeed;
        rb.linearVelocity = velocity;
    }
}
