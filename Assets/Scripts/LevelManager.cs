using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private GameObject loadingCanvas;
    [SerializeField] private Image progressBar;
    private float progressFill = 0.0f;

    protected override void Awake()
    {
        loadingCanvas.SetActive(false);
    }

    private void Update()
    {
        progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, progressFill, 3 * Time.deltaTime);
    }
    
    public async void LoadScene(string sceneName)
    {
        progressFill = 0.0f;
        progressBar.fillAmount = 0.0f;
        
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;
        loadingCanvas.SetActive(true);

        do
        {
            progressFill = scene.progress;

        } while (scene.progress < 0.9f);

        scene.allowSceneActivation = true;
        loadingCanvas.SetActive(false);
    }

    // IEnumerator LoadAsyncScene()
    // {
    //     AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Scene2");

    //     // Wait until the asynchronous scene fully loads
    //     while (!asyncLoad.isDone)
    //     {
    //         yield return null;
    //     }
    // }
}
