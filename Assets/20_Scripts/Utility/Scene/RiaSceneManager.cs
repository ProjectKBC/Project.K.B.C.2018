using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RiaSceneManager : SingletonMonoBehaviour<RiaSceneManager>
{
    protected override void OnInit()
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

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
}
