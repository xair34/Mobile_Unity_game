using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public Image img;
    private void Start()
    {
        StartCoroutine(LoadSceneLevel(2));
    }
    IEnumerator LoadSceneLevel(int sceneIndex)
    {
        Time.timeScale = 1f;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            img.fillAmount = operation.progress;
            yield return null;
        }
        yield return new WaitForEndOfFrame();
    }
}
