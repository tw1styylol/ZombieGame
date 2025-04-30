using System;
using System.Collections.Generic;
using System.Linq;
using ZombieApocalypse;

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

    }
}
