using System;
using GlobalLighting.Core;

namespace GlobalLighting.Material
{
	public class DiffuseMaterial : IMaterial
	{
		private Radiance radiance;

		public DiffuseMaterial(Radiance radiance)
		{
			this.radiance = radiance;
		}

		public Radiance BRDF(Vector direction, Vector ndirection, Vector normal)
		{
			return radiance / Math.PI;
		}

		public Vector SelectDirection(Vector direction, Vector normal, out Radiance factor, double ksi)
		{
			double cosa = Utils.Random.NextDouble();
			double sina = Math.Sqrt(1 - cosa * cosa);
			double b = (2 * Math.PI * Utils.Random.NextDouble());

			factor = radiance / 2;
			return new Vector(sina * Math.Cos(b), sina * Math.Sin(b), cosa).Transform(normal);
			;
		}
	}
}