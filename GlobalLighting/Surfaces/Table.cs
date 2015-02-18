using System;
using GlobalLighting.Core;
using GlobalLighting.Material;

namespace GlobalLighting.Surfaces
{
	public class Table : AGeometry
	{
		private Vector center;
		private IMaterial materialOfTabletop;
		private IMaterial materialOfTableLeg;

		public Table(Vector center, Size3D halfSize, double widthOfLeg, IMaterial materialOfTabletop, IMaterial materialOfTableLeg)
			: base()
		{
			this.center = center;
			this.materialOfTabletop = materialOfTabletop;
			this.materialOfTableLeg = materialOfTableLeg;
			double offsetFromEdge = 0.05;
			Size3D sizeOfLeg = new Size3D(widthOfLeg, Math.Abs(-0.5 - center.Y + halfSize.Height) / 2, widthOfLeg);

			// Крышка стола
			Add(new Plane(
					new Vector(center.X + halfSize.Width, center.Y - halfSize.Height, center.Z - halfSize.Depth),
					new Vector(center.X - halfSize.Width, center.Y - halfSize.Height, center.Z - halfSize.Depth),
					new Vector(center.X + halfSize.Width, center.Y - halfSize.Height, center.Z + halfSize.Depth),
					materialOfTabletop),
				new Plane(
					new Vector(center.X + halfSize.Width, center.Y + halfSize.Height, center.Z - halfSize.Depth),
					new Vector(center.X - halfSize.Width, center.Y + halfSize.Height, center.Z - halfSize.Depth),
					new Vector(center.X + halfSize.Width, center.Y + halfSize.Height, center.Z + halfSize.Depth),
					materialOfTabletop),

				new Plane(
					new Vector(center.X - halfSize.Width, center.Y + halfSize.Height, center.Z - halfSize.Depth),
					new Vector(center.X - halfSize.Width, center.Y - halfSize.Height, center.Z - halfSize.Depth),
					new Vector(center.X + halfSize.Width, center.Y + halfSize.Height, center.Z - halfSize.Depth),
					materialOfTabletop),
				new Plane(
					new Vector(center.X - halfSize.Width, center.Y + halfSize.Height, center.Z + halfSize.Depth),
					new Vector(center.X - halfSize.Width, center.Y - halfSize.Height, center.Z + halfSize.Depth),
					new Vector(center.X + halfSize.Width, center.Y + halfSize.Height, center.Z + halfSize.Depth),
					materialOfTabletop),

				new Plane(
					new Vector(center.X - halfSize.Width, center.Y + halfSize.Height, center.Z - halfSize.Depth),
					new Vector(center.X - halfSize.Width, center.Y - halfSize.Height, center.Z - halfSize.Depth),
					new Vector(center.X - halfSize.Width, center.Y + halfSize.Height, center.Z + halfSize.Depth),
					materialOfTabletop),
				new Plane(
					new Vector(center.X + halfSize.Width, center.Y + halfSize.Height, center.Z - halfSize.Depth),
					new Vector(center.X + halfSize.Width, center.Y - halfSize.Height, center.Z - halfSize.Depth),
					new Vector(center.X + halfSize.Width, center.Y + halfSize.Height, center.Z + halfSize.Depth),
					materialOfTabletop),

				// Левая дальняя ножка стола
				new Leg(
					new Vector(
						center.X - halfSize.Width + offsetFromEdge + widthOfLeg,
						-0.5 + sizeOfLeg.Height,
						center.Z + halfSize.Depth - offsetFromEdge),
					materialOfTableLeg, sizeOfLeg),

				// Левая ближняя ножка стола
				new Leg(
					new Vector(
						center.X - halfSize.Width + offsetFromEdge + widthOfLeg,
						-0.5 + sizeOfLeg.Height,
						center.Z - halfSize.Depth + offsetFromEdge),
					materialOfTableLeg, sizeOfLeg),

				// Правая дальняя ножка стола
				new Leg(
					new Vector(
						center.X + halfSize.Width - offsetFromEdge - widthOfLeg,
						-0.5 + sizeOfLeg.Height,
						center.Z - halfSize.Depth + offsetFromEdge),
					materialOfTableLeg, sizeOfLeg),

				// Правая ближняя ножка стола
				new Leg(
					new Vector(
						center.X + halfSize.Width - offsetFromEdge - widthOfLeg,
						-0.5 + sizeOfLeg.Height,
						center.Z + halfSize.Depth - offsetFromEdge),
					materialOfTableLeg, sizeOfLeg)
				);
		}
	}
}