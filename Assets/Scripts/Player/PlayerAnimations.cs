using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    PlayerMovement playerMovement;
    PlayerController playerController;
    Animator animator;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerController = GetComponent<PlayerController>();
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        playerMovement.OnJump += PlayerMovement_OnJump;
        playerMovement.OnLand += PlayerMovement_OnLand;
        playerMovement.OnDashStart += PlayerMovement_OnDashStart;
        playerMovement.OnDashComplete += PlayerMovement_OnDashComplete;

        PlayerController.OnStart += PlayerController_OnStart;
        PlayerController.OnDeath += PlayerController_OnDeath;
    }

    void Update()
    {
        if (playerController.isDead)
            return;

        if (!playerMovement.isFacingRight && playerMovement.horizontal > 0f)
            playerMovement.Flip();
        else if (playerMovement.isFacingRight && playerMovement.horizontal < 0f)
            playerMovement.Flip();

        if (!playerMovement.isJumping)
            animator.SetFloat("Speed", Mathf.Abs(playerMovement.horizontal));
    }

    private void PlayerMovement_OnJump(object sender, EventArgs e)
    {
        animator.SetBool("Jump", true);
    }

    private void PlayerMovement_OnLand(object sender, EventArgs e)
    {
        animator.SetBool("Jump", false);
    }

    private void PlayerMovement_OnDashStart(object sender, EventArgs e)
    {
        animator.SetBool("isDashing", true);
    }

    private void PlayerMovement_OnDashComplete(object sender, EventArgs e)
    {
        animator.SetBool("isDashing", false);
    }

    private void PlayerController_OnDeath(object sender, EventArgs e)
    {
        animator.SetBool("isDead", true);
    }

    private void PlayerController_OnStart(object sender, EventArgs e)
    {
        animator.SetBool("isDead", false);
    }


}
