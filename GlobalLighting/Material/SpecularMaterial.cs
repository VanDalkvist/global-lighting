using System;
using GlobalLighting.Core;

namespace GlobalLighting.Material
{
	public class SpecularMaterial : IMaterial
	{
		public SpecularMaterial(Radiance ks, double n)
		{
			this.ks = ks;
			this.n = n;
		}

		public Radiance BRDF(Vector direction, Vector ndirection, Vector normal)
		{
			Radiance r = new Radiance(0);

			Vector R = 2 * normal.DotProduct(ndirection) * normal - ndirection;
			double cosphi = -direction.DotProduct(R);

			if (ks.R != 0)
			{
				r.R = ks.R * (n + 2) * Math.Pow(cosphi, n) / (2 * Math.PI);
			}

			if (ks.G != 0)
			{
				r.G = ks.G * (n + 2) * Math.Pow(cosphi, n) / (2 * Math.PI);
			}

			if (ks.B != 0)
			{
				r.B = ks.B * (n + 2) * Math.Pow(cosphi, n) / (2 * Math.PI);
			}

			return r;
		}

		public Vector SelectDirection(Vector direction, Vector normal, out Radiance factor, double ksi)
		{
			double cosa = Math.Pow(Utils.Random.NextDouble(), 1 / (n + 1));
			double sina = Math.Sqrt(1 - cosa * cosa);
			double b = 2 * Math.PI * Utils.Random.NextDouble();

			Vector R = direction - 2 * normal.DotProduct(direction) * normal;
			Vector ndirection = new Vector(sina * Math.Cos(b), sina * Math.Sin(b), cosa).Transform(R);

			factor = new Radiance(0);

			if (normal.DotProduct(ndirection) <= 0)
			{
				return null;
			}

			if (ks.R != 0)
			{
				factor.R = ks.R * (n + 2) * normal.DotProduct(ndirection) / (n + 1);
			}

			if (ks.G != 0)
			{
				factor.G = ks.G * (n + 2) * normal.DotProduct(ndirection) / (n + 1);
			}

			if (ks.B != 0)
			{
				factor.B = ks.B * (n + 2) * normal.DotProduct(ndirection) / (n + 1);
			}

			return ndirection;
		}

		private Radiance ks;
		private double n;
	}
}