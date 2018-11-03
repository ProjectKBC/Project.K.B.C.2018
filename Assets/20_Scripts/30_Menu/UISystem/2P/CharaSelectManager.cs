using System;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public sealed class CharaSelectManager : SingletonMonoBehaviour<CharaSelectManager>
{
    [System.Serializable]
    private struct PlayerKey
    {
        public KeyCode UpKey;
        public KeyCode DownKey;
        public KeyCode LeftKey;
        public KeyCode RightKey;
        public KeyCode ReturnKey;
        public KeyCode CancelKey;
    }

    private class SelecterStates
    {
        public int NowIndex;
        public float KeyIntervalStartTime;
        public float RapidKeyWaitStartTime;
        public bool IsFirstCursorMove;
        public bool IsSelected;
        public PlayerCharacterEnum PlayerCharacter;
    }

    [System.Serializable]
    private struct CharaSelectorSet
    {
        public PlayerCharacterEnum PlayerCharacter;
        public Sprite StandPL1;
        public Sprite StandPL2;
        public Sprite KanaName;
        public Sprite AlpName;
        public Sprite Selector;

        public Image CursorImage;
        public Image SelectorImage;
    }

    [System.Serializable]
    private struct Sprites
    { 
        public Sprite Normal;
        public Sprite Over1;
        public Sprite Over2;
        public Sprite OverAll;
        public Sprite Select1;
        public Sprite Select2;
        public Sprite Over1Select2;
        public Sprite Over2Select1;
        public Sprite SelectAll;

        public Sprite StandBackNormal1;
        public Sprite StandBackActive1;
        public Sprite StandBackNormal2;
        public Sprite StandBackActive2;
    }

    [System.Serializable]
    private class Images
    {
        public Image StandBack = null;
        public Image StandFront = null;
        public Image Kana = null;
        public Image Alp = null;

        public void SetNameImages(Sprite _kana, Sprite _alp)
        {
            this.Kana.sprite = _kana;
            this.Alp.sprite = _alp;
        }
    }

    // serialize fields
    [SerializeField, Header("Key Config")]
    private PlayerKey pl1key;
    [SerializeField]
    private int pl1FirstIndex;
    [SerializeField]
    private PlayerKey pl2key;
    [SerializeField]
    private int pl2FirstIndex;
    [Space(8)]
    [SerializeField]
    private float pressedWaitTime = 1.0f;
    [SerializeField]
    private float pressedReactionIntervalTime = .03f;
    [SerializeField]
    private float pressedRapidIntervalTime = .1f;

    [Space(16)]

    [SerializeField, Header("Selector's Sprites")]
    private Sprites sprites = new Sprites();

    [Space(16)]

    [SerializeField, Header("CharaSelectorSets")]
    private CharaSelectorSet[] charaSelectorSets = new CharaSelectorSet[0];

    [Space(16)]

    [SerializeField]
    private Images pl1Images = null;

    [Space(8)]

	[SerializeField]
    private Images pl2Images = null;

	[Space(16)]

	[SerializeField, Header("NextToStageSelect")]
	private Image nextToStageSelectImage = null;

	[SerializeField]
	private float nextToStageSelectIntervalTime = 0.5f;

	[SerializeField, Header("PrevToTitle")]
	private Image prevToTitleImage = null;

	[SerializeField]
	private float prevToTitleIntervalTime = 0.5f;

	// private fields
	private float elapsedTime;
    private SelecterStates pl1States;
    private SelecterStates pl2States;
    private float allSelectedTime;
	private float dispPrevToTitleWindowTime;

	private bool IsPrevToTitleWindow { get { return this.prevToTitleImage.enabled; } }

    protected override void OnInit()
    {
        this.elapsedTime = 0;

        // private fields
        this.pl1States = new SelecterStates();
        this.pl1States.NowIndex = this.pl1FirstIndex;
        this.pl1States.KeyIntervalStartTime = 0f;
        this.pl1States.RapidKeyWaitStartTime = 0f;
        this.pl1States.IsFirstCursorMove = false;
        this.pl1States.IsSelected = false;
        this.pl1States.PlayerCharacter = PlayerCharacterEnum.length_empty;

        this.pl2States = new SelecterStates();
        this.pl2States.NowIndex = this.pl2FirstIndex;
        this.pl2States.KeyIntervalStartTime = 0f;
        this.pl2States.RapidKeyWaitStartTime = 0f;
        this.pl2States.IsFirstCursorMove = false;
        this.pl2States.IsSelected = false;
        this.pl2States.PlayerCharacter = PlayerCharacterEnum.length_empty;

		this.nextToStageSelectImage.enabled = false;
		this.prevToTitleImage.enabled = false;
		this.allSelectedTime = 0;

        // selectorの初期化
        for (int i = 0; i < this.charaSelectorSets.Length; ++i)
        {
            var set = this.charaSelectorSets[i];
            set.SelectorImage.sprite = set.Selector;
            set.CursorImage.sprite = this.sprites.Normal;
            UpdateCursor(i);
        }

        UpdateStand(PlayerNumber.player1);
        UpdateStand(PlayerNumber.player2);
        UpdateNames(PlayerNumber.player1);
        UpdateNames(PlayerNumber.player2);
    }

    public void Init()
    {
        this.pl1States.KeyIntervalStartTime = 0f;
        this.pl1States.RapidKeyWaitStartTime = 0f;
        this.pl1States.IsFirstCursorMove = false;
        this.pl1States.IsSelected = false;
        this.pl1States.PlayerCharacter = PlayerCharacterEnum.length_empty;
        
        this.pl2States.KeyIntervalStartTime = 0f;
        this.pl2States.RapidKeyWaitStartTime = 0f;
        this.pl2States.IsFirstCursorMove = false;
        this.pl2States.IsSelected = false;
        this.pl2States.PlayerCharacter = PlayerCharacterEnum.length_empty;

        this.nextToStageSelectImage.enabled = false;
        this.allSelectedTime = 0;

        for (int i = 0; i < charaSelectorSets.Length; ++i)
        {
            UpdateCursor(i);
        }

        this.pl1Images.StandBack.sprite = this.sprites.StandBackNormal1;
        this.pl2Images.StandBack.sprite = this.sprites.StandBackNormal2;
    }

    public void Run()
    {
        this.elapsedTime += Time.deltaTime;

        this.MoveCursor();
        this.ActionCursor();
    }

    private void MoveCursor()
    {
        // Player1
        if (!this.pl1States.IsSelected)
        {
            MoveCursorFunc(PlayerNumber.player1);
        }

        // Player2
        if (!this.pl2States.IsSelected)
        {
            MoveCursorFunc(PlayerNumber.player2);
        }

    }

    private void MoveCursorFunc(PlayerNumber _pl)
    {
        SelecterStates states = (_pl == PlayerNumber.player1) ? this.pl1States :
                                (_pl == PlayerNumber.player2) ? this.pl2States : null;
//        PlayerKey key = (_pl == PlayerNumber.player1) ? this.pl1key :
//                        (_pl == PlayerNumber.player2) ? this.pl2key : new PlayerKey();

        // いずれの方向キーも押されていない
//        if (!Input.GetKey(key.UpKey) && !Input.GetKey(key.DownKey) && !Input.GetKey(key.RightKey) && !Input.GetKey(key.LeftKey))
        if (!RiaInput.Instance.GetPush(RiaInput.KeyType.Up, _pl) &&
            !RiaInput.Instance.GetPush(RiaInput.KeyType.Down, _pl) &&
            !RiaInput.Instance.GetPush(RiaInput.KeyType.Right, _pl) &&
            !RiaInput.Instance.GetPush(RiaInput.KeyType.Left, _pl))
	    {
            states.KeyIntervalStartTime = -1;
            states.RapidKeyWaitStartTime = -1;
            states.IsFirstCursorMove = false;
            return;
        }
        // いずれかの方向キーがいずれも押されていない状態からKeyDownした
        else if (states.KeyIntervalStartTime == -1)
        {
            states.KeyIntervalStartTime = this.elapsedTime;
            states.RapidKeyWaitStartTime = this.elapsedTime;
            return;
        }

        // キー入力のインターバル時間に達していない
        if (this.elapsedTime - states.KeyIntervalStartTime < this.pressedReactionIntervalTime) { return; }
        
        // キーの連射入力時間に達しておらず、連射扱いとなる場合
        if (this.elapsedTime - states.RapidKeyWaitStartTime < this.pressedWaitTime && states.IsFirstCursorMove) { return; }

        // キーの連射速度時間に達していない場合
        if (this.elapsedTime - states.KeyIntervalStartTime < this.pressedRapidIntervalTime && states.IsFirstCursorMove) { return; }

        if (!states.IsFirstCursorMove)
        {
            states.IsFirstCursorMove = true;
        }

//        var isUp = Input.GetKey(key.UpKey) &&
//                   !Input.GetKey(key.DownKey) && !Input.GetKey(key.RightKey) && !Input.GetKey(key.LeftKey);
//
//        var isDown = Input.GetKey(key.DownKey) &&
//                     !Input.GetKey(key.UpKey) && !Input.GetKey(key.RightKey) && !Input.GetKey(key.LeftKey);
//
//        var isRight = Input.GetKey(key.RightKey) &&
//                      !Input.GetKey(key.UpKey) && !Input.GetKey(key.DownKey) && !Input.GetKey(key.LeftKey);
//
//        var isLeft = Input.GetKey(key.LeftKey) &&
//                     !Input.GetKey(key.UpKey) && !Input.GetKey(key.DownKey) && !Input.GetKey(key.RightKey);
//
//        var isUpLeft = Input.GetKey(key.UpKey) && Input.GetKey(key.LeftKey) &&
//                       !Input.GetKey(key.DownKey) && !Input.GetKey(key.RightKey);
//
//        var isUpRight = Input.GetKey(key.UpKey) && Input.GetKey(key.RightKey) &&
//                         !Input.GetKey(key.DownKey) && !Input.GetKey(key.LeftKey);
//
//        var isDownLeft = Input.GetKey(key.DownKey) && Input.GetKey(key.LeftKey) &&
//                          !Input.GetKey(key.UpKey) && !Input.GetKey(key.RightKey);
//
//        var isDownRight = Input.GetKey(key.DownKey) && Input.GetKey(key.RightKey) &&
//                           !Input.GetKey(key.UpKey) && !Input.GetKey(key.LeftKey);

	    // todo: ビットマスクでもっとスマートに書ける by flanny7
	    
	    var isUp = RiaInput.Instance.GetPush(RiaInput.KeyType.Up, _pl) &&
	               !RiaInput.Instance.GetPush(RiaInput.KeyType.Down, _pl) &&
	               !RiaInput.Instance.GetPush(RiaInput.KeyType.Right, _pl) &&
	               !RiaInput.Instance.GetPush(RiaInput.KeyType.Left, _pl);

	    var isDown = !RiaInput.Instance.GetPush(RiaInput.KeyType.Up, _pl) &&
				     RiaInput.Instance.GetPush(RiaInput.KeyType.Down, _pl) &&
				     !RiaInput.Instance.GetPush(RiaInput.KeyType.Right, _pl) &&
				     !RiaInput.Instance.GetPush(RiaInput.KeyType.Left, _pl);

	    var isRight = !RiaInput.Instance.GetPush(RiaInput.KeyType.Up, _pl) &&
	                  !RiaInput.Instance.GetPush(RiaInput.KeyType.Down, _pl) &&
	                  RiaInput.Instance.GetPush(RiaInput.KeyType.Right, _pl) &&
	                  !RiaInput.Instance.GetPush(RiaInput.KeyType.Left, _pl);

	    var isLeft = !RiaInput.Instance.GetPush(RiaInput.KeyType.Up, _pl) &&
	                 !RiaInput.Instance.GetPush(RiaInput.KeyType.Down, _pl) &&
	                 !RiaInput.Instance.GetPush(RiaInput.KeyType.Right, _pl) &&
	                 RiaInput.Instance.GetPush(RiaInput.KeyType.Left, _pl);

	    var isUpRight = RiaInput.Instance.GetPush(RiaInput.KeyType.Up, _pl) &&
	                   !RiaInput.Instance.GetPush(RiaInput.KeyType.Down, _pl) &&
	                   RiaInput.Instance.GetPush(RiaInput.KeyType.Right, _pl) &&
	                   !RiaInput.Instance.GetPush(RiaInput.KeyType.Left, _pl);

	    var isUpLeft = RiaInput.Instance.GetPush(RiaInput.KeyType.Up, _pl) &&
	                   !RiaInput.Instance.GetPush(RiaInput.KeyType.Down, _pl) &&
	                   !RiaInput.Instance.GetPush(RiaInput.KeyType.Right, _pl) &&
	                   RiaInput.Instance.GetPush(RiaInput.KeyType.Left, _pl);

	    var isDownRight = !RiaInput.Instance.GetPush(RiaInput.KeyType.Up, _pl) &&
	                      RiaInput.Instance.GetPush(RiaInput.KeyType.Down, _pl) &&
	                      RiaInput.Instance.GetPush(RiaInput.KeyType.Right, _pl) &&
	                      !RiaInput.Instance.GetPush(RiaInput.KeyType.Left, _pl);

	    var isDownLeft = !RiaInput.Instance.GetPush(RiaInput.KeyType.Up, _pl) &&
					     RiaInput.Instance.GetPush(RiaInput.KeyType.Down, _pl) &&
					     !RiaInput.Instance.GetPush(RiaInput.KeyType.Right, _pl) &&
					     RiaInput.Instance.GetPush(RiaInput.KeyType.Left, _pl);

        var index = states.NowIndex;

        if (isUp)
        {
            if (index == 0 || index == 1 || index == 2)
            {
                this.PrevContent(_pl, 3);
            }
            else if (index == 3 || index == 4)
            {
                this.PrevContent(_pl, 8);
            }
            else
            {
                this.PrevContent(_pl, 5);
            }
            states.KeyIntervalStartTime = this.elapsedTime;
        }

        if (isDown)
        {
            if (index == 12 || index == 11 || index == 10)
            {
                this.NextContent(_pl, 3);
            }
            else if (index == 9 || index == 8)
            {
                this.NextContent(_pl, 8);
            }
            else
            {
                this.NextContent(_pl, 5);
            }
            states.KeyIntervalStartTime = this.elapsedTime;
        }

        if (isRight)
        {
            if (index == 2 || index == 7 || index == 12)
            {
                this.PrevContent(_pl, 2);
            }
            else if (index == 4 || index == 9)
            {
                this.PrevContent(_pl, 1);
            }
            else
            {
                this.NextContent(_pl);
            }
            states.KeyIntervalStartTime = this.elapsedTime;
        }

        if (isLeft)
        {
            if (index == 0 || index == 5 || index == 10)
            {
                this.NextContent(_pl, 2);
            }
            else if (index == 3 || index == 8)
            {
                this.NextContent(_pl, 1);
            }
            else
            {
                this.PrevContent(_pl);
            }
            states.KeyIntervalStartTime = this.elapsedTime;
        }

        if (isUpLeft)
        {
            if (index == 2 || index == 10)
            {
                this.PrevContent(_pl, 0);
            }
            else if (index == 1 || index == 5)
            {
                this.NextContent(_pl, 6);
            }
            else if (index == 0)
            {
                this.PrevContent(_pl, 1);
            }
            else
            {
                this.PrevContent(_pl, 3);
            }
            states.KeyIntervalStartTime = this.elapsedTime;
        }

        if (isUpRight)
        {
            if (index == 0 || index == 12)
            {
                this.PrevContent(_pl, 0);
            }
            else if (index == 1 || index == 7)
            {
                this.NextContent(_pl, 4);
            }
            else if (index == 2)
            {
                this.PrevContent(_pl, 5);
            }
            else
            {
                this.PrevContent(_pl, 2);
            }
            states.KeyIntervalStartTime = this.elapsedTime;
        }

        if (isDownLeft)
        {
            if (index == 0 || index == 12)
            {
                this.NextContent(_pl, 0);
            }
            else if (index == 5 || index == 11)
            {
                this.PrevContent(_pl, 4);
            }
            else if (index == 10)
            {
                this.NextContent(_pl, 5);
            }
            else
            {
                this.NextContent(_pl, 2);
            }
            states.KeyIntervalStartTime = this.elapsedTime;
        }

        if (isDownRight)
        {
            if (index == 2 || index == 10)
            {
                this.NextContent(_pl, 0);
            }
            else if (index == 7 || index == 11)
            {
                this.PrevContent(_pl, 6);
            }
            else if (index == 12)
            {
                this.NextContent(_pl, 1);
            }
            else
            {
                this.NextContent(_pl, 3);
            }
            states.KeyIntervalStartTime = this.elapsedTime;
        }
    }
	
    private void ActionCursor()
    {
//        if (Input.GetKeyDown(this.pl1key.ReturnKey))
	    if (RiaInput.Instance.GetPushDown(RiaInput.KeyType.Return, PlayerNumber.player1))
        {
            if (this.ReturnAction(PlayerNumber.player1))
			{
				AudioManager.Instance.PlaySe(SoundEffectEnum.cursor);
			}
        }

//        if (Input.GetKeyDown(this.pl1key.CancelKey))
	    if (RiaInput.Instance.GetPushDown(RiaInput.KeyType.Cancel, PlayerNumber.player1))
        {
            if (this.CancelAction(PlayerNumber.player1))
			{
				AudioManager.Instance.PlaySe(SoundEffectEnum.cansel);
			}
        }

//        if (Input.GetKeyDown(this.pl2key.ReturnKey))
	    if (RiaInput.Instance.GetPushDown(RiaInput.KeyType.Return, PlayerNumber.player2))
        {
            if (this.ReturnAction(PlayerNumber.player2))
			{
				AudioManager.Instance.PlaySe(SoundEffectEnum.cursor);
			}
		}

//        if (Input.GetKeyDown(this.pl2key.CancelKey))
	    if (RiaInput.Instance.GetPushDown(RiaInput.KeyType.Cancel, PlayerNumber.player2))
        {
            if (this.CancelAction(PlayerNumber.player2))
			{
				AudioManager.Instance.PlaySe(SoundEffectEnum.cansel);
			}
		}
    }

    /// <summary>
    /// カーソルを1つ進める
    /// </summary>
    /// <param name="_pl">操作したプレイヤー</param>
    private void NextContent(PlayerNumber _pl)
    {
        NextContent(_pl, 1);
    }

    /// <summary>
    /// カーソルを_advanceだけ進める
    /// </summary>
    /// <param name="_pl">操作したプレイヤー</param>
    /// <param name="_advance">進める数</param>
    private void NextContent(PlayerNumber _pl, int _advance)
    {
        SelecterStates state = (_pl == PlayerNumber.player1) ? this.pl1States :
                               (_pl == PlayerNumber.player2) ? this.pl2States : null;

        var prevIndex = state.NowIndex;
        state.NowIndex = (state.NowIndex + _advance) % this.charaSelectorSets.Length;
        UpdateCursor(prevIndex);
        UpdateCursor(state.NowIndex);
        UpdateStand(_pl);
        UpdateNames(_pl);

		AudioManager.Instance.PlaySe(SoundEffectEnum.cursor);
	}

    /// <summary>
    /// カーソルを1つ戻す
    /// </summary>
    /// <param name="_pl">操作したプレイヤー</param>
    private void PrevContent(PlayerNumber _pl)
    {
        PrevContent(_pl, 1);
    }

    /// <summary>
    /// カーソルを_advanceだけ戻す
    /// </summary>
    /// <param name="_pl">操作したプレイヤー</param>
    /// <param name="_advance">戻す数</param>
    private void PrevContent(PlayerNumber _pl, int _advance)
    {
        SelecterStates state = (_pl == PlayerNumber.player1) ? this.pl1States :
                               (_pl == PlayerNumber.player2) ? this.pl2States : null;

        var prevIndex = state.NowIndex;
        state.NowIndex =
            (this.charaSelectorSets.Length + state.NowIndex - _advance) % this.charaSelectorSets.Length;
        UpdateCursor(prevIndex);
        UpdateCursor(state.NowIndex);
        UpdateStand(_pl);
        UpdateNames(_pl);

		AudioManager.Instance.PlaySe(SoundEffectEnum.cursor);
    }

    /// <summary>
    /// 決定ボタンを押したときの処理
    /// </summary>
    /// <param name="_pl">押したプレイヤー</param>
    /// <returns>正常に処理されたか</returns>
    private bool ReturnAction(PlayerNumber _pl)
	{
		if (this.IsPrevToTitleWindow)
		{
			this.prevToTitleImage.enabled = false;
			this.dispPrevToTitleWindowTime = 0;
			return true;
		}

		if (this.pl1States.IsSelected && this.pl2States.IsSelected &&
		   (this.nextToStageSelectIntervalTime <= this.elapsedTime - this.allSelectedTime))
		{
			var chara1 = this.charaSelectorSets[this.pl1States.NowIndex].PlayerCharacter;
			var chara2 = this.charaSelectorSets[this.pl1States.NowIndex].PlayerCharacter;

			SelectUIManager.Instance.TransitionToStageSelect(chara1, chara2);

			return true;
		}

		var result = ReturnFunc(_pl);

        if (this.pl1States.IsSelected && this.pl2States.IsSelected)
        {
            this.nextToStageSelectImage.enabled = true;
            this.allSelectedTime = this.elapsedTime;
        }

		return result;
    }

    private bool ReturnFunc(PlayerNumber _pl)
    {
        SelecterStates state = null;
        PlayerCharacterEnum pc = PlayerCharacterEnum.length_empty;
        Images images = null;
        Sprite sprite = null;

        if (_pl == PlayerNumber.player1)
        {
            state = this.pl1States;
            pc = this.charaSelectorSets[this.pl1States.NowIndex].PlayerCharacter;
            images = this.pl1Images;
            sprite = this.sprites.StandBackActive1;
        }
        if (_pl == PlayerNumber.player2)
        {
            state = this.pl2States;
            pc = this.charaSelectorSets[this.pl2States.NowIndex].PlayerCharacter;
            images = this.pl2Images;
            sprite = this.sprites.StandBackActive2;
        }

        if (state.IsSelected) { return true; }

        if (pc == PlayerCharacterEnum.length_empty ||
			pc == PlayerCharacterEnum.emilia ||
			pc == PlayerCharacterEnum.laxa ||
			pc == PlayerCharacterEnum.vega_al)
        {
			//AudioManager.Instance.PlaySe(SoundEffect.error);
            return false;
        }
        else if (pc == PlayerCharacterEnum.random)
        {
            var random = new Random();
            int index;
            while (true)
            {
                index = random.Next(0, (int)PlayerCharacterEnum.length_empty);
                pc = this.charaSelectorSets[index].PlayerCharacter;
                if (pc != PlayerCharacterEnum.random && pc != PlayerCharacterEnum.length_empty) { break; }
            }

            state.PlayerCharacter = pc;
            state.IsSelected = true;
            images.StandBack.sprite = sprite;
            var tmp = state.NowIndex;
            state.NowIndex = index;
            UpdateCursor(tmp);
            UpdateCursor(state.NowIndex);
            UpdateStand(_pl);
            UpdateNames(_pl);

			//AudioManager.Instance.PlaySe(SoundEffect.decision);
			return true;
        }
        else
        {
            state.PlayerCharacter = pc;
            state.IsSelected = true;
            images.StandBack.sprite = sprite;
            UpdateCursor(state.NowIndex);

			//AudioManager.Instance.PlaySe(SoundEffect.decision);
			return true;
        }
    }

    /// <summary>
    /// キャンセルボタンを押したときの処理
    /// </summary>
    /// <param name="_pl">押したプレイヤー</param>
    /// <returns>正常に処理されたか</returns>
    private bool CancelAction(PlayerNumber _pl)
	{
		var state = (_pl == PlayerNumber.player1) ? this.pl1States : this.pl2States;
		if (!state.IsSelected)
		{
			if (this.IsPrevToTitleWindow &&
			   (this.prevToTitleIntervalTime <= this.elapsedTime - this.dispPrevToTitleWindowTime))
			{
				SelectUIManager.Instance.TransitionToTitleScene();
			}
			else
			{
				this.dispPrevToTitleWindowTime = this.elapsedTime;
				this.prevToTitleImage.enabled = true;
			}
			return true;
		}

		bool result = CancelFunc(_pl);

        if (!this.pl1States.IsSelected || !this.pl2States.IsSelected)
        {
            this.nextToStageSelectImage.enabled = false;
        }
		
        return result;
    }

    private bool CancelFunc(PlayerNumber _pl)
	{
		SelecterStates state = null;
        Images images = null;
        Sprite sprite = null;
        if (_pl == PlayerNumber.player1)
        {
            state = this.pl1States;
            images = this.pl1Images;
            sprite = this.sprites.StandBackNormal1;
        }
        if (_pl == PlayerNumber.player2)
        {
            state = this.pl2States;
            images = this.pl2Images;
            sprite = this.sprites.StandBackNormal2;
        }

        state.PlayerCharacter = PlayerCharacterEnum.length_empty;
        state.IsSelected = false;
        images.StandBack.sprite = sprite;
        UpdateCursor(state.NowIndex);

        return true;
    }
    
    /// <summary>
    /// 指定されたカーソル画像を更新する
    /// </summary>
    /// <param name="_index">カーソル（選択）のインデックス</param>
    private void UpdateCursor(int _index)
    {
        var set = this.charaSelectorSets[_index];
        var isOveredPL1 = (_index == this.pl1States.NowIndex && !this.pl1States.IsSelected);
        var isOveredPL2 = (_index == this.pl2States.NowIndex && !this.pl2States.IsSelected);
        var isSelectedPL1 = (_index == this.pl1States.NowIndex && this.pl1States.IsSelected);
        var isSelectedPL2 = (_index == this.pl2States.NowIndex && this.pl2States.IsSelected);

        // どちらもSelectされている
        if (isSelectedPL1 && isSelectedPL2)
        {
            set.CursorImage.sprite = this.sprites.SelectAll;
        }
        else if (isSelectedPL1) // PL1はSelectされている
        {
            set.CursorImage.sprite = (isOveredPL2) ? this.sprites.Over2Select1 : this.sprites.Select1;
        }
        else if (isSelectedPL2) // PL2はSelectされている
        {
            set.CursorImage.sprite = (isOveredPL1) ? this.sprites.Over1Select2 : this.sprites.Select2;
        }
        else // どちらもSelectされていない
        {
            if (isOveredPL1 && isOveredPL2) // どちらもOverされている
            {
                set.CursorImage.sprite = this.sprites.OverAll;
            }
            else if (isOveredPL1)
            {
                set.CursorImage.sprite = this.sprites.Over1;
            }
            else if (isOveredPL2)
            {
                set.CursorImage.sprite = this.sprites.Over2;
            }
            else // どちらもOverされていない
            {
                set.CursorImage.sprite = this.sprites.Normal;
            }
        }
    }

    private void UpdateStand(PlayerNumber _pl)
    {
        CharaSelectorSet set = new CharaSelectorSet();
        SelecterStates state = null;
        Images images = null;
        Sprite sprite = null;

        if (_pl == PlayerNumber.player1)
        {
            state = this.pl1States;
            set = this.charaSelectorSets[state.NowIndex];
            images = this.pl1Images;
            sprite = set.StandPL1;
        }
        if (_pl == PlayerNumber.player2)
        {
            state = this.pl2States;
            set = this.charaSelectorSets[state.NowIndex];
            images = this.pl2Images;
            sprite = set.StandPL2;
        }

        if (set.PlayerCharacter == PlayerCharacterEnum.length_empty)
        {
            images.StandFront.sprite = null;
            images.StandFront.enabled = false;
        }
        else
        {
            images.StandFront.enabled = true;
            images.StandFront.sprite = sprite;
        }
    }

    private void UpdateNames(PlayerNumber _pl)
    {
        SelecterStates state = null;
        CharaSelectorSet set = new CharaSelectorSet();
        Images images = null;

        if (_pl == PlayerNumber.player1)
        {
            state = this.pl1States;
            set = this.charaSelectorSets[state.NowIndex];
            images = this.pl1Images;
            
        }
        if (_pl == PlayerNumber.player2)
        {
            state = this.pl2States;
            set = this.charaSelectorSets[state.NowIndex];
            images = this.pl2Images;
        }

        if (set.PlayerCharacter == PlayerCharacterEnum.length_empty)
        {
            images.SetNameImages(null, null);
            images.Kana.enabled = false;
            images.Alp.enabled = false;
        }
        else
        {
            images.Kana.enabled = true;
            images.Alp.enabled = true;
            images.SetNameImages(set.KanaName, set.AlpName);
        }
    }
}
