using UnityEngine;

namespace RopeSystem
{
	public abstract class RopePartBase
	{
		public abstract AnchoredJoint2D Joint { get; }
		public abstract Rigidbody2D PhisycsBody { get; }
		public abstract GameObject Object { get; set; }

		public abstract RopePartBase Clone();
		public abstract void Destroy();
	}
}