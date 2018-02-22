using UnityEngine;

namespace RopeSystem
{
	public abstract class RopeBase
	{
		/// <summary>
		/// конец веревки привязанный к статическому объекту
		/// с данного конца происходит прирост и уменьшение длины веревки
		/// </summary>
		public abstract Rigidbody2D Head { get; protected set; }
		
		/// <summary>
		/// нижний конец веревки который свободен
		/// </summary>
		public abstract Rigidbody2D Tail { get; protected set; }

		public abstract void Increase();
		public abstract void Decrease();

		/// <summary>
		/// Длина веревки
		/// </summary>
		public abstract float GetLength();

		/// <summary>
		/// массив координат частей веревки
		/// </summary>
		public abstract Vector2[] GetPoints();
	}
}