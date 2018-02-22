using System.Collections.Generic;
using UnityEngine;

namespace RopeSystem
{
	public class Rope : RopeBase
	{
		public override Rigidbody2D Head
		{
			get { return _head; }
			protected set { _head = value; }
		}

		public override Rigidbody2D Tail
		{
			get { return _tail; }
			protected set { _tail = value; }
		}

		private Stack<RopePartBase> _stack = new Stack<RopePartBase>();
		private Rigidbody2D _head = null;
		private Rigidbody2D _tail = null;
		private RopePartBase _partEtalon = null;

		private readonly string pathToRopePart = "Prefabs/simplePart";
		private readonly string pathToBobber = "Prefabs/bobber";

		public Rope(Rigidbody2D head)
		{
			_head = head;
			Initialize();
			
		}

		private void Initialize()
		{
			_partEtalon = new SimplePart();
			_partEtalon.Object = Resources.Load(pathToRopePart) as GameObject;

			RopePartBase bobber = new Bobber();
			bobber.Object = Resources.Load(pathToBobber) as GameObject;

			InitializeTail(bobber);
			Increase();
		}

		public override float GetLength()
		{
			return _stack.Count * SimplePart.JointSize;
		}

		public override void Decrease()
		{
			if (Head == null)
				return;
			if (_stack.Count <= 2)
				return;
			RopePartBase part = _stack.Peek();
			part.Destroy();
			_stack.Pop();
			_stack.Peek().Joint.connectedBody = Head;
		}

		public override void Increase()
		{
			Push(_partEtalon);
		}

		public override Vector2[] GetPoints()
		{
			Vector2[] points = new Vector2[_stack.Count + 1];
			points[0] = Head.position;
			int i = 1;
			foreach (RopePartBase part in _stack)
			{
				points[i] = part.Object.transform.position;
				i++;
			}
			return points;
		}

		/// <summary>
		/// добавление частей веревки путем инстанцирования(!) объектов
		/// </summary>
		private void Push(RopePartBase part)
		{
			RopePartBase newPart = part.Clone();
			if (_stack.Count > 0)
			{
				RopePartBase prevPart = _stack.Peek();
				prevPart.Joint.connectedBody = newPart.PhisycsBody;
			}
			_stack.Push(newPart);
			newPart.Joint.connectedBody = Head;
		}

		private void InitializeTail(RopePartBase tail)
		{
			Push(tail);
			_tail = _stack.Peek().PhisycsBody;
		}
	}
}