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
        public KeyCode nextKey;
        public KeyCode prevKey;
        public KeyCode returnKey;
        public KeyCode cancelKey;
    }

    private struct SelecterStates
    {
        public int nowIndex;
        public float nextKeyPressedTime;
        public float prevKeyPressedTime;
        public bool isSelected;
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
        elapsedTime = Time.deltaTime;

        // private fields
        this.pl1States.nowIndex = this.pl1FirstIndex;
        this.pl1States.nextKeyPressedTime = 0f;
        this.pl1States.prevKeyPressedTime = 0f;
        this.pl1States.isSelected = false;
        this.pl1States.PlayerCharacter = PlayerCharacterEnum.length_empty;

        this.pl2States.nowIndex = this.pl2FirstIndex;
        this.pl2States.nextKeyPressedTime = 0f;
        this.pl2States.prevKeyPressedTime = 0f;
        this.pl2States.isSelected = false;
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
            this.ReturnAction(Player.pl1);
        }

        if (Input.GetKeyDown(this.pl1key.cancelKey))
        {
            this.CancelAction(Player.pl1);
        }

        if (Input.GetKeyDown(this.pl2key.returnKey))
        {
            this.ReturnAction(Player.pl2);
        }

        if (Input.GetKeyDown(this.pl2key.cancelKey))
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
        if (_pl == Player.pl1)
        {
            var prevIndex = this.pl1States.nowIndex;
            this.pl1States.nowIndex = (this.pl1States.nowIndex + 1) % this.charaSelectorSets.Length;
            UpdateCursor(prevIndex);
            UpdateCursor(this.pl1States.nowIndex);
            UpdateStand(_pl);
            UpdateNames(_pl);
        }
        else if (_pl == Player.pl2)
        {
            var prevIndex = this.pl2States.nowIndex;
            this.pl2States.nowIndex = (this.pl2States.nowIndex + 1) % this.charaSelectorSets.Length;
            UpdateCursor(prevIndex);
            UpdateCursor(this.pl2States.nowIndex);
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
        if (_pl == Player.pl1)
        {
            var prevIndex = this.pl1States.nowIndex;
            this.pl1States.nowIndex = (this.charaSelectorSets.Length + this.pl1States.nowIndex - 1) % this.charaSelectorSets.Length;
            UpdateCursor(prevIndex);
            UpdateCursor(this.pl1States.nowIndex);
            UpdateStand(_pl);
            UpdateNames(_pl);
        }
        else if (_pl == Player.pl2)
        {
            var prevIndex = this.pl2States.nowIndex;
            this.pl2States.nowIndex = (this.charaSelectorSets.Length + this.pl2States.nowIndex - 1) % this.charaSelectorSets.Length;
            UpdateCursor(prevIndex);
            UpdateCursor(this.pl2States.nowIndex);
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
        if (this.pl1States.isSelected && this.pl2States.isSelected)
        {
            // todo: StageSelect画面に移行する
            return true;
        }

        if (_pl == Player.pl1)
        {
            if (this.pl1States.isSelected) { return true; }

            PlayerCharacterEnum pc = this.charaSelectorSets[this.pl1States.nowIndex].PlayerCharacter;
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
                this.pl1States.isSelected = true;
                this.pl1Images.StandBack.sprite = this.sprites.StandBackActive1;
                var tmp = this.pl1States.nowIndex;
                this.pl1States.nowIndex = index;
                UpdateCursor(tmp);
                UpdateCursor(this.pl1States.nowIndex);
                UpdateStand(_pl);
                UpdateNames(_pl);

                return true;
            }
            else
            {
                this.pl1States.PlayerCharacter = pc;
                this.pl1States.isSelected = true;
                this.pl1Images.StandBack.sprite = this.sprites.StandBackActive1;
                UpdateCursor(this.pl1States.nowIndex);

                // todo: SE [選択した]
                return true;
            }
        }
        else if (_pl == Player.pl2)
        {
            if (this.pl2States.isSelected) { return true; }

            PlayerCharacterEnum pc = this.charaSelectorSets[this.pl2States.nowIndex].PlayerCharacter;
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
                this.pl2States.isSelected = true;
                this.pl2Images.StandBack.sprite = this.sprites.StandBackActive2;
                var tmp = this.pl2States.nowIndex;
                this.pl2States.nowIndex = index;
                UpdateCursor(tmp);
                UpdateCursor(this.pl2States.nowIndex);
                UpdateStand(_pl);
                UpdateNames(_pl);

                return true;
            }
            else
            {
                this.pl2States.PlayerCharacter = pc;
                this.pl2States.isSelected = true;
                this.pl2Images.StandBack.sprite = this.sprites.StandBackActive2;
                UpdateCursor(this.pl2States.nowIndex);

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
            if (!this.pl1States.isSelected)
            {
                // 対戦を終了します [選択を続ける]　[*前の画面に戻る]
                return true;
            }

            this.pl1States.PlayerCharacter = PlayerCharacterEnum.length_empty;
            this.pl1States.isSelected = false;
            this.pl1Images.StandBack.sprite = this.sprites.StandBackNormal1;
            UpdateCursor(this.pl1States.nowIndex);
        }
        else if (_pl == Player.pl2)
        {
            if (!this.pl2States.isSelected)
            {
                // 対戦を終了します [選択を続ける]　[*前の画面に戻る]
                return true;
            }

            this.pl2States.PlayerCharacter = PlayerCharacterEnum.length_empty;
            this.pl2States.isSelected = false;
            this.pl2Images.StandBack.sprite = this.sprites.StandBackNormal2;
            UpdateCursor(this.pl2States.nowIndex);
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
        var isOveredPL1 = (_index == this.pl1States.nowIndex && !this.pl1States.isSelected);
        var isOveredPL2 = (_index == this.pl2States.nowIndex && !this.pl2States.isSelected);
        var isSelectedPL1 = (_index == this.pl1States.nowIndex && this.pl1States.isSelected);
        var isSelectedPL2 = (_index == this.pl2States.nowIndex && this.pl2States.isSelected);

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
            var set = this.charaSelectorSets[this.pl1States.nowIndex];
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
            var set = this.charaSelectorSets[this.pl2States.nowIndex];
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
            var set = this.charaSelectorSets[this.pl1States.nowIndex];
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
            var set = this.charaSelectorSets[this.pl2States.nowIndex];
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
