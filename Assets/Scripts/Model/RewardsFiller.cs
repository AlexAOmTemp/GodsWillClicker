using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class RewardsFiller : MonoBehaviour
{
    [SerializeField] private Sprite _testImage;
    [SerializeField] private PlayerBaseStats _stats;

    private List<RewardData> _rewards = new List<RewardData>();
    private bool _isInitialized = false;

    public RewardData[] GetRewards(int stage)
    {
        if (_isInitialized == false)
            Awake();
        System.Random rnd = new System.Random();
        var rewards = _rewards.Take(2);
        rewards = rewards.Concat(_rewards.Skip(2).OrderBy(x => rnd.Next()).Take(1));
        return rewards.ToArray();
    }

    private void Awake()
    {
        if (_isInitialized)
            return;

        Sprite image = _testImage;
        
        _rewards.Add(new RewardData(image, "Increase Damage", _stats.IncreaseDamage));
        _rewards.Add(new RewardData(image, "Increase Life", _stats.IncreaseLife));
        _rewards.Add(new RewardData(image, "Increase Sword Damage", _stats.IncreaseWeaponDamage));
        _rewards.Add(new RewardData(image, "Increase Armor Value", _stats.IncreaseArmorValue));

        foreach (AbilityNames name in Enum.GetValues(typeof(AbilityNames)))
        {
            var description = "Increase " + name.ToString() + " Resourse";
            _rewards.Add(new RewardData(image, description, _stats.IncreaseResource, name));
            description = "Increase " + name.ToString() + " Effect";
            _rewards.Add(new RewardData(image, description, _stats.IncreaseEffectDuration, name));
        }

        _isInitialized = true;
    }
}