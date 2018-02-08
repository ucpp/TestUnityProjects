using UnityEngine;

namespace RopeSystem
{
	public abstract class RopePart
	{
		public abstract AnchoredJoint2D Joint { get; }
		public abstract Rigidbody2D PhisycsBody { get; }
		public abstract GameObject Object { get; set; }

		public abstract RopePart Clone();
		public abstract void Destroy();
	}
}