using GlobalLighting.Core;
using GlobalLighting.Material;

namespace GlobalLighting.Surfaces
{
	public class Room : AGeometry
	{
		public class Materials
		{
			protected internal IMaterial materialOfFloor;
			protected internal IMaterial materialOfRoof;
			protected internal IMaterial materialOfBackWall;
			protected internal IMaterial materialOfRightWall;
			protected internal IMaterial materialOfLeftWall;

			public Materials()
			{
				materialOfFloor = new DiffuseMaterial(new Radiance());
				materialOfRoof = new SpecularMaterial(new Radiance(), 200);
				materialOfBackWall = new DiffuseMaterial(new Radiance());
				materialOfRightWall = new DiffuseMaterial(new Radiance(1, 1, 0));
				materialOfLeftWall = new DiffuseMaterial(new Radiance(1, 0, 1));
			}

			public Materials(IMaterial materialOfFloor, IMaterial materialOfRoof,
				IMaterial materialOfBackWall, IMaterial materialOfRightWall, IMaterial materialOfLeftWall)
			{
				this.materialOfFloor = materialOfFloor;
				this.materialOfRoof = materialOfRoof;
				this.materialOfBackWall = materialOfBackWall;
				this.materialOfRightWall = materialOfRightWall;
				this.materialOfLeftWall = materialOfLeftWall;
			}
		}

		public Room(Size3D halfSize, double distance, Materials materials)
			: base()
		{
			Add(// Пол
				new Plane(
					new Vector(-halfSize.Width, -halfSize.Height, 2 * halfSize.Depth),
					new Vector(-halfSize.Width, -halfSize.Height, 2 * halfSize.Depth + distance),
					new Vector(halfSize.Width, -halfSize.Height, 2 * halfSize.Depth), materials.materialOfFloor),
				// Потолок
				new Plane(
					new Vector(-halfSize.Width, halfSize.Height, 2 * halfSize.Depth),
					new Vector(halfSize.Width, halfSize.Height, 2 * halfSize.Depth),
					new Vector(-halfSize.Width, halfSize.Height, 2 * halfSize.Depth + distance), materials.materialOfRoof),
				// Задняя стена
				new Plane(
					new Vector(-halfSize.Width, -halfSize.Height, 2 * halfSize.Depth + distance),
					new Vector(-halfSize.Width, halfSize.Height, 2 * halfSize.Depth + distance),
					new Vector(halfSize.Width, -halfSize.Height, 2 * halfSize.Depth + distance), materials.materialOfBackWall),
				// Правая стена
				new Plane(
					new Vector(-halfSize.Width, halfSize.Height, 2 * halfSize.Depth),
					new Vector(-halfSize.Width, halfSize.Height, 2 * halfSize.Depth + distance),
					new Vector(-halfSize.Width, -halfSize.Height, 2 * halfSize.Depth), materials.materialOfRightWall),
				// Левая стена
				new Plane(
					new Vector(halfSize.Width, halfSize.Height, 2 * halfSize.Depth),
					new Vector(halfSize.Width, -halfSize.Height, 2 * halfSize.Depth),
					new Vector(halfSize.Width, halfSize.Height, 2 * halfSize.Depth + distance), materials.materialOfLeftWall));
		}
	}
}