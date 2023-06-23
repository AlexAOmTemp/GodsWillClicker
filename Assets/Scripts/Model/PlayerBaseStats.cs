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
        _stats.Damage += 2;
    }
    public void IncreaseLife(int value)
    {
        _stats.Life += 10;
    }
    public void IncreaseWeaponDamage(int value)
    {
        _stats.WeaponDamage += 2;
    }
    public void IncreaseArmorValue(int value)
    {
        _stats.ArmorValue += value;
    }
    public void IncreaseResource(AbilityNames name, int value)
    {
        countersPack.IncreaseResource(name, value);
    }
    public void IncreaseEffectDuration(AbilityNames name, int value)
    {
        countersPack.IncreaseAddEffectDurationOnActivation(name, value);
    }
    #endregion
    
    private void Awake()
    {
        _stats.SetDefault();
        _stats.Life = 1000;
        _stats.Damage = 30;
        countersSet.AddEffectDurationOnActivation = 1;
        countersSet.Resource = 20;
        countersPack.SetValue(AbilityNames.Punch, countersSet);
        countersPack.SetValue(AbilityNames.Armor, countersSet);
        countersPack.SetValue(AbilityNames.Sword, countersSet);
        countersPack.SetValue(AbilityNames.Wings, countersSet);
        countersPack.SetValue(AbilityNames.Nimbus, countersSet);
        countersPack.SetValue(AbilityNames.Wrath, countersSet);
    }

}
