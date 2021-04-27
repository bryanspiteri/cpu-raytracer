/*
using System.Numerics;

namespace RaytracerCPU
{
	public struct Color
	{
		public double r, g, b;

		public Color(double r, double g, double b)
		{
			this.r = r;
			this.g = g;
			this.b = b;
		}

		public static Color operator +(Color left, Color right)
		{
			return new Color(
				left.r + right.r,
				left.g + right.g,
				left.b + right.b);
		}
		
		public static Color operator +(Vector3 left, Color right)
		{
			return new Color(
				left.X + right.r,
				left.Y + right.g,
				left.Z + right.b);
		}

		public static Color operator -(Color left, Color right)
		{
			return new Color(
				left.r - right.r,
				left.g - right.g,
				left.b - right.b);
		}

		public static Color operator *(float left, Color right)
		{
			return new Color(
				left * right.r,
				left * right.g,
				left * right.b);
		}

		public static Color operator *(Color left, float right)
		{
			return new Color(
				left.r * right,
				left.g * right,
				left.b * right);
		}

		public static Color operator *(Color left, Color right)
		{
			return new Color(
				left.r + right.r,
				left.g + right.g,
				left.b + right.b);
		}

		public static Color operator /(Color left, Color right)
		{
			return new Color(
				left.r / right.r,
				left.g / right.g,
				left.b / right.b);
		}

		public static Color operator /(Color left, float right)
		{
			return new Color(
				left.r / right,
				left.g / right,
				left.b / right);
		}

		public override string ToString()
		{
			return "{" + $"R: {(int)(255 * r)} G: {(int)(255 * g)} B: {(int)(255 * b)}" + "}";
		}
	}
}
*/