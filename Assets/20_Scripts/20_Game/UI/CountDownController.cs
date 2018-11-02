using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public sealed class CountDownController
{
	[SerializeField]
	private Text player1Text = null;
	[SerializeField]
	private Text player2Text = null;

	public void Start()
	{
		this.player1Text.enabled = true;
		this.player2Text.enabled = true;
		this.player1Text.text = " ";
		this.player2Text.text = " ";
	}

	public void Update(string _text)
	{
		this.player1Text.text = _text;
		this.player2Text.text = _text;
	}

	public void End()
	{
		this.player1Text.enabled = false;
		this.player2Text.enabled = false;
	}
}
