using System.Numerics;

namespace _25_KT3
{
    internal class System
    {
        static void Main(string[] args)
        {
            //KT3-1
            List<object> list = new List<object>() { (int) 5, 1.1f, 56f, (int) 69, 1.4f };

            float sum = PackagingUnpackaging.GetSumOfObjectList(list);

            Console.WriteLine($"The sum of numeric values in the list is: {sum}");
            //KT3-2
            Book<int> book1 = new Book<int>(1, "The Great Gatsby", "F. Scott Fitzgerald", 180);
            Book<string> book2 = new Book<string>("ISBN-12345", "To Kill a Mockingbird", "Harper Lee", 281);
            Book<Guid> book3 = new Book<Guid>(Guid.NewGuid(), "1984", "George Orwell", 328);

            Console.WriteLine(book1.ToString());
            Console.WriteLine(book2.ToString());
            Console.WriteLine(book3.ToString());
            //KT3-3
            GenerlizedClass<int> intInstance = new GenerlizedClass<int>(42);
            GenerlizedClass<Book<int>> bookInstance = new GenerlizedClass<Book<int>>(book1);

            Console.WriteLine(intInstance.Value.ToString());
            Console.WriteLine(bookInstance.Value.ToString());

            intInstance.Reset();
            bookInstance.Reset();

            Console.WriteLine(intInstance.Value.ToString());
            Console.WriteLine(bookInstance.Value.ToString());
            //KT4-4
            Circle<int> circle1 = new Circle<int>(5);
            Circle<string> circle2 = new Circle<string>("7");
            Circle<double> circle3 = new Circle<double>(3.2);
            Circle<float> circle4 = new Circle<float>(2.4f);

            Console.WriteLine($"Circle1 area: {circle1.GetArea()}");
            Console.WriteLine($"Circle2 area: {circle2.GetArea()}");
            Console.WriteLine($"Circle3 area: {circle3.GetArea()}");
            Console.WriteLine($"Circle4 area: {circle4.GetArea()}");

            circle1.SetRadius(15);
            circle2.SetRadius("17");
            circle3.SetRadius(0.7);
            circle4.SetRadius(69.07f);

            Console.WriteLine($"Circle1 area: {circle1.GetArea()}");
            Console.WriteLine($"Circle2 area: {circle2.GetArea()}");
            Console.WriteLine($"Circle3 area: {circle3.GetArea()}");
            Console.WriteLine($"Circle4 area: {circle4.GetArea()}");
            //KT4-5
            Rectangle<int, string> rectangle1 = new Rectangle<int, string>(2, "4");
            Rectangle<double, float> rectangle2 = new Rectangle<double, float>(new Vector2(1.5f,2.5f),3.3, 2.5f);
            Rectangle<float, string> rectangle3 = new Rectangle<float, string>(new Vector2(-2, 3), 4.2f, "3");

            Console.WriteLine($"1 - Minimum: {rectangle1.MinPoint}, Maximum: {rectangle1.MaxPoint}");
            Console.WriteLine($"2 - Minimum: {rectangle2.MinPoint}, Maximum: {rectangle2.MaxPoint}");
            Console.WriteLine($"3 - Minimum: {rectangle3.MinPoint}, Maximum: {rectangle3.MaxPoint}");
        }
    }
}
