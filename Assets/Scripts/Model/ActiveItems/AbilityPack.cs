using System.Collections.Generic;
using System;
using UnityEngine;
public class AbilityPack : MonoBehaviour
{
    [SerializeField] private Animations _animations;
    [SerializeField] private GuiController _guiController;
    [SerializeField] private Timers _timers;
    [SerializeField] private bool _isPlayer;
    private Sounds _sounds;
    private Dictionary<AbilityNames, Ability> _abilities = new Dictionary<AbilityNames, Ability>();

    public Ability GetAbility(AbilityNames name)
    {
        return _abilities[name];
    }
    public void SetCountersData(CountersPack counters)
    {
        foreach (AbilityNames abilityName in Enum.GetValues(typeof(AbilityNames)))
            _abilities[abilityName].SetCounters(counters.GetValue(abilityName));
    }
    public void Init(CombatSystem combatSystem)
    {
        foreach (AbilityNames abilityName in Enum.GetValues(typeof(AbilityNames)))
        {
            Ability ability;
            if (abilityName == AbilityNames.Punch)
                ability = createPunchAbility(combatSystem);
            else 
                ability = createBuff(_animations.GetBuffAnimation(abilityName), _sounds.GetBuffSound(abilityName));

            ability.CountersIsChanged +=
                _guiController.GetGuiItem(_isPlayer, abilityName).UpdateGui;

            _abilities.Add(abilityName, ability);
        }
        setLinksToPrevItems();
        foreach (AbilityNames abilityName in Enum.GetValues(typeof(AbilityNames)))
        {
            if (abilityName != AbilityNames.Wrath)
                setTimer(abilityName);
        }
    }
    public void BuffChargeUsed(AbilityNames abilityName)
    {
        AbilityBuff item = (AbilityBuff)_abilities[abilityName];
        item.BuffChargeUsed();
    }
    public bool IsActive(AbilityNames abilityName)
    {
        AbilityBuff item = (AbilityBuff)_abilities[abilityName];
        return item.IsActive();
    }
    public bool IsAwailible(AbilityNames abilityName)
    {
        AbilityBuff item = (AbilityBuff)_abilities[abilityName];
        return item.IsAwailible();
    }
    private void Awake()
    {
        _sounds = this.GetComponent<Sounds>();
    }
    private void setTimer(AbilityNames abilityName)
    {
        _abilities[abilityName].ResourceCountIsChanged += _timers.GetTemer(abilityName).ResourceCountChanged;
        _timers.SetCountFinishDelegate(abilityName, _abilities[abilityName].OnTimerFinished);
    }
    private void setLinksToPrevItems()
    {
        Ability prev = null;
        int i = 0;
        foreach (Ability item in _abilities.Values)
        {
            if (prev != null)
                prev.Init((AbilityBuff)item);
            prev = item;
            i++;
        }
    }
    private AbilityPunch createPunchAbility(CombatSystem combatSystem)
    {
        AbilityPunch punch = new AbilityPunch();
        punch.SetCombat(combatSystem);
        return punch;
    }
    private AbilityBuff createBuff(BuffAnimation animation, AudioSource sound)
    {
        AbilityBuff buff = new AbilityBuff();
        buff.AnimationIsPlaying +=
            animation.ActivateBuff;
        buff.SetSound(sound);
        return buff;
    }
}
