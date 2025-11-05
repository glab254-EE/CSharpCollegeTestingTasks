using ConsoleDungeons.Dungeons;
using ConsoleDungeons.Enemies;
using ConsoleDungeons.Heroes;

namespace ConsoleDungeons
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Hero ourhero = HeroUtils.GetHero().Result;
            Dungeon dungeon = new();
            AEnemy enemy = dungeon.OnEnemyDefeated().Result;
            Console.Clear();
            Console.WriteLine($"An hostile {enemy.EnemyName} stands infront of you.");
            while (ourhero.Health > 0) 
            {
                Console.WriteLine($"{enemy.EnemyName} - {enemy.Health} HP.\n\n{ourhero.Name} - {ourhero.Health}/{ourhero.Class.MaxHealth}.\n\nPress any key to attack.");
                Console.ReadKey(); // as there is no other option for combat, this is the way.
                Console.WriteLine();
                ourhero.Attack(enemy);
                if (enemy.Health <= 0)
                {
                    Console.WriteLine($"The {enemy.EnemyName} have been defeated. Press any key to continue.");
                    Console.ReadKey();
                    Console.Clear();
                    await Task.Delay(500);
                    enemy = dungeon.OnEnemyDefeated().Result;
                    Console.WriteLine($"An hostile {enemy.EnemyName} stands infront of you.");
                    continue;
                }
                enemy.Attack(ourhero);
                if (ourhero.Health <= 0)
                {
                    Console.WriteLine("You have taken final hit to yourself.");
                    break;
                }
                await Task.Delay(2500);
                Console.Clear();
            }
            Console.WriteLine("You have died.");
        }
    }
    public static class GameUtils
    {
        public static int ForceParseIntInput(string startingText,string retrytext,bool clearConsole=false)
        {
            int result = 0;
            bool parsed = false;
            Console.WriteLine(startingText);
            do
            {
                var input = Console.ReadKey(true);
                parsed = int.TryParse(input.KeyChar.ToString(), out result);
                if (!parsed)
                {
                    if (clearConsole) Console.Clear();
                    Console.WriteLine(retrytext);
                }
                Console.WriteLine();
            } while (!parsed);
            return result;
        }
    }
}
