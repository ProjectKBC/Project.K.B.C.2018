/* Author: flanny7
 * Update: 2018/10/28
*/

// todo: ListじゃなくてQueじゃね？

using UnityEngine;

public sealed class Collider2DSupporter
{
	// それぞれのイベント関数が呼ばれたか否か
	public bool IsTriggerEnter2D { get; private set; }
	public bool IsTriggerStay2D { get; private set; }
	public bool IsTriggerExit2D { get; private set; }
	
	// それぞれのイベント関数が呼び出されたときの衝突対象List
	public NoaList<GameObject> TriggerEnter2DGameObjects;
	public NoaList<GameObject> TriggerStay2DGameObjects;
	public NoaList<GameObject> TriggerExit2DGameObjects;

	// ListはListじゃない（？） 実はcapacityがある（プーリングしてるだけ？）
	public Collider2DSupporter(int _capacity = 10)
	{
		this.TriggerEnter2DGameObjects = new NoaList<GameObject>(_capacity);
		this.TriggerStay2DGameObjects = new NoaList<GameObject>(_capacity);
		this.TriggerExit2DGameObjects = new NoaList<GameObject>(_capacity);
	}

	// 各処理後の後始末 毎Updateで呼び出す必要がある
	public void AfterUpdate()
	{
		this.IsTriggerEnter2D = false;
		this.IsTriggerStay2D = false;
		this.IsTriggerExit2D = false;

		this.TriggerEnter2DGameObjects.Clear();
		this.TriggerStay2DGameObjects.Clear();
		this.TriggerExit2DGameObjects.Clear();
	}

	/// <summary>
	/// OnTriggerEnter2D内で呼び出す
	/// </summary>
	/// <param name="_collision"></param>
	public void CollectionTriggerEnter2D(Collider2D _collision)
	{
		this.IsTriggerEnter2D = true;
		this.TriggerEnter2DGameObjects.Add(_collision.gameObject);
	}

	/// <summary>
	/// OnTriggerStay2D内で呼び出す
	/// </summary>
	/// <param name="_collision"></param>
	public void CollectionTriggerStay2D(Collider2D _collision)
	{
		this.IsTriggerStay2D = true;
		this.TriggerStay2DGameObjects.Add(_collision.gameObject);
	}

	/// <summary>
	/// OnTriggerExit2D内で呼び出す
	/// </summary>
	/// <param name="_collision"></param>
	public void CollectionTriggerExit2D(Collider2D _collision)
	{
		this.IsTriggerExit2D = true;
		this.TriggerExit2DGameObjects.Add(_collision.gameObject);
	}
}