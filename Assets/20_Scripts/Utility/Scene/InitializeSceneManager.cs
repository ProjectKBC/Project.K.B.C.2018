using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializeSceneManager : MonoBehaviour
{
    [SerializeField]
    private string commonSceneName = "CommonScene";
    [SerializeField]
    private string firstSceneName = "TitleScene";

    private bool isFirstUpdate;

    private void Awake()
    {
        SceneManager.LoadScene(this.commonSceneName, LoadSceneMode.Additive);
        isFirstUpdate = true;
    }

    private void Update()
    {
        if (isFirstUpdate) { this.isFirstUpdate = false; return; }

        SceneManager.LoadScene(this.firstSceneName);
    }
}
