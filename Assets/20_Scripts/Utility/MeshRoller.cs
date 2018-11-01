/* Author: flanny7
 * Update: 2018/11/1
*/

using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class MeshRoller : MonoBehaviour
{
	[SerializeField]
	private float scrollSpeed = 2f;

	private MeshRenderer meshRender;
	private float elapsedTime;

	private float Offset { get { return Mathf.Repeat(this.elapsedTime * this.scrollSpeed, 1f); } }

	protected void Start()
	{
		this.meshRender = GetComponent<MeshRenderer>();
		this.elapsedTime = 0;
	}

	private void Update()
	{
		this.elapsedTime += Time.deltaTime;
		this.meshRender.material.SetTextureOffset("_MainTex", new Vector2(0f, this.Offset));
	}
}

