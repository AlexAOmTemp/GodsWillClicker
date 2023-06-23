using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonsGenerator : MonoBehaviour
{
    [SerializeField] private Demon _demon;
    private Stats _stats = new Stats();
    private CountersPack countersPack = new CountersPack();
    private CountersSet countersSet = new CountersSet();
    public void GenerateDemon(int stage)
    {
        _stats.SetDefault();
        _stats.ClickDelay = 0.5f;
        _stats.Life = 1000 + stage * 5;
        _stats.Damage = 30 + stage / 2;
        countersSet.AddEffectDurationOnActivation = 1 + stage / 50;
        countersSet.Resource = 20 + stage / 15;
        countersPack.SetValue(AbilityNames.Punch, countersSet);
        countersPack.SetValue(AbilityNames.Armor, countersSet);
        countersPack.SetValue(AbilityNames.Sword, countersSet);
        countersPack.SetValue(AbilityNames.Wings, countersSet);
        countersPack.SetValue(AbilityNames.Nimbus, countersSet);
        countersPack.SetValue(AbilityNames.Wrath, countersSet);
        _demon.StartRound(_stats, new CountersPack(countersPack));

    }
}
