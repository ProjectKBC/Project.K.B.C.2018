/* Author: flanny7
 * Update: 2018/11/1
*/

using UnityEngine;

[CreateAssetMenu(menuName = "RiaStage/StageMaterialCatalog", fileName = "StageMaterialCatalog")]
public class StageMaterialCatalog : ScriptableObject
{
	// アクセサー
	public Material Stage1Future { get { return this.stage1Future; } }
	public Material Stage2Ancient { get { return this.stage2Ancient; } }
	public Material Stage2AncientBoss { get { return this.stage2AncientBoss; } }

	// シリアライズ
	[SerializeField]
	private Material stage1Future;
	[SerializeField]
	private Material stage2Ancient;
	[SerializeField]
	private Material stage2AncientBoss;
}
