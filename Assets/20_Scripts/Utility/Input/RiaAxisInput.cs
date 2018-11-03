/* Author: flanny7
 * Update: 2018/11/2
*/

using UnityEngine;

/// <summary>
/// 前回の入力状態を保存して、AxisにPush/Downを対応させたクラス
/// </summary>
public sealed class RiaAxisInput
{
	private float margin;
	private bool isPlus;
	private string btnStr;
	
	private float prevAxis = 0;

	public RiaAxisInput(float _margin, bool _isPlus, string _btnStr)
	{
		this.margin = _margin;
		this.isPlus = _isPlus;
		this.btnStr = _btnStr;
	}
		
	public bool PushDown()
	{
		var currentAxis = Input.GetAxis(this.btnStr);

		var result = false;
		if (this.isPlus)
		{
			result = (!(this.margin < this.prevAxis)) && (this.margin <= currentAxis);
		}
		else
		{
			result = (!(this.prevAxis < -this.margin)) && (currentAxis <= -this.margin);
		}
		return result;
	}

	public bool PushUp()
	{
		var currentAxis = Input.GetAxis(this.btnStr);

		var result = false;
		if (this.isPlus)
		{
			result = (this.margin < this.prevAxis) && (!(this.margin <= currentAxis));
		}
		else
		{
			result = (this.prevAxis < -this.margin) && (!(currentAxis <= -this.margin));
		}
		return result;
	}

	public bool Push()
	{
		var currentAxis = Input.GetAxis(this.btnStr);
		
		var result = false;
		if (this.isPlus)
		{
			result = this.margin <= currentAxis;	
		}
		else
		{
			result = currentAxis <= -this.margin;
		}
		return result;
	}

	/// <summary>
	/// 入力処理後に必ず呼ぶ必要がある
	/// </summary>
	public void LateUpdate()
	{
		var currentAxis = Input.GetAxis(this.btnStr);
		this.prevAxis = currentAxis;
	}
}