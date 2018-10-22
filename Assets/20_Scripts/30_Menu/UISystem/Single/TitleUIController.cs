using UnityEngine;
using UnityEngine.UI;

public sealed class TitleUIController : SingletonMonoBehaviour<TitleUIController>
{
	[System.Serializable]
	private class KeyConfig
	{
		public KeyCode nextKey = KeyCode.RightArrow;
		public KeyCode prevKey = KeyCode.LeftArrow;
		public KeyCode returnKey = KeyCode.Return;
		public KeyCode cancelKey = KeyCode.Delete;
	}

	[Space(16)]

	[SerializeField]
	private KeyConfig keyConfig = null;

	[Space(16)]


	[SerializeField, Header("TitleUIContents")]
    private TitleUIContent[] contents = new TitleUIContent[0];

	[SerializeField]
	private int exitIndex = 0;

	[Space(8)]

	[SerializeField]
    private int firstIndex = 0;

    [SerializeField]
    private float pressedWaitTime = 1.0f;

    private float elapsedTime;
    private int nowIndex;
    private float nextKeyPressedTime;
    private float prevKeyPressedTime;

    protected override void OnInit()
    {
        elapsedTime = Time.deltaTime;
        this.nowIndex = this.firstIndex;
        this.nextKeyPressedTime = 0f;
        this.prevKeyPressedTime = 0f;

        this.GetImage(this.nowIndex).sprite = this.contents[this.nowIndex].ActiveSprite;
        for (int i = 0; i < this.contents.Length; ++i)
        {
            if (i == this.nowIndex) { continue; }

            this.GetImage(i).sprite = this.contents[i].NormalSprite;
        }
    }

    private void Update()
    {
        this.elapsedTime += Time.deltaTime;

        this.MoveSelect();

        if (Input.GetKeyDown(this.keyConfig.returnKey))
        {
            this.contents[this.nowIndex].ReturnAction();

			// todo: 決定音
		}

		if (Input.GetKeyDown(this.keyConfig.cancelKey))
        {
			// this.contents[this.nowIndex].CancelAction();

			this.GetImage(this.nowIndex).sprite = this.contents[this.nowIndex].NormalSprite;
			this.nowIndex = this.exitIndex;
			this.GetImage(this.nowIndex).sprite = this.contents[this.nowIndex].ActiveSprite;

			// todo: キャンセル音
		}
	}

    private void MoveSelect()
    {
        if (Input.GetKeyDown(this.keyConfig.nextKey))
        {
            this.NextContent();
            this.nextKeyPressedTime = this.elapsedTime;
        }
		else if (Input.GetKeyDown(this.keyConfig.prevKey))
        {
            this.PrevContent();
            this.prevKeyPressedTime = this.elapsedTime;
        }
		else if (Input.GetKey(this.keyConfig.nextKey) && this.pressedWaitTime <= this.elapsedTime - this.nextKeyPressedTime)
        {
            this.NextContent();
        }
		else if (Input.GetKey(this.keyConfig.prevKey) && this.pressedWaitTime <= this.elapsedTime - this.prevKeyPressedTime)
        {
            this.PrevContent();
        }
    }

    private Image GetImage(int _index)
    {
        if (this.contents.Length <= _index) { return null; }

        return this.contents[_index].Image;
    }

    private void NextContent()
    {
        this.GetImage(this.nowIndex).sprite = this.contents[this.nowIndex].NormalSprite;
        this.nowIndex = (this.nowIndex + 1) % this.contents.Length;
        this.GetImage(this.nowIndex).sprite = this.contents[this.nowIndex].ActiveSprite;

		// todo: 移動音
	}

	private void PrevContent()
    {
        this.GetImage(this.nowIndex).sprite = this.contents[this.nowIndex].NormalSprite;
        this.nowIndex = (this.contents.Length + this.nowIndex - 1) % this.contents.Length;
        this.GetImage(this.nowIndex).sprite = this.contents[this.nowIndex].ActiveSprite;

		// todo: 移動音
	}
}