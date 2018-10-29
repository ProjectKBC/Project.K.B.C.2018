/* Author: flanny7
 * Update: 2018/10/28
*/

using UnityEngine;
using RiaSpriteAnimation;

public class TestAnimation : MonoBehaviour
{
	[SerializeField]
	private string firstAnimationKey = "wait";

	public enum AnimState
	{
		wait,
		left,
		right,
	}

	private RiaSpriteAnimator animator;

	private static readonly KeyCode KeyLeft = KeyCode.A;
	private static readonly KeyCode KeyRight = KeyCode.D;

	private void Start()
	{
		this.animator = GetComponent<RiaSpriteAnimator>();
		this.animator.Init(this.firstAnimationKey);
	}

	private void Update()
	{
		var prevPos = this.transform.position;
		if (Input.GetKey(KeyRight))
		{
			this.transform.position += Vector3.right * Time.deltaTime * 60 * 0.1f;
		}
		if (Input.GetKey(KeyLeft))
		{
			this.transform.position += Vector3.left * Time.deltaTime * 60 * 0.1f;
		}

		var currentPos = this.transform.position;
		if (prevPos.x < currentPos.x /*右に移動していたら*/)
		{
			if (Input.GetKeyDown(KeyRight) || Input.GetKeyUp(KeyLeft))
			{
				this.animator.ChangeAnim("right");
			}
		}
		else if (currentPos.x < prevPos.x /*左に移動していたら*/)
		{
			if (Input.GetKeyDown(KeyLeft) || Input.GetKeyUp(KeyRight))
			{
				this.animator.ChangeAnim("left");
			}
		}
		else /*移動していなければ*/
		{
			if ((Input.GetKeyDown(KeyRight) && Input.GetKey(KeyLeft)) ||
				(Input.GetKeyDown(KeyLeft) && Input.GetKey(KeyRight)) ||
				(Input.GetKeyUp(KeyRight) || Input.GetKeyUp(KeyLeft)))
			{
				this.animator.ChangeAnim("wait");
			}
		}

		this.animator.Run();
	}
}
