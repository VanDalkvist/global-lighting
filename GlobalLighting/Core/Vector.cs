using System;

namespace GlobalLighting.Core
{
	public class Vector
	{
		public double X { get; private set; }

		public double Y { get; private set; }

		public double Z { get; private set; }

		public double Length { get; private set; }

		public Vector()
		{
			X = 0;
			Y = 0;
			Z = 0;
			Length = 0;
		}

		public Vector(double x, double y, double z)
		{
			X = x;
			Y = y;
			Z = z;
			Length = SetLength();
		}

		public Vector(Vector v)
		{
			X = v.X;
			Y = v.Y;
			Z = v.Z;
			Length = v.Length;
		}

		private double SetLength()
		{
			return Math.Sqrt(X * X + Y * Y + Z * Z);
		}

		public double SquareOfLength()
		{
			return Length * Length;
		}

		public Vector Normalize()
		{
			double length = (1 / Length);
			return new Vector(X * length, Y * length, Z * length);
		}

		public Vector Transform(Vector axis)
		{
			Vector t = new Vector(axis);
			Vector M1;
			Vector M2;

			if (Math.Abs(axis.X) < Math.Abs(axis.Y))
			{
				if (Math.Abs(axis.X) < Math.Abs(axis.Z))
					t.X = 1;
				else
					t.Z = 1;
			}

			else
			{
				if (Math.Abs(axis.Y) < Math.Abs(axis.Z))
					t.Y = 1;
				else
					t.Z = 1;
			}

			M1 = axis.VectorProduct(t).Normalize();
			M2 = axis.VectorProduct(M1);

			return new Vector(
				X * M1.X + Y * M2.X + Z * axis.X,
				X * M1.Y + Y * M2.Y + Z * axis.Y,
				X * M1.Z + Y * M2.Z + Z * axis.Z);
		}

		public double DotProduct(Vector vector)
		{
			return (X * vector.X + Y * vector.Y + Z * vector.Z);
		}

		public Vector VectorProduct(Vector direction)
		{
			return new Vector(
				Y * direction.Z - direction.Y * Z,
				Z * direction.X - direction.Z * X,
				X * direction.Y - direction.X * Y);
		}

		public static Vector operator +(Vector point1, Vector point2)
		{
			return new Vector(point1.X + point2.X, point1.Y + point2.Y, point1.Z + point2.Z);
		}

		public static Vector operator -(Vector point1, Vector point2)
		{
			return new Vector(point1.X - point2.X, point1.Y - point2.Y, point1.Z - point2.Z);
		}

		public static Vector operator *(Vector vector, double number)
		{
			return new Vector(vector.X * number, vector.Y * number, vector.Z * number);
		}

		public static Vector operator *(double number, Vector vector)
		{
			return new Vector(vector.X * number, vector.Y * number, vector.Z * number);
		}

		public static Vector operator /(Vector vector, double number)
		{
			return new Vector(vector.X / number, vector.Y / number, vector.Z / number);
		}
	}
}