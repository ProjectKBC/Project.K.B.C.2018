/* Author : flanny7
 * Update : 2018/10/22
*/

using UnityEngine;

namespace RiaActorSystem
{
	public sealed class RiaActor : MonoBehaviour
	{
		private readonly Vector3 VECTOR3_ONE = Vector3.one;
		private readonly Vector3 VECTOR3_ZERO = Vector3.zero;
		private readonly Quaternion QUATERNION_IDENTITY = Quaternion.identity;

		public bool IsActive { get; private set; }

		public RiaCharacter Character { get; private set; }
		public RiaCharacterScript CharacterScript { get; private set; }
		public Collider2DSupporter ColliderSupporter { get; private set; }

		public GameObject Go { get; private set; }
		public Transform Trans { get; private set; }

		public void Init()
		{
			this.ColliderSupporter = new Collider2DSupporter();

			this.Go = this.gameObject;
			this.Trans = this.transform;

			this.SetActive(false);
		}

		public void WakeUp(RiaCharacter _character, RiaCharacterScript _characterScript,
			Vector3 _position, Quaternion? _rotation = null, Vector3? _scale = null)
		{
			this.Character = _character;
			this.CharacterScript = _characterScript;

			this.Trans.localPosition = _position;
			this.Trans.localRotation = _rotation ?? QUATERNION_IDENTITY;
			this.Trans.localScale = _scale ?? VECTOR3_ONE;

			this.Character.Init();

			this.SetActive(true);
		}

		public void Play()
		{
			if (!IsActive) { return; }
			this.Character.Play();
		}

		public void Sleep()
		{
			this.Character.End();

			this.Character = null;
			this.CharacterScript = null;

			this.Trans.localPosition = VECTOR3_ZERO;
			this.Trans.localRotation = QUATERNION_IDENTITY;
			this.Trans.localScale = VECTOR3_ONE;

			this.SetActive(false);
		}

		private void SetActive(bool _isActive)
		{
			this.Go.SetActive(_isActive);
			this.IsActive = _isActive;
		}

		public void OnTriggerEnter2D(Collider2D _collision)
		{
			this.ColliderSupporter.CollectionTriggerEnter2D(_collision);
		}

		public void OnTriggerStay2D(Collider2D _collision)
		{
			this.ColliderSupporter.CollectionTriggerStay2D(_collision);
		}

		public void OnTriggerExit2D(Collider2D _collision)
		{
			this.ColliderSupporter.CollectionTriggerExit2D(_collision);
		}
	}
}