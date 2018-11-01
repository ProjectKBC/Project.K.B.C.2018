using System.Runtime.CompilerServices;
using UnityEngine;

// todo: Up, Down, Right, Leftがおかしい
public class RiaInput : SingletonMonoBehaviour<RiaInput>
{
	private const float AxisMargin = 0.9f;
	[SerializeField]
	private RiaInputConfig player1Congif = null;
	[SerializeField]
	private RiaInputConfig player2Congif = null;
	
	struct RiaAxisInputs
	{
		public RiaAxisInput up;
		public RiaAxisInput down;
		public RiaAxisInput right;
		public RiaAxisInput left;

		public RiaAxisInputs(float _margin, RiaInputConfig _config)
		{
			this.up = new RiaAxisInput(_margin, true, _config.buttonString.Up);
			this.down = new RiaAxisInput(_margin, false, _config.buttonString.Down);
			this.right = new RiaAxisInput(_margin, true, _config.buttonString.Right);
			this.left = new RiaAxisInput(_margin, false, _config.buttonString.Left);
		}
	}

	private RiaAxisInputs axisPL1;
	private RiaAxisInputs axisPL2;

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
	
	protected override void OnInit()
	{
		DontDestroyOnLoad(this.gameObject);

		if (!this.player1Congif || !this.player2Congif)
		{
			Debug.LogError("configがありません", this);
		}
		
		this.axisPL1 = new RiaAxisInputs(AxisMargin, this.player1Congif);
		this.axisPL2 = new RiaAxisInputs(AxisMargin, this.player2Congif);
	}

	private void LateUpdate()
	{
		this.axisPL1.up.LastUpdate();
		this.axisPL1.down.LastUpdate();
		this.axisPL1.right.LastUpdate();
		this.axisPL1.left.LastUpdate();
		
		this.axisPL2.up.LastUpdate();
		this.axisPL2.down.LastUpdate();
		this.axisPL2.right.LastUpdate();
		this.axisPL2.left.LastUpdate();
	}

	#region GetKey
	
	public bool GetKey(KeyType _keyType, PlayerNumber _playerNumber)
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

	#endregion

	#region GetPush
	
	public bool GetPush(KeyType _keyType, PlayerNumber _playerNumber)
	{
		var config = (_playerNumber == PlayerNumber.player1) ? this.player1Congif : this.player2Congif;
		var axis = (_playerNumber == PlayerNumber.player1) ? this.axisPL1 : this.axisPL2;
		
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
				return (Input.GetKey(config.keyCode.Up) || axis.up.Push());

			case KeyType.Down:
				return (Input.GetKey(config.keyCode.Down) || axis.down.Push());

			case KeyType.Right:
				return (Input.GetKey(config.keyCode.Right) || axis.right.Push());

			case KeyType.Left:
				return (Input.GetKey(config.keyCode.Left) || axis.left.Push());

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
					 Input.GetButton(config.buttonString.LowSpeeds[1]) ||
					 Input.GetButton(config.buttonString.LowSpeeds[2]) ||
					 Input.GetButton(config.buttonString.LowSpeeds[3]));

			case KeyType.Pause:
				return
					(Input.GetKey(config.keyCode.Pose) ||
					 Input.GetButton(config.buttonString.Pose));
		}

		return false;
	}
	
	public bool GetPushUp(KeyType _keyType, PlayerNumber _playerNumber)
	{
		var config = (_playerNumber == PlayerNumber.player1) ? this.player1Congif : this.player2Congif;
		var axis = (_playerNumber == PlayerNumber.player1) ? this.axisPL1 : this.axisPL2;

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
				return (Input.GetKeyUp(config.keyCode.Up) || axis.up.PushUp());

			case KeyType.Down:
				return (Input.GetKeyUp(config.keyCode.Down) || axis.down.PushUp());

			case KeyType.Right:
				return (Input.GetKeyUp(config.keyCode.Right) || axis.right.PushUp());

			case KeyType.Left:
				return (Input.GetKeyUp(config.keyCode.Left) || axis.left.PushUp());

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
					 Input.GetButtonUp(config.buttonString.LowSpeeds[1]) ||
					 Input.GetButtonUp(config.buttonString.LowSpeeds[2]) ||
					 Input.GetButtonUp(config.buttonString.LowSpeeds[3]));

			case KeyType.Pause:
				return
					(Input.GetKeyUp(config.keyCode.Pose) ||
					 Input.GetButtonUp(config.buttonString.Pose));
		}

		return false;
	}

	public bool GetPushDown(KeyType _keyType, PlayerNumber _playerNumber)
	{
		var config = (_playerNumber == PlayerNumber.player1) ? this.player1Congif : this.player2Congif;
		var axis = (_playerNumber == PlayerNumber.player1) ? this.axisPL1 : this.axisPL2;

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
				return (Input.GetKeyDown(config.keyCode.Up) || axis.up.PushDown());

			case KeyType.Down:
				return (Input.GetKeyDown(config.keyCode.Down) || axis.down.PushDown());

			case KeyType.Right:
				return (Input.GetKeyDown(config.keyCode.Right) || axis.right.PushDown());

			case KeyType.Left:
				return (Input.GetKeyDown(config.keyCode.Left) || axis.left.PushDown());

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
					 Input.GetButtonDown(config.buttonString.LowSpeeds[1]) ||
					 Input.GetButtonDown(config.buttonString.LowSpeeds[2]) ||
					 Input.GetButtonDown(config.buttonString.LowSpeeds[3]));

			case KeyType.Pause:
				return
					(Input.GetKeyDown(config.keyCode.Pose) ||
					 Input.GetButtonDown(config.buttonString.Pose));
		}

		return false;
	}
	
	#endregion
}