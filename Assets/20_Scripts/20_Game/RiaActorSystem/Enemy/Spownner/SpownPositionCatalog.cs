using UnityEngine;

namespace Game.Enemy
{
	
	[CreateAssetMenu(menuName = "Spown/SpownPositionCatalog", fileName = "SpownPositionCatalog")]
	public sealed class SpownPositionCatalog : ScriptableObject
	{
		[SerializeField] private Vector2 centerTop;
		[SerializeField] private Vector2 rightEdgeTop;
		[SerializeField] private Vector2 leftEdgeTop;
		[SerializeField] private Vector2 rightCenter;
		[SerializeField] private Vector2 leftCenter;
		[SerializeField] private Vector2 rightUpside;
		[SerializeField] private Vector2 leftUpside;
		[SerializeField] private Vector2 rightSideTop;
		[SerializeField] private Vector2 leftSideTop;

		public Vector2 CenterTop { get { return this.centerTop; } }
		public Vector2 RightEdgeTop { get { return this.rightEdgeTop; } }
		public Vector2 LeftEdgeTop { get { return this.leftEdgeTop; } }
		public Vector2 RightCenter { get { return this.RightCenter; } }
		public Vector2 LeftCenter { get { return this.leftCenter; } }
		public Vector2 RightUpside { get { return this.rightUpside; } }
		public Vector2 LeftUpside { get { return this.leftUpside; } }
		public Vector2 RightSideTop { get { return this.rightSideTop; } }
		public Vector2 LeftSideTop { get { return this.leftSideTop; } }
	}
}