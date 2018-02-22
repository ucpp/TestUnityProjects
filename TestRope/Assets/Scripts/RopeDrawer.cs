using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RopeSystem
{
	public class RopeDrawer
	{
		public float LineWidth
		{
			get
			{
				return _lineWidth;
			}
		}

		public Color LineColor
		{
			get
			{
				return _lineColor;
			}
		}

		private Color _lineColor = Color.black;
		private float _lineWidth = 0.06f;
		private GameObject _line = null;
		private LineRenderer _lineRenderer = null;

		public RopeDrawer(float lineWidth = 0.06f)
		{
			_lineWidth = lineWidth;
			Initialize();
		}

		/// <summary>
		/// Создание и настройка LineRenderer'а происходит один раз в конструкторе
		/// шейдер "Particles/Alpha Blended Premultiply" выбран для решения проблем
		/// со "ступенчатостью" линии, но это проблему не решило
		/// TODO: найти способы сглаживания линии
		/// </summary>
		private void Initialize()
		{
			_line = new GameObject("Rope");
			_line.transform.position = Vector3.zero;
			_line.transform.localPosition = Vector3.zero;
			_line.transform.localScale = Vector3.one;
			_line.AddComponent<RectTransform>();
			_line.AddComponent<LineRenderer>();
			_lineRenderer = _line.GetComponent<LineRenderer>();
			_lineRenderer.useWorldSpace = false;
			_lineRenderer.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
			_lineRenderer.startColor = _lineColor;
			_lineRenderer.endColor = _lineColor;
			_lineRenderer.startWidth = _lineWidth;
			_lineRenderer.endWidth = _lineWidth;
		}

		public void Draw(RopeBase rope)
		{
			DrawCurve(rope.GetPoints());
		}

		private void DrawCurve(Vector2[] points)
		{
			List<Vector2> list = GetBezierSplinePoints(points, 2.0f).ToList();
			_lineRenderer.positionCount = list.Count;

			for (int i = 0; i < list.Count; i++)
				_lineRenderer.SetPosition(i, list[i]);
		}

		/// <summary>
		///http://ibiblio.org/e-notes/Splines/Bezier.htm
		/// использовать smoothness - 1..n (оптимально результат/производительность - 3)
		/// </summary>
		private Vector2[] GetBezierSplinePoints(Vector2[] arrayPoints, float smoothness)
		{
			List<Vector2> points;
			List<Vector2> curvedPoints;
			int pointsLength = 0;
			int curvedLength = 0;

			if (smoothness < 1.0f)
				smoothness = 1.0f;

			pointsLength = arrayPoints.Length;
			curvedLength = (pointsLength * Mathf.RoundToInt(smoothness)) - 1;
			curvedPoints = new List<Vector2>(curvedLength);

			float t = 0.0f;

			for (int pointInTimeOnCurve = 0; pointInTimeOnCurve < curvedLength + 1; pointInTimeOnCurve++)
			{
				t = Mathf.InverseLerp(0, curvedLength, pointInTimeOnCurve);
				points = new List<Vector2>(arrayPoints);
				for (int j = pointsLength - 1; j > 0; j--)
				{
					for (int i = 0; i < j; i++)
						points[i] = (1 - t) * points[i] + t * points[i + 1];
				}
				curvedPoints.Add(points[0]);
			}

			return (curvedPoints.ToArray());
		}
	}
}
