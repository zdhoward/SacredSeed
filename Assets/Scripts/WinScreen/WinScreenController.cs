using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Febucci.UI;

public class WinScreenController : MonoBehaviour
{
    [Header("You Win Label")]
    [SerializeField] Transform youWinLabel;
    [SerializeField] TextAnimatorPlayer winLabelTextAnimatorPlayer;
    [SerializeField][TextArea] string winLabelText;
    [SerializeField] float winLabelStartDelay = 2f;
    [SerializeField] float winLabelFadeOutSpeed = 2f;

    [Header("Credits Label")]
    [SerializeField] Transform creditsLabel;
    [SerializeField] TextAnimatorPlayer creditsLabelTextAnimatorPlayer;
    [SerializeField] float creditsScrollTimeInSeconds = 20f;

    [SerializeField] float fireworksDelay = 1f;
    [SerializeField] ParticleSystem fireworksParticleSystem;

    void Start()
    {
        StartCoroutine(ShowText());
        StartCoroutine(TriggerFireworks());

        winLabelTextAnimatorPlayer.onTextDisappeared.AddListener(onYouWinTextDisappeared);
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            winLabelTextAnimatorPlayer.SetTypewriterSpeed(winLabelFadeOutSpeed);
            winLabelTextAnimatorPlayer.StartDisappearingText();
        }
    }

    void onYouWinTextDisappeared()
    {
        creditsLabel.LeanMoveY(creditsLabel.position.y * -1f, creditsScrollTimeInSeconds).setOnComplete(onCreditsTextDisappeared);
    }

    void onCreditsTextDisappeared()
    {
        LoadingManager.Instance.LoadScene("MainMenu");
    }

    IEnumerator ShowText()
    {
        yield return new WaitForSeconds(winLabelStartDelay);
        winLabelTextAnimatorPlayer.ShowText(winLabelText);
    }

    IEnumerator TriggerFireworks()
    {
        yield return new WaitForSeconds(fireworksDelay);

        fireworksParticleSystem.transform.position = Random.insideUnitCircle * 4;

        fireworksParticleSystem.Play();

        yield return TriggerFireworks();
    }
}
