using UnityEngine;

namespace RopeSystem
{
	public class SimplePart : RopePart
	{
		public const float JointSize = 0.5f;

		public override GameObject Object { get; set; }

		public Transform Parent
		{
			get
			{
				return Object != null ? Object.transform.parent : null;
			}
		}

		public override AnchoredJoint2D Joint
		{
			get
			{
				if (_joint == null && Object != null)
					_joint = Object.GetComponent<HingeJoint2D>();
				return _joint;
			}
		}

		public override Rigidbody2D PhisycsBody
		{
			get
			{
				if (_rigidbody2D == null && Object != null)
					_rigidbody2D = Object.GetComponent<Rigidbody2D>();
				return _rigidbody2D;
			}
		}

		private Rigidbody2D _rigidbody2D = null;
		private HingeJoint2D _joint = null;

		public override RopePart Clone()
		{
			RopePart clone = new SimplePart();
			clone.Object = GameObject.Instantiate(Object);
			clone.Object.SetActive(true);
			clone.Object.transform.parent = Parent;
			clone.Joint.connectedAnchor = new Vector2(0, -JointSize);
			return clone;
		}

		public override void Destroy()
		{
			if (Object == null)
				return;
			GameObject.Destroy(Object);
		}
	}
}