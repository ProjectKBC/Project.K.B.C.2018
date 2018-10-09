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
        if (this.pl1Selected && this.pl2Selected)
        {
            this.image.sprite = this.activeAllSelectedSprite;
        }
        else if (this.pl1Selected)
        {
            this.image.sprite = this.pl2Overed ? this.activeAllSelectedSprite : this.activePl1SelectedSprite;
        }
        else if (this.pl2Selected)
        {
            this.image.sprite = this.pl1Overed ? this.activeAllSelectedSprite : this.activePl2SelectedSprite;
        }
        else if (this.pl1Overed && this.pl2Overed)
        {
            this.image.sprite = this.activeAllSprite;
        }
        else if (this.pl1Overed)
        {
            this.image.sprite = this.activePl1Sprite;
        }
        else if (this.pl2Overed)
        {
            this.image.sprite = this.activePl2Sprite;
        }
        else
        {
            this.image.sprite = this.normalSprite;
        }
    }

    [SerializeField]
    private Sprite normalSprite = null;
    [SerializeField]
    private Sprite activePl1Sprite = null;
    [SerializeField]
    private Sprite activePl2Sprite = null;
    [SerializeField]
    private Sprite activeAllSprite = null;
    [SerializeField]
    private Sprite activePl1SelectedSprite = null;
    [SerializeField]
    private Sprite activePl2SelectedSprite = null;
    [SerializeField]
    private Sprite activeAllSelectedSprite = null;

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
