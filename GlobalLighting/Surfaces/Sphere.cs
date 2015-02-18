using System;
using GlobalLighting.Core;
using GlobalLighting.Material;

namespace GlobalLighting.Surfaces
{
	public class Sphere : IGeometry
	{
		private double radius;
		private Vector center;
		private IMaterial material;

		public Sphere(double radius, Vector center, IMaterial material)
		{
			this.radius = radius;
			this.center = center;
			this.material = material;
		}

		public HitPoint Intersect(Vector start, Vector direction)
		{
			Vector ac = start - center;

			double b = ac.DotProduct(direction);
			double c = ac.SquareOfLength() - radius * radius;

			double desc = b * b - c;

			if (desc < 0)
				return null;

			desc = Math.Sqrt(desc);
			double t = -b - desc;

			if (t < double.Epsilon)
			{
				t = -b + desc;

				if (t < double.Epsilon)
					return null;
			}

			return new HitPoint((start + t * direction - center) / radius, t, material);
		}
	}
}