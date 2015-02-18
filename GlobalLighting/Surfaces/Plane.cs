using System;
using GlobalLighting.Core;
using GlobalLighting.Material;

namespace GlobalLighting.Surfaces
{
	public class Plane : IGeometry
	{
		private Vector a;
		private Vector ba;
		private Vector ca;
		private Vector normal;
		private Vector n;
		private IMaterial material;

		public Plane(Vector a, Vector b, Vector c, IMaterial material)
		{
			this.a = a;
			ba = b - a;
			ca = c - a;
			normal = ba.VectorProduct(ca);
			n = normal.Normalize();
			this.material = material;
		}

		public HitPoint Intersect(Vector start, Vector direction)
		{
			double t = 0;
			double t1 = 0;
			double t2 = 0;

			double divident = -direction.DotProduct(normal);

			if (Math.Abs(divident - 0) < double.Epsilon)
				return null;

			double factor = 1 / divident;

			Vector sa = start - a;
			Vector saxdir = sa.VectorProduct(direction);
			t1 = -ba.DotProduct(saxdir) * factor;

			if ((t1 < 0) || (1 < t1))
				return null;

			t2 = ca.DotProduct(saxdir) * factor;

			if ((t2 < 0) || (1 < t2))
				return null;

			t = sa.DotProduct(normal) * factor;

			if (t < double.Epsilon)
				return null;

			return new HitPoint(n, t, material);
		}
	}
}