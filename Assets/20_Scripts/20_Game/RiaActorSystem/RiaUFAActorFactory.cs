using UnityEngine;

[System.Serializable]
public class RiaUFAActorFactory
{
    public enum UFACategory
    {
        UFA1,
        UFA2,
    }

    [SerializeField]
    private UFA1Script ufa1Script = null;
    [SerializeField]
    private UFA2Script ufa2Script = null;

    public void CreateUFA(UFACategory _category, PlayerNumber _playerNumber, RiaActor _actor,
        Vector3 _position, Quaternion? _rotation = null, Vector3? _scale = null)
    {
        switch(_category)
        {
            case UFACategory.UFA1:
            {
                var status = new UFA1Status(_actor.gameObject, _playerNumber);
                var script = this.ufa1Script;
                _actor.WakeUp(status, script, _position);
                break;
            }
            case UFACategory.UFA2:
            {
                var status = new UFA2Status(_actor.gameObject, _playerNumber);
                var script = this.ufa2Script;
                _actor.WakeUp(status, script, _position);
                break;
            }
        }
    }
}