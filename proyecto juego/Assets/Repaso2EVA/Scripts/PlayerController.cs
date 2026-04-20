using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] float speed = 8f;
    [SerializeField] float crouchSpeed = 4f; // Nueva velocidad reducida
    [SerializeField] float jumpForce;
    [SerializeField] bool isFacingRight = true;

    [Header("Jump Arc Settings")]
    [SerializeField] float fallMultiplier = 2.5f;
    [SerializeField] float lowJumpMultiplier = 2f;
    bool isJumping;

    [Header("Jump Buffer & Coyote")]
    [SerializeField] float jumpBufferTime = 0.2f;
    float jumpBufferCounter;
    [Range(0, 1)][SerializeField] float airControl = 0.8f;
    [SerializeField] int extraJumpsValue = 1;
    int extraJumps;
    [SerializeField] float coyoteTime = 0.15f;
    float coyoteCounter;

    [Header("Wall Jump Configuration")]
    [SerializeField] Transform wallCheck;
    [SerializeField] float wallCheckRadius = 0.2f;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] Vector2 wallJumpForce = new Vector2(10f, 15f);
    [SerializeField] float wallSlidingSpeed = 2f;
    float wallJumpTime;
    bool isWalled;
    bool isWallSliding;

    [Header("Crouch & GroundCheck")]
    [SerializeField] bool isGrounded;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] LayerMask groundLayer;

    [SerializeField] Transform ceilingCheck; // Para evitar levantarse si hay techo
    [SerializeField] float ceilingCheckRadius = 0.2f;
    [SerializeField] bool isCrouching;
    bool wantToStandUp = true; // Control interno

    [Header("Collider Settings")]
    [SerializeField] Vector2 colliderSizeNormal = new Vector2(0.8f, 1.6f);
    [SerializeField] Vector2 colliderOffsetNormal = new Vector2(0f, 0.8f);
    [SerializeField] Vector2 colliderSizeCrouch = new Vector2(0.8f, 0.8f);
    [SerializeField] Vector2 colliderOffsetCrouch = new Vector2(0f, 0.4f);

    [Header("Respawn Configuration")]
    [SerializeField] Transform respawnPoint;

    // Auto references
    Rigidbody2D rb;
    Animator anim;
    BoxCollider2D col; // Usaremos el BoxCollider2D
    Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
        extraJumps = extraJumpsValue;
    }

    void Update()
    {
        CheckStatus();
        HandleCrouchLogic();
        WallSlide();
    }

    void CheckStatus()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        isWalled = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, wallLayer);

        if (isGrounded)
        {
            coyoteCounter = coyoteTime;
            extraJumps = extraJumpsValue;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }

        if (jumpBufferCounter > 0) jumpBufferCounter -= Time.deltaTime;
    }

    void HandleCrouchLogic()
    {
        // Usamos un pequeño truco: el OverlapCircle ignorará al propio jugador si las capas están bien.
        // Además, lanzamos el círculo un poco más arriba de la cabeza actual.
        bool ceilingAbove = Physics2D.OverlapCircle(ceilingCheck.position, ceilingCheckRadius, groundLayer);

        if (!wantToStandUp || ceilingAbove)
        {
            isCrouching = true;
            // Ajuste de colisionador: Mitad de altura, pero el offset compensa para que los pies no se muevan
            col.size = colliderSizeCrouch;
            col.offset = colliderOffsetCrouch;
        }
        else
        {
            isCrouching = false;
            col.size = colliderSizeNormal;
            col.offset = colliderOffsetNormal;
        }
    }

    void Movement()
    {
        if (wallJumpTime <= 0)
        {
            // Usamos crouchSpeed si está agachado
            float currentMaxSpeed = isCrouching ? crouchSpeed : speed;
            float targetSpeed = moveInput.x * currentMaxSpeed;

            if (!isGrounded) targetSpeed *= airControl;

            rb.linearVelocity = new Vector2(targetSpeed, rb.linearVelocity.y);
        }
        else
        {
            wallJumpTime -= Time.deltaTime;
        }
    }

    // --- MÉTODOS DE INPUT SYSTEM ---

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started) jumpBufferCounter = jumpBufferTime;
        if (context.performed) isJumping = true;
        if (context.canceled) isJumping = false;
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed) wantToStandUp = false;
        if (context.canceled) wantToStandUp = true;
    }

    // --- RESTO DE LÓGICA (Jump, WallJump, Flip, etc) ---

    void FixedUpdate()
    {
        Movement();
        ApplyCustomGravity();
    }

    void LateUpdate()
    {
        // No permitimos saltar si estamos agachados (opcional)
        if (jumpBufferCounter > 0 && !isCrouching)
        {
            if (isGrounded || coyoteCounter > 0)
            {
                Jump();
                jumpBufferCounter = 0;
            }
            else if (isWallSliding || isWalled)
            {
                WallJump();
                jumpBufferCounter = 0;
            }
            else if (extraJumps > 0)
            {
                Jump();
                extraJumps--;
                jumpBufferCounter = 0;
            }
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void WallJump()
    {
        extraJumps = extraJumpsValue;
        wallJumpTime = 0.2f;
        float forceX = isFacingRight ? -wallJumpForce.x : wallJumpForce.x;
        rb.linearVelocity = new Vector2(forceX, wallJumpForce.y);
        FlipWallJump();
    }

    void ApplyCustomGravity()
    {
        float gravityScale = Physics2D.gravity.y;
        if (Mathf.Abs(rb.linearVelocity.y) < 0.5f && !isGrounded)
        {
            rb.linearVelocity += Vector2.up * gravityScale * 0.5f * Time.fixedDeltaTime;
        }
        else if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * gravityScale * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }

        if (rb.linearVelocity.y > 0 && !isJumping)
        {
            rb.linearVelocity += Vector2.up * gravityScale * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    void WallSlide()
    {
        if (isWalled && !isGrounded && moveInput.x != 0)
        {
            isWallSliding = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }

        Flip();
        AnimatorHandler();
    }

    void Flip()
    {
        if (moveInput.x > 0f && !isFacingRight)
        {
            isFacingRight = true;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (moveInput.x < 0f && isFacingRight)
        {
            isFacingRight = false;
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    void FlipWallJump()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
    }

    void AnimatorHandler()
    {
        anim.SetBool("Jump", !isGrounded);
        anim.SetBool("Run", Mathf.Abs(moveInput.x) > 0.1f);
        anim.SetBool("Crouch", isCrouching); // Nueva animación
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle")) Respawn();
    }

    void Respawn()
    {
        if (respawnPoint != null) transform.position = respawnPoint.position;
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = isGrounded ? Color.green : Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
        if (ceilingCheck != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(ceilingCheck.position, ceilingCheckRadius);
        }
    }
}