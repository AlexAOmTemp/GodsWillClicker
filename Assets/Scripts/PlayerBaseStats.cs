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
        /*BaseDemonParts.Blood = 60;
        BaseDemonParts.Armor = 30;
        BaseDemonParts.Weapons = 20;
        BaseDemonParts.Wings = 10;
        BaseDemonParts.Horns = 5;*/

        _combat.Init(Stats, BaseDemonParts, BaseCounters, BaseEffects);
    }

}
