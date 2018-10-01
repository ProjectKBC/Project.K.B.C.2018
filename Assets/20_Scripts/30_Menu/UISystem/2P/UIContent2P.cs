using UnityEngine;
using UnityEngine.UI;

public class UIContent2P : MonoBehaviour
{
    public Image Image { get { return this.image; } }
    public Sprite NormalSprite { get { return this.normalSprite; } set { this.normalSprite = value; } }
    public Sprite ActivePl1Sprite { get { return this.activePl1Sprite; } set { this.activePl1Sprite = value; } }
    public Sprite ActivePl2Sprite { get { return this.activePl2Sprite; } set { this.activePl2Sprite = value; } }
    public Sprite ActiveAllSprite { get { return this.activeAllSprite; } set { this.activeAllSprite = value; } }

    [SerializeField]
    private Sprite normalSprite = null;
    [SerializeField]
    private Sprite activePl1Sprite = null;
    [SerializeField]
    private Sprite activePl2Sprite = null;
    [SerializeField]
    private Sprite activeAllSprite = null;

    private Image image = null;

    public virtual void ReturnAction() { }
    public virtual void CancelAction() { }

    private void Awake()
    {
        this.image = this.GetComponent<Image>();
    }
}
