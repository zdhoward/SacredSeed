using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingHazard : MonoBehaviour
{
    [SerializeField] Vector2 relativeTargetPosition;
    [SerializeField] float moveSpeedInSeconds;

    [SerializeField] bool shouldFlip = true;

    SpriteRenderer spriteRender;

    Vector2 startPosition;
    Vector2 endPosition;

    void Awake()
    {
        startPosition = transform.position;
        endPosition = startPosition + relativeTargetPosition;

        spriteRender = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        LeanTween.move(gameObject, endPosition, moveSpeedInSeconds).setLoopPingPong().setOnCompleteOnRepeat(true).setOnComplete(FlipSprite);
    }

    void FlipSprite()
    {
        if (shouldFlip)
            spriteRender.flipX = !spriteRender.flipX;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(startPosition, endPosition);
    }
}
