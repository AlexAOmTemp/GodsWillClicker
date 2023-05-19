using System.Collections.Generic;
using System;
using UnityEngine;
public class ActiveItemsPack
{
    private Dictionary<ItemNames, ActiveItem> _activeItems = new Dictionary<ItemNames, ActiveItem>();
    public ActiveItem GetActiveItem(ItemNames name)
    {
        return _activeItems[name];
    }
    public void SetCountersData(CountersPack counters)
    {
        foreach (ItemNames itemName in Enum.GetValues(typeof(ItemNames)))
            _activeItems[itemName].SetCounters(counters.GetValue(itemName));
    }
    public ActiveItemsPack(bool isPlayer, CombatSystem combatSystem, Animations animations,
        Sounds sounds, GuiController guiController, ResourceTimer[] timer)
    {
        foreach (ItemNames itemName in Enum.GetValues(typeof(ItemNames)))
        {
            ActiveItem activeItem;
            if (itemName == ItemNames.Punch)
            {
                PunchItem PunchItem = new PunchItem();
                PunchItem.SetCombat(combatSystem);
                activeItem = PunchItem;
            }
            else
            {
                BuffItem buffItem = new BuffItem();
                buffItem.AnimationIsPlaying +=
                    animations.GetBuffAnimation(itemName).ActivateBuff;
                buffItem.SetSound(sounds.GetBuffSound(itemName));
                activeItem = buffItem;
            }
            activeItem.CountersIsChanged +=
                guiController.GetGuiItem(isPlayer, itemName).UpdateGui;
            _activeItems.Add(itemName, activeItem);
        }
        ActiveItem prev = null;
        int i=0;
        foreach (ActiveItem item in _activeItems.Values)
        {
            if (prev != null)
                prev.Init(timer[i-1],(BuffItem)item);
            prev = item;
            i++;
        }
    }
    public void BuffChargeUsed(ItemNames itemName)
    {
        BuffItem item = (BuffItem)_activeItems[itemName];
        item.BuffChargeUsed();
    }
    public bool IsActive(ItemNames itemName)
    {
        BuffItem item = (BuffItem)_activeItems[itemName];
        return item.IsActive();
    }
    public bool IsAwailible(ItemNames itemName)
    {
        BuffItem item = (BuffItem)_activeItems[itemName];
        return item.IsAwailible();
    }
}
