using UnityEngine;
using UnityEngine.UI;

public abstract class UIContent : MonoBehaviour
{
    public Image Image { get { return this.image; } }
    public Sprite NormalSprite { get { return this.normalSprite; } set { this.normalSprite = value; } }
    public Sprite ActiveSprite { get { return this.activeSprite; } set { this.activeSprite = value; } }

    [SerializeField]
    private Sprite normalSprite = null;
    [SerializeField]
    private Sprite activeSprite = null;

    private Image image = null;

    public abstract void ReturnAction();
    public abstract void CancelAction();

    private void Awake()
    {
        this.image = this.GetComponent<Image>();
    }
}
