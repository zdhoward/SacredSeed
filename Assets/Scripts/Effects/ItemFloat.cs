using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFloat : MonoBehaviour
{
    [SerializeField] float moveAmount = .2f;
    [SerializeField] float moveSpeed = 2f;

    void Start()
    {
        LeanTween.moveY(gameObject, transform.position.y + moveAmount, moveSpeed).setEaseInOutQuad().setLoopPingPong();
    }
}
