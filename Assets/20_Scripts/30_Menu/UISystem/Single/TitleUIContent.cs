using System;
using UnityEngine;
using UnityEditor;

public class TitleUIContent : UIContent
{
	private enum BUTTON
	{
		GameStart,
		Exit,
	}

	[SerializeField]
	private BUTTON button = BUTTON.GameStart;

	public override void ReturnAction()
	{
		switch (this.button)
		{
			case BUTTON.GameStart:
				FadeManager.Instance.LoadScene(2.0f, SceneEnum.Select.ToDescription());
				return;

			case BUTTON.Exit:
				FadeManager.Instance.FadeOut(1.0f, () =>
				{
#if UNITY_EDITOR
					EditorApplication.isPlaying = false;
#else
					Application.Quit();
#endif
				});
				return;
		}
	}

	public override void CancelAction()
	{
		switch (this.button)
		{
			case BUTTON.GameStart:
				return;

			case BUTTON.Exit:
				return;
		}
	}
}
