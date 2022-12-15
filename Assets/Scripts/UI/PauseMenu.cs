using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuCanvas;

    [SerializeField] Image volumeMenuItem;
    [SerializeField] Image mutedMenuItem;
    [SerializeField] Image returnMenuItem;
    [SerializeField] Image exitMenuItem;

    [SerializeField] TextMeshProUGUI volumeMenuItemLabel;
    [SerializeField] TextMeshProUGUI mutedMenuItemLabel;
    [SerializeField] TextMeshProUGUI returnMenuItemLabel;
    [SerializeField] TextMeshProUGUI exitMenuItemLabel;

    int selectedIndex = 0;

    int currentVolume = 10;
    bool isMuted = false;

    bool isOpen = false;

    void Start()
    {
        UpdateSelected();
    }

    void Update()
    {
        if (InputManager.playerInput.actions.FindAction("Pause", false).WasPressedThisFrame())
        {
            isOpen = !isOpen;
            pauseMenuCanvas.SetActive(isOpen);
            Time.timeScale = isOpen ? 0 : 1;

            if (isOpen)
                InputManager.ActivateUIControls();
            else
            {
                InputManager.ActivatePlayerControls();
            }
        }

        if (!isOpen)
            return;

        HandleSelection();
        HandleInteration();

    }

    void HandleSelection()
    {
        if (InputManager.playerInput.actions.FindAction("Navigate", false).WasPressedThisFrame())
        {
            Vector2 input = InputManager.playerInput.actions.FindAction("Navigate", false).ReadValue<Vector2>();

            if (input.y > 0)
            {
                if (selectedIndex > 0)
                {
                    selectedIndex--;
                    UpdateSelected();
                }
            }
            else if (input.y < 0)
            {
                if (selectedIndex < 3)
                {
                    selectedIndex++;
                    UpdateSelected();
                }
            }
        }
    }

    void HandleInteration()
    {
        // Volume
        if (selectedIndex == 0)
        {
            if (InputManager.playerInput.actions.FindAction("Navigate", false).WasPressedThisFrame())
            {
                Vector2 input = InputManager.playerInput.actions.FindAction("Navigate", false).ReadValue<Vector2>();

                if (input.x < 0)
                {
                    if (currentVolume > 0)
                    {
                        currentVolume--;
                        volumeMenuItemLabel.text = "Volume: < " + currentVolume + " >";
                        SetVolume();
                    }
                }
                else if (input.x > 0)
                {
                    if (currentVolume < 10)
                    {
                        currentVolume++;
                        volumeMenuItemLabel.text = "Volume: < " + currentVolume + " >";
                        SetVolume();
                    }
                }
            }
        }

        // Muted
        if (selectedIndex == 1)
        {
            if (InputManager.playerInput.actions.FindAction("Navigate", false).WasPressedThisFrame())
            {
                Vector2 input = InputManager.playerInput.actions.FindAction("Navigate", false).ReadValue<Vector2>();

                if (input.x > 0 || input.x < 0)
                {
                    isMuted = !isMuted;
                    mutedMenuItemLabel.text = "Muted: " + (isMuted ? "Yes" : "No");
                    SetMuted();
                }
            }
        }

        // Return
        if (selectedIndex == 2)
        {
            if (InputManager.playerInput.actions.FindAction("Submit", false).WasPressedThisFrame())
            {
                Time.timeScale = 1;
                LoadingManager.Instance.LoadScene("MainMenu");
            }
        }

        // Exit
        if (selectedIndex == 3)
        {
            if (InputManager.playerInput.actions.FindAction("Submit", false).WasPressedThisFrame())
            {
                Application.Quit();
            }
        }
    }

    void UpdateSelected()
    {
        UnselectAll();

        if (selectedIndex == 0)
            volumeMenuItem.enabled = true;
        else if (selectedIndex == 1)
            mutedMenuItem.enabled = true;
        else if (selectedIndex == 2)
            returnMenuItem.enabled = true;
        else if (selectedIndex == 3)
            exitMenuItem.enabled = true;
    }

    void UnselectAll()
    {
        volumeMenuItem.enabled = false;
        mutedMenuItem.enabled = false;
        returnMenuItem.enabled = false;
        exitMenuItem.enabled = false;
    }

    void SetVolume()
    {
        AudioManager.Instance.SetVolumeLevel(currentVolume / 10f);
    }

    void SetMuted()
    {
        AudioManager.Instance.SetMuted(isMuted);
    }

    // CALLBACKS //
    public void Navigate(InputAction.CallbackContext context)
    {
        //Debug.Log("Navigating");
    }

    public void Submit(InputAction.CallbackContext context)
    {
        //Debug.Log("Navigating");
    }
}
