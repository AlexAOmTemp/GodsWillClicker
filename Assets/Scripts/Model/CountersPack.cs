using System;
using System.Collections.Generic;

public class CountersPack
{
    private Dictionary<ItemNames, CountersSet> _countersPack = new Dictionary<ItemNames, CountersSet>();

    public CountersPack()
    {
        foreach (ItemNames itemName in Enum.GetValues(typeof(ItemNames)))
        {
            _countersPack.Add(itemName, new CountersSet());
        }
    }
    public CountersPack(CountersPack PreviousPack)
    {
        _countersPack = PreviousPack._countersPack;
    }

    public void SetValue(ItemNames itemName, CountersSet countersSet)
    {
        _countersPack[itemName] = countersSet;
    }
    public CountersSet GetValue(ItemNames itemName)
    {
        CountersSet data = _countersPack[itemName];
        return data;
    }

    public void IncreaseResource(ItemNames name, int value)
    {
        CountersSet set = GetValue(name);
        set.Resource += value;
        SetValue(name, set);
    }
    public void IncreaseAddEffectDurationOnActivation(ItemNames name, int value)
    {
        CountersSet set = GetValue(name);
        set.AddEffectDurationOnActivation += value;
        SetValue(name, set);
    }
}
