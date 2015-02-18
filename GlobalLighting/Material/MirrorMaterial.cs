using GlobalLighting.Core;

namespace GlobalLighting.Material
{
	public class MirrorMaterial : IMaterial
	{
		public MirrorMaterial(Radiance ks)
		{
			this.ks = ks;
		}

		public Radiance BRDF(Vector direction, Vector ndirection, Vector normal)
		{
			return new Radiance(0);
		}

		public Vector SelectDirection(Vector direction, Vector normal, out Radiance factor, double ksi)
		{
			factor = ks;
			return direction - 2 * normal.DotProduct(direction) * normal;
		}

		private Radiance ks;
	}
}