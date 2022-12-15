using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static PlayerInput playerInput;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public static void ActivatePlayerControls()
    {
        playerInput.SwitchCurrentActionMap("Player");
    }

    public static void ActivateUIControls()
    {
        playerInput.SwitchCurrentActionMap("UI");
    }
}
