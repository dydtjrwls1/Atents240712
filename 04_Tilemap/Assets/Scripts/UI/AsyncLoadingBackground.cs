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
    TextMeshProUGUI pressText;
    Slider loadingSlider;

    PlayerInputActions inputAction;

    AsyncOperation async;

    // 로딩이 완료되었는지를 표시하는 변수
    bool loadingDone = false;

    string[] tickTexts = { "Loading .", "Loading . .", "Loading . . .", "Loading . . . .", "Loading . . . . ." };

    private void Awake()
    {
        Transform child = transform.GetChild(0);
        loadingText = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(1);
        loadingSlider = child.GetComponent<Slider>();

        child = transform.GetChild(2);
        pressText = child.GetComponent<TextMeshProUGUI>();

        inputAction = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputAction.UI.Enable();
        inputAction.UI.AnyInput.performed += AnyInput_performed;
    }

    

    private void OnDisable()
    {
        inputAction.UI.AnyInput.performed -= AnyInput_performed;
        inputAction.UI.Disable();
    }

    private void Start()
    {
        async = SceneManager.LoadSceneAsync(nextSceneName);
        async.allowSceneActivation = false;

        StartCoroutine(LoadingSliderUpdater());       // 슬라이더용 코루틴 시작
        StartCoroutine(LoadingTextUpdater());         // 텍스트용 코루틴 시작
    }
    private void AnyInput_performed(UnityEngine.InputSystem.InputAction.CallbackContext _)
    {
        async.allowSceneActivation = loadingDone;
    }

    // 로딩 슬라이더 업데이트 코루틴
    IEnumerator LoadingSliderUpdater()
    {
        loadingSlider.value = 0.0f;
        while (async.progress < 0.9f) // 로딩 완료까지 일정한 속도로 슬라이더 증가
        {
            loadingSlider.value += Time.deltaTime * loadingBarSpeed;
            yield return null;
        }

        float remainTime = (1 - loadingSlider.value) / loadingBarSpeed; // 슬라이더가 가득 찰 때까지 남은시간
        float elapsedTime = 0.0f;
        while (remainTime > elapsedTime) // 남은 시간동안 슬라이더 진행
        {
            elapsedTime += Time.deltaTime;
            loadingSlider.value += Time.deltaTime * loadingBarSpeed;
            yield return null;
        }

        loadingDone = true;

        StartCoroutine(FadeInOut());
    }

    // 로딩 중 텍스트 표시 코루틴
    IEnumerator LoadingTextUpdater()
    {
        WaitForSeconds wait = new WaitForSeconds(tickTime);

        int index = 0;
        while (!loadingDone) // ticktime 간격으로 문자 출력하기
        {
            loadingText.text = tickTexts[index];
            index = index + 1 % tickTexts.Length;

            yield return wait;
        }

        loadingText.text = "Loading\nComplete!";
    }

    // 로딩 완료후 텍스트 표시 코루틴
    IEnumerator FadeInOut()
    {
        Color c = pressText.color;
        float elapsedTime = 0.0f;

        while (true)
        {
            elapsedTime += Time.deltaTime;
            float delta = (Mathf.Sin(elapsedTime * 3.0f) + 1) * 0.5f;

            c.a = delta;
            pressText.color = c;

            yield return null;
        }
    }
}
