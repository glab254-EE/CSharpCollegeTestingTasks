using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Text;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Net.Http;
using System.Threading.Tasks;

namespace y25kt_2_1
{
    internal class Program 
    {
        static Random ranad = new Random();
        static void Main(string[] args)
        {
            DNDFight();
        }
        static void DNDFight()
        {
            bool d20(int successnum, int ammount)
            {
                int sum = 0;
                for (int a = 0; a < ammount; a++)
                {
                    int number = ranad.Next(1, 21);
                    sum += number;
                    Console.WriteLine(number);
                }
                if (sum >= successnum)
                {
                    return true;
                }
                else return false;
            }
            int attackd20(int successnum, int debuf)
            {
                int sum = 0;
                sum = ranad.Next(1, 21);
                Debug.WriteLine(sum);
                if (sum - debuf >= successnum || sum >= successnum)
                {
                    Debug.WriteLine(sum - debuf);
                    if (sum - debuf - 10 >= successnum || sum - 10 >= successnum)
                    {
                        return 2;
                    }
                    else return 1;
                }
                else return 0;
            }
            bool finished = false;
            bool fight = false;
            DNDHero hero = new DNDHero();
            DificultyFactory dificultyFactory = null;
            Console.Clear();
            bool dificultySelected = false;
            int result = 0;
            while (!dificultySelected)
            {
                Console.WriteLine("Now, select your dificulty... 1-4. from easier, to harder.");
                bool trypick = int.TryParse(Console.ReadLine(), out result);
                if (trypick)
                {
                    dificultySelected = true;
                    break;
                }
                else Console.WriteLine("Error,could not pick. ");
            }
            result = Math.Clamp(result, 1, 4);
            switch (result)
            {
                case 2:
                    dificultyFactory = new NormalDificultyFactory();
                    break;
                case 3:
                    dificultyFactory = new HardDificultyFactory();
                    break;
                case 4:
                    dificultyFactory = new DragonlordDificultyFactory();
                    break;
                default:
                    dificultyFactory = new EasyDificultyFactory();
                    break;
            }
            Console.WriteLine("\nSelected: " + dificultyFactory.GetType().Name + ".");
            Thread.Sleep(2000);
            Console.Clear();
            DNDEnemy enemy = null;
            while (!finished)
            {
                if (hero.hp > 0)
                {
                    Console.Clear();
                    if (enemy == null)
                    {
                        enemy = dificultyFactory.GetEnemy();
                        Console.WriteLine($" An {enemy.name} appears!");
                    }
                    Console.Write($"\nYou: {hero.hp}/{hero.HClas.maxhp}. M:{hero.mana}/{hero.HClas.maxmana}");
                    Console.WriteLine("\n" + enemy.name + $" {enemy.hp}/{enemy.maxhp} \n");
                    Console.WriteLine("What will you do?");
                    Console.WriteLine("1 - Light Attack\t 2-Heavy Attack(mana/chance)\t 3-heal");
                    bool picked = false;
                    do
                    {
                        if (hero.hp <= 0) break;
                        var action = Console.ReadKey(true);
                        if (action.Key == ConsoleKey.D1)
                        {
                            picked = true;
                            int chance = 20 - hero.HClas.atkcn;
                            int attk = attackd20(chance, enemy.dodgc);
                            if (attk == 0)
                            {
                                Console.WriteLine("You missed your attack.");
                            }
                            else if (attk == 1)
                            {
                                Console.WriteLine($"You hit your attack for {hero.HClas.atk}");
                                enemy.Damage(hero.HClas.atk);
                            }
                            else
                            {
                                Console.WriteLine($"You had critical hit on enemy! They take {hero.HClas.atk * 2} damage!");
                                enemy.Damage(hero.HClas.atk * 2);
                            }
                        }
                        else if (action.Key == ConsoleKey.D2)
                        {
                            picked = true;
                            int chance = 22 - hero.HClas.atkcn;
                            if (hero.mana > 0)
                            {
                                chance = 15 - hero.HClas.atkcn;
                                hero.mana -= 2;
                            }
                            int attk = attackd20(chance, enemy.dodgc);
                            if (attk == 0)
                            {
                                Console.WriteLine("You missed your S. attack.");
                            }
                            else if (attk == 1)
                            {
                                Console.WriteLine($"You hit your S. attack for {hero.HClas.atk * 2}");
                                enemy.Damage(hero.HClas.atk * 2);
                            }
                            else
                            {
                                Console.WriteLine($"You had super-critical hit on enemy! They take {hero.HClas.atk * 3} damage!");
                                enemy.Damage(hero.HClas.atk * 3);
                            }
                        }
                        else if (action.Key == ConsoleKey.D3)
                        {
                            picked = true;
                            decimal a = hero.HClas.maxhp / 2;
                            int healingfactor = int.Parse(Math.Round(a).ToString());
                            decimal b = hero.HClas.maxmana / 5;
                            int manaa = int.Parse(Math.Round(b).ToString());
                            hero.mana = Math.Clamp(hero.mana + manaa, hero.mana, hero.HClas.maxmana);
                            hero.changehealth(-healingfactor);
                        }
                    } while (!picked);
                    Thread.Sleep(1000);
                    if (enemy.hp <= 0)
                    {
                        Console.WriteLine($"You have defeated {enemy.name}!");
                        Console.WriteLine("To leave, press one.\nto reset, press two.\notherwise, it will continue.");
                        var input = Console.ReadKey(true);
                        if (input.Key == ConsoleKey.D1)
                        {
                            finished = true;
                            Application.Exit();
                        }
                        else if (input.Key == ConsoleKey.D2)
                        {
                            Console.Clear();
                            DNDFight();
                            return;
                        }
                        enemy = null;
                        fight = false;
                        Console.ReadKey();
                    }
                    else
                    {
                        int chance = 20 - enemy.atkc;
                        int attk = attackd20(chance, enemy.dodgc);
                        if (attk == 0)
                        {
                            Console.WriteLine("It missed.");
                        }
                        else if (attk == 1)
                        {
                            Console.WriteLine($"It hit you for {enemy.atk}");
                            hero.changehealth(enemy.atk);
                        }
                        else
                        {
                            Console.WriteLine($"It have hit critical attack on you! You take {enemy.atk * 2} damage!");
                            hero.changehealth(enemy.atk * 2);
                        }
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("You have died.");
                    Console.WriteLine("To leave, press one.\notherwise, it will reset progress.");
                    var input = Console.ReadKey(true);
                    if (input.Key == ConsoleKey.D1)
                    {
                        finished = true;
                        Application.Exit();
                        return;
                    }
                    Console.Clear();
                    DNDFight();
                    return;
                }
            }
        }
    }
    public class DNDHeroClass
    {
        public int maxhp = 20;
        public int atk = 5;
        public int defence = 0;
        public int atkcn = 10;
        public int dodgcn = 2;
        public int maxmana = 0;
        public DNDHeroClass(params int[] ints)
        {
            maxhp = ints.Length >= 1 && ints[0] != 0 ? ints[0] : 20;
            atk = ints.Length >= 2 && ints[1] != 0 ? ints[1] : 5;
            defence = ints.Length >= 3 && ints[2] != 0 ? ints[2] : 0;
            atkcn = ints.Length >= 4 && ints[3] != 0 ? ints[3] : 10;
            dodgcn = ints.Length >= 5 && ints[4] != 0 ? ints[4] : 2;
            maxmana = ints.Length >= 6 && ints[5] != 0 ? ints[5] : 0;
        }
    }
    public class DNDHero
    {
        public string name;
        public int hp = 20;
        public int mana = 0;
        public DNDHeroClass HClas;
        public DNDHero()
        {
            StringBuilder tempbuilder = new StringBuilder();
            tempbuilder.AppendLine("Character creation started.");
            tempbuilder.AppendLine("Insert your name, hero. \n");
            Console.WriteLine(tempbuilder.ToString());
            string nam = "\0";
            while (nam == "\0")
            {
                var inp = Console.ReadLine();
                if (inp.Length >= 1)
                {
                    nam = inp;
                }
            }
            name = nam;
            Console.Clear();
            tempbuilder.Clear();
            Thread.Sleep(1250);
            tempbuilder.AppendLine($"Very well, {name}");
            tempbuilder.AppendLine("Now, we pick your class number.");
            Console.WriteLine(tempbuilder.ToString());
            Thread.Sleep(1000);
            Console.Clear();
            tempbuilder.Clear();
            tempbuilder.AppendLine("N-1 is an Shieldsman, an tank.");
            tempbuilder.AppendLine("N-2 is an warrior, an universal fighter.");
            tempbuilder.AppendLine("N-3 is an rouge, an hidden backstabber.");
            tempbuilder.AppendLine("N-4 is an hunter, an primary DPS dealer.");
            Console.WriteLine(tempbuilder.ToString());
            Thread.Sleep(1000);
            Console.WriteLine("And finally, ");
            Thread.Sleep(1000);
            Console.Clear();
            tempbuilder.AppendLine("N-5 is an Mage, an magic-based caster.");
            Console.WriteLine(tempbuilder.ToString());
            Thread.Sleep(1000);
            bool localselect = false;
            int result = 0;
            while (!localselect)
            {
                Console.WriteLine("Now, select your class... 1-5.");
                bool trypick = int.TryParse(Console.ReadLine(), out result);
                if (trypick)
                {
                    localselect = true;
                    break;
                }
                else Console.WriteLine("Error,could not pick. ");
            }
            DNDHeroClass tempclass;
            if (result == 1) tempclass = new DNDHeroClass(40, 5, 5, 10, 0);
            else if (result == 2) tempclass = new DNDHeroClass(20, 10, 1, 10, 2);
            else if (result == 3) tempclass = new DNDHeroClass(15, 10, 0, 15, 5);
            else if (result == 4) tempclass = new DNDHeroClass(15, 15, 0, 9, 2);
            else if (result == 5) tempclass = new DNDHeroClass(18, 10, 0, 10, 1, 20);
            else tempclass = new DNDHeroClass(15, 10, 1, 10, 2);
            HClas = tempclass;
            hp = HClas.maxhp;
            mana = HClas.maxmana;
        }
        public void changehealth(int value)
        {
            int newhp = Math.Clamp(hp - value, 0, HClas.maxhp);
            hp = newhp;
        }
    }
    public class DNDEnemy
    {
        public string name;
        public int maxhp, hp = 10;
        public int atk = 5;
        public int defence = 0;
        public int dodgc = 1;
        public int atkc = 7;
        public DNDEnemy(string enemyname)
        {
            name = enemyname;
        }
        public DNDEnemy(string enemyname, params int[] stats)
        {
            name = enemyname;
            maxhp = stats[0];
            hp = maxhp;
            atk = stats[1];
            defence = stats[2];
            dodgc = stats[3];
            atkc = stats[4];
        }
        public void Damage(int baseatk)
        {
            int newhp = Math.Clamp(hp - (baseatk - defence), 0, maxhp);
            hp = newhp;
        }
    }
    public abstract class DificultyFactory
    {
        public abstract DNDEnemy GetEnemy();
    }
    public class EasyDificultyFactory : DificultyFactory
    {
        private static List<DNDEnemy> _AvailableEnemiesList = new List<DNDEnemy>()
        {
            new DNDEnemy("Small Rat", 2, 1, 0, 10, 5),
            new DNDEnemy("Rat", 4, 2, 0, 10, 6),
            new DNDEnemy("Big Rat", 6, 2, 0, 7, 6),
            new DNDEnemy("Wild racoon", 10, 3, 0, 6, 7),
        };
        public override DNDEnemy GetEnemy()
        {
            int EnemyIndex = new Random().Next(0, _AvailableEnemiesList.Count);
            return _AvailableEnemiesList[EnemyIndex];
        }
    }
    public class NormalDificultyFactory : DificultyFactory
    {
        private static List<DNDEnemy> _AvailableEnemiesList = new List<DNDEnemy>()
        {
            new DNDEnemy("Small Rat", 2, 1, 0, 10, 5),
            new DNDEnemy("Rat", 4, 2, 0, 10, 6),
            new DNDEnemy("Big Rat", 6, 2, 0, 7, 6),
            new DNDEnemy("Wild racoon", 10, 3, 0, 6, 7),
            new DNDEnemy("Unarmed Goblin Mugger", 15, 3, 0, 2, 10),
            new DNDEnemy("Armored Goblin Mugger", 16, 4, 0, 2, 9),
        };
        public override DNDEnemy GetEnemy()
        {
            int EnemyIndex = new Random().Next(0, _AvailableEnemiesList.Count);
            return _AvailableEnemiesList[EnemyIndex];
        }
    }
    public class HardDificultyFactory : DificultyFactory
    {
        private static List<DNDEnemy> _AvailableEnemiesList = new List<DNDEnemy>()
        {
            new DNDEnemy("Wild racoon", 10, 3, 0, 6, 7),
            new DNDEnemy("Unarmed Goblin Mugger", 15, 3, 0, 2, 10),
            new DNDEnemy("Armored Goblin Mugger", 16, 4, 0, 2, 9),
            new DNDEnemy("Heavily armored Goblin Mugger", 18, 4, 2, 1, 9),
            new DNDEnemy("Weak Human Bandit", 20, 5, 1, 4, 10),
            new DNDEnemy("Human Bandit", 22, 5, 0, 3, 10),
            new DNDEnemy("Baby dragon", 30, 5, 1, 0, 8),
        };
        public override DNDEnemy GetEnemy()
        {
            int EnemyIndex = new Random().Next(0, _AvailableEnemiesList.Count);
            return _AvailableEnemiesList[EnemyIndex];
        }
    }
    public class DragonlordDificultyFactory : DificultyFactory
    {
        private static List<DNDEnemy> _AvailableEnemiesList = new List<DNDEnemy>()
        {
            new DNDEnemy("Baby dragon", 30, 5, 2, 2, 10),
            new DNDEnemy("Teen dragon", 45, 8, 2, 2, 10),
            new DNDEnemy("Adult dragon", 60, 10, 2, 2, 11),
            new DNDEnemy("Alpha Senior dragon", 65, 11, 2, 2, 12),
        };
        public override DNDEnemy GetEnemy()
        {
            int EnemyIndex = new Random().Next(0, _AvailableEnemiesList.Count);
            return _AvailableEnemiesList[EnemyIndex];
        }
    }
}