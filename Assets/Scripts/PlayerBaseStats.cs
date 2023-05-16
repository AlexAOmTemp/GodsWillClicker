using UnityEngine;

public class PlayerBaseStats : MonoBehaviour
{
    [SerializeField] private CombatSystem _combat;
    public DemonParts BaseDemonParts = new DemonParts();
    public Counters BaseCounters = new Counters();
    public ActiveEffects BaseEffects = new ActiveEffects();
    public Stats Stats = new Stats();
    public void PlayerInit()
    {
        Stats.SetDefault();
        BaseDemonParts.Blood = 5;
        BaseDemonParts.Armor = 5;
        BaseDemonParts.Weapons = 5;
        BaseDemonParts.Wings = 5;
        BaseDemonParts.Horns = 5;

        _combat.Init(Stats, BaseDemonParts, BaseCounters, BaseEffects);
    }

}
