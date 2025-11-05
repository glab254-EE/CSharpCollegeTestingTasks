using ConsoleDungeons.Enemies;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeons.Heroes
{
    public struct PlayersExperienceStat
    {
        public float Current = 0;
        public float Max = 5;
        public float PerLevelGain = 5;
        public int CurrentLevel = 0;
        public int AvailableStatPoints = 0;
        public PlayersExperienceStat() { }
    }
    public class Attribute(string name, float value,float gain)
    {
        public string Name { get; set; } = name;
        public float Value { get; set; } = value;
        public float PerLevelGain { get; set; } = gain;
        public override bool Equals(object? obj)
        {
            return Name.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
    public class AttributeChart(List<Attribute> startingAttributes)
    {
        public List<Attribute> Attributes { get; private set; } = startingAttributes;
        public bool TryGetAttribute(string name, out Attribute value) 
        {
            value = GetAttribute(name);
            if (value != null)
            {
                return true;
            }
            return false;
        }
        public Attribute? GetAttribute(string name)
        {
            if (Attributes != null)
            {
                foreach (Attribute att in Attributes)
                {
                    if (att.Equals(name))
                    {
                        return att;
                    }
                }
            }
            return null;
        }
        public bool TrySetAttribute(string name, float value)
        {
            Attribute? attribute = GetAttribute(name);
            if (attribute != null && Attributes.Contains(attribute))
            {
                int index = Attributes.IndexOf(attribute);
                Attributes[index].Value = value;
                return true;
            }
            return false;
        }
    }
    public static class HeroUtils
    {
        public static readonly List<AHeroClass> selection = [
            new WarriorClass(),
            new ThiefClass(),
            new MageClass(),
            ];

        public static Hero OnLevelUp(Hero hero)
        {
            if (hero.XP.AvailableStatPoints > 0)
            {
                hero.XP.AvailableStatPoints -= 1;
                string availableAttributes = "";
                int lastAttributeIndex = hero.PlayerStats.Attributes.Count;
                for(int i = 0; i<lastAttributeIndex ; i++)
                {
                    Attribute att = hero.PlayerStats.Attributes[i];
                    availableAttributes += $"{i+1} - {att.Name}\n";
                }
                availableAttributes += $"{lastAttributeIndex +1} - Heal up with out an stat up.\n";
                while (true)
                {
                    int pick = GameUtils.ForceParseIntInput("Level up!\nPlease select stat to change.\nAvailable stats:\n"+availableAttributes,"Wrong input. This have to be from 1 to " + lastAttributeIndex+1,false);
                    pick--;
                    if (pick < lastAttributeIndex)
                    {
                        Attribute att = hero.PlayerStats.Attributes[pick];
                        att.Value += att.PerLevelGain;
                        Console.WriteLine("Attribute "+att.Name+" Have been raised by "+att.PerLevelGain+". and now it is "+att.Value+".");
                        break;
                    } else if (pick == lastAttributeIndex)
                    {
                        hero.Health = Math.Clamp(hero.Health + hero.Class.MaxHealth / 2, 0, hero.Class.MaxHealth);
                        Console.WriteLine("Healed up to "+hero.Health+"!");
                        break;
                    }

                }
            }
            return hero;
        }
        public static void GainXP(AEnemy enemy, ref PlayersExperienceStat XP)
        {
            if (enemy.Health <= 0)
            {
                XP.Current += enemy.XPGain;
                if (XP.Current >= XP.Max)
                {
                    XP.Current -= XP.Max;
                    XP.Max += XP.PerLevelGain;
                    XP.CurrentLevel++;
                    XP.AvailableStatPoints++;
                }
            }
        }
        public static AHeroClass PickHeroClass()
        {
            AHeroClass output = selection[0];
            StringBuilder text = new StringBuilder("Pick your class from the following:\n");
            for (int ind = 0; ind < selection.Count; ind++)
            {
                AHeroClass hero = selection[ind];
                text.AppendLine($"{ind+1} - {hero.ClassName} {hero.Description}");
            }
            text.AppendLine("\n(you can pick one of them by typing their desired number)\n");
            while (true)
            {
                int targetIndex = GameUtils.ForceParseIntInput(text.ToString(), text.ToString(), true);
                if (targetIndex-1 >= 0 && targetIndex-1 < selection.Count)
                {
                    output = selection[targetIndex-1];
                    Console.WriteLine("You have picked the " + selection[targetIndex-1].ClassName+" Class. Wait for a moment, while i get your things ready.");
                    break;
                }
            }

            output.MaxHealth = output.BaseMaxHealth;
            return output;
        }
        public static async Task<Hero> GetHero()
        {
            string heroName = "John Default";

            await Task.Delay(500);
            Console.WriteLine("Greetings adventuer,");
            await Task.Delay(500);
            Console.WriteLine("What is your name?");
            while (true)
            {
                string? input = Console.ReadLine();
                if (input != null && input.Length >= 2 && input != "" && input != string.Empty)
                {
                    heroName = input; 
                    break;
                } else
                {
                    Console.WriteLine("Thou name, is not correct.");
                }
            }
            await Task.Delay(1000);
            Console.Clear();
            await Task.Delay(500);
            Console.WriteLine("Very well. Now...");
            await Task.Delay(1000);
            Console.Clear();
            await Task.Delay(500);
            AHeroClass classy = PickHeroClass();
            await Task.Delay(2000);
            Hero newhero = new(heroName, classy);
            Console.WriteLine("I have given your equipment, so now, you are free to go, "+heroName+". ");
            await Task.Delay(2000);
            Console.WriteLine("Good luck.");
            await Task.Delay(2000);
            return newhero;
        }
    }
    public class Hero(string heroName,AHeroClass classy)
    {
        public AHeroClass Class { get; internal set; } = classy;
        public float Health { get; internal set; } = classy.BaseMaxHealth;
        public string Name { get; private set; } = heroName;
        public AttributeChart PlayerStats { get; protected set; } = classy.attributeChart;
        public PlayersExperienceStat XP = new();
        public void Attack(AEnemy enemy)
        {
            if (Health <= 0) return;
            Class.Attack(enemy);
            if (enemy.Health <= 0)
            {
                HeroUtils.GainXP(enemy, ref XP);    
                Hero newhero = HeroUtils.OnLevelUp(this);
                Health = newhero.Health;
                PlayerStats = newhero.PlayerStats; 
                XP = newhero.XP;
                Class.UpdateStatistics();
            }
        }
        public bool TakeDamage(float damage)
        {
            if (damage >= 0)
            {
                if (Class != null && Class.attributeChart.TryGetAttribute("Durability", out Attribute durab))
                {
                    float NewDamage = MathF.Round((damage - durab.Value / 20)*10)/10;
                    damage = NewDamage;
                }
                Health = Math.Clamp(Health - damage, 0, Health);
                Console.WriteLine(Name+" took "+damage.ToString()+" damage.\nnow his health are at "+Health.ToString()+" points.");
                if (Health <= 0) return true;
            }
            else
            {
                Health = Math.Clamp(Health - damage, 0, Health);
                Console.WriteLine(Name + " healed himself for " + (-damage).ToString() + " points.\nnow his health are at " + Health.ToString() + " points.");
                if (Health <= 0) return true;
            }
            Health = MathF.Round(Health * 10) / 10;
            return false;
        }
    }
    public abstract class AHeroClass
    {
        public abstract string ClassName { get; }
        public abstract string Description { get; }
        public abstract float BaseMaxHealth { get; protected set; }
        public abstract float BaseHealthPerLevel { get; protected set; }
        public abstract float MaxHealth { get; protected internal set; }
        public abstract float BaseAttack { get; set; }
        public abstract float CriticalChance { get; set; }
        public abstract AttributeChart attributeChart { get; set; }
        public void UpdateStatistics()
        {
            if (attributeChart.TryGetAttribute("Vitality",out Attribute vitality))
            {
                MaxHealth = BaseMaxHealth + BaseHealthPerLevel * vitality.Value;
            } else
            {
                MaxHealth = BaseMaxHealth;
            }
        }
        public void Attack(AEnemy enemy)
        {
            float damage = BaseAttack;
            if (attributeChart.TryGetAttribute("Strenght", out Attribute str))
            {
                damage = BaseAttack * (1+str.Value/10);
            }
            if (new Random().Next(0, 100) >= CriticalChance)
            {
                if (attributeChart.TryGetAttribute("SpecialATK", out Attribute spcl))
                {
                    damage *= spcl.Value;
                }
            }
            enemy.TakeDamage(damage);
        }
    }
    public class WarriorClass : AHeroClass
    {
        public override string ClassName => "Warrior";
        public override string Description => "An tank, what can take many hits";
        public override float BaseHealthPerLevel { get; protected set; } = 2;
        public override float BaseMaxHealth { get; protected set; } = 20;
        public override float MaxHealth { get; protected internal set; } = 20;
        public override float BaseAttack { get; set; } = 1f;
        public override float CriticalChance { get; set; } = 15f;
        public override AttributeChart attributeChart { get; set; } = new(
            [new("Strenght", 5,1), new("Durability", 10,1), new("SpecialATK", 1.5f,0.1f), new("Vitality",0,1)]
        );
    }
    public class ThiefClass : AHeroClass
    {
        public override string ClassName => "Thief";
        public override string Description => "An backstabber for main damage.";
        public override float BaseHealthPerLevel { get; protected set; } = 2;
        public override float BaseMaxHealth { get; protected set; } = 15;
        public override float MaxHealth { get; protected internal set; } = 15;
        public override float BaseAttack { get; set; } = 1f;
        public override float CriticalChance { get; set; } = 25f;
        public override AttributeChart attributeChart { get; set; } = new(
            [new("Strenght", 10, 1), new("Durability", 5, 1), new("SpecialATK", 2f, 0.1f), new("Vitality", 0, 1)]
        );
    }
    public class MageClass : AHeroClass
    {
        public override string ClassName => "Mage";
        public override string Description => "Weakest of them by health, but strongest by damage.";
        public override float BaseHealthPerLevel { get; protected set; } = 2;
        public override float BaseMaxHealth { get; protected set; } = 12;
        public override float MaxHealth { get; protected internal set; } = 12;
        public override float BaseAttack { get; set; } = 1.5f;
        public override float CriticalChance { get; set; } = 15f;
        public override AttributeChart attributeChart { get; set; } = new(
            [new("Strenght", 10, 1), new("Durability", 5, 1), new("SpecialATK", 2.1f, 0.1f), new("Vitality", 0, 1)]
        );
    }
}
