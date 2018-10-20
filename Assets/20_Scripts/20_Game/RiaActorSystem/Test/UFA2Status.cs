using UnityEngine;

public sealed class UFA2Status : RiaCharacterStatus
{
    public PlayerNumber PlayerNumber { get; set; }
    public float HitPoint { get; set; }
    public SpriteRenderer SpriteRenderer { get; set; }

    public UFA2Status(GameObject _go, PlayerNumber _playerNumber) : base(_go)
    {
        this.SpriteRenderer = go_.GetComponent<SpriteRenderer>();
        this.PlayerNumber = _playerNumber;
    }
}