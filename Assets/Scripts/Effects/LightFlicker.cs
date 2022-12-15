using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    [Header("Intensity")]
    [SerializeField] private float intensityRange = 0.14f;
    [SerializeField] private float intensityTimeMin = 0.05f;
    [SerializeField] private float intensityTimeMax = 0.1f;
    private float originalIntensity;

    [Header("Reposition")]
    [SerializeField] private float repositionRadius = .1f;
    //[SerializeField] private float repositionAngle = 40f;
    [SerializeField] private float repositionTimeMin = .4f;
    [SerializeField] private float repositionTimeMax = .6f;
    private Vector3 originalPosition;

    [Header("ColorShift")]
    [SerializeField] private float colorRadius = 0.01f;
    [SerializeField] private float colorTimeMin = 0.05f;
    [SerializeField] private float colorTimeMax = .5f;
    private Color originalColor;

    private Light2D lightSource;

    void Awake()
    {
        lightSource = GetComponent<Light2D>();
    }

    void Start()
    {
        originalIntensity = lightSource.intensity;
        originalPosition = lightSource.transform.position;
        originalColor = lightSource.color;

        StartCoroutine(Flicker());
        StartCoroutine(Reposition());
        StartCoroutine(ColorShift());
    }

    private IEnumerator Flicker()
    {
        float newIntensity = Random.Range(originalIntensity - intensityRange, originalIntensity + intensityRange);
        lightSource.intensity = newIntensity;
        yield return new WaitForSeconds(Random.Range(intensityTimeMin, intensityTimeMax));
        yield return Flicker();
    }

    private IEnumerator Reposition()
    {
        float newX = originalPosition.x + Random.Range(-repositionRadius, repositionRadius);
        float newY = originalPosition.y + Random.Range(-repositionRadius, repositionRadius);
        lightSource.transform.position = new Vector3(newX, newY, lightSource.transform.position.z);
        yield return new WaitForSeconds(Random.Range(repositionTimeMin, repositionTimeMax));
        yield return Reposition();
    }

    private IEnumerator ColorShift()
    {
        Vector3 originalColorVector = new Vector3(originalColor.r, originalColor.g, originalColor.b);
        Vector3 newColorVector = Random.insideUnitSphere * colorRadius + originalColorVector;
        Color newColor = new Color(newColorVector.x, newColorVector.y, newColorVector.z);
        lightSource.color = newColor;
        yield return new WaitForSeconds(Random.Range(colorTimeMin, colorTimeMax));
        yield return ColorShift();
    }

}
