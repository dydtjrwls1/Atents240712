using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncLoadingBackground : MonoBehaviour
{
    public string nextSceneName = "LoadSampleScene";

    public float tickTime = 0.2f;

    // slider 의 value가 증가하는 속도
    public float loadingBarSpeed = 1.0f;

    TextMeshProUGUI loadingText;
    Slider loadingSlider;

    PlayerInputActions inputAction;

    AsyncOperation async;

    bool onInput = false;

    string[] tickTexts = { "Loading .", "Loading . .", "Loading . . .", "Loading . . . .", "Loading . . . . ." };

    private void Awake()
    {
        loadingText = GetComponentInChildren<TextMeshProUGUI>();
        loadingSlider = GetComponentInChildren<Slider>();

        inputAction = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputAction.UI.Enable();
        loadingText.text = tickTexts[0];
    }

    private void Start()
    {
        StartCoroutine(SceneAsyncLoad());
        StartCoroutine(LoadingTextUpdater());
    }

    IEnumerator SceneAsyncLoad()
    {
        float elapsedTime = 0.0f;

        async = SceneManager.LoadSceneAsync(nextSceneName);
        async.allowSceneActivation = false;

        while(async.progress < 0.9f || elapsedTime < loadingBarSpeed)
        {
            elapsedTime += Time.deltaTime;

            loadingSlider.value = elapsedTime;

            yield return null;
        }

        inputAction.UI.AnyInput.performed += (_) => { onInput = true; };

        while (!onInput)
        {
            yield return null;
        }

        async.allowSceneActivation = true;
    }

    IEnumerator LoadingTextUpdater()
    {
        float elapsedTime = 0.0f;
        int index = 0;


        while (loadingSlider.value < 1.0f)
        {
            elapsedTime += Time.deltaTime;

            if(elapsedTime > tickTime)
            {
                elapsedTime = 0.0f;
                index = index + 1 % tickTexts.Length;
                loadingText.text = tickTexts[index];
            }

            yield return null;
        }

        loadingText.text = "Loading Complete!";
    }

    //IEnumerator FadeInOut()
    //{

    //}
}
