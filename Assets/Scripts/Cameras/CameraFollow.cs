using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    private float minY = 0f;
    [SerializeField][Range(0.1f, 5f)] private float moveSpeed = .01f;

    private float rangeX = 1.5f;

    void Update()
    {
        float newY = Mathf.Max(player.position.y, minY);
        float newX = Mathf.Clamp(player.position.x, -rangeX, rangeX);

        Vector3 newPosition = new Vector3(newX, newY, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, newPosition, moveSpeed);
    }
}
