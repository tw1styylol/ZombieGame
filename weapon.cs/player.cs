using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public class Player
{
    private static Random rand = new Random();
    public bool IsInfected { get; set; } = false;
    public bool HasAntidote { get; set; } = false;
    public string Role { get; private set; }
    public int Health { get; set; } = 100;
    public int Food { get; set; }
    public int Water { get; set; }
    public List<Weapon> Inventory { get; private set; } = new List<Weapon>();
    public bool HasMap { get; set; } = false;
    public bool HasLock { get; set; } = false;
    public bool EvacuationPointUnlocked { get; set; } = false;
    public bool LockedShelter { get; set; } = false;
    public bool FlareGunUsed { get; set; } = false;
    public bool HasFlareGun { get; set; } = false;


    public bool HasTransistor { get; set; } = false;
    public bool HasCable { get; set; } = false;
    public bool HasScrew { get; set; } = false;
    public bool HasCapacitor { get; set; } = false;
    public bool HasChipPlate { get; set; } = false;
    public bool HasResistor { get; set; } = false;
    public bool HasSpeeker { get; set; } = false;
    public bool HasRelay { get; set; } = false;
    public bool RadiostationHasRepaired { get; set; } = false;

    public int FoodChance { get; set; }
    public int WaterChance { get; set; }
    public int InfectionChance { get; set; }
    public int DaysToSurvive { get; set; }
    public int ZombieAttackProbability { get; set; }

    public static void SleepInShelter(Player player, int daysSurvived)
    {
        Console.WriteLine("Ви проспали ніч в укритті.");
        daysSurvived++;
    }
    public Player(string role, string difficulty)
    {
        Role = role;
        switch (role)
        {

            case "Медик": Food = 1; Water = 1; Inventory.Add(new MeleeWeapon()); break;
            case "Солдат": Food = 5; Water = 5; Inventory.Add(new Rifle()); break;
            case "Виживший": Food = 0; Water = 2; break;
            case "Фермер": Food = 3; Water = 2; break;
        }


        switch (difficulty)
        {
            case "Легкий":

                FoodChance = 70;
                WaterChance = 70;
                InfectionChance = 10;
                DaysToSurvive = 10;
                ZombieAttackProbability = 15;
                break;

            case "Середній":
                FoodChance = 50;
                WaterChance = 50;
                InfectionChance = 20;
                DaysToSurvive = 15;
                ZombieAttackProbability = 25;
                break;

            case "Важкий":
                FoodChance = 30;
                WaterChance = 30;
                InfectionChance = 40;
                DaysToSurvive = 31;
                ZombieAttackProbability = 55;
                break;

            default:
                FoodChance = 50;
                WaterChance = 50;
                InfectionChance = 20;
                DaysToSurvive = 15;
                ZombieAttackProbability = 25;
                break;
        }
    }



    public void ShowInventory()
    {
        Console.WriteLine("Ваш інвентар:");
        Console.WriteLine($"Їжа: {Food}, Вода: {Water}");
        Console.WriteLine("Зброя:");
        if (Inventory.Count == 0)
        {
            Console.WriteLine("У вас немає зброї.");
        }
        else
        {
            foreach (var weapon in Inventory)
            {
                Console.WriteLine($"- {weapon.Name} (Захист: {weapon.DamageReduction}%)");
            }
        }
    }


    public void ZombieAttackWithWeapon()
    {
        int zombieAttackChance = ZombieAttackProbability;

        if (zombieAttackChance > rand.Next(1, 100))
        {
            Console.WriteLine("Зомбі атакують вас!");


            if (Inventory.Count() > 0)
            {
                Console.WriteLine("Ваш інвентар зброї:");
                foreach (var weapon in Inventory)
                {
                    Console.WriteLine($"{weapon.Name} - Захист: {weapon.DamageReduction}%");
                }

                Weapon chosenWeapon = Inventory.OrderByDescending(w => w.DamageReduction).First();
                Console.WriteLine($"Ви використовуєте {chosenWeapon.Name} для відбиття атаки.");

                int blockChance = rand.Next(1, 99);
                Console.WriteLine($"Шанс на відбиття атаки: {chosenWeapon.DamageReduction}%, випадкове число: {blockChance}");

                if (blockChance < chosenWeapon.DamageReduction)
                {
                    Console.WriteLine($"Атака відбита за допомогою {chosenWeapon.Name}!");

                    if (chosenWeapon.TryBreak())
                    {
                        Console.WriteLine($"Ваша зброя {chosenWeapon.Name} поламалася!");
                        Inventory.Remove(chosenWeapon);
                    }
                }
                else
                {
                    int attackDamage = rand.Next(10, 30);
                    Health -= attackDamage;
                    Console.WriteLine($"Атака не відбита! Ви отримали {attackDamage} шкоди. Ваше здоров'я: {Health}%");
                }
            }
            else
            {
                int attackDamage = rand.Next(10, 30);
                Health -= attackDamage;
                Console.WriteLine($"У вас немає зброї! Ви отримали {attackDamage} шкоди. Ваше здоров'я: {Health}%");
            }


            if (Health <= 0)
            {
                Console.WriteLine("Ви померли від нападу зомбі!");
            }
        }
    }



    public void FindFlareGun()
    {
        if (!FlareGunUsed && rand.Next(100) < 3 && !HasFlareGun)
        {
            Inventory.Add(new FlareGun());
            Console.WriteLine("Ви знайшли сигнальний пістолет!");
            HasFlareGun = true;
        }
    }

    public static int DaysAfterUseFlareGun = 0;
    public void UpdateDays()
    {
        if (FlareGunUsed && Health > 0)
        {
            DaysAfterUseFlareGun++;
            if (DaysAfterUseFlareGun >= 4)
            {
                Console.WriteLine("Вас знайшли рятівники, які побачили слід від сигнального пістолета! Ви врятовані!");
                Environment.Exit(0);
            }
        }
        else if (FlareGunUsed && Health <= 0)
        {
            Console.WriteLine("Ви померли в надії на порятунок...");
        }
    }


    public void UseFlareGun()
    {
        if (HasFlareGun)
        {
            Inventory.Remove(Inventory.FirstOrDefault(item => item is FlareGun));
            Console.WriteLine("Ви скористались сигнальним пістолетом, тим самим подали сигнал допомоги!");
            HasFlareGun = false;
            FlareGunUsed = true;
            DaysAfterUseFlareGun = 0;
            Game.daysSurvived++;
        }
    }
    public void HandleInfection()

    {
        if (IsInfected)
        {
            if (IsInfected && Health >= 0)
            {
                Health -= 5;
                Console.WriteLine($"Ви втратили 5 здоров'я через зараження! Ваше здоров'я: {Health}%");


                if (HasAntidote)
                {
                    Console.WriteLine("Ви знайшли антидот у лабораторії і вилікувались від зараження!");
                    IsInfected = false;
                    HasAntidote = false;
                }

                if (Health <= 0)
                {
                    Console.WriteLine("Ви померли від зараження!");

                }
            }
        }
    }
}