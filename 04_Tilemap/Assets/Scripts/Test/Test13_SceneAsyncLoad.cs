using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Test13_SceneAsyncLoad : TestBase
{
    public string sceneName = "LoadSampleScene";

    AsyncOperation async; // 비동기 관련 정보나 명령을 내리기 위한 객체. 비동기 함수의 return 값으로 받음.

    protected override void Test1_performed(InputAction.CallbackContext context)
    {   
        SceneManager.LoadScene(sceneName);
    }

    protected override void Test2_performed(InputAction.CallbackContext context)
    {
        async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false; // 비동기 씬 로딩 작업이 완료되어도 자동으로 씬 전환을 하지 않는다.
    }

    protected override void Test3_performed(InputAction.CallbackContext context)
    {
        async.allowSceneActivation = true;
    }

    protected override void Test4_performed(InputAction.CallbackContext context)
    {
        StartCoroutine(LoadSceneCoroutine());
    }

    IEnumerator LoadSceneCoroutine()
    {
        async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;

        // allowSceneActivation 이 false 면 progress는 0.9 까지만 올라간다.
        while (async.progress < 0.9f)
        {
            Debug.Log($"Progress : {async.progress}");
            yield return null;  
        }

        Debug.Log("Loading Complete.");
    }
}
