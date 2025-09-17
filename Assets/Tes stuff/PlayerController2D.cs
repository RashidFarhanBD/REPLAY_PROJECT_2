using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10f;
    public float airControlFactor = 0.8f; // reduce control in air

    [Header("Jump")]
    public float jumpForce = 14f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    [Header("Dash")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.15f;
    private bool isDashing = false;
    private int dashDirection;

    private Rigidbody2D rb;
    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true; // avoid physics tilt
    }

    void Update()
    {
        // --- Ground Check ---
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // --- Jump ---
        if (Input.GetButtonDown("Jump") && isGrounded && !isDashing)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // --- Dash ---
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
        {
            dashDirection = (int)Input.GetAxisRaw("Horizontal");
            if (dashDirection != 0)
            {
                StartCoroutine(Dash());
            }
        }
    }

    void FixedUpdate()
    {
        if (!isDashing)
        {
            float moveInput = Input.GetAxisRaw("Horizontal");

            // --- Ground vs Air movement ---
            float effectiveSpeed = isGrounded ? moveSpeed : moveSpeed * airControlFactor;

            rb.linearVelocity = new Vector2(moveInput * effectiveSpeed, rb.linearVelocity.y);
        }
    }

    private System.Collections.IEnumerator Dash()
    {
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0; // keep dash flat

        rb.linearVelocity = new Vector2(dashDirection * dashSpeed, 0f);

        yield return new WaitForSeconds(dashDuration);

        rb.gravityScale = originalGravity;
        isDashing = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
