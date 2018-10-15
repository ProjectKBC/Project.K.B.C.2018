//using UnityEngine;
//using UnityEngine.UI;

//public class CharaUIContent2P : MonoBehaviour
//{
//    [SerializeField]
//    private PlayerCharacterEnum chara = PlayerCharacterEnum.veronica;
    
//    private Image image;
//    private bool pl1Overed;
//    private bool pl2Overed;
//    private bool pl1Selected;
//    private bool pl2Selected;
//    private UIController2P.SelectorSprites selectorSprites;

//    public PlayerCharacterEnum ReturnAction(Player _pl)
//    {
//        if (_pl == Player.pl1)
//        {
//            this.pl1Selected = true;
//        }
//        else
//        {
//            this.pl2Selected = true;
//        }

//        return this.chara;
//    }

//    public void CancelAction(Player _pl)
//    {
//        if (_pl == Player.pl1)
//        {
//            this.pl1Selected = false;
//        }
//        else
//        {
//            this.pl2Selected = false;
//        }
//    }

//    public PlayerCharacterEnum SetOver(Player _player, bool _over)
//    {
//        if (_player == Player.pl1)
//        {
//            this.pl1Overed = _over;
//        }
//        else
//        {
//            this.pl2Overed = _over;
//        }

//        this.UpdateSprite();
//        return this.chara;
//    }

//    private void Awake()
//    {
//        this.image = this.GetComponent<Image>();

//        this.pl1Overed = false;
//        this.pl2Overed = false;
//        this.pl1Selected = false;
//        this.pl2Selected = false;

//        this.selectorSprites = UIController2P.Instance.SelectorSprite;
//    }

//    private void UpdateSprite()
//    {
//        if (this.pl1Selected && this.pl2Selected) // どちらもSelectされている
//        {
//            this.image.sprite = this.selectorSprites.SelectAll;
//        }
//        else if (this.pl1Selected) // PL1はSelectされている
//        {
//            this.image.sprite = (this.pl2Overed) ? this.selectorSprites.Over2Select1 : this.selectorSprites.Select1;
//        }
//        else if (this.pl2Selected) // PL2はSelectされている
//        {
//            this.image.sprite = (this.pl1Overed) ? this.selectorSprites.Over1Select2 : this.selectorSprites.Select2;
//        }
//        else // どちらもSelectされていない
//        {
//            if (this.pl1Overed && this.pl2Overed) // どちらもOverされている
//            {
//                this.image.sprite = this.selectorSprites.OverAll;
//            }
//            else if (this.pl1Overed)
//            {
//                this.image.sprite = this.selectorSprites.Over1;
//            }
//            else if (this.pl2Overed)
//            {
//                this.image.sprite = this.selectorSprites.Over2;
//            }
//            else // どちらもOverされていない
//            {
//                this.image.sprite = this.selectorSprites.Normal;
//            }
//        }
//    }
//}
