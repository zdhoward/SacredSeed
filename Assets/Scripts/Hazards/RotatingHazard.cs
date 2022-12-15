using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingHazard : MonoBehaviour
{
    [SerializeField] bool rotateCounterClockwise = true;
    [SerializeField] float rotationSpeedInSeconds = 1f;

    [SerializeField] GameObject rotatingPiece;

    void Start()
    {
        Vector3 axis = Vector3.back;

        if (!rotateCounterClockwise)
            axis = Vector3.forward;

        LeanTween.rotateAround(gameObject, Vector3.back, 360f, rotationSpeedInSeconds).setLoopClamp();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rotatingPiece.GetComponent<SpriteRenderer>().sprite.bounds.size.y * 2f);
    }
}
