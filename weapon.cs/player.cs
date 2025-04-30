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
}