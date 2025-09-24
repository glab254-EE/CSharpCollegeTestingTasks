using System.Numerics;

namespace _25_KT3
{
    public class Figure
    {
        public Vector2 center;
        public virtual Vector2 MinPoint { get; }
        public virtual Vector2 MaxPoint { get; }
        public Figure(Vector2 center)
        {
            this.center = center;
        }
        public Figure()
        {
            this.center = Vector2.Zero;
        }
    }
    public class Circle<T> : Figure
    {
        public T radius { get; private set; }
        public override Vector2 MinPoint 
        { get => new Vector2(center.X - Convert.ToSingle(radius)/2, center.Y - Convert.ToSingle(radius))/2; }
        public override Vector2 MaxPoint
        { get => new Vector2(center.X + Convert.ToSingle(radius)/2, center.Y + Convert.ToSingle(radius))/2; }
        public Circle(Vector2 center, T radius) : base(center)
        {   
            this.radius = radius;
        }
        public Circle(T radius) : base()
        {
            this.radius = radius;
        }
        public void SetRadius(T value)
        {
            this.radius = value;
        }
        public double GetArea()
        {
            try
            {
                double r = Convert.ToDouble(radius);
                return Math.PI * r * r;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return double.NaN;
            }
        }
    }
    public class Rectangle<T1, T2> : Figure
    {
        public T1 width { get; private set; }
        public T2 height { get; private set; }
        public override Vector2 MinPoint
        { get => new Vector2(center.X - Convert.ToSingle(width), center.Y - Convert.ToSingle(height)) ; }
        public override Vector2 MaxPoint
        { get => new Vector2(center.X + Convert.ToSingle(width) , center.Y + Convert.ToSingle(height)) ; }
        public Rectangle(Vector2 center, T1 width, T2 height) : base(center)
        {
            this.width = width;
            this.height = height;
        }
        public Rectangle(T1 width, T2 height) : base()
        {
            this.width = width;
            this.height = height;
        }
    }
}   
