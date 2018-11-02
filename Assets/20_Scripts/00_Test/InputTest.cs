/* Author: flnany7
 * Update: 2018/11/2
*/

using UnityEngine;

public class InputTest : MonoBehaviour
{
	private void TestPushUp()
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

	private void TestPush()
	{
		if (RiaInput.Instance.GetPush(RiaInput.KeyType.Return, PlayerNumber.player1) ||
			RiaInput.Instance.GetPush(RiaInput.KeyType.Return, PlayerNumber.player2))
		{
			Debug.Log("Push Return");
		}

		if (RiaInput.Instance.GetPush(RiaInput.KeyType.Up, PlayerNumber.player1) ||
			RiaInput.Instance.GetPush(RiaInput.KeyType.Up, PlayerNumber.player2))
		{
			Debug.Log("Push Up");
		}

		if (RiaInput.Instance.GetPush(RiaInput.KeyType.Down, PlayerNumber.player1) ||
			RiaInput.Instance.GetPush(RiaInput.KeyType.Down, PlayerNumber.player2))
		{
			Debug.Log("Push Down");
		}

		if (RiaInput.Instance.GetPush(RiaInput.KeyType.Right, PlayerNumber.player1) ||
			RiaInput.Instance.GetPush(RiaInput.KeyType.Right, PlayerNumber.player2))
		{
			Debug.Log("Push Right");
		}

		if (RiaInput.Instance.GetPush(RiaInput.KeyType.Left, PlayerNumber.player1) ||
			RiaInput.Instance.GetPush(RiaInput.KeyType.Left, PlayerNumber.player2))
		{
			Debug.Log("Push Left");
		}
	}

	private void TestPushDown()
	{
		if (RiaInput.Instance.GetPushDown(RiaInput.KeyType.Return, PlayerNumber.player1) ||
			RiaInput.Instance.GetPushDown(RiaInput.KeyType.Return, PlayerNumber.player2))
		{
			Debug.Log("PushDown Return");
		}

		if (RiaInput.Instance.GetPushDown(RiaInput.KeyType.Up, PlayerNumber.player1) ||
			RiaInput.Instance.GetPushDown(RiaInput.KeyType.Up, PlayerNumber.player2))
		{
			Debug.Log("PushDown Up");
		}

		if (RiaInput.Instance.GetPushDown(RiaInput.KeyType.Down, PlayerNumber.player1) ||
			RiaInput.Instance.GetPushDown(RiaInput.KeyType.Down, PlayerNumber.player2))
		{
			Debug.Log("PushDown Down");
		}

		if (RiaInput.Instance.GetPushDown(RiaInput.KeyType.Right, PlayerNumber.player1) ||
			RiaInput.Instance.GetPushDown(RiaInput.KeyType.Right, PlayerNumber.player2))
		{
			Debug.Log("PushDown Right");
		}

		if (RiaInput.Instance.GetPushDown(RiaInput.KeyType.Left, PlayerNumber.player1) ||
			RiaInput.Instance.GetPushDown(RiaInput.KeyType.Left, PlayerNumber.player2))
		{
			Debug.Log("PushDown Left");
		}
	}

	private void Update ()
	{
		this.TestPushDown();
		this.TestPush();
		this.TestPushUp();
	}
}
