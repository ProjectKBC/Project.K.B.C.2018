using UnityEngine;

public class SampleUIContent : UIContent
{
    public override void ReturnAction()
    {
        Debug.Log("return action!", this.gameObject);
    }

    public override void CancelAction()
    {
        Debug.Log("cancel action!", this.gameObject);
    }
}
