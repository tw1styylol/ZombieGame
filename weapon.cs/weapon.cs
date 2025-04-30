using System;
using System.Collections.Generic;
using System.Linq;

class Weapon
{
    public string Name { get; protected set; }
    public int DamageReduction { get; protected set; }

    public bool TryBreak()
    {
        Random rand = new Random();
        return rand.Next(100) < 50;
    }
}

class MeleeWeapon : Weapon
{
    public MeleeWeapon()
    {
        Name = "Ніж";
        DamageReduction = 5;
    }
}

class Pistol : Weapon
{
    public Pistol()
    {
        Nama = "Пістолет";
        DamageReduction = 10;
    }
}

class Rifle : Weapon
{
    public Rifle()
    {
        Name = "Гвинтівка";
        DamageReduction = 25;
    }
}

class Shotgun : Weapon
{
    public Shotgun()
    {
        Name = "Дробовик";
        DamageReduction = 35;
    }
}


class FlareGun : Weapon
{
    public FlareGun()
    {
        Name = "Сигнальний пістолет";
        DamageReduction = 0;
    }
}