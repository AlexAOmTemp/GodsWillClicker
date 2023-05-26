using UnityEngine;

public class PlayerBaseStats : MonoBehaviour
{
    [SerializeField] private CombatSystem _combat;
    private CountersPack countersPack = new CountersPack();
    private CountersSet countersSet = new CountersSet();
    private Stats _stats = new Stats();
    public void PlayerInit()
    {
        _stats.SetDefault();
        _stats.Damage = 50;
        countersSet.AddEffectDurationOnActivation = 1;

        countersPack.SetValue(ItemNames.Punch, countersSet);
        //countersSet.Resource = 30;
        countersPack.SetValue(ItemNames.Armor, countersSet);
        countersPack.SetValue(ItemNames.Sword, countersSet);
        countersPack.SetValue(ItemNames.Wings, countersSet);
        countersPack.SetValue(ItemNames.Nimbus, countersSet);
        countersPack.SetValue(ItemNames.Wrath, countersSet);
        _combat.Init(_stats, new CountersPack(countersPack));
    }


}
