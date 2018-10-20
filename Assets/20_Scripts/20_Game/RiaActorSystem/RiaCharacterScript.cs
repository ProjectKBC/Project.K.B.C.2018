/* Author : flanny7
 * Update : 2018/10/19
*/ 

using UnityEngine;
using UnityEditor;

public abstract class RiaCharacterScript : ScriptableObject
{
    protected readonly Vector3 VECTOR3_ONE = Vector3.one;
    protected readonly Vector3 VECTOR3_ZERO = Vector3.zero;
    protected readonly Quaternion QUATERNION_IDENTITY = Quaternion.identity;
    
    public void Init(RiaCharacterStatus _status, Vector3 _position, Quaternion? _rotation = null, Vector3? _scale = null)
    {
        _status.trans_.localPosition = _position;
        _status.trans_.localRotation = _rotation ?? QUATERNION_IDENTITY;
        _status.trans_.localScale = _scale ?? VECTOR3_ONE;

        _status.playElapsedTime = 0;
        OnInit(_status);
    }

    public void Play(RiaCharacterStatus _status)
    {
        _status.playElapsedTime += Time.deltaTime;
        OnPlay(_status);
    }

    public void End(RiaCharacterStatus _status)
    {
        _status.playElapsedTime = 0;
        OnEnd(_status);
    }

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="_go">ActorがアタッチされているGameObaject</param>
    protected abstract void OnInit(RiaCharacterStatus _status);

    /// <summary>
    /// メインループ
    /// </summary>
    protected abstract void OnPlay(RiaCharacterStatus _status);

    /// <summary>
    /// 廃棄処理
    /// </summary>
    protected abstract void OnEnd(RiaCharacterStatus _status);
}

// ScritableObjectは、1つのScritableObejctを共有して使うものです
// 例えば、以下のようなクラスがあるとする
// StrongEnemyActorScript : ScritableObject
// WeakEnemyActorScript : ScritableObject
// StrongEnemyActor : MonoBehavior
// WeakEnemyActor : MonoBehavior
// StrongEnemyActorは10体、WeakEnemyActorは100体いるとする
// ここで、定数の「最大HP」をActorが持っていたとすると、
// StrongEnemyActorでは9つ、WeakEnemyActorは99つのムダな定数がメモリを食ってしまいます
// staticにすればメモリの消費量は抑えられますが、
// EnemyActorが必要ない場面でも常にメモリに滞在してしまいます
// そこで、それぞれのActorに対応する1つのActorScriptの参照を持たせることにします
// こうすることで、10体のStorngEnemyActorは1つのStrongEnemyActorScriptを参照し、
// 100体のWeakEnemyActorは1つのWeakEnemyActorScriptを参照することになり、
// メモリ消費を抑えることができます
// また、staticとは違い、ScritableObjectはどれからも参照されていない場合は自動的に破棄されるので
// 常にメモリ上に滞在することはありません

// 以上の性質より、ActorScript(ScritableObject)には定数を宣言すると良いです
// ex) 最大HP, 移動速度, リキャストタイムなど
// 以下のような変数は、ActorStatusクラスを作って管理することをお勧めします
// ex) 現在のHP, elapsedTime(経過時間), チャージショットのチャージ率など

// 詳しくはSampleActor系を見てください

// ActorはActorManagerの持つPoolから取り出す
// 取り出した際の初期化でInit()が呼び出される
// 取り出された後のメインループでPlay()が呼び出される
// Play()はActorManagerのUpdate()で呼び出される
// 破棄する場合はEnd()が呼び出される
