using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrippingHazardDroplet : MonoBehaviour
{
    public float dropletFallSpeed = 1f;

    bool allowCollisions = false;

    void Start()
    {
        StartCoroutine(IgnoreCollisionWhenStarting());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (allowCollisions)
            Destroy(gameObject);
    }

    IEnumerator IgnoreCollisionWhenStarting()
    {
        yield return new WaitForSeconds(.1f);
        allowCollisions = true;
    }
}
