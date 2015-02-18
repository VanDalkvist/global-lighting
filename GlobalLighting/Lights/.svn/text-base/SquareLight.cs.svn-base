using GlobalLighting.Core;
using GlobalLighting.Utils;

namespace GlobalLighting.Lights
{
	public class SquareLight : ILightSource
	{
		private Vector a;
		private Vector ba;
		private Vector ca;
		private Vector normal;
		private Radiance LE;

		public SquareLight(Vector a, Vector b, Vector c, Radiance le)
		{
			this.a = a;
			ba = b - a;
			ca = c - a;
			normal = ba.VectorProduct(ca).Normalize();
			this.LE = le;
		}

		#region ILightSource Members

		public LightPoint SelectPoint()
		{
			return new LightPoint(
				LE,
				a + Random.NextDouble() * ba + Random.NextDouble() * ca,
				normal,
				1 / ba.VectorProduct(ca).Length);
		}

		#endregion ILightSource Members
	}
}