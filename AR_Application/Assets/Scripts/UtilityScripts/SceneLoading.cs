using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    public string defaultSceneName = "StartScene";

    public void LoadStartScene()
    {
        SceneManager.LoadScene(defaultSceneName);
    }

    public void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadSceneBySceneName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
