using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource audioSource;
    private AudioSource uiAudioSource;

    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip dashClip;
    [SerializeField] private AudioClip landClip;
    [SerializeField] private AudioClip damagedClip;
    [SerializeField] private AudioClip diedClip;
    [SerializeField] private AudioClip pickupClip;
    [SerializeField] private AudioClip levelEndClip;

    [SerializeField] private AudioClip fireworksClip;

    [SerializeField] private AudioClip uiPauseClip;
    [SerializeField] private AudioClip uiUnpauseClip;
    [SerializeField] private AudioClip uiSelectionClip;
    [SerializeField] private AudioClip uiValueChangeClip;

    [SerializeField] private AudioClip bgmMainMenuClip;
    [SerializeField] private AudioClip bgmLevelClip;
    [SerializeField] private AudioClip bgmWinGameClip;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one AudioManager in the scene! " + transform.position + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;

        audioSource = GetComponent<AudioSource>();
        uiAudioSource = GetComponentInChildren<AudioSource>();

        LoadSettings();
    }

    private void Start()
    {

    }

    public float GetVolumeLevel()
    {
        return audioSource.volume;
    }

    public void SetVolumeLevel(float volumeLevel)
    {
        audioSource.volume = volumeLevel;
        uiAudioSource.volume = volumeLevel;
        PlayerPrefs.SetFloat("Volume", volumeLevel);
    }

    public void SetMuted(bool isMuted)
    {
        audioSource.mute = isMuted;
        uiAudioSource.mute = isMuted;
        PlayerPrefs.SetInt("IsMuted", isMuted ? 1 : 0);
    }

    void LoadSettings()
    {
        SetVolumeLevel(PlayerPrefs.GetFloat("Volume", 1));
        SetMuted(PlayerPrefs.GetInt("Muted", 0) == 1);
    }

    void ChangeLevelMusic()
    {
        // if main menu, load mainmenuclip

        // if in a level and not currently playing level music, load level music

        //if on end screen, load end screen music
    }

    public void TriggerJumpClip() { audioSource.PlayOneShot(jumpClip); }
    public void TriggerDashClip() { audioSource.PlayOneShot(dashClip); }
    public void TriggerLandClip() { audioSource.PlayOneShot(landClip); }
    public void TriggerDamagedClip() { audioSource.PlayOneShot(damagedClip); }
    public void TriggerDiedClip() { audioSource.PlayOneShot(diedClip); }
    public void TriggerPickupClip() { audioSource.PlayOneShot(pickupClip); }
    public void TriggerLevelEndClip() { audioSource.PlayOneShot(levelEndClip); }
    public void TriggerFireworksClip() { audioSource.PlayOneShot(fireworksClip); }

    public void TriggerUIPauseClip() { audioSource.PlayOneShot(uiPauseClip); }
    public void TriggerUIUnpauseClip() { audioSource.PlayOneShot(uiUnpauseClip); }
    public void TriggerUISelectionClip() { audioSource.PlayOneShot(uiSelectionClip); }
    public void TriggerUIValueChangeClip() { audioSource.PlayOneShot(uiValueChangeClip); }
}
