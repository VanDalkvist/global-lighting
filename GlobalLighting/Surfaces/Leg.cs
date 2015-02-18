using GlobalLighting.Core;
using GlobalLighting.Material;

namespace GlobalLighting.Surfaces
{
	public class Leg : AGeometry
	{
		private IMaterial material;
		private Vector center;
		private Size3D size;

		public Leg(Vector center, IMaterial material, Size3D halfSize)
			: base()
		{
			this.material = material;
			this.center = center;
			this.size = halfSize;

			Add(new Plane(
					new Vector(center.X - halfSize.Width, center.Y - halfSize.Height, center.Z - halfSize.Depth),
					new Vector(center.X - halfSize.Width, center.Y + halfSize.Height, center.Z - halfSize.Depth),
					new Vector(center.X + halfSize.Width, center.Y - halfSize.Height, center.Z - halfSize.Depth),
					material),
				new Plane(
					new Vector(center.X - halfSize.Width, center.Y - halfSize.Height, center.Z + halfSize.Depth),
					new Vector(center.X - halfSize.Width, center.Y + halfSize.Height, center.Z + halfSize.Depth),
					new Vector(center.X + halfSize.Width, center.Y - halfSize.Height, center.Z + halfSize.Depth),
					material),
				new Plane(
					new Vector(center.X - halfSize.Width, center.Y - halfSize.Height, center.Z + halfSize.Depth),
					new Vector(center.X - halfSize.Width, center.Y + halfSize.Height, center.Z + halfSize.Depth),
					new Vector(center.X - halfSize.Width, center.Y - halfSize.Height, center.Z - halfSize.Depth),
					material),
				new Plane(
					new Vector(center.X + halfSize.Width, center.Y - halfSize.Height, center.Z - halfSize.Depth),
					new Vector(center.X + halfSize.Width, center.Y + halfSize.Height, center.Z - halfSize.Depth),
					new Vector(center.X + halfSize.Width, center.Y - halfSize.Height, center.Z + halfSize.Depth),
					material));
		}
	}
}