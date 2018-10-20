using UnityEngine;

public sealed class UFA1Status : RiaCharacterStatus
{
    public PlayerNumber PlayerNumber { get; set; }
    public float HitPoint { get; set; }
    public SpriteRenderer SpriteRenderer { get; set; }

    public UFA1Status(GameObject _go, PlayerNumber _playerNumber) : base(_go)
    {
        this.SpriteRenderer = this.go_.GetComponent<SpriteRenderer>();
        if (!this.SpriteRenderer) { Debug.LogWarning("SpriteRendererがありません", this.go_); }

        this.PlayerNumber = _playerNumber;

        this.go_.tag = (_playerNumber == PlayerNumber.player1) ? "Enemy1" : "Enemy2";
    }
}