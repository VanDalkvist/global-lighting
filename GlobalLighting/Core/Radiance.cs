namespace GlobalLighting.Core
{
	public class Radiance
	{
		public double R;
		public double G;
		public double B;

		/// <summary>
		/// Яркость по умолчанию устанавливаем в белый цвет
		/// </summary>
		public Radiance()
		{
			R = 1;
			G = 1;
			B = 1;
		}

		public Radiance(double r, double g, double b)
		{
			R = r;
			G = g;
			B = b;
		}

		public Radiance(double color)
		{
			R = color;
			G = color;
			B = color;
		}

		public static Radiance operator +(Radiance radiance1, Radiance radiance2)
		{
			return new Radiance(radiance1.R + radiance2.R, radiance1.G + radiance2.G, radiance1.B + radiance2.B);
		}

		public static Radiance operator *(Radiance radiance1, Radiance radiance2)
		{
			return new Radiance(radiance1.R * radiance2.R, radiance1.G * radiance2.G, radiance1.B * radiance2.B);
		}

		public static Radiance operator *(Radiance radiance1, double number)
		{
			return new Radiance(radiance1.R * number, radiance1.G * number, radiance1.B * number);
		}

		public static Radiance operator /(Radiance radiance1, double number)
		{
			return new Radiance(radiance1.R / number, radiance1.G / number, radiance1.B / number);
		}
	}
}