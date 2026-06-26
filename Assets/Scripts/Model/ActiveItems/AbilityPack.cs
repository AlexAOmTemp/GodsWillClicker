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
                ability = CreatePunchAbility(combatSystem);
            else
                ability = CreateBuff(_animations.GetBuffAnimation(abilityName), _sounds.GetBuffSound(abilityName));

            ability.CountersIsChanged +=
                _guiController.GetGuiItem(_isPlayer, abilityName).UpdateGui;

            _abilities.Add(abilityName, ability);
        }

        SetLinksToPrevItems();
        foreach (AbilityNames abilityName in Enum.GetValues(typeof(AbilityNames)))
        {
            if (abilityName != AbilityNames.Wrath)
                SetTimer(abilityName);
        }
    }

    public void BuffChargeUsed(AbilityNames abilityName)
    {
        var item = (AbilityBuff) _abilities[abilityName];
        item.BuffChargeUsed();
    }

    public bool IsActive(AbilityNames abilityName)
    {
        var item = (AbilityBuff) _abilities[abilityName];
        return item.IsActive();
    }

    public bool IsAvailable(AbilityNames abilityName)
    {
        var item = (AbilityBuff) _abilities[abilityName];
        return item.IsAwailible();
    }

    private void Awake()
    {
        _sounds = this.GetComponent<Sounds>();
    }

    private void SetTimer(AbilityNames abilityName)
    {
        _abilities[abilityName].ResourceCountIsChanged += _timers.GetTemer(abilityName).ResourceCountChanged;
        _timers.SetCountFinishDelegate(abilityName, _abilities[abilityName].OnTimerFinished);
    }

    private void SetLinksToPrevItems()
    {
        Ability prev = null;
        var i = 0;
        foreach (Ability item in _abilities.Values)
        {
            prev?.Init((AbilityBuff) item);
            prev = item;
            i++;
        }
    }

    private AbilityPunch CreatePunchAbility(CombatSystem combatSystem)
    {
        var punch = new AbilityPunch();
        punch.SetCombat(combatSystem);
        return punch;
    }

    private AbilityBuff CreateBuff(BuffAnimation animation, AudioSource sound)
    {
        var buff = new AbilityBuff();
        buff.AnimationIsPlaying += animation.ActivateBuff;
        buff.SetSound(sound);
        return buff;
    }
}