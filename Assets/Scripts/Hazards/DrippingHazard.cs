using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrippingHazard : MonoBehaviour
{
    [SerializeField] Transform dripPrefab;
    [SerializeField] float dripRateInSeconds = 1f;
    [SerializeField] float dropletFallSpeed = 1f;

    void Start()
    {
        StartCoroutine(Drip());
    }

    IEnumerator Drip()
    {
        yield return new WaitForSeconds(dripRateInSeconds);
        Instantiate(dripPrefab, transform);
        yield return Drip();
    }
}
