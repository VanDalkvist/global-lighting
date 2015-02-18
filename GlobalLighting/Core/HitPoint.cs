using GlobalLighting.Material;

namespace GlobalLighting.Core
{
	public class HitPoint
	{
		public Vector Normal;
		public double t;
		public IMaterial Material;

		public HitPoint(Vector normal, double t, IMaterial material)
		{
			Material = material;
			Normal = normal;
			this.t = t;
		}
	}
}