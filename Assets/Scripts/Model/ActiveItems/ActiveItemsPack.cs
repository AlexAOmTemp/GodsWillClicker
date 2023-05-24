using System.Collections.Generic;
using System;
using UnityEngine;
public class ActiveItemsPack : MonoBehaviour
{
    [SerializeField] private Animations _animations;
    [SerializeField] private GuiController _guiController;
    [SerializeField] private Timers _timers;
    [SerializeField] private bool _isPlayer;
    private Sounds _sounds;
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
    public void Init(CombatSystem combatSystem)
    {
        foreach (ItemNames itemName in Enum.GetValues(typeof(ItemNames)))
        {
            ActiveItem activeItem;
            if (itemName == ItemNames.Punch)
                activeItem = createPunchIem(combatSystem);
            else
                activeItem = createBuffItem(_animations.GetBuffAnimation(itemName), _sounds.GetBuffSound(itemName));
           
            activeItem.CountersIsChanged +=
                _guiController.GetGuiItem(_isPlayer, itemName).UpdateGui;
            
            _activeItems.Add(itemName, activeItem);
        }
        setLinksToPrevItems();
        foreach (ItemNames itemName in Enum.GetValues(typeof(ItemNames)))
        {
            if (itemName != ItemNames.Wrath)
                setTimer(itemName);
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
    private void Awake ()
    {
        _sounds = this.GetComponent<Sounds>();
    }
    private void setTimer(ItemNames itemName)
    {
        _activeItems[itemName].ResourceCountIsChanged += _timers.GetTemer(itemName).ResourceCountChanged;
        _timers.SetCountFinishDelegate(itemName, _activeItems[itemName].OnTimerFinished);
    }
    private void setLinksToPrevItems()
    {
        ActiveItem prev = null;
        int i = 0;
        foreach (ActiveItem item in _activeItems.Values)
        {
            if (prev != null)
                prev.Init((BuffItem)item);
            prev = item;
            i++;
        }
    }
    private PunchItem createPunchIem(CombatSystem combatSystem)
    {
        PunchItem PunchItem = new PunchItem();
        PunchItem.SetCombat(combatSystem);
        return PunchItem;
    }
    private BuffItem createBuffItem(BuffAnimation animation, AudioSource sound)
    {
        BuffItem buffItem = new BuffItem();
        buffItem.AnimationIsPlaying +=
            animation.ActivateBuff;
        buffItem.SetSound(sound);
        return buffItem;
    }
}
