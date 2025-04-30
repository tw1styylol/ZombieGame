using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class Player
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
}