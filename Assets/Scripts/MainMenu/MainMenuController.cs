using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Febucci.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private TextAnimatorPlayer titleLabelTextAnimatorPlayer;
    [SerializeField] private string titleLabelText;
    [SerializeField] private float titleLabelStartDelay = 2f;

    [Space]

    [SerializeField] private TextAnimatorPlayer pressToStartLabelTextAnimatorPlayer;
    [SerializeField] private string pressToStartLabelText;
    [SerializeField] private float pressToStartLabelStartDelay = 2f;

    bool canStart = false;

    void Awake()
    {
    }

    void Start()
    {
        StartCoroutine(ShowText());
    }

    void Update()
    {
        if (Input.anyKeyDown && canStart)
        {
            //LoadingManager.Instance.LoadScene("Game");
            LoadingManager.Instance.LoadScene("AM_Depths");
        }
    }

    IEnumerator ShowText()
    {
        yield return new WaitForSeconds(titleLabelStartDelay);
        titleLabelTextAnimatorPlayer.ShowText(titleLabelText);

        yield return new WaitForSeconds(pressToStartLabelStartDelay);
        pressToStartLabelTextAnimatorPlayer.ShowText(pressToStartLabelText);

        canStart = true;
    }
}
