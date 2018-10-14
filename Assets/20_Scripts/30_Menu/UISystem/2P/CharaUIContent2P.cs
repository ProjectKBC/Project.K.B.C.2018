using UnityEngine;
using UnityEngine.UI;

public class CharaUIContent2P : MonoBehaviour
{
    public bool Pl1Overed
    {
        set
        {
            this.pl1Overed = value;
            this.UpdateSprite();
        }
    }

    public bool Pl2Overed
    {
        set
        {
            this.pl2Overed = value;
            this.UpdateSprite();
        }
    }

    public bool Pl1Selected
    {
        set
        {
            this.pl1Selected = value;
            this.UpdateSprite();
        }
    }

    public bool Pl2Selected
    {
        set
        {
            this.pl2Selected = value;
            this.UpdateSprite();
        }
    }

    private void UpdateSprite()
    {
        if (this.pl1Selected && this.pl2Selected) // どちらもSelectされている
        {
            this.image.sprite = this.spSelectAll;
        }
        else if (this.pl1Selected) // PL1はSelectされている
        {
            this.image.sprite = (this.pl2Overed) ? this.spOver2Select1 : this.spSelect1;
        }
        else if (this.pl2Selected) // PL2はSelectされている
        {
            this.image.sprite = (this.pl1Overed) ? this.spOver1Select2 : this.spSelect2;
        }
        else // どちらもSelectされていない
        {
            if (this.pl1Overed && this.pl2Overed) // どちらもOverされている
            {
                this.image.sprite = this.spOverAll;
            }
            else if (this.pl1Overed)
            {
                this.image.sprite = this.spOver1;
            }
            else if (this.pl2Overed)
            {
                this.image.sprite = this.spOver2;
            }
            else // どちらもOverされていない
            {
                this.image.sprite = this.spNormal;
            }
        }
    }

    [SerializeField]
    private Sprite spNormal = null;
    [SerializeField]
    private Sprite spOver1 = null;
    [SerializeField]
    private Sprite spOver2 = null;
    [SerializeField]
    private Sprite spOverAll = null;
    [SerializeField]
    private Sprite spSelect1 = null;
    [SerializeField]
    private Sprite spSelect2 = null;
    [SerializeField]
    private Sprite spOver1Select2 = null;
    [SerializeField]
    private Sprite spOver2Select1 = null;
    [SerializeField]
    private Sprite spSelectAll = null;

    [SerializeField]
    private PlayerCharacterEnum chara = PlayerCharacterEnum.veronica;

    private Image image;
    private bool pl1Overed;
    private bool pl2Overed;
    private bool pl1Selected;
    private bool pl2Selected;

    public PlayerCharacterEnum ReturnAction(Player _pl)
    {
        if (_pl == Player.pl1)
        {
            Pl1Selected = true;
        }
        else
        {
            Pl2Selected = true;
        }

        return this.chara;
    }

    public void CancelAction(Player _pl)
    {
        if (_pl == Player.pl1)
        {
            Pl1Selected = false;
        }
        else
        {
            Pl2Selected = false;
        }
    }

    private void Awake()
    {
        this.image = this.GetComponent<Image>();

        this.pl1Overed = false;
        this.pl2Overed = false;
        this.pl1Selected = false;
        this.pl2Selected = false;
    }
}
