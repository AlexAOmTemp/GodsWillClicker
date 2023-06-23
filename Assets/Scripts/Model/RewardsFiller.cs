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
        if (_isInitialized == false)
        {
            Sprite image = _testImage;
            string description = "Increase Damage";
            _rewards.Add(new RewardData(image, description, _stats.IncreaseDamage));
            description = "Increase Life";
            _rewards.Add(new RewardData(image, description, _stats.IncreaseLife));
            description = "Increase Sword Damage";
            _rewards.Add(new RewardData(image, description, _stats.IncreaseWeaponDamage));
            description = "Increase Armor Value";
            _rewards.Add(new RewardData(image, description, _stats.IncreaseArmorValue));

            foreach (AbilityNames name in Enum.GetValues(typeof(AbilityNames)))
            {
                description = "Increase " + name.ToString() + " Resourse";
                _rewards.Add(new RewardData(image, description, _stats.IncreaseResource, name));
                description = "Increase " + name.ToString() + " Effect";
                _rewards.Add(new RewardData(image, description, _stats.IncreaseEffectDuration, name));
            }
            _isInitialized = true;
        }
    }
}