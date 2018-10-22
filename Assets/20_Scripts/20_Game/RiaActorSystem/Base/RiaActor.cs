/* Author : flanny7
 * Update : 2018/10/19
*/

using UnityEngine;

public sealed class RiaActor : MonoBehaviour
{
    public bool IsActive { get; private set; }

    public RiaCharacterStatus Status { get; private set; }
	public RiaCharacterScript Script { get; private set; }

    private GameObject go_;

    public void Init()
    {
        this.go_ = this.gameObject;

        this.SetActive(false);
    }

    public void WakeUp(RiaCharacterStatus _status, RiaCharacterScript _script,
        Vector3 _position, Quaternion? _rotation = null, Vector3? _scale = null)
    {
        this.Status = _status;
        this.Script = _script;
        
        this.Script.Init(this.Status, _position, _rotation, _scale);
        this.SetActive(true);
    }

    public void Play()
    {
        if (!IsActive) { return; }

        this.Script.Play(this.Status);
    }

    public void Sleep()
    {
        this.Script.End(this.Status);
        this.SetActive(false);
    }

    private void SetActive(bool _isActive)
    {
        this.go_.SetActive(_isActive);
        this.IsActive = _isActive;
    }
}
