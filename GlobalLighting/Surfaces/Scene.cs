namespace GlobalLighting.Surfaces
{
	public class Scene : AGeometry
	{
		public Scene(params IGeometry[] surfaces)
			: base()
		{
			Add(surfaces);
		}
	}
}