using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingBar;
    public Text loadingText;
    public GameObject pressAnyKey;
    public void LoadLevel(int index)
    {
        StartCoroutine(LoadAsynchronously(index));
    }

    IEnumerator LoadAsynchronously(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        operation.allowSceneActivation = false;
        loadingScreen.SetActive(true);
        pressAnyKey.SetActive(false);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadingBar.value = progress;
            loadingText.text = progress * 100f + "%";

            if (progress == 1)
                pressAnyKey.SetActive(true);

            if (Input.anyKeyDown)
                operation.allowSceneActivation = true;
            yield return null;
        }
        


    }
}
