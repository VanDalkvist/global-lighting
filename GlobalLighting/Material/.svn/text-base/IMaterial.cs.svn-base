using GlobalLighting.Core;

namespace GlobalLighting.Material
{
	public interface IMaterial
	{
		Radiance BRDF(Vector direction, Vector ndirection, Vector normal);

		Vector SelectDirection(Vector direction, Vector normal, out Radiance factor, double ksi);
	}
}