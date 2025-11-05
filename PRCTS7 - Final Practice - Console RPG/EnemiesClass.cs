using ConsoleDungeons.Heroes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeons.Enemies
{
    public abstract class AEnemy
    {
        public abstract string EnemyName { get; }
        public abstract float Health { get; protected set; }
        public abstract float MinDamage { get;  }
        public abstract float MaxDamage { get;  }
        public abstract float XPGain { get;  }
        public bool TakeDamage(float d)
        { 
            Health -= d;
            Health = MathF.Round(Health * 10) / 10;
            Console.WriteLine(EnemyName+" took "+d.ToString()+" damage. now it have "+Health+" points.");
            return Health <= 0;
        }
        public void Attack(Hero target)
        {
            if (Health > 0)
            {
                Random r = new();
                float randomNum = r.NextSingle();
                float damage = MathF.Round((randomNum + MinDamage) * (MaxDamage - MinDamage) * 10f) / 10f;
                target.TakeDamage(damage);
            }
        }
    }
    public class BigRat : AEnemy
    {
        public override string EnemyName { get; } = "[Level 1] Rat";
        public override float Health { get; protected set; } = 4;
        public override float MinDamage { get; } = 1;
        public override float MaxDamage { get; } = 1.5f;
        public override float XPGain { get; } = 1;
    }
    public class KingRat : AEnemy
    {
        public override string EnemyName { get; } = "[Level 1] King Rat";
        public override float Health { get; protected set; } = 6;
        public override float MinDamage { get; } = 1.5f;
        public override float MaxDamage { get; } = 2f;
        public override float XPGain { get; } = 2;
    }
    public class LesserGoblin : AEnemy
    {
        public override string EnemyName { get; } = "[Level 2] Weak Goblin Thief";
        public override float Health { get; protected set; } = 5;
        public override float MinDamage { get; } = 2f;
        public override float MaxDamage { get; } = 2.5f;
        public override float XPGain { get; } = 2;
    }
    public class Goblin : AEnemy
    {
        public override string EnemyName { get; } = "[Level 2] Goblin Thief";
        public override float Health { get; protected set; } = 8;
        public override float MinDamage { get; } = 2f;
        public override float MaxDamage { get; } = 2.5f;
        public override float XPGain { get; } = 4;
    }
    public class GoblinThievesLeader : AEnemy
    {
        public override string EnemyName { get; } = "[Level 2] Goblin Thieves Leader";
        public override float Health { get; protected set; } = 10;
        public override float MinDamage { get; } = 2f;
        public override float MaxDamage { get; } = 2.5f;
        public override float XPGain { get; } = 6;
    }
    public class FirstDemon : AEnemy
    {
        public override string EnemyName { get; } = "[Level 3] Lesser Demon";
        public override float Health { get; protected set; } = 14;
        public override float MinDamage { get; } = 2f;
        public override float MaxDamage { get; } = 3f;
        public override float XPGain { get; } = 10;
    }
    public class DemonBoss : AEnemy
    {
        public override string EnemyName { get; } = "[Level 3] Demon Manager";
        public override float Health { get; protected set; } = 20;
        public override float MinDamage { get; } = 2.5f;
        public override float MaxDamage { get; } = 3f;
        public override float XPGain { get; } = 15;
    }
}
