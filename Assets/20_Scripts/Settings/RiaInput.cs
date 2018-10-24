using UnityEngine;

// todo: Up, Down, Right, Leftがおかしい
public class RiaInput : SingletonMonoBehaviour<RiaInput>
{
	private const float AxisMargin = 0.9f;
	[SerializeField]
	private RiaInputConfig player1Congif = null;
	[SerializeField]
	private RiaInputConfig player2Congif = null;

	public enum KeyType
	{
		Return,
		Cancel,
		Up,
		Down,
		Right,
		Left,

		NormalShot,
		SpecialShot,
		Skil,
		LowSpeed,
		Pause,
	}
	
	public bool GetPush(KeyType _keyType, PlayerNumber _playerNumber)
	{
		var config = (_playerNumber == PlayerNumber.player1) ?
			this.player1Congif : this.player2Congif;

		switch (_keyType)
		{
			case KeyType.Return:
				return
					(Input.GetKey(config.keyCode.Return));

			case KeyType.Cancel:
				return
					(Input.GetKey(config.keyCode.Cancel));

			case KeyType.Up:
				return
					(Input.GetKey(config.keyCode.Up));

			case KeyType.Down:
				return
					(Input.GetKey(config.keyCode.Down));

			case KeyType.Right:
				return
					(Input.GetKey(config.keyCode.Right));

			case KeyType.Left:
				return
					(Input.GetKey(config.keyCode.Left));

			case KeyType.NormalShot:
				return
					(Input.GetKey(config.keyCode.NormalShot));

			case KeyType.SpecialShot:
				return
					(Input.GetKey(config.keyCode.SpecialShot));

			case KeyType.Skil:
				return
					(Input.GetKey(config.keyCode.Skil));

			case KeyType.LowSpeed:
				return
					(Input.GetKey(config.keyCode.LowSpeed));

			case KeyType.Pause:
				return
					(Input.GetKey(config.keyCode.Pose));
		}

		return false;
	}

	public bool GetKeyDown(KeyType _keyType, PlayerNumber _playerNumber)
	{
		var config = (_playerNumber == PlayerNumber.player1) ?
			this.player1Congif : this.player2Congif;

		switch (_keyType)
		{
			case KeyType.Return:
				return
					(Input.GetKeyDown(config.keyCode.Return));

			case KeyType.Cancel:
				return
					(Input.GetKeyDown(config.keyCode.Cancel));

			case KeyType.Up:
				return
					(Input.GetKeyDown(config.keyCode.Up));

			case KeyType.Down:
				return
					(Input.GetKeyDown(config.keyCode.Down));

			case KeyType.Right:
				return
					(Input.GetKeyDown(config.keyCode.Right));

			case KeyType.Left:
				return
					(Input.GetKeyDown(config.keyCode.Left));

			case KeyType.NormalShot:
				return
					(Input.GetKeyDown(config.keyCode.NormalShot));

			case KeyType.SpecialShot:
				return
					(Input.GetKeyDown(config.keyCode.SpecialShot));

			case KeyType.Skil:
				return
					(Input.GetKeyDown(config.keyCode.Skil));

			case KeyType.LowSpeed:
				return
					(Input.GetKeyDown(config.keyCode.LowSpeed));

			case KeyType.Pause:
				return
					(Input.GetKeyDown(config.keyCode.Pose));
		}

		return false;
	}

	public bool GetKeyUp(KeyType _keyType, PlayerNumber _playerNumber)
	{
		var config = (_playerNumber == PlayerNumber.player1) ?
			this.player1Congif : this.player2Congif;

		switch (_keyType)
		{
			case KeyType.Return:
				return
					(Input.GetKeyUp(config.keyCode.Return));

			case KeyType.Cancel:
				return
					(Input.GetKeyUp(config.keyCode.Cancel));

			case KeyType.Up:
				return
					(Input.GetKeyUp(config.keyCode.Up));

			case KeyType.Down:
				return
					(Input.GetKeyUp(config.keyCode.Down));

			case KeyType.Right:
				return
					(Input.GetKeyUp(config.keyCode.Right));

			case KeyType.Left:
				return
					(Input.GetKeyUp(config.keyCode.Left));

			case KeyType.NormalShot:
				return
					(Input.GetKeyUp(config.keyCode.NormalShot));

			case KeyType.SpecialShot:
				return
					(Input.GetKeyUp(config.keyCode.SpecialShot));

			case KeyType.Skil:
				return
					(Input.GetKeyUp(config.keyCode.Skil));

			case KeyType.LowSpeed:
				return
					(Input.GetKeyUp(config.keyCode.LowSpeed));

			case KeyType.Pause:
				return
					(Input.GetKeyUp(config.keyCode.Pose));
		}

		return false;
	}
	public bool GetPushUp(KeyType _keyType, PlayerNumber _playerNumber)
	{
		var config = (_playerNumber == PlayerNumber.player1) ?
			this.player1Congif : this.player2Congif;

		switch (_keyType)
		{
			case KeyType.Return:
				return
					(Input.GetKey(config.keyCode.Return) ||
					 Input.GetButton(config.buttonString.Return));

			case KeyType.Cancel:
				return
					(Input.GetKey(config.keyCode.Cancel) ||
					 Input.GetButton(config.buttonString.Cancel));

			case KeyType.Up:
				return
					(Input.GetKey(config.keyCode.Up) ||
					 Input.GetButton(config.buttonString.Up) ||
					 AxisMargin < Input.GetAxis(config.buttonString.Up));

			case KeyType.Down:
				return
					(Input.GetKey(config.keyCode.Down) ||
					 Input.GetButton(config.buttonString.Down) ||
					 Input.GetAxis(config.buttonString.Down) < AxisMargin);

			case KeyType.Right:
				return
					(Input.GetKey(config.keyCode.Right) ||
					 Input.GetButton(config.buttonString.Right) ||
					 AxisMargin < Input.GetAxis(config.buttonString.Right));

			case KeyType.Left:
				return
					(Input.GetKey(config.keyCode.Left) ||
					 Input.GetButton(config.buttonString.Left) ||
					 Input.GetAxis(config.buttonString.Left) < AxisMargin);

			case KeyType.NormalShot:
				return
					(Input.GetKey(config.keyCode.NormalShot) ||
					 Input.GetButton(config.buttonString.NormalShot));

			case KeyType.SpecialShot:
				return
					(Input.GetKey(config.keyCode.SpecialShot) ||
					 Input.GetButton(config.buttonString.SpecialShot));

			case KeyType.Skil:
				return
					(Input.GetKey(config.keyCode.Skil) ||
					 Input.GetButton(config.buttonString.Skil));

			case KeyType.LowSpeed:
				return
					(Input.GetKey(config.keyCode.LowSpeed) ||
					 Input.GetButton(config.buttonString.LowSpeeds[0]) ||
					 Input.GetButton(config.buttonString.LowSpeeds[1]));

			case KeyType.Pause:
				return
					(Input.GetKey(config.keyCode.Pose) ||
					 Input.GetButton(config.buttonString.Pose));
		}

		return false;
	}

	public bool GetPushDown(KeyType _keyType, PlayerNumber _playerNumber)
	{
		var config = (_playerNumber == PlayerNumber.player1) ?
			this.player1Congif : this.player2Congif;

		switch (_keyType)
		{
			case KeyType.Return:
				return
					(Input.GetKeyDown(config.keyCode.Return) ||
					 Input.GetButtonDown(config.buttonString.Return));

			case KeyType.Cancel:
				return
					(Input.GetKeyDown(config.keyCode.Cancel) ||
					 Input.GetButtonDown(config.buttonString.Cancel));

			case KeyType.Up:
				return
					(Input.GetKeyDown(config.keyCode.Up) ||
					 Input.GetButtonDown(config.buttonString.Up));

			case KeyType.Down:
				return
					(Input.GetKeyDown(config.keyCode.Down) ||
					 Input.GetButtonDown(config.buttonString.Down));

			case KeyType.Right:
				return
					(Input.GetKeyDown(config.keyCode.Right) ||
					 Input.GetButtonDown(config.buttonString.Right));

			case KeyType.Left:
				return
					(Input.GetKeyDown(config.keyCode.Left) ||
					 Input.GetButtonDown(config.buttonString.Left));

			case KeyType.NormalShot:
				return
					(Input.GetKeyDown(config.keyCode.NormalShot) ||
					 Input.GetButtonDown(config.buttonString.NormalShot));

			case KeyType.SpecialShot:
				return
					(Input.GetKeyDown(config.keyCode.SpecialShot) ||
					 Input.GetButtonDown(config.buttonString.SpecialShot));

			case KeyType.Skil:
				return
					(Input.GetKeyDown(config.keyCode.Skil) ||
					 Input.GetButtonDown(config.buttonString.Skil));

			case KeyType.LowSpeed:
				return
					(Input.GetKeyDown(config.keyCode.LowSpeed) ||
					 Input.GetButtonDown(config.buttonString.LowSpeeds[0]) ||
					 Input.GetButtonDown(config.buttonString.LowSpeeds[1]));

			case KeyType.Pause:
				return
					(Input.GetKeyDown(config.keyCode.Pose) ||
					 Input.GetButtonDown(config.buttonString.Pose));
		}

		return false;
	}

	public bool GetRelease(KeyType _keyType, PlayerNumber _playerNumber)
	{
		var config = (_playerNumber == PlayerNumber.player1) ?
			this.player1Congif : this.player2Congif;

		switch (_keyType)
		{
			case KeyType.Return:
				return
					(Input.GetKeyUp(config.keyCode.Return) ||
					 Input.GetButtonUp(config.buttonString.Return));

			case KeyType.Cancel:
				return
					(Input.GetKeyUp(config.keyCode.Cancel) ||
					 Input.GetButtonUp(config.buttonString.Cancel));

			case KeyType.Up:
				return
					(Input.GetKeyUp(config.keyCode.Up) ||
					 Input.GetButtonUp(config.buttonString.Up));

			case KeyType.Down:
				return
					(Input.GetKeyUp(config.keyCode.Down) ||
					 Input.GetButtonUp(config.buttonString.Down));

			case KeyType.Right:
				return
					(Input.GetKeyUp(config.keyCode.Right) ||
					 Input.GetButtonUp(config.buttonString.Right));

			case KeyType.Left:
				return
					(Input.GetKeyUp(config.keyCode.Left) ||
					 Input.GetButtonUp(config.buttonString.Left));

			case KeyType.NormalShot:
				return
					(Input.GetKeyUp(config.keyCode.NormalShot) ||
					 Input.GetButtonUp(config.buttonString.NormalShot));

			case KeyType.SpecialShot:
				return
					(Input.GetKeyUp(config.keyCode.SpecialShot) ||
					 Input.GetButtonUp(config.buttonString.SpecialShot));

			case KeyType.Skil:
				return
					(Input.GetKeyUp(config.keyCode.Skil) ||
					 Input.GetButtonUp(config.buttonString.Skil));

			case KeyType.LowSpeed:
				return
					(Input.GetKeyUp(config.keyCode.LowSpeed) ||
					 Input.GetButtonUp(config.buttonString.LowSpeeds[0]) ||
					 Input.GetButtonUp(config.buttonString.LowSpeeds[1]));

			case KeyType.Pause:
				return
					(Input.GetKeyUp(config.keyCode.Pose) ||
					 Input.GetButtonUp(config.buttonString.Pose));
		}

		return false;
	}
	protected override void OnInit()
	{
		DontDestroyOnLoad(this.gameObject);

		if (!this.player1Congif || !this.player2Congif)
		{
			Debug.LogError("configがありません", this);
		}
	}
}