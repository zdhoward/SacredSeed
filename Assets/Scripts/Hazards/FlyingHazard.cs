using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingHazard : MonoBehaviour
{
    [SerializeField] LayerMask playerLayerMask;
    [SerializeField] float detectRange = 10f;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float wobbleAmount = 1f;
    [SerializeField] float wobbleSpeed = 1f;
    // Wait for player to get near target

    // seek the player
    Vector3 startingPosition;
    Vector3 targetPosition;
    Vector3 targetDirection;
    bool hasTarget = false;

    void Awake()
    {
        startingPosition = transform.position;
    }

    void OnEnable()
    {
        PlayerController.OnDeath += PlayerController_OnDeath;
    }

    void OnDisable()
    {
        PlayerController.OnDeath -= PlayerController_OnDeath;
    }

    void Start()
    {
        LeanTween.moveLocalX(gameObject, transform.position.x + 1 * wobbleAmount, 1.75f * wobbleSpeed).setEaseInOutSine().setLoopPingPong();
        LeanTween.moveLocalY(gameObject, transform.position.y + .75f * wobbleAmount, 2f * wobbleSpeed).setEaseInOutSine().setLoopPingPong();
    }

    void Update()
    {
        AcquireTarget();
        MoveInTargetDirection();
    }

    void AcquireTarget()
    {
        if (hasTarget)
            return;

        Collider2D other = Physics2D.OverlapCircle(transform.position, detectRange, playerLayerMask);

        if (other != null && other.tag == "Player")
        {
            targetPosition = other.transform.position;
            targetPosition.y += 1;
            targetDirection = (targetPosition - transform.position).normalized;
            hasTarget = true;
            LeanTween.cancelAll();
        }
    }

    void MoveInTargetDirection()
    {
        if (!hasTarget)
            return;

        transform.position += targetDirection * Time.deltaTime * moveSpeed;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }

    void PlayerController_OnDeath(object sender, EventArgs e)
    {
        transform.position = startingPosition;
        hasTarget = false;
        Start();
    }
}
