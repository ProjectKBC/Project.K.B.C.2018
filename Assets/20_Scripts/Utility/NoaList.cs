using System.Linq;
using UnityEngine;

public class NoaListItem<T>
{
	public NoaListItem<T> prev;
	public NoaListItem<T> next;
	public T item;
}

public class NoaList<T>
{
	public int Length { get; private set; }

	private int capacity;
	private int useCapacity;
	private NoaListItem<T> top;
	private NoaListItem<T> end;
	private NoaListItem<T>[] pool;

	public T GetItem(int _index)
	{
		if (this.useCapacity <= _index) { return default(T); }

		return this.pool[_index].item;
	}

	public T[] GetItems()
	{
		return this.pool.Where(x => x != null).Select(x => x.item).ToArray();
	}

	public NoaList(int _maxCapacity)
	{
		this.capacity = _maxCapacity;
		this.useCapacity = 0;
		this.pool = new NoaListItem<T>[this.capacity];

		this.top = null;
		this.end = null;
	}

	public void Clear()
	{
		this.useCapacity = 0;
		this.pool = this.pool.Select(x => x = null).ToArray();

		this.top = null;
		this.end = null;
	}

	public void Add(T _item)
	{
		var item = new NoaListItem<T>();
		item.item = _item;

		if (useCapacity <= 0)
		{
			this.pool[this.useCapacity] = item;

			this.top = item;
			this.top.prev = null;
			this.top.next = null;

			this.end = item;
			this.end.prev = null;
			this.end.next = null;

			return;
		}

		++this.useCapacity;

		this.pool[this.useCapacity] = item;

		// 1つ前のItem
		this.pool[this.useCapacity - 1].next = this.pool[this.useCapacity];

		// 追加したItem
		this.pool[this.useCapacity].prev = this.pool[this.useCapacity - 1];
		this.pool[this.useCapacity].next = null;

		// 末尾の修正
		this.end = this.pool[this.useCapacity];
	}
}
