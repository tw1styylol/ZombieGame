using System;
using System.Collections.Generic;
using System.Linq;

class Game
{
    public static int daysSurvived = 0;

    static void Main()
    {
        Random rand = new Random();
        Console.WriteLine("Оберіть рівень складності: 1. Легкий, 2. Середній, 3. Важкий");
        string difficultyChoice = Console.ReadLine();
        string difficulty = difficultyChoice switch
        {
            "1" => "Легкий",
            "2" => "Середній",
            "3" => "Важкий",
            _ => "Середній"
        };

        Console.WriteLine("Оберіть режим гри: 1. Виживання, 2. Нескінченний");
        string modeChoice = Console.ReadLine();
        bool endlessMode = modeChoice == "2";


        Console.WriteLine("Оберіть роль: 1. Медик, 2. Солдат, 3. Виживший, 4. Фермер");
        string choice = Console.ReadLine();
        string role = choice switch
        {
            "1" => "Медик",
            "2" => "Солдат",
            "3" => "Виживший",
            "4" => "Фермер",
            _ => "Виживший"
        };

        Player player = new Player(role, difficulty);
        Console.WriteLine($"Ви обрали роль: {player.Role}");

        List<Location> locations = new List<Location>
        {
            new Location("Магазин", "Міська", true),
            new Location("Лікарня", "Міська", true),
            new Location("Ліс", "Природна", false),
            new Location("Ферма", "Природна", false),
            new Location("Лабораторія", "Спеціальна", true, true),
            new Location("Радіостанція", "Спеціальна", false, true),
            new Location("Завод", "Спеціальна", true, true),
            new Location("Тюрма", "Спеціальна", true, true),
            new Location("Військова база", "Спеціальна", true, true)
        };

        int daysWithoutFood = 0;
        int daysWithoutWater = 0;

        while (player.Health > 0 && (endlessMode || daysSurvived < player.DaysToSurvive) || (player.Food > 0 || player.Water == 0))
        {
            Console.WriteLine($"\nДень {daysSurvived + 1}. Ваше здоров'я: {player.Health}%");
            Console.WriteLine("Оберіть дію:");
            Console.WriteLine("1. Переглянути інвентар");
            Console.WriteLine("2. Дослідити локацію");
            Console.WriteLine("3. Проспати ніч");

            if (player.HasFlareGun)
            {
                Console.WriteLine("4. Використати сигнальний пістолет");
            }

            string actionChoice = Console.ReadLine();

            if (actionChoice == "1")
            {
                player.ShowInventory();
            }
            else if (actionChoice == "2")
            {
                Console.WriteLine("Оберіть локацію для дослідження:");
                for (int i = 0; i < locations.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {locations[i].Name} ({locations[i].Type})");
                }

                bool locationExplored = false;

                if (int.TryParse(Console.ReadLine(), out int locationChoice) && locationChoice >= 1 && locationChoice <= locations.Count)
                {
                    var selectedLocation = locations[locationChoice - 1];

                    if (selectedLocation.RequiresMap && !player.HasMap)
                    {
                        Console.WriteLine("Ця локація недоступна без карти! Оберіть іншу.");
                    }
                    else
                    {
                        selectedLocation.Explore(player);
                        locationExplored = true;
                        if (player.Food > 0) player.Food--;
                        if (player.Water > 0) player.Water--;

                        if (player.Food == 0) daysWithoutFood++;
                        if (player.Water == 0) daysWithoutWater++;

                        if (!player.HasLock && rand.Next(100) < 15)
                        {
                            player.HasLock = true;
                            Console.WriteLine("Ви знайшли замок для дверей бункера!");
                        }

                        if (!player.HasMap && rand.Next(100) < 15)
                        {
                            Console.WriteLine("Ви знайшли карту, яка відкриває доступ до особливих локацій!");
                            player.HasMap = true;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Невірний вибір.");
                }

                if (locationExplored)
                {
                    player.HandleInfection();
                    daysSurvived++;
                    player.UpdateDays();
                    player.FindFlareGun();

                    if (player.EvacuationPointUnlocked)
                    {
                        bool alreadyExists = locations.Exists(loc => loc.Name == "Пункт евакуації");
                        if (!alreadyExists)
                        {
                            Console.WriteLine("Нова локація: Пункт евакуації (Спеціальна) тепер доступна!");
                            locations.Add(new Location("Пункт евакуації", "Спеціальна", false, true));
                        }
                    }
                }
            }
            else if (actionChoice == "3")
            {
                if (player.HasLock && !player.LockedShelter)
                {
                    Console.WriteLine("Ви хочете повісити замок на двері? (y/n)");
                    string lockChoice = Console.ReadLine();
                    if (lockChoice.ToLower() == "y")
                    {
                        player.LockedShelter = true;
                        Console.WriteLine("Ви повісили замок на двері бункера.");
                        Console.WriteLine("Замок ламається...");
                        player.LockedShelter = false;
                        player.HasLock = false;
                    }
                }

                if (!player.LockedShelter)
                {
                    int attackChance = difficulty switch
                    {
                        "Легкий" => 10,
                        "Середній" => 25,
                        "Важкий" => 45,
                        _ => 25
                    };

                    if (rand.Next(100) < attackChance && !player.LockedShelter)
                    {
                        Console.WriteLine("Вас атакували зомбі під час сну!");
                        player.Health -= rand.Next(10, 30);
                        Console.WriteLine($"Ви отримали поранення, ваше здоров'я: {player.Health}%");
                    }
                }

                Player.SleepInShelter(player, daysSurvived);
                player.HandleInfection();
                daysSurvived++;
                player.UpdateDays();
                if (player.Food > 0) player.Food--;
                if (player.Water > 0) player.Water--;

                if (player.Food == 0) daysWithoutFood++;
                if (player.Water == 0) daysWithoutWater++;

                if (daysWithoutFood > 3 && daysWithoutWater > 3)
                {
                    Console.WriteLine("Ви не маєте їжі та води більше ніж 2 дні!");
                    break;
                }
            }
            else if (actionChoice == "4")
            {
                player.UseFlareGun();
            }
            else
            {
                Console.WriteLine("Невірний вибір.");
            }

            if (player.Health <= 0)
            {
                break;
            }
        }
        Console.WriteLine("Гра закінчена.");
    }
}