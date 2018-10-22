/* Author : flanny7
 * Update : 2018/10/19
*/

using UnityEngine;

/// <summary>
/// Actorが持つ変数群クラス
/// </summary>
public abstract class RiaCharacterStatus
{
    public GameObject go_;
    public Transform trans_;
    public RiaActor actor_;
    public float playElapsedTime;

    public RiaCharacterStatus(GameObject _go)
    {
        this.go_ = _go;
        this.trans_ = this.go_.transform;
        this.actor_ = this.go_.GetComponent<RiaActor>();

        this.playElapsedTime = 0;
    }
}
