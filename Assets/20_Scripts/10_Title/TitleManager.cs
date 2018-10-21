using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    private string nextSceneName = string.Empty;
    [Space(8.0f)]
    [SerializeField]
    private Canvas canvas = null;
    [SerializeField]
    private CanvasScaler canvasScale = null;
    [SerializeField]
    private GameObject pEDPSImage = null;
    [SerializeField]
    private GameObject pKBCProImage = null;
    [SerializeField]
    private GameObject pTitleImage = null;

    private IEnumerator Start()
    {
        yield return FadeManager.Instance.FadeOut(0);

        this.canvas.renderMode = RenderMode.ScreenSpaceCamera;
        
        this.canvasScale.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        this.canvasScale.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
        this.canvasScale.referenceResolution = 
            new Vector2(FadeManager.Instance.ScreenWidth, FadeManager.Instance.ScreenHeight);

        this.pEDPSImage.SetActive(false);
        this.pKBCProImage.SetActive(false);
        this.pTitleImage.SetActive(false);

        // EDPSの描画
        this.pEDPSImage.SetActive(true);
        yield return FadeManager.Instance.FadeIn(1.0f);
        yield return FadeManager.Instance.Wait(3.0f, _isBreak: () => Input.GetKeyDown(KeyCode.Return));
        yield return FadeManager.Instance.FadeOut(1.0f);

        // EDPSの描画 -> KBCProの描画
        this.pEDPSImage.SetActive(false);
        this.pKBCProImage.SetActive(true);
        yield return FadeManager.Instance.Wait(1.0f);
        yield return FadeManager.Instance.FadeIn(1.0f);
        yield return FadeManager.Instance.Wait(3.0f, _isBreak: () => Input.GetKeyDown(KeyCode.Return));
        yield return FadeManager.Instance.FadeOut(1.0f);

        // KBCProの描画 -> Titleの描画
        this.pKBCProImage.SetActive(false);
        this.pTitleImage.SetActive(true);
        yield return FadeManager.Instance.Wait(1.0f);
        yield return FadeManager.Instance.FadeIn(1.0f);
        
        // 入力待ち
        yield return FadeManager.Instance.Wait(0.0f, _isBreak: () => Input.GetKeyDown(KeyCode.Return));

        yield return FadeManager.Instance.FadeOut(1.0f);
        SceneManager.LoadScene(this.nextSceneName);
    }
}
