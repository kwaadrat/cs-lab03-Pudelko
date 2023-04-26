using System;

namespace MyLib
{
    public sealed class Pudelko
    {
        private readonly double a;
        private readonly double b;
        private readonly double c;

        public double A { get => a / 1000.0; }
        public double B { get => b / 1000.0; }
        public double C { get => c / 1000.0; }

        private Pudelko(double a, double b, double c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public static Pudelko DefaultBox { get => new Pudelko(100, 100, 100); }

        public static Pudelko Create(double a = 100, double b = 100, double c = 100, UnitOfMeasure unit = UnitOfMeasure.Meter)
        {
            double factor = GetFactor(unit);
            if (a <= 0 || b <= 0 || c <= 0)
            {
                throw new ArgumentOutOfRangeException("Dimensions must be positive");
            }
            if (a > 10000 || b > 10000 || c > 10000)
            {
                throw new ArgumentOutOfRangeException("Dimensions must not exceed 10 meters");
            }

            return new Pudelko(a * factor, b * factor, c * factor);
        }

        private static double GetFactor(UnitOfMeasure unit)
        {
            switch (unit)
            {
                case UnitOfMeasure.Milimeter:
                    return 0.001;
                case UnitOfMeasure.Centimeter:
                    return 0.01;
                case UnitOfMeasure.Meter:
                default:
                    return 1;
            }
        }

        public override string ToString()
        {
            return $"{A:F3} m × {B:F3} m × {C:F3} m";
        }

        public string ToString(string format)
        {
            switch (format)
            {
                case "m":
                    return ToString();
                case "cm":
                    return $"{A * 100:F1} cm × {B * 100:F1} cm × {C * 100:F1} cm";
                case "mm":
                    return $"{A * 1000:F0} mm × {B * 1000:F0} mm × {C * 1000:F0} mm";
                default:
                    throw new FormatException($"Invalid format: {format}");
            }
        }
    }

    public enum UnitOfMeasure
    {
        Milimeter,
        Centimeter,
        Meter
    }
}