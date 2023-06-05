using UnityEngine;

public class PlayerBaseStats : MonoBehaviour
{
    [SerializeField] private CombatSystem _combat;
    private CountersPack countersPack = new CountersPack();
    private CountersSet countersSet = new CountersSet();
    private Stats _stats = new Stats();

    public void StartNewRound(int stage)
    {
        _combat.Init(_stats, new CountersPack(countersPack));
    }

    #region Public Methods for Reward delegates
    public void IncreaseDamage (int value)
    {
        _stats.Damage += value;
    }
    public void IncreaseLife(int value)
    {
        _stats.Life += value;
    }
    public void IncreaseEvasonRating(int value)
    {
        _stats.EvasonRating += value;
    }
    public void IncreaseDropRating(int value)
    {
        _stats.DropRating += value;
    }
    public void IncreaseCriticalRating(int value)
    {
        _stats.CriticalRating += value;
    }
    public void IncreaseExtraFillingChance(int value)
    {
        _stats.ExtraFillingChance += value;
    }
    public void IncreaseCriticalDamage(int value)
    {
        _stats.CriticalDamage += value;
    }
    public void IncreaseWeaponDamage(int value)
    {
        _stats.WeaponDamage += value;
    }
    public void IncreaseArmorValue(int value)
    {
        _stats.ArmorValue += value;
    }

    public void IncreaseResource(ItemNames name, int value)
    {
        countersPack.IncreaseResource(name, value);
    }
    public void IncreaseEffectDuration(ItemNames name, int value)
    {
        countersPack.IncreaseAddEffectDurationOnActivation(name, value);
    }
    #endregion
    
    private void Awake()
    {
        _stats.SetDefault();
        _stats.Life = 2000;
        _stats.Damage = 1;
        countersSet.AddEffectDurationOnActivation = 1;
        countersSet.Resource = 30;
        countersPack.SetValue(ItemNames.Punch, countersSet);
        countersPack.SetValue(ItemNames.Armor, countersSet);
        countersPack.SetValue(ItemNames.Sword, countersSet);
        countersPack.SetValue(ItemNames.Wings, countersSet);
        countersPack.SetValue(ItemNames.Nimbus, countersSet);
        countersPack.SetValue(ItemNames.Wrath, countersSet);
    }

}
