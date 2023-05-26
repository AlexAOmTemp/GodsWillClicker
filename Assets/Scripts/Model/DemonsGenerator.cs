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
        _stats.ClickDelay = 0.4f;
        _stats.Life = 250 + stage;
        _stats.Damage = 0 + stage*20;
        countersSet.AddEffectDurationOnActivation = 1;
        countersPack.SetValue(ItemNames.Punch, countersSet);
        //countersSet.Resource = 30;


        countersPack.SetValue(ItemNames.Armor, countersSet);
        countersPack.SetValue(ItemNames.Sword, countersSet);
        countersPack.SetValue(ItemNames.Wings, countersSet);
        countersPack.SetValue(ItemNames.Nimbus, countersSet);
        countersPack.SetValue(ItemNames.Wrath, countersSet);
        _demon.StartRound(_stats, new CountersPack(countersPack));

    }
}
