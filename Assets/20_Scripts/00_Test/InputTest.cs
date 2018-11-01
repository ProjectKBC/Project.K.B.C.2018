using UnityEngine;

public class InputTest : MonoBehaviour
{
	void testUp()
	{
		if (RiaInput.Instance.GetPushUp(RiaInput.KeyType.Return, PlayerNumber.player1) ||
			RiaInput.Instance.GetPushUp(RiaInput.KeyType.Return, PlayerNumber.player2))
		{
			Debug.Log("PushUp Return");
		}

		if (RiaInput.Instance.GetPushUp(RiaInput.KeyType.Up, PlayerNumber.player1) ||
			RiaInput.Instance.GetPushUp(RiaInput.KeyType.Up, PlayerNumber.player2))
		{
			Debug.Log("PushUp Up");
		}

		if (RiaInput.Instance.GetPushUp(RiaInput.KeyType.Down, PlayerNumber.player1) ||
			RiaInput.Instance.GetPushUp(RiaInput.KeyType.Down, PlayerNumber.player2))
		{
			Debug.Log("PushUp Down");
		}

		if (RiaInput.Instance.GetPushUp(RiaInput.KeyType.Right, PlayerNumber.player1) ||
			RiaInput.Instance.GetPushUp(RiaInput.KeyType.Right, PlayerNumber.player2))
		{
			Debug.Log("PushUp Right");
		}

		if (RiaInput.Instance.GetPushUp(RiaInput.KeyType.Left, PlayerNumber.player1) ||
			RiaInput.Instance.GetPushUp(RiaInput.KeyType.Left, PlayerNumber.player2))
		{
			Debug.Log("PushUp Left");
		}
	}

	void test()
	{
		if (RiaInput.Instance.GetPushUp(RiaInput.KeyType.Return, PlayerNumber.player1) ||
			RiaInput.Instance.GetPushUp(RiaInput.KeyType.Return, PlayerNumber.player2))
		{
			Debug.Log("Push Return");
		}

		if (RiaInput.Instance.GetPushUp(RiaInput.KeyType.Up, PlayerNumber.player1) ||
			RiaInput.Instance.GetPushUp(RiaInput.KeyType.Up, PlayerNumber.player2))
		{
			Debug.Log("Push Up");
		}

		if (RiaInput.Instance.GetPushUp(RiaInput.KeyType.Down, PlayerNumber.player1) ||
			RiaInput.Instance.GetPushUp(RiaInput.KeyType.Down, PlayerNumber.player2))
		{
			Debug.Log("Push Down");
		}

		if (RiaInput.Instance.GetPushUp(RiaInput.KeyType.Right, PlayerNumber.player1) ||
			RiaInput.Instance.GetPushUp(RiaInput.KeyType.Right, PlayerNumber.player2))
		{
			Debug.Log("Push Right");
		}

		if (RiaInput.Instance.GetPushUp(RiaInput.KeyType.Left, PlayerNumber.player1) ||
			RiaInput.Instance.GetPushUp(RiaInput.KeyType.Left, PlayerNumber.player2))
		{
			Debug.Log("Push Left");
		}
	}

	void testDown()
	{
		if (RiaInput.Instance.GetPushUp(RiaInput.KeyType.Return, PlayerNumber.player1) ||
			RiaInput.Instance.GetPushUp(RiaInput.KeyType.Return, PlayerNumber.player2))
		{
			Debug.Log("PushDown Return");
		}

		if (RiaInput.Instance.GetPushUp(RiaInput.KeyType.Up, PlayerNumber.player1) ||
			RiaInput.Instance.GetPushUp(RiaInput.KeyType.Up, PlayerNumber.player2))
		{
			Debug.Log("PushDown Up");
		}

		if (RiaInput.Instance.GetPushUp(RiaInput.KeyType.Down, PlayerNumber.player1) ||
			RiaInput.Instance.GetPushUp(RiaInput.KeyType.Down, PlayerNumber.player2))
		{
			Debug.Log("PushDown Down");
		}

		if (RiaInput.Instance.GetPushUp(RiaInput.KeyType.Right, PlayerNumber.player1) ||
			RiaInput.Instance.GetPushUp(RiaInput.KeyType.Right, PlayerNumber.player2))
		{
			Debug.Log("PushDown Right");
		}

		if (RiaInput.Instance.GetPushUp(RiaInput.KeyType.Left, PlayerNumber.player1) ||
			RiaInput.Instance.GetPushUp(RiaInput.KeyType.Left, PlayerNumber.player2))
		{
			Debug.Log("PushDown Left");
		}
	}

	void Update ()
	{
		this.test();
	}
}
