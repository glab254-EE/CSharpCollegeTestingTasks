using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Part1
    {
        public static void Run()
        {
            Cube cube = new(Vector3.Zero, new Vector3(2, 1, 2), "Red");
            Console.WriteLine(cube.ToString());
            Cube? cloned = cube.clone() as Cube;
            Console.WriteLine(cloned?.ToString());
            cloned.Position = new Vector3(-1, -0.5f, -1);
            cloned.ColorName = "Purple";
            Console.WriteLine(cube.ToString());
            Console.WriteLine(cloned?.ToString());

            Sphere sphere = new(Vector3.Zero, 5, "Blue");
            Console.WriteLine(sphere.ToString());
            Sphere? cloned1 = sphere.clone() as Sphere;
            cloned1.Radius = 2;
            cloned1.ColorName = "Red";
            cloned1.Position = new Vector3(-2, 0, 0);
            Console.WriteLine(sphere.ToString());
            Console.WriteLine(cloned1.ToString());

        }
    }
    public interface IClonable
    {
        public object clone();
    }
    public class Cube(Vector3 position, Vector3 size, string colorName) : IClonable
    {
        public Vector3 Position = position;
        public Vector3 Size = size;
        public string ColorName = colorName;
        public object clone()
        {
            Cube clone = new(position,size,colorName);
            return clone;
        }
        public override string ToString()
        {
            return $"Cube at coords {position.X},{position.Y},{position.Z} with size of {size.X}, {size.Y}, {size.Z}, and color of {ColorName}";
        }
    }
    public class Sphere(Vector3 position, float radius, string colorName) : IClonable
    {
        public Vector3 Position = position;
        public float Radius = radius;
        public string ColorName = colorName;
        public object clone()
        {
            Sphere clone = new(Position, Radius, ColorName);
            return clone;
        }
        public override string ToString()
        {
            return $"Sphere at coords {position.X},{position.Y},{position.Z} with radius of {Radius}, and color of {ColorName}";
        }
    }
}
