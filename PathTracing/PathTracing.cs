using System.Drawing;
using System.Threading;
using GlobalLighting.Core;
using GlobalLighting.Lights;
using GlobalLighting.Surfaces;
using GlobalLighting.Utils;

namespace PathTracing
{
	public class PathTracing
	{
		private ILightSource light;
		private Scene scene;
		private int countOfPixelsOnHorizontal;
		private int countOfPixelsOnVertical;
		private int shadowRays;
		private double absorption;
		private Radiance[,] arrayOfRadiances;

		public PathTracing(int shadowRays, double absorption, int countOfPixelsOnHorizontal,
			int countOfPixelsOnVertical, Scene scene, ILightSource light)
		{
			this.shadowRays = shadowRays;
			this.absorption = absorption;
			this.countOfPixelsOnHorizontal = countOfPixelsOnHorizontal;
			this.countOfPixelsOnVertical = countOfPixelsOnVertical;
			this.scene = scene;
			this.light = light;

			arrayOfRadiances = new Radiance[countOfPixelsOnHorizontal, countOfPixelsOnVertical];
			for (int i = 0; i < countOfPixelsOnHorizontal; i++)
				for (int j = 0; j < countOfPixelsOnVertical; j++)
					arrayOfRadiances[i, j] = new Radiance(0);
		}

		public Radiance Radiance(HitPoint hitPoint, Vector point, Vector direction)
		{
			Radiance result = new Radiance(0);
			Radiance factor = new Radiance(1);

			Vector currentPoint = point;
			Vector currentDirection = direction;
			HitPoint currentHitPoint = hitPoint;

			while (true)
			{
				Radiance direct = new Radiance(0);
				LightPoint lightPoint = null;
				Vector nDirection = null;
				double cosDirNormal;
				double cosDirLightNormal;
				double length;
				HitPoint nHitPoint = null;

				for (int i = 0; i < shadowRays; i++)
				{
					lightPoint = light.SelectPoint();
					nDirection = lightPoint.Point - currentPoint;

					cosDirNormal = currentHitPoint.Normal.DotProduct(nDirection);

					if (cosDirNormal < 0)
						continue;

					cosDirLightNormal = -(nDirection.DotProduct(lightPoint.Normal));
					if (cosDirLightNormal < 0)
						continue;

					length = nDirection.Length;
					if (length * length < double.Epsilon)
						continue;

					double linv = 1 / length;
					nDirection *= linv;
					cosDirNormal *= linv;
					cosDirLightNormal *= linv;

					nHitPoint = scene.Intersect(currentPoint, nDirection);

					if (nHitPoint != null)
						if (nHitPoint.t <= length - double.Epsilon)
							continue;

					direct += lightPoint.LE *
						currentHitPoint.Material.BRDF(currentDirection, nDirection, currentHitPoint.Normal) *
						(cosDirNormal * cosDirLightNormal / (lightPoint.Probability * length * length));
				}

				direct /= shadowRays;

				result += factor * direct;
				//break;
				//Compute indirect luminancy

				double ksi = Random.NextDouble();

				if (ksi < absorption)
					break;

				ksi = (ksi - absorption) / (1 - absorption);

				Radiance f;
				Vector rndd = currentHitPoint.Material.SelectDirection(currentDirection, currentHitPoint.Normal, out f, ksi);

				if (rndd == null)
					break;

				HitPoint rhp = scene.Intersect(currentPoint, rndd);

				if (rhp == null)
					break;

				factor *= f / (1 - absorption);

				currentDirection = rndd;
				currentHitPoint = rhp;
				currentPoint += currentDirection * currentHitPoint.t;
			}

			return result;
		}

		public void ComputeImage(int countFrame)
		{
			int countOfThreads = System.Environment.ProcessorCount;
			Thread[] threads = new Thread[countOfThreads + 1];
			//int sizeOfChunk = countOfPixelsOnHorizontal / countOfThreads;
			int sizeOfChunk = countOfPixelsOnVertical / countOfThreads;

			for (int f = 0; f < countFrame; f++)
			{
				//for (int i = 0; i < threads.Length; i++)
				//{
				//int iStart = i * sizeOfChunk;
				//int end = iStart + sizeOfChunk;

				//threads[i] =
				//    new Thread(delegate()
				//    {
				//for (iStart = 0; iStart < end; iStart++)
				//Parallel.For(0, countOfPixelsOnHorizontal, i =>
				for (int i = 0; i < countOfPixelsOnHorizontal; i++)
				{
					for (int t = 0; t < threads.Length - 1; t++)
					{
						Vector start = null;
						Vector direction = null;
						HitPoint hp = null;

						double distance = 1;
						double px;
						double py;
						int jStart = t * sizeOfChunk;
						int end = jStart + sizeOfChunk;

						threads[t] =
						new Thread(delegate()
							{
								for (jStart = 0; jStart < end; jStart++)
								//for (int j = 0; j < countOfPixelsOnVertical; j++)
								{
									px = ((((double)i + Random.NextDouble() - 0.5) / countOfPixelsOnHorizontal) * 2 - 1) * 0.25;
									py = -((((double)jStart + Random.NextDouble() - 0.5) / countOfPixelsOnVertical) * 2 - 1) * 0.25;

									start = new Vector(0, 0, -distance);
									direction = new Vector(px, py, distance).Normalize();
									hp = scene.Intersect(start, direction);
									if (hp != null)
									{
										start += direction * hp.t;
										//arrayOfRadiances[iStart, j] = arrayOfRadiances[iStart, j] + Radiance(hp, start, direction);
										arrayOfRadiances[i, jStart] = arrayOfRadiances[i, jStart] + Radiance(hp, start, direction);
									}
								}
							});
					}
					for (int t = 0; t < threads.Length - 1; t++)
						threads[t].Start();
					for (int t = 0; t < threads.Length - 1; t++)
						threads[t].Join();
				}
			}
			threads[threads.Length - 1] =
				new Thread(delegate()
			{
				for (int i = 0; i < countOfPixelsOnHorizontal; i++)
					for (int j = 0; j < countOfPixelsOnVertical; j++)
						arrayOfRadiances[i, j] = arrayOfRadiances[i, j] / countFrame;
			});

			bool ready = true;
			for (int i = 0; i < threads.Length - 1; i++)
				if (threads[i].ThreadState == ThreadState.Running)
					ready = false;
			if (ready)
			{
				threads[threads.Length - 1].Start();
				threads[threads.Length - 1].Join();
			}
		}

		public Bitmap Draw()
		{
			Bitmap bitmap = new Bitmap(countOfPixelsOnHorizontal, countOfPixelsOnVertical);
			for (int i = 0; i < countOfPixelsOnHorizontal; i++)
				for (int j = 0; j < countOfPixelsOnVertical; j++)
					bitmap.SetPixel(i, j, Color.FromArgb(
						(int)(arrayOfRadiances[i, j].R > 1 ? 255 : arrayOfRadiances[i, j].R * 255),
						(int)(arrayOfRadiances[i, j].G > 1 ? 255 : arrayOfRadiances[i, j].G * 255),
						(int)(arrayOfRadiances[i, j].B > 1 ? 255 : arrayOfRadiances[i, j].B * 255)));
			return bitmap;
		}
	}
}