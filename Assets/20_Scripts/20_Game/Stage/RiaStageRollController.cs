/* Author: flanny7
 * Update: 2018/11/1
*/

using UnityEngine;

[System.Serializable]
public class RiaStageRollController
{
	[SerializeField]
	private StageMaterialCatalog catalog;
	[SerializeField]
	private MeshRenderer meshRender;
	[SerializeField]
	private float scrollSpeed = 0.05f;

	private float elapsedTime = 0;
	private float Offset { get { return Mathf.Repeat(this.elapsedTime * this.scrollSpeed, 1f); } }

	public void Init()
	{
		this.elapsedTime = 0;
	}

	public void Run()
	{
		// 経過時間の更新
		this.elapsedTime += Time.deltaTime;

		meshRender.material.SetTextureOffset("_MainTex", new Vector2(0f, this.Offset));
	}

	public void ChangeMaterial(StageEnum _type, bool _isBoss)
	{
		Material mat = null;
		switch (_type)
		{
			case StageEnum.stage1:
				mat = this.catalog.Stage1Future;
				break;
			case StageEnum.stage2:
				mat = (!_isBoss) ? this.catalog.Stage2Ancient : this.catalog.Stage2AncientBoss;
				break;
			default: return;
		}

		this.meshRender.material = GameObject.Instantiate<Material>(mat);
	}
}
