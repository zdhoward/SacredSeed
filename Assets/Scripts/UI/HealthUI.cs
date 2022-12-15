using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    Transform[] hearts;



    private void Awake()
    {
        hearts = transform.GetComponentsInChildren<Transform>();
    }

    private void Start()
    {
        SetHealth(3);
    }

    private void OnEnable()
    {
        PlayerController.OnHealthChange += PlayerController_OnHealthChange;
    }

    private void OnDisable()
    {
        PlayerController.OnHealthChange -= PlayerController_OnHealthChange;
    }

    public void SetHealth(int currentHealth)
    {
        foreach (Transform heart in hearts)
        {
            heart.gameObject.SetActive(false);
        }

        for (int i = 0; i <= currentHealth; i++)
        {
            hearts[i].gameObject.SetActive(true);
        }
    }

    private void PlayerController_OnHealthChange(object sender, int currentHealth)
    {
        SetHealth(currentHealth);
    }
}
