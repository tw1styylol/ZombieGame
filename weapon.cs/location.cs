using System;
using System.Security.Cryptography;
class Location
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

    }
}