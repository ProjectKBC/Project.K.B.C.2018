using System;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectManager : SingletonMonoBehaviour<StageSelectManager>
{
    [System.Serializable]
    private struct PlayerKey
    {
        public KeyCode NextKey;
        public KeyCode PrevKey;
        public KeyCode ReturnKey;
        public KeyCode CancelKey;
    }

    private bool IsNextWindow
    {
        get { return this.isNextWindow; }

        set
        {
            this.isNextWindow = value;
            this.nextWindowImage.enabled = value;
        }
    }

    private bool IsPrevWindow
    {
        get { return this.isPrevWindow; }

        set
        {
            this.isPrevWindow = value;
            this.prevWindowImage.enabled = value;
        }
    }

    [SerializeField, Header("Key Config")]
    private PlayerKey pl1key;
    [SerializeField]
    private PlayerKey pl2key;
    [Space(16)]
    [SerializeField]
    private Image stageFrontImage = null;
    [SerializeField]
    private Image prevWindowImage = null;
    [SerializeField]
    private Image nextWindowImage = null;
    [Space(8)]
    [SerializeField]
    private Sprite[] sprites = new Sprite[0];

    private StageEnum stage;
    private int nowIndex;
    private bool isNextWindow;
    private bool isPrevWindow;

    protected override void OnInit()
    {
        this.stage = StageEnum.stage1;
        this.nowIndex = 0;
        this.IsNextWindow = false;
        this.IsPrevWindow = false;
    }

    public void Run()
    {
        if (Input.GetKeyDown(this.pl1key.NextKey) || Input.GetKeyDown(this.pl2key.NextKey))
        {
            NextStage();
        }

        if (Input.GetKeyDown(this.pl1key.PrevKey) || Input.GetKeyDown(this.pl2key.PrevKey))
        {
            PrevStage();
        }

        if (Input.GetKeyDown(this.pl1key.ReturnKey) || Input.GetKeyDown(this.pl2key.ReturnKey))
        {
            ReturnAction();
        }

        if (Input.GetKeyDown(this.pl1key.CancelKey) || Input.GetKeyDown(this.pl2key.CancelKey))
        {
            CancelAction();
        }
    }

    private void NextStage()
    {
        this.nowIndex = (this.nowIndex + 1) % (int)StageEnum.length_empty;
        this.stage = (StageEnum)Enum.ToObject(typeof(StageEnum), this.nowIndex);
        UpdateImage();
    }

    private void PrevStage()
    {
        this.nowIndex = ((int)StageEnum.length_empty + this.nowIndex - 1) % (int)StageEnum.length_empty;
        this.stage = (StageEnum)Enum.ToObject(typeof(StageEnum), this.nowIndex);
        UpdateImage();
    }

    private void ReturnAction()
    {
        if (this.IsNextWindow)
        {
            SelectUIManager.Instance.TransitionToGameScene(this.stage);
        }
        else if (this.IsPrevWindow)
        {
            this.IsPrevWindow = false;
        }
        else
        {
            if (this.stage == StageEnum.stage3 ||
                this.stage == StageEnum.stage4 ||
                this.stage == StageEnum.stage5)
            {
                // todo: 選択不能のSE
                return;
            }
            else
            {
                this.IsNextWindow = true;
            }
        }
    }

    private void CancelAction()
    {
        if (this.IsNextWindow)
        {
            this.IsNextWindow = false;
        }
        else if (this.IsPrevWindow)
        {
            this.OnInit();
            SelectUIManager.Instance.TransitionToCharactetSelect();
        }
        else
        {
            this.IsPrevWindow = true;
        }
    }

    private void UpdateImage()
    {
        this.stageFrontImage.sprite = this.sprites[this.nowIndex];
    }
}
