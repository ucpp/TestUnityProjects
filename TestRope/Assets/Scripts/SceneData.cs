using System;
using UnityEngine;

namespace RopeSystem
{
	[Serializable]
	public class SceneData
	{
		/// <summary>
		/// удочка с центром в месте крепления веревки
		/// </summary>
		public Transform RodGameObject;

		private RopeBase _rope = null;
		private RopeController _ropeController = null;
		private RopeDrawer _ropeDrawer = null;

		public void Initialize()
		{
			Rigidbody2D rodRigidbody = RodGameObject.GetComponent<Rigidbody2D>();
			_rope = new Rope(rodRigidbody);
			_ropeController = new RopeController(_rope);
			_ropeDrawer = new RopeDrawer();
		}

		public void Update()
		{
			_ropeDrawer.Draw(_rope);
			_ropeController.Update();
		}
	}
}
