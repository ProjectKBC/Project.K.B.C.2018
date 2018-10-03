using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializeSceneManager : MonoBehaviour
{
    [SerializeField]
    private string commonSceneName = "CommonScene";
    [SerializeField]
    private string firstSceneName = "TitleScene";

    private void Awake()
    {
        SceneManager.LoadScene(this.commonSceneName, LoadSceneMode.Additive);
        SceneManager.LoadScene(this.firstSceneName);
    }
}
