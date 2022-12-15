using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthLabel;
    [SerializeField] private TextMeshProUGUI extentsGeneratedLabel;
    [SerializeField] private TextMeshProUGUI extentsUntilEndLabel;

    void Start()
    {
        PlayerController.OnHealthChange += PlayerController_OnHealthChange;
        LevelGenerator.OnExtentGenerated += LevelGenerator_OnExtentGenerated;
    }

    private void PlayerController_OnHealthChange(object sender, int health)
    {
        healthLabel.text = "Health: " + health;
    }

    private void LevelGenerator_OnExtentGenerated(object sender, LevelGenerator.OnExtentGeneratedArgs args)
    {
        extentsGeneratedLabel.text = "ExtentsGenerated: " + args.extentsGenerated;
        extentsUntilEndLabel.text = "ExtendsUntilEnd: " + args.extentsBeforeEnd;
    }
}
