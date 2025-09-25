using WarlamovGleb_CatFramework;
namespace WarlamovGleb_CatApplicationInAerodynamics
{
    internal class Program
    {
        static void DisplayCatInfo(Cat[] LocalCatsArray, string path)
        {
            string toWrite = "";
            for (int i = 0; i < LocalCatsArray.Length; i++)
            {
                toWrite += $"Cat_{i}={{\n\tFluffinessCheck: {LocalCatsArray[i].FluffinessCheck()};\n\tToString: {LocalCatsArray[i]};\n}}\n";
            }
            File.WriteAllText(path, toWrite);
        }
        static Cat[] GenerateRandomCats(uint count)
        {
            Random random = new();
            Cat[] cats = new Cat[count];
            int pingpongcounter = 0;
            int lastCatIndex = 0;
            do 
            {
                try
                {
                    if (pingpongcounter == 0)
                    {
                        cats[lastCatIndex] = new Tiger(random.Next(-20, 120), random.Next(50, 160));
                    } else
                    {
                        cats[lastCatIndex] = new CuteCat(random.Next(-20, 120));
                    }
                    pingpongcounter = pingpongcounter == 1 ? 0 : pingpongcounter+1;
                    lastCatIndex++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception passed: '"+ex.Message+"'");
                }
            } 
            while (lastCatIndex < count);
            return cats;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Input your desired cats count");
            uint count = Convert.ToUInt32(Console.ReadLine());
            Console.Clear();
            Cat[] cats = GenerateRandomCats(count);
            Console.WriteLine("Input your information path (WITH .txt ending)");
            string? path = Console.ReadLine();
            if (path != null)
            {
                DisplayCatInfo(cats, path);
                Console.WriteLine("Information written to " + path);
            }
            else
            {
                Console.WriteLine("Path is null");
            }
        }
    }
}
