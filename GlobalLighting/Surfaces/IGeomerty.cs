using GlobalLighting.Core;

namespace GlobalLighting.Surfaces
{
	public interface IGeometry
	{
		HitPoint Intersect(Vector start, Vector direction);
	}
}