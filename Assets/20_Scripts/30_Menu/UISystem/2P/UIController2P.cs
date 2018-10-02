using UnityEngine;
using UnityEngine.UI;

public enum Player
{
    pl1,
    pl2,
}

public sealed class UIController2P : MonoBehaviour
{
    [System.Serializable]
    public struct PlayerKey
    {
        public KeyCode nextKey;
        public KeyCode prevKey;
        public KeyCode returnKey;
        public KeyCode cancelKey;
    }

    public struct SelecterStates
    {
        public int nowIndex;
        public float nextKeyPressedTime;
        public float prevKeyPressedTime;
        public bool isSelected;
    }

    [SerializeField]
    private CharaUIContent2P[] contents = new CharaUIContent2P[0];

    [Space(16)]

    [SerializeField]
    private float pressedWaitTime = 1.0f;

    [Space(16)]

    [SerializeField]
    private PlayerKey pl1key;
    [SerializeField]
    private int pl1FirstIndex;

    [Space(16)]

    [SerializeField]
    private PlayerKey pl2key;
    [SerializeField]
    private int pl2FirstIndex;

    private float elapsedTime;

    private SelecterStates pl1States;
    private SelecterStates pl2States;
    private PlayerCharacterEnum pl1chara;
    private PlayerCharacterEnum pl2chara;

    private void Start()
    {
        elapsedTime = Time.deltaTime;

        this.pl1States.nowIndex = this.pl1FirstIndex;
        this.pl1States.nextKeyPressedTime = 0f;
        this.pl1States.prevKeyPressedTime = 0f;
        this.pl1States.isSelected = false;

        this.pl2States.nowIndex = this.pl2FirstIndex;
        this.pl2States.nextKeyPressedTime = 0f;
        this.pl2States.prevKeyPressedTime = 0f;
        this.pl2States.isSelected = false;

        this.contents[this.pl1States.nowIndex].Pl1Overed = true;
        this.contents[this.pl2States.nowIndex].Pl2Overed = true;
    }

    private void Update()
    {
        this.elapsedTime += Time.deltaTime;

        this.MoveSelect();
        this.ActionSelect();
    }

    private void MoveSelect()
    {
        if (pl1States.isSelected) { goto pl2MoveSelect; }

        if (Input.GetKeyDown(this.pl1key.nextKey))
        {
            this.NextContent(Player.pl1);
            this.pl1States.nextKeyPressedTime = this.elapsedTime;
        }

        if (Input.GetKeyDown(this.pl1key.prevKey))
        {
            this.PrevContent(Player.pl1);
            this.pl1States.prevKeyPressedTime = this.elapsedTime;
        }

        if (Input.GetKey(this.pl1key.nextKey) && this.pressedWaitTime <= this.elapsedTime - this.pl1States.nextKeyPressedTime)
        {
            this.NextContent(Player.pl1);
        }

        if (Input.GetKey(this.pl1key.prevKey) && this.pressedWaitTime <= this.elapsedTime - this.pl1States.prevKeyPressedTime)
        {
            this.PrevContent(Player.pl1);
        }

        pl2MoveSelect:

        if (pl2States.isSelected) { return; }

        if (Input.GetKeyDown(this.pl2key.nextKey))
        {
            this.NextContent(Player.pl2);
            this.pl2States.nextKeyPressedTime = this.elapsedTime;
        }

        if (Input.GetKeyDown(this.pl2key.prevKey))
        {
            this.PrevContent(Player.pl2);
            this.pl2States.prevKeyPressedTime = this.elapsedTime;
        }

        if (Input.GetKey(this.pl2key.nextKey) && this.pressedWaitTime <= this.elapsedTime - this.pl2States.nextKeyPressedTime)
        {
            this.NextContent(Player.pl2);
        }

        if (Input.GetKey(this.pl2key.prevKey) && this.pressedWaitTime <= this.elapsedTime - this.pl2States.prevKeyPressedTime)
        {
            this.PrevContent(Player.pl2);
        }
    }

    private void ActionSelect()
    {
        if (Input.GetKeyDown(this.pl1key.returnKey))
        {
            this.pl1chara = this.contents[this.pl1States.nowIndex].ReturnAction(Player.pl1);
            this.pl1States.isSelected = true;
        }

        if (Input.GetKeyDown(this.pl1key.cancelKey))
        {
            this.contents[this.pl1States.nowIndex].CancelAction(Player.pl1);
            this.pl1States.isSelected = false;
        }

        if (Input.GetKeyDown(this.pl2key.returnKey))
        {
            this.pl2chara = this.contents[this.pl2States.nowIndex].ReturnAction(Player.pl2);
            this.pl2States.isSelected = true;
        }

        if (Input.GetKeyDown(this.pl2key.cancelKey))
        {
            this.contents[this.pl2States.nowIndex].CancelAction(Player.pl2);
            this.pl2States.isSelected = false;
        }
    }
    
    private void NextContent(Player _pl)
    {
        if (_pl == Player.pl1)
        {
            this.contents[this.pl1States.nowIndex].Pl1Overed = false;
            this.pl1States.nowIndex = (this.pl1States.nowIndex + 1) % this.contents.Length;
            this.contents[this.pl1States.nowIndex].Pl1Overed = true;
        }
        else
        {
            this.contents[this.pl2States.nowIndex].Pl2Overed = false;
            this.pl2States.nowIndex = (this.pl2States.nowIndex + 1) % this.contents.Length;
            this.contents[this.pl2States.nowIndex].Pl2Overed = true;
        }
    }

    private void PrevContent(Player _pl)
    {
        if (_pl == Player.pl1)
        {
            this.contents[this.pl1States.nowIndex].Pl1Overed = false;
            this.pl1States.nowIndex = (this.contents.Length + pl1States.nowIndex - 1) % this.contents.Length;
            this.contents[this.pl1States.nowIndex].Pl1Overed = true;
        }
        else
        {
            this.contents[this.pl2States.nowIndex].Pl2Overed = false;
            this.pl2States.nowIndex = (this.contents.Length + pl2States.nowIndex - 1) % this.contents.Length;
            this.contents[this.pl2States.nowIndex].Pl2Overed = true;
        }
    }
}
