using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public float length, startpos;
    public float parallaxFactor;

    void Awake()
    {
        startpos = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        float temp = Camera.main.transform.position.y * (1 - parallaxFactor);
        float distance = Camera.main.transform.position.y * parallaxFactor;

        Vector3 newPosition = new Vector3(transform.position.x, startpos + distance, transform.position.z);

        transform.position = newPosition;

        if (temp > startpos + (length / 2)) startpos += length;
        else if (temp < startpos - (length / 2)) startpos -= length;
    }
}
