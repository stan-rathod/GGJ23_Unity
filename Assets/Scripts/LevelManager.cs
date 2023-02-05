using System.Collections;
using System.Threading.Tasks;
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
        await LoadingScreenTransition(0, 1);

        do
        {
            progressFill = scene.progress;

        } while (scene.progress < 0.9f);

        scene.allowSceneActivation = true;
        await LoadingScreenTransition(1, 0);
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

    [SerializeField] private Image transitionPanel;

    public async Task LoadingScreenTransition(float from = 0, float to = 1)
    {
        transitionPanel.fillAmount = from;
        transitionPanel.fillMethod = Image.FillMethod.Horizontal; // (Image.FillMethod)Random.Range(0, 4);
        transitionPanel.fillOrigin = 1; // Random.Range(0, 4);
        // transitionPanel.fillClockwise = (Random.Range(0, 2) == 1);

        while (transitionPanel.fillAmount != to)
        {
            transitionPanel.fillAmount = Mathf.MoveTowards(transitionPanel.fillAmount, to, 1 * Time.deltaTime);
            await Task.Yield();
        }
    }
}
