using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

[RequireComponent(typeof(CanvasGroup))]
public class LoadingManager : MonoBehaviour
{
    public static LoadingManager Instance;

    [SerializeField] private float fadeTimeInSeconds = 0.2f;
    [SerializeField] private GameObject screen;
    //[SerializeField]
    //private Image progressBar;

    private CanvasGroup screenCanvasGroup;
    private Canvas screenCanvas;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        screenCanvasGroup = GetComponent<CanvasGroup>();
        screenCanvas = transform.Find("Screen").GetComponent<Canvas>();
    }

    public void Update()
    {
        if (screenCanvas.worldCamera == null)
        {
            screenCanvas.worldCamera = Camera.main;
        }
    }

    public async void LoadScene(string sceneName)
    {
        screen.SetActive(true);
        screenCanvasGroup.alpha = 0f;
        screenCanvasGroup.LeanAlpha(1f, fadeTimeInSeconds);
        await Task.Delay(SecondsToMilliseconds(fadeTimeInSeconds));

        AsyncOperation scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        while (!scene.isDone)
        {
            //progressBar.fillAmount = scene.progress;
            if (Mathf.Approximately(scene.progress, 0.9f))
            {
                scene.allowSceneActivation = true;
            }
            await Task.Delay(100);
        }

        await Task.Delay(100);

        screenCanvasGroup.LeanAlpha(0f, fadeTimeInSeconds);
        await Task.Delay(SecondsToMilliseconds(fadeTimeInSeconds));

        screen.SetActive(false);
    }

    public void ReloadCurrentScene()
    {
        LoadScene(SceneManager.GetActiveScene().name);
    }

    private int SecondsToMilliseconds(float seconds)
    {
        return Mathf.RoundToInt(fadeTimeInSeconds * 1000);
    }
}
