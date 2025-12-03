using System;
using UnityEngine;

[Serializable]

public class StatOffenseGroup
{
    public Stat attackSpeed;

    // physical damage
    public Stat damage;
    public Stat critPower;
    public Stat critChance;
    public Stat armorReduction;

    // elemental damage
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat lightningDamage;
}
