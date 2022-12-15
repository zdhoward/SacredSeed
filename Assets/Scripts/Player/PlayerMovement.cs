using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MoreMountains.Feedbacks;

public class PlayerMovement : MonoBehaviour
{
    //[SerializeField] Mesh groundCheckMesh;

    public event EventHandler OnJump;
    public event EventHandler OnLand;
    public event EventHandler OnDashStart;
    public event EventHandler OnDashComplete;

    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private PlayerController playerController;

    public float horizontal { get; private set; }
    private float speed = 4f;
    private float jumpingPower = 12f;

    public bool isFacingRight { get; private set; } = true;
    public bool isJumping { get; private set; } = false;
    private float jumpStartTime;

    public bool isDashing = false;
    private bool canDash = true;
    private float dashStartTime = 0f;
    private float dashDuration = 0.2f;
    private float dashPower = 10f;
    private float dashCoolDown = 1f;

    private MMF_Player mmfPlayer_OnDash;

    // MONOBEHAVIOUR //
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        mmfPlayer_OnDash = transform.Find("MMF_Player_OnDash").GetComponent<MMF_Player>();
        mmfPlayer_OnDash.Initialization();
    }

    private void Update()
    {
        if (playerController.isDead)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (isJumping && IsGrounded() && Time.time > jumpStartTime + .1f)
        {
            Land();
        }

        DashUpdate();
    }

    // HELPERS //
    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, .2f, groundLayer);
        //return Physics2D.OverlapBox(groundCheck.position, new Vector2(.8f, .4f), 0f);
    }

    // void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.yellow;
    //     Gizmos.DrawWireSphere(groundCheck.position, .2f);


    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireCube(groundCheck.position, new Vector3(.8f, .4f, .2f));
    // }

    // CALLBACKS //
    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        OnJump?.Invoke(this, EventArgs.Empty);

        if (context.performed && IsGrounded())
        {
            jumpStartTime = Time.time;
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            isJumping = true;
        }

        if (context.canceled && rb.velocity.y > 0f)
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && !isDashing && canDash)
        {
            OnDashStart?.Invoke(this, EventArgs.Empty);

            isDashing = true;
            canDash = false;
            dashStartTime = Time.time;
            mmfPlayer_OnDash.PlayFeedbacks();
        }
    }

    public void Land()
    {
        OnLand?.Invoke(this, EventArgs.Empty);

        isJumping = false;
    }

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private void DashUpdate()
    {
        if (!canDash && Time.time > dashStartTime + dashCoolDown)
            canDash = true;

        if (isDashing && Time.time < dashStartTime + dashDuration)
        {
            if (isFacingRight)
                rb.velocity = Vector2.right * dashPower;
            else
                rb.velocity = Vector2.left * dashPower;
        }
        else if (isDashing)
        {
            OnDashComplete?.Invoke(this, EventArgs.Empty);
            isDashing = false;
        }
    }
}
