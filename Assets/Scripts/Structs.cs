public enum Parts
{
    Blood,
    Armor,
    Weapons,
    Wings,
    Horns
}
public struct DemonParts
{
    public int Blood;
    public int Armor;
    public int Weapons;
    public int Wings;
    public int Horns;
}
public struct Counters
{
    public int Punch;
    public int Armor;
    public int Wings;
    public int Swords;
    public int Nimbus;
    public int Wrath;
}
public struct ActiveEffects
{
    public int ArmorLayers;
    public int WingsHits;
    public int WeaponHits;
    public int NimbusHits;
    public int WrathHits;
}
public struct Stats
{
    public int Damage;
    public int Life;
    public float ClickDelay; // for enemy only
    public int EvasonRating; //rating should be recalculated to stat by using RatingToStat
    public int DropRating;
    public int CriticalRating;
    public float EvasonChance { get; private set; }
    public float DropChance { get; private set; }
    public float CriticalChance { get; private set; }
    public int ExtraFillingChance;
    public int CriticalDamage;
    public int WeaponDamage;
    public int ArmorValue;
    public int ArmorApplied;
    public int WeaponApplied;
    public int WingApplied;
    public int NimbusApplied;
    public int WrathApplied;
    private float ratingToStat(int rating)
    {
        //100 = 50%, 300 = 75%
        if (rating <= 0)
            return 0;
        float Value = (1f - 100f / (100f + (float)rating)) * 100f;
        return Value;
    }
    public void calculateStats()
    {
        EvasonChance = ratingToStat(EvasonRating);
        CriticalChance = ratingToStat(CriticalRating);
        DropChance = ratingToStat(DropRating);
    }
    public void SetDefault()
    {
        Damage = 10;
        Life = 250;
        ClickDelay = 0.4f;
        EvasonRating = 5;
        DropRating = 10;
        CriticalRating = 5;
        CriticalDamage = 200;
        WeaponDamage = 10;
        ArmorValue = 10;
        ArmorApplied = 1;
        WeaponApplied = 1;
        WingApplied = 1;
        NimbusApplied = 1;
        WrathApplied = 1;
        calculateStats();
    }
}
public enum Buffs
{
    Punch = 0,
    Armor = 1,
    Sword = 2,
    Wings = 3,
    Nimbus = 4,
    Wrath = 5
}

