using System;

namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a
{
    struct Vector
    {
        public double X { get; }
        public double Y { get; }

        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y);
        }

        public static Vector operator -(Vector a, Vector b) => new Vector(a.X - b.X, a.Y - b.Y);
        public static Vector operator -(Vector a) => new Vector(-a.X, -a.Y);
        public static Vector operator *(Vector a, double b) => new Vector(a.X * b, a.Y * b);
        public static Vector operator *(double a, Vector b) => b * a;
        public static Vector operator /(Vector a, double b) => new Vector(a.X / b, a.Y / b);
        public double Dot(Vector a) => X * a.X + Y * a.Y;
        public double Cross(Vector a) => X * a.Y - Y * a.X;
        public double Length => Math.Sqrt(X * X + Y * Y);
        public double LengthSquared => X * X + Y * Y;

        public static Vector Zero => new Vector(0, 0);
        public static Vector UnitX => new Vector(1, 0);
        public static Vector UnitY => new Vector(0, 1);
    }
}
