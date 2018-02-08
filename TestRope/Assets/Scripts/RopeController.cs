using UnityEngine;

namespace RopeSystem
{
	/// <summary>
	/// контроллер веревки
	/// при зажатой клавише "W" происходит увеличение длины веревки
	/// при зажатой клавише "S" происходит уменьшение длины веревки
	/// при зажатой ЛКМ происходит следование конца веревки за мышью
	/// </summary>
	public class RopeController
	{
		private Rope _rope = null;
		private bool _isIncrease = false;
		private bool _isDecrease = false;
		private float _dt = 0.0f;

		private const float KeyPressDelay = 0.2f;

		public RopeController(Rope rope)
		{
			_rope = rope;
		}

		public void Update()
		{
			_isIncrease = Input.GetKey(KeyCode.W);
			_isDecrease = Input.GetKey(KeyCode.S);

			_dt += Time.deltaTime;
			if (_dt > KeyPressDelay)
			{
				_dt = 0;
				if (_isIncrease)
					Increase();
				else if(_isDecrease)
					Decrease();
			}
			if (Input.GetMouseButton(0))
				DragBobber();
		}

		private void Increase()
		{
			if (_rope == null)
				return;

			_rope.Increase();
		}

		private void Decrease()
		{
			if (_rope == null)
				return;

			_rope.Decrease();
		}

		/// <summary>
		/// TODO: ПЕРЕДЕЛАТЬ механизм сделования конца веревки за мышью
		/// поскольку наблюдается негативный эффект от воздействия на физические объекты
		/// путем перемещения трансформа, но если добавлять просто силу для того чтобы объект следовал
		/// за мышью то необходимого эффекта не удается добиться.
		/// </summary>
		private void DragBobber()
		{
			Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);
			Vector2 mouseInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
			Vector2 offset = mouseInWorld - (Vector2)_rope.Head.transform.position;
			//при первом клике мышью моментально перетаскиваем трансформ в нужное место
			//затем каждый кадр пока зажата кнопка мыши держим поплавок физическими силами
			//в выбранной нами позиции
			if (Input.GetMouseButtonDown(0))
				DragByTransform(offset);
			DragByRigidbody(mouseInWorld);
		}

		private void DragByTransform(Vector2 offset)
		{
			_rope.Tail.transform.position = _rope.Head.transform.position + Vector3.ClampMagnitude(offset, _rope.GetLength() - 0.5f);
		}

		private void DragByRigidbody(Vector2 mousePosition)
		{
			Vector2 pos = mousePosition - (Vector2)_rope.Tail.transform.position;
			float forceValue = 500;
			_rope.Tail.AddForce(pos * forceValue);
		}
	}
}
