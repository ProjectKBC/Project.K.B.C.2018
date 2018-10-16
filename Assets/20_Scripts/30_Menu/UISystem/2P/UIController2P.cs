using System;

using Boo.Lang;

using JetBrains.Annotations;

using UnityEngine;
using UnityEngine.UI;

using Random = System.Random;

public enum Player
{
    pl1,
    pl2,
}

public sealed class UIController2P : SingletonMonoBehaviour<UIController2P>
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
        public Image StandBack;
        public Image StandFront;
        public Image Kana;
        public Image Alp;

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

    // private fields
    private float elapsedTime;
    private SelecterStates pl1States;
    private SelecterStates pl2States;
    private PlayerCharacterEnum pl1chara;
    private PlayerCharacterEnum pl2chara;

    protected override void OnInit()
    {
        this.elapsedTime = 0;

        // private fields
        this.pl1States = new SelecterStates();;
        this.pl1States.NowIndex = this.pl1FirstIndex;
        this.pl1States.KeyIntervalStartTime = 0f;
        this.pl1States.RapidKeyWaitStartTime = 0f;
        this.pl1States.IsFirstCursorMove = false;
        this.pl1States.IsSelected = false;
        this.pl1States.PlayerCharacter = PlayerCharacterEnum.length_empty;

        this.pl2States = new SelecterStates(); ;
        this.pl2States.NowIndex = this.pl2FirstIndex;
        this.pl2States.KeyIntervalStartTime = 0f;
        this.pl2States.RapidKeyWaitStartTime = 0f;
        this.pl2States.IsFirstCursorMove = false;
        this.pl2States.IsSelected = false;
        this.pl2States.PlayerCharacter = PlayerCharacterEnum.length_empty;

        // selectorの初期化
        for (int i = 0; i < this.charaSelectorSets.Length; ++i)
        {
            var set = this.charaSelectorSets[i];
            set.SelectorImage.sprite = set.Selector;
            set.CursorImage.sprite = this.sprites.Normal;
            UpdateCursor(i);
        }

        UpdateStand(Player.pl1);
        UpdateStand(Player.pl2);
        UpdateNames(Player.pl1);
        UpdateNames(Player.pl2);
    }

    private void Update()
    {
        this.elapsedTime += Time.deltaTime;

        this.MoveSelect();
        this.ActionSelect();
    }

    private void MoveSelect()
    {
        // Player1
        var _states = this.pl1States;
        var _key = this.pl1key;
        var _pl = Player.pl1;
        if (!_states.IsSelected)
        {
            MoveCursor(_states, _key, _pl);
        }

        // Player2
        _states = this.pl2States;
        _key = this.pl2key;
        _pl = Player.pl2;
        if (!_states.IsSelected)
        {
            MoveCursor(_states, _key, _pl);
        }

    }

    private void MoveCursor(SelecterStates _states, PlayerKey _key, Player _pl)
    {

        // いずれの方向キーも押されていない
        if (!Input.GetKey(_key.UpKey) && !Input.GetKey(_key.DownKey) && !Input.GetKey(_key.RightKey) && !Input.GetKey(_key.LeftKey))
        {
            _states.KeyIntervalStartTime = -1;
            _states.RapidKeyWaitStartTime = -1;
            _states.IsFirstCursorMove = false;
            return;
        }
        // いずれかの方向キーがいずれも押されていない状態からKeyDownした
        else if (_states.KeyIntervalStartTime == -1)
        {
            _states.KeyIntervalStartTime = this.elapsedTime;
            _states.RapidKeyWaitStartTime = this.elapsedTime;
            return;
        }

        // キー入力のインターバル時間に達していない
        if (this.elapsedTime - _states.KeyIntervalStartTime < this.pressedReactionIntervalTime) { return; }
        
        // キーの連射入力時間に達しておらず、連射扱いとなる場合
        if (this.elapsedTime - _states.RapidKeyWaitStartTime < this.pressedWaitTime && _states.IsFirstCursorMove) { return; }

        // キーの連射速度時間に達していない場合
        if (this.elapsedTime - _states.KeyIntervalStartTime < this.pressedRapidIntervalTime && _states.IsFirstCursorMove) { return; }

        if (!_states.IsFirstCursorMove)
        {
            _states.IsFirstCursorMove = true;
        }

        var isUp = Input.GetKey(_key.UpKey) &&
                   !Input.GetKey(_key.DownKey) && !Input.GetKey(_key.RightKey) && !Input.GetKey(_key.LeftKey);

        var isDown = Input.GetKey(_key.DownKey) &&
                     !Input.GetKey(_key.UpKey) && !Input.GetKey(_key.RightKey) && !Input.GetKey(_key.LeftKey);

        var isRight = Input.GetKey(_key.RightKey) &&
                      !Input.GetKey(_key.UpKey) && !Input.GetKey(_key.DownKey) && !Input.GetKey(_key.LeftKey);

        var isLeft = Input.GetKey(_key.LeftKey) &&
                     !Input.GetKey(_key.UpKey) && !Input.GetKey(_key.DownKey) && !Input.GetKey(_key.RightKey);

        var isUpLeft = Input.GetKey(_key.UpKey) && Input.GetKey(_key.LeftKey) &&
                       !Input.GetKey(_key.DownKey) && !Input.GetKey(_key.RightKey);

        var isUpRight = Input.GetKey(_key.UpKey) && Input.GetKey(_key.RightKey) &&
                         !Input.GetKey(_key.DownKey) && !Input.GetKey(_key.LeftKey);

        var isDownLeft = Input.GetKey(_key.DownKey) && Input.GetKey(_key.LeftKey) &&
                          !Input.GetKey(_key.UpKey) && !Input.GetKey(_key.RightKey);

        var isDownRight = Input.GetKey(_key.DownKey) && Input.GetKey(_key.RightKey) &&
                           !Input.GetKey(_key.UpKey) && !Input.GetKey(_key.LeftKey);

        var index = _states.NowIndex;
        
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
            _states.KeyIntervalStartTime = this.elapsedTime;
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
            _states.KeyIntervalStartTime = this.elapsedTime;
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
            _states.KeyIntervalStartTime = this.elapsedTime;
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
            _states.KeyIntervalStartTime = this.elapsedTime;
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
            _states.KeyIntervalStartTime = this.elapsedTime;
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
            _states.KeyIntervalStartTime = this.elapsedTime;
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
            _states.KeyIntervalStartTime = this.elapsedTime;
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
            _states.KeyIntervalStartTime = this.elapsedTime;
        }
    }

    private void ActionSelect()
    {
        if (Input.GetKeyDown(this.pl1key.ReturnKey))
        {
            this.ReturnAction(Player.pl1);
        }

        if (Input.GetKeyDown(this.pl1key.CancelKey))
        {
            this.CancelAction(Player.pl1);
        }

        if (Input.GetKeyDown(this.pl2key.ReturnKey))
        {
            this.ReturnAction(Player.pl2);
        }

        if (Input.GetKeyDown(this.pl2key.CancelKey))
        {
            this.CancelAction(Player.pl2);
        }
    }

    /// <summary>
    /// カーソルを1つ進める
    /// </summary>
    /// <param name="_pl">操作したプレイヤー</param>
    private void NextContent(Player _pl)
    {
        NextContent(_pl, 1);
    }

    /// <summary>
    /// カーソルを_advanceだけ進める
    /// </summary>
    /// <param name="_pl">操作したプレイヤー</param>
    /// <param name="_advance">進める数</param>
    private void NextContent(Player _pl, int _advance)
    {
        if (_pl == Player.pl1)
        {
            var prevIndex = this.pl1States.NowIndex;
            this.pl1States.NowIndex = (this.pl1States.NowIndex + _advance) % this.charaSelectorSets.Length;
            UpdateCursor(prevIndex);
            UpdateCursor(this.pl1States.NowIndex);
            UpdateStand(_pl);
            UpdateNames(_pl);
        }
        else if (_pl == Player.pl2)
        {
            var prevIndex = this.pl2States.NowIndex;
            this.pl2States.NowIndex = (this.pl2States.NowIndex + _advance) % this.charaSelectorSets.Length;
            UpdateCursor(prevIndex);
            UpdateCursor(this.pl2States.NowIndex);
            UpdateStand(_pl);
            UpdateNames(_pl);
        }
    }

    /// <summary>
    /// カーソルを1つ戻す
    /// </summary>
    /// <param name="_pl">操作したプレイヤー</param>
    private void PrevContent(Player _pl)
    {
        PrevContent(_pl, 1);
    }

    /// <summary>
    /// カーソルを_advanceだけ戻す
    /// </summary>
    /// <param name="_pl">操作したプレイヤー</param>
    /// <param name="_advance">戻す数</param>
    private void PrevContent(Player _pl, int _advance)
    {
        if (_pl == Player.pl1)
        {
            var prevIndex = this.pl1States.NowIndex;
            this.pl1States.NowIndex = (this.charaSelectorSets.Length + this.pl1States.NowIndex - _advance) % this.charaSelectorSets.Length;
            UpdateCursor(prevIndex);
            UpdateCursor(this.pl1States.NowIndex);
            UpdateStand(_pl);
            UpdateNames(_pl);
        }
        else if (_pl == Player.pl2)
        {
            var prevIndex = this.pl2States.NowIndex;
            this.pl2States.NowIndex = (this.charaSelectorSets.Length + this.pl2States.NowIndex - _advance) % this.charaSelectorSets.Length;
            UpdateCursor(prevIndex);
            UpdateCursor(this.pl2States.NowIndex);
            UpdateStand(_pl);
            UpdateNames(_pl);
        }
    }

    /// <summary>
    /// 決定ボタンを押したときの処理
    /// </summary>
    /// <param name="_pl">押したプレイヤー</param>
    /// <returns>正常に処理されたか</returns>
    private bool ReturnAction(Player _pl)
    {
        if (this.pl1States.IsSelected && this.pl2States.IsSelected)
        {
            // todo: StageSelect画面に移行する
            return true;
        }

        if (_pl == Player.pl1)
        {
            if (this.pl1States.IsSelected) { return true; }

            PlayerCharacterEnum pc = this.charaSelectorSets[this.pl1States.NowIndex].PlayerCharacter;
            if (pc == PlayerCharacterEnum.length_empty)
            {
                // todo: SE [選択できない]
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

                this.pl1States.PlayerCharacter = pc;
                this.pl1States.IsSelected = true;
                this.pl1Images.StandBack.sprite = this.sprites.StandBackActive1;
                var tmp = this.pl1States.NowIndex;
                this.pl1States.NowIndex = index;
                UpdateCursor(tmp);
                UpdateCursor(this.pl1States.NowIndex);
                UpdateStand(_pl);
                UpdateNames(_pl);

                // todo: SE [選択した]
                return true;
            }
            else
            {
                this.pl1States.PlayerCharacter = pc;
                this.pl1States.IsSelected = true;
                this.pl1Images.StandBack.sprite = this.sprites.StandBackActive1;
                UpdateCursor(this.pl1States.NowIndex);

                // todo: SE [選択した]
                return true;
            }
        }
        else if (_pl == Player.pl2)
        {
            if (this.pl2States.IsSelected) { return true; }

            PlayerCharacterEnum pc = this.charaSelectorSets[this.pl2States.NowIndex].PlayerCharacter;
            if (pc == PlayerCharacterEnum.length_empty)
            {
                // todo: SE [選択できない]
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

                this.pl2States.PlayerCharacter = pc;
                this.pl2States.IsSelected = true;
                this.pl2Images.StandBack.sprite = this.sprites.StandBackActive2;
                var tmp = this.pl2States.NowIndex;
                this.pl2States.NowIndex = index;
                UpdateCursor(tmp);
                UpdateCursor(this.pl2States.NowIndex);
                UpdateStand(_pl);
                UpdateNames(_pl);

                // todo: SE [選択した]
                return true;
            }
            else
            {
                this.pl2States.PlayerCharacter = pc;
                this.pl2States.IsSelected = true;
                this.pl2Images.StandBack.sprite = this.sprites.StandBackActive2;
                UpdateCursor(this.pl2States.NowIndex);

                // todo: SE [選択した]
                return true;
            }
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// キャンセルボタンを押したときの処理
    /// </summary>
    /// <param name="_pl">押したプレイヤー</param>
    /// <returns>正常に処理されたか</returns>
    private bool CancelAction(Player _pl)
    {
        if (_pl == Player.pl1)
        {
            if (!this.pl1States.IsSelected)
            {
                // todo: 対戦を終了します [選択を続ける]　[*前の画面に戻る]
                return true;
            }

            this.pl1States.PlayerCharacter = PlayerCharacterEnum.length_empty;
            this.pl1States.IsSelected = false;
            this.pl1Images.StandBack.sprite = this.sprites.StandBackNormal1;
            UpdateCursor(this.pl1States.NowIndex);
        }
        else if (_pl == Player.pl2)
        {
            if (!this.pl2States.IsSelected)
            {
                // todo: 対戦を終了します [選択を続ける]　[*前の画面に戻る]
                return true;
            }

            this.pl2States.PlayerCharacter = PlayerCharacterEnum.length_empty;
            this.pl2States.IsSelected = false;
            this.pl2Images.StandBack.sprite = this.sprites.StandBackNormal2;
            UpdateCursor(this.pl2States.NowIndex);
        }

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

    private void UpdateStand(Player _pl)
    {
        if (_pl == Player.pl1)
        {
            var set = this.charaSelectorSets[this.pl1States.NowIndex];
            if (set.PlayerCharacter == PlayerCharacterEnum.length_empty)
            {
                this.pl1Images.StandFront.sprite = null;
                this.pl1Images.StandFront.enabled = false;
            }
            else
            {
                this.pl1Images.StandFront.enabled = true;
                this.pl1Images.StandFront.sprite = set.StandPL1;
            }
        }
        else if (_pl == Player.pl2)
        {
            var set = this.charaSelectorSets[this.pl2States.NowIndex];
            if (set.PlayerCharacter == PlayerCharacterEnum.length_empty)
            {
                this.pl2Images.StandFront.sprite = null;
                this.pl2Images.StandFront.enabled = false;
            }
            else
            {
                this.pl2Images.StandFront.enabled = true;
                this.pl2Images.StandFront.sprite = set.StandPL2;
            }
        }
    }

    private void UpdateNames(Player _pl)
    {
        if (_pl == Player.pl1)
        {
            var set = this.charaSelectorSets[this.pl1States.NowIndex];
            if (set.PlayerCharacter == PlayerCharacterEnum.length_empty)
            {
                this.pl1Images.SetNameImages(null, null);
                this.pl1Images.Kana.enabled = false;
                this.pl1Images.Alp.enabled = false;
            }
            else
            {
                this.pl1Images.Kana.enabled = true;
                this.pl1Images.Alp.enabled = true;
                this.pl1Images.SetNameImages(set.KanaName, set.AlpName);
            }
        }
        else if (_pl == Player.pl2)
        {
            var set = this.charaSelectorSets[this.pl2States.NowIndex];
            if (set.PlayerCharacter == PlayerCharacterEnum.length_empty)
            {
                this.pl2Images.SetNameImages(null, null);
                this.pl2Images.Kana.enabled = false;
                this.pl2Images.Alp.enabled = false;
            }
            else
            {
                this.pl2Images.Kana.enabled = true;
                this.pl2Images.Alp.enabled = true;
                this.pl2Images.SetNameImages(set.KanaName, set.AlpName);
            }
        }
    }
}
