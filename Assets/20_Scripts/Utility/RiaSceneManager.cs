using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RiaSceneManager : SingletonMonoBehaviour<RiaSceneManager>
{
    protected override void OnAwake()
    {
        StartCoroutine(this.CheckManagers());
    }

    private IEnumerator CheckManagers()
    {
        while (true)
        {
            if (FadeManager.Instance) { break; }
            if (AudioManager.Instance) { break; }

            yield return null;
        }
    }
}
