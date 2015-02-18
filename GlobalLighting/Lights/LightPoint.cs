using GlobalLighting.Core;

namespace GlobalLighting.Lights
{
	public class LightPoint
	{
		public Radiance LE { get; private set; }

		public Vector Point { get; private set; }

		public Vector Normal { get; private set; }

		public double Probability { get; private set; }

		public LightPoint(Radiance le, Vector point, Vector normal, double probability)
		{
			LE = le;
			Point = point;
			Normal = normal;
			Probability = probability;
		}
	}
}