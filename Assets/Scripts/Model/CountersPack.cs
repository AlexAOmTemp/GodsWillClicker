using System;
using System.Collections.Generic;

public class CountersPack
{
    private Dictionary<AbilityNames, CountersSet> _countersPack = new Dictionary<AbilityNames, CountersSet>();

    public CountersPack()
    {
        foreach (AbilityNames abilityName in Enum.GetValues(typeof(AbilityNames)))
        {
            _countersPack.Add(abilityName, new CountersSet());
        }
    }
    public CountersPack(CountersPack PreviousPack)
    {
        _countersPack = PreviousPack._countersPack;
    }

    public void SetValue(AbilityNames abilityName, CountersSet countersSet)
    {
        _countersPack[abilityName] = countersSet;
    }
    public CountersSet GetValue(AbilityNames abilityName)
    {
        CountersSet data = _countersPack[abilityName];
        return data;
    }

    public void IncreaseResource(AbilityNames name, int value)
    {
        CountersSet set = GetValue(name);
        set.Resource += value;
        SetValue(name, set);
    }
    public void IncreaseAddEffectDurationOnActivation(AbilityNames name, int value)
    {
        CountersSet set = GetValue(name);
        set.AddEffectDurationOnActivation += value;
        SetValue(name, set);
    }
}
