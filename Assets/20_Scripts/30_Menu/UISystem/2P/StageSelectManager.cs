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

            this.nextWindowSpownTime = value ? this.elapsedTime : float.MaxValue;
        }
    }

    private bool IsPrevWindow
    {
        get { return this.isPrevWindow; }

        set
        {
            this.isPrevWindow = value;
            this.prevWindowImage.enabled = value;
            
            this.prevWindowSpownTime = value ? this.elapsedTime : float.MaxValue;
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
    private float prevToCharaSelectIntervalTime = 0.5f;
    [SerializeField]
    private Image nextWindowImage = null;
    [SerializeField]
    private float nextToCharaSelectIntervalTime = 0.5f;
    [Space(8)]
    [SerializeField]
    private Sprite[] sprites = new Sprite[0];

    private float elapsedTime;
    private StageEnum stage;
    private int nowIndex;
    private bool isNextWindow;
    private float nextWindowSpownTime;
    private bool isPrevWindow;
    private float prevWindowSpownTime;

    protected override void OnInit()
    {
        this.elapsedTime = 0;

        this.stage = StageEnum.stage1;
        this.nowIndex = 0;
        this.IsNextWindow = false;
        this.IsPrevWindow = false;
        this.prevWindowSpownTime = float.MaxValue;
        this.nextWindowSpownTime = float.MaxValue;
    }

    public void Run()
    {
        this.elapsedTime += Time.deltaTime;

//        if (Input.GetKeyDown(this.pl1key.NextKey) || Input.GetKeyDown(this.pl2key.NextKey))
	    if (RiaInput.Instance.GetPushDown(RiaInput.KeyType.Right, PlayerNumber.player1) || 
	        RiaInput.Instance.GetPushDown(RiaInput.KeyType.Right, PlayerNumber.player2))
        {
            NextStage();
        }

//        if (Input.GetKeyDown(this.pl1key.PrevKey) || Input.GetKeyDown(this.pl2key.PrevKey))
	    if (RiaInput.Instance.GetPushDown(RiaInput.KeyType.Left, PlayerNumber.player1) || 
	        RiaInput.Instance.GetPushDown(RiaInput.KeyType.Left, PlayerNumber.player2))
        {
            PrevStage();
        }

//        if (Input.GetKeyDown(this.pl1key.ReturnKey) || Input.GetKeyDown(this.pl2key.ReturnKey))
	    if (RiaInput.Instance.GetPushDown(RiaInput.KeyType.Return, PlayerNumber.player1) || 
	        RiaInput.Instance.GetPushDown(RiaInput.KeyType.Return, PlayerNumber.player2))
        {
            ReturnAction();
        }

//        if (Input.GetKeyDown(this.pl1key.CancelKey) || Input.GetKeyDown(this.pl2key.CancelKey))
	    if (RiaInput.Instance.GetPushDown(RiaInput.KeyType.Cancel, PlayerNumber.player1) || 
	        RiaInput.Instance.GetPushDown(RiaInput.KeyType.Cancel, PlayerNumber.player2))
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
        if (this.IsNextWindow && 
            this.nextToCharaSelectIntervalTime <= this.elapsedTime - this.nextWindowSpownTime)
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
        else if (this.IsPrevWindow &&
                 this.prevToCharaSelectIntervalTime <= this.elapsedTime - this.prevWindowSpownTime)
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
