using System.Collections.Generic;
using GlobalLighting.Core;

namespace GlobalLighting.Surfaces
{
	public abstract class AGeometry : IGeometry
	{
		protected List<IGeometry> listOfSurfaces;

		protected AGeometry()
		{
			listOfSurfaces = new List<IGeometry>();
		}

		protected void Add(params IGeometry[] surfaces)
		{
			for (int i = 0; i < surfaces.Length; i++)
				listOfSurfaces.Add(surfaces[i]);
		}

		#region IGeometry Members

		public HitPoint Intersect(Vector start, Vector direction)
		{
			HitPoint closeHitPoint = null;
			foreach (var g in listOfSurfaces)
			{
				var hp = g.Intersect(start, direction);
				if (closeHitPoint == null)
					closeHitPoint = hp;
				else
					if ((hp != null) && (closeHitPoint.t > hp.t))
						closeHitPoint = hp;
			}
			return closeHitPoint;
		}

		#endregion IGeometry Members
	}
}