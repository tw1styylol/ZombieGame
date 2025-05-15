using System;
using System.Security.Cryptography;
public class Location
{
    public string Name { get; private set; }
    public string Type { get; private set; }
    public bool HasZombies { get; private set; }
    public bool RequiresMap { get; private set; }

    private static Random rand = new Random();

    public Location(string name, string type, bool hasZombies, bool requiresMap = false)
    {
        Name = name;
        Type = type;
        HasZombies = hasZombies;
        RequiresMap = requiresMap;
    }

    public void Explore(Player player)
    {

        if (RequiresMap && player.HasMap == false)
        {
            Console.WriteLine("Ця локація недоступна без карти! Оберіть іншу.");
            return;
        }

        Console.WriteLine($"Ви досліджуєте локацію {Name}...");


        if (Name == "Лабораторія" && player.IsInfected && rand.Next(100) < 20)
        {
            player.HasAntidote = true;
            Console.WriteLine("Ви знайшли антидот!");

        }

        if (Name == "Ліс" && player.HasCable == false && rand.Next(100) < 10)
        {
            Console.WriteLine("Ви знайшли кабель!");
            player.HasCable = true;
        }
        if (Name == "Лабораторія" && !player.HasRelay && rand.Next(100) < 10)
        {
            Console.WriteLine("Ви знайшли реле!");
            player.HasRelay = true;
        }
        if (Name == "Завод" && !player.HasChipPlate && rand.Next(100) < 10)
        {
            Console.WriteLine("Ви знайшли плату!");
            player.HasChipPlate = true;
        }
        if (Name == "Магазин" && !player.HasCapacitor && rand.Next(100) < 10)
        {
            Console.WriteLine("Ви знайшли конденсатор!");
            player.HasCapacitor = true;
        }
        if (Name == "Лікарня" && !player.HasResistor && rand.Next(100) < 10)
        {
            Console.WriteLine("Ви знайшли резистор!");
            player.HasResistor = true;
        }
        if (Name == "Ферма" && !player.HasScrew && rand.Next(100) < 10)
        {
            Console.WriteLine("Ви знайшли гвинтик!");
            player.HasScrew = true;
        }
        if (Name == "Військова база" && !player.HasSpeeker && rand.Next(100) < 10)
        {
            Console.WriteLine("Ви знайли динамік!");
            player.HasSpeeker = true;
        }
        if (Name == "Тюрма" && !player.HasTransistor && rand.Next(100) < 10)
        {
            Console.WriteLine("Ви знайли транзистор!");
            player.HasTransistor = true;
        }



        if (Name == "Магазин" && rand.Next(100) < 45)
        {
            int foodAmount = rand.Next(1, 3);
            int waterAmount = rand.Next(1, 2);
            foodAmount += 1;
            waterAmount += 1;
            player.Food += foodAmount;
            player.Water += waterAmount;
            Console.WriteLine($"Ви знайшли {foodAmount} їжі та {waterAmount} води в магазині!");
        }



        if (Name == "Ферма")
        {
            int baseChance = 45;
            int foodAmount = rand.Next(1, 3);
            int waterAmount = rand.Next(1, 2);
            if (player.Role == "Фермер")
            {
                baseChance += 15;
                foodAmount += 1;
                waterAmount += 1;
            }

            if (rand.Next(100) < baseChance)
            {
                player.Food += foodAmount;
                player.Water += waterAmount;
                Console.WriteLine($"Ви знайшли {foodAmount} їжі та {waterAmount} води на фермі!");
                Console.WriteLine($"Запаси їжі: {player.Food}, запаси води: {player.Water}");
            }

        }


        if (Name == "Лікарня" && player.Health < 100)
        {
            int baseChance = 15;
            if (player.Role == "Медик")
            {
                baseChance = 50;
            }

            if (rand.Next(100) < baseChance)
            {
                int healthRecovered = rand.Next(10, 30);
                int previousHealth = player.Health;

                player.Health = Math.Min(100, player.Health + healthRecovered);
                int actualRecovered = player.Health - previousHealth;

                Console.WriteLine($"Ви знайшли ліки та відновили {actualRecovered}% здоров'я!");
                Console.WriteLine($"Ваше здоров'я: {player.Health}%");
            }

        }



        if (Name == "Радіостанція" && !player.EvacuationPointUnlocked &&
            (!player.HasCable || !player.HasRelay ||
              !player.HasResistor || !player.HasCapacitor || !player.HasChipPlate
            || !player.HasScrew || !player.HasSpeeker || !player.HasTransistor))
        {
            Console.WriteLine("У вас недостатньо деталей, щоб запустити радіостанцію...");
        }
        else if (Name == "Радіостанція" && player.HasCable && player.HasRelay
            && player.HasResistor && player.HasCapacitor && player.HasChipPlate
            && player.HasScrew && player.HasSpeeker && player.HasTransistor)
        {
            Console.WriteLine("Ви відремонтували радіостанцію!");
            player.RadiostationHasRepaired = true;
        }

        if (Name == "Радіостанція" && !player.EvacuationPointUnlocked && rand.Next(100) < 8 && player.RadiostationHasRepaired)
        {
            Console.WriteLine("Ви з надією перемикаєте частоти, у пошуку хочаб чогось...");
            Console.WriteLine("Ви отримали сигнал про евакуацію! Пункт евакуації тепер доступний!");
            player.EvacuationPointUnlocked = true;

        }
        else if (Name == "Радіостанція" && !player.EvacuationPointUnlocked && player.RadiostationHasRepaired)
        {
            Console.WriteLine("Ви з надією перемикаєте частоти у пошуку хочаб чогось...");
            Console.WriteLine("Радіостанція мовчить. Цей шум лякає Вас більше, ніж смерть...");
        }

        if (Name == "Пункт евакуації" && player.Food >= 5 && player.Water >= 5)
        {
            Console.WriteLine("Ви з надією вирушили на координати пункту евакуації...");
            Console.WriteLine($"Ви дісталися до пункту евакуації на {Game.daysSurvived += 3}-ий день! Ви врятовані!");
            Environment.Exit(0);
        }

        if (Name == "Пункт евакуації" && player.Food < 5 && player.Water < 5)
        {
            Console.WriteLine("Ви з надією вирушили на координати пункту евакуації...");
            Console.WriteLine("Вам не вистачило провіанту, щоб дістатись до пункту евакуації. Ви померли важкою смерттю...");
            Environment.Exit(0);
        }

        if (Name == "Тюрма" && rand.Next(100) < 10)
        {
            List<Weapon> weapons = new List<Weapon>
            {
                new Pistol(),
                new Shotgun(),
            };

            Weapon foundWeapon = weapons[rand.Next(weapons.Count)];
            if (player.Inventory.Any(w => w.Name == foundWeapon.Name))
            {
                Console.WriteLine($"Ви знайшли {foundWeapon.Name}, проте він у вас вже є!");
            }
            else
            {
                player.Inventory.Add(foundWeapon);
                Console.WriteLine($"Ви знайшли {foundWeapon.Name} в тюрмі!");
            }

        }



        if (Name == "Військова база" && rand.Next(100) < 10)
        {
            List<Weapon> weapons = new List<Weapon>
            {
                new MeleeWeapon(),
                new Pistol(),
                new Rifle(),
                new Shotgun(),
                new FlareGun()
            };

            Weapon foundWeapon = weapons[rand.Next(weapons.Count)];

            if (foundWeapon is FlareGun)
            {
                if (player.HasFlareGun)
                {
                    Console.WriteLine("Ви знайшли сигнальний пістолет, але у вас він вже є!");
                }
                else
                {
                    player.Inventory.Add(foundWeapon);
                    player.HasFlareGun = true;
                    Console.WriteLine("Ви знайшли сигнальний пістолет на військовій базі!");
                }
            }
            else
            {
                if (player.Inventory.Any(w => w.Name == foundWeapon.Name))
                {
                    Console.WriteLine($"Ви знайшли {foundWeapon.Name}, проте він у вас вже є!");
                }
                else
                {
                    player.Inventory.Add(foundWeapon);
                    Console.WriteLine($"Ви знайшли {foundWeapon.Name} на військовій базі!");
                }
            }

        }


        if (HasZombies && rand.Next(100) < player.ZombieAttackProbability)
        {
            Console.WriteLine("Зомбі атакують вас!");
            if (player.Inventory.Count() > 0)
            {
                Console.WriteLine("Ваш інвентар зброї:");
                foreach (var weapon in player.Inventory)
                {
                    Console.WriteLine($"{weapon.Name} - Захист: {weapon.DamageReduction}%");
                }

                Weapon chosenWeapon = player.Inventory.OrderByDescending(w => w.DamageReduction).First();
                Console.WriteLine($"Ви використовуєте {chosenWeapon.Name} для відбиття атаки.");
                int blockChance = rand.Next(1, 99);
                Console.WriteLine($"Шанс на відбиття атаки: {chosenWeapon.DamageReduction}%");

                if (blockChance < chosenWeapon.DamageReduction)
                {
                    Console.WriteLine($"Атака відбита за допомогою {chosenWeapon.Name}!");
                    if (chosenWeapon.TryBreak())
                    {
                        Console.WriteLine($"Ваша зброя {chosenWeapon.Name} поламалася!");
                        player.Inventory.Remove(chosenWeapon);
                    }
                }
                else
                {
                    int attackDamage = rand.Next(10, 30);
                    player.Health -= attackDamage;
                    Console.WriteLine($"Атака не відбита! Ви отримали {attackDamage} шкоди. Ваше здоров'я: {player.Health}%");
                    if (rand.Next(100) < player.InfectionChance)
                    {
                        Console.WriteLine("Ви заразилися після атаки зомбі!");
                        player.IsInfected = true;
                    }
                }
            }
            else
            {
                int attackDamage = rand.Next(10, 30);
                player.Health -= attackDamage;
                Console.WriteLine($"У вас немає зброї! Ви отримали {attackDamage} шкоди. Ваше здоров'я: {player.Health}%");
                if (rand.Next(100) < player.InfectionChance)
                {
                    Console.WriteLine("Ви заразилися після атаки зомбі!");
                    player.IsInfected = true;
                }
            }

            if (player.Health <= 0)
            {
                Console.WriteLine("Ви померли від нападу зомбі!");
            }
        }
        else
        {
            Console.WriteLine("Локація безпечна.");
            if (rand.Next(100) < player.FoodChance)
            {
                player.Food += 1;
                Console.WriteLine("По дорозі до дому Ви знайшли їжу !");
            }
            if (rand.Next(100) < player.WaterChance)
            {
                player.Water += 1;
                Console.WriteLine("По дорозі до дому Ви знайшли воду!");
            }
        }
    }

}