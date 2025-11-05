using ConsoleDungeons.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeons.Dungeons
{
    public class Dungeon
    {
        public Queue<ADificulty> DificultyQueue { get; private set; } = new([new EasyDificulty(),new MediumDificulty(), new HardDificulty()]);
        public ADificulty CurrentDificulty { get; private set; } 
        public async Task<AEnemy> OnEnemyDefeated()
        {
            AEnemy nextenemy = null;
            if (CurrentDificulty.IsCompleted)
            {
                Console.WriteLine($"You have defeated the {CurrentDificulty.Name} part of the dungeon.");
                await Task.Delay(1000);
                if (DificultyQueue.TryDequeue(out ADificulty dif))
                {
                    CurrentDificulty = dif;
                    Console.WriteLine($"Now, you are moving to the next dungeon. The {CurrentDificulty.Name} part of the dungeon.");
                    await Task.Delay(1000);
                } else
                {
                    Console.WriteLine("You have completed the dungeon.");
                    await Task.Delay(1000);
                    Console.WriteLine("Now, you are living an lavish live...");
                    await Task.Delay(2000);
                    Console.WriteLine("\nThe end.");
                    await Task.Delay(1000);
                    Environment.Exit(0);
                }
            }
            nextenemy = CurrentDificulty.GetNextEnemy();
            return nextenemy;
        }
        public Dungeon()
        {
            CurrentDificulty = DificultyQueue.Dequeue();
        }
    }
    public abstract class ADificulty
    {
        public abstract string Name { get; }
        public abstract List<AEnemy> Enemies { get; protected set; }
        public AEnemy GetNextEnemy()
        {
            if (IsCompleted) return null;
            int index = new Random().Next(0, Enemies.Count);
            if (Enemies.Count > 1)
            {
                index = Math.Clamp(index - 1, 0, Enemies.Count-1);
            }
            AEnemy outputedEnemy = Enemies[index];
            Enemies.Remove(outputedEnemy);
            return outputedEnemy;
        }
        public bool IsCompleted 
        {
            get
            {
                if (Enemies == null || Enemies.Count == 0) return true;
                return false;
            }
        }
    }
    public class EasyDificulty : ADificulty
    {
        public override string Name => "Easy";
        public override List<AEnemy> Enemies { get; protected set; } = [new BigRat(),new BigRat(),new BigRat(),new BigRat(),new KingRat()];
    }
    public class MediumDificulty : ADificulty
    {
        public override string Name => "Intermediate";
        public override List<AEnemy> Enemies { get; protected set; } = [new KingRat(),new LesserGoblin(),new LesserGoblin(),new Goblin(), new GoblinThievesLeader()];
    }
    public class HardDificulty : ADificulty
    {
        public override string Name => "Hard";
        public override List<AEnemy> Enemies { get; protected set; } = [new Goblin(), new Goblin(), new Goblin(), new GoblinThievesLeader(), new FirstDemon(), new FirstDemon(), new DemonBoss()];
    }
}
