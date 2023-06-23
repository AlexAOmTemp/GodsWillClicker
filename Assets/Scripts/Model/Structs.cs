public enum AbilityNames
{
    Punch = 0,
    Armor = 1,
    Sword = 2,
    Wings = 3,
    Nimbus = 4,
    Wrath = 5
}
public struct CountersSet
{
    public int Resource;
    public int ButtonAwailability; 
    public int CurrentEffectDuration; // how many hits effect will work
    public int AddEffectDurationOnActivation;  
    public int Clicks;
}
public struct Stats
{
    public int Damage;
    public int Life;
    public float ClickDelay; // for enemy only
    public int ExtraFillingChance;
    public int CriticalDamage;
    public int WeaponDamage;
    public int ArmorValue;
    public void SetDefault()
    {
        Damage = 10;
        Life = 100;
        ClickDelay = 0.4f;
        CriticalDamage = 200;
        WeaponDamage = 10;
        ArmorValue = 10;
    }
}
