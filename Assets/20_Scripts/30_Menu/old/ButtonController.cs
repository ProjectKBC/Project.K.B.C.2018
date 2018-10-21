//using System.Diagnostics.CodeAnalysis;
//using UnityEngine;

//[SuppressMessage("ReSharper", "StyleCop.SA1401")]
//public abstract class ButtonController : MonoBehaviour
//{
//    [SerializeField]
//    protected ButtonController button = null;

//    public void OnClick()
//    {
//        if (button)
//        {
//            var e = new System.Exception("Button instance is null!!");
//            Debug.LogError(e, this);
//            throw e;
//        }

//        button.OnClick(this.gameObject.name);
//    }

//    protected virtual void OnClick(string _objectName)
//    {
//        // 呼ばれることはない
//        Debug.LogWarning("Base Button", this);
//    }
//}
