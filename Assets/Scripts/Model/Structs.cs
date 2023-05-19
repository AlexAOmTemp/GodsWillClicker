public enum ItemNames
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
        calculateStats();
    }
}