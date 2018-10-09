using UnityEngine;
using UnityEngine.UI;

public sealed class UIController : MonoBehaviour
{
    [SerializeField]
    private UIContent[] contents = new UIContent[0];

    [Space(16)]

    [SerializeField]
    private KeyCode nextKey = KeyCode.RightArrow;
    [SerializeField]
    private KeyCode prevKey = KeyCode.LeftArrow;
    [SerializeField]
    private KeyCode returnKey = KeyCode.Return;
    [SerializeField]
    private KeyCode cancelKey = KeyCode.Delete;

    [SerializeField]
    private int firstIndex = 0;

    [SerializeField]
    private float pressedWaitTime = 1.0f;

    private float elapsedTime;
    private int nowIndex;
    private float nextKeyPressedTime;
    private float prevKeyPressedTime;

    private void Awake()
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

        if (Input.GetKeyDown(this.returnKey))
        {
            this.contents[this.nowIndex].ReturnAction();
        }

        if (Input.GetKeyDown(this.cancelKey))
        {
            this.contents[this.nowIndex].CancelAction();
        }
    }

    private void MoveSelect()
    {
        if (Input.GetKeyDown(this.nextKey))
        {
            this.NextContent();
            this.nextKeyPressedTime = this.elapsedTime;
        }

        if (Input.GetKeyDown(this.prevKey))
        {
            this.PrevContent();
            this.prevKeyPressedTime = this.elapsedTime;
        }

        if (Input.GetKey(this.nextKey) && this.pressedWaitTime <= this.elapsedTime - this.nextKeyPressedTime)
        {
            this.NextContent();
        }

        if (Input.GetKey(this.prevKey) && this.pressedWaitTime <= this.elapsedTime - this.prevKeyPressedTime)
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
    }

    private void PrevContent()
    {
        this.GetImage(this.nowIndex).sprite = this.contents[this.nowIndex].NormalSprite;
        this.nowIndex = (this.contents.Length + this.nowIndex - 1) % this.contents.Length;
        this.GetImage(this.nowIndex).sprite = this.contents[this.nowIndex].ActiveSprite;
    }
}
