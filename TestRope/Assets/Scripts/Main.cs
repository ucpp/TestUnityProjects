using UnityEngine;

namespace RopeSystem
{
	public class Main : MonoBehaviour
	{
		public SceneData Data;

		private void Awake()
		{
			Data.Initialize();
		}

		private void Update()
		{
			Data.Update();
		}
	}
}
