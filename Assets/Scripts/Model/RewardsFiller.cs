using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class RewardsFiller : MonoBehaviour
{
    [SerializeField] private Image _testImage;
    [SerializeField] private PlayerBaseStats _stats;
    private List<RewardData> _rewards = new List<RewardData>();
    public RewardData[] GetRewards()
    {
        return _rewards.ToArray();
    }
    private void Awake()
    {
        Image image = _testImage;
        string description = "Increase Damage";
        _rewards.Add(new RewardData(image, description, _stats.IncreaseDamage));
        description = "Increase Life";
        _rewards.Add(new RewardData(image, description, _stats.IncreaseLife));
        description = "Increase Evason Rating";
        _rewards.Add(new RewardData(image, description, _stats.IncreaseEvasonRating));
        description = "Increase Drop Rating";
        _rewards.Add(new RewardData(image, description, _stats.IncreaseDropRating));
        description = "Increase Critical Rating";
        _rewards.Add(new RewardData(image, description, _stats.IncreaseCriticalRating));
        description = "Increase Critical Damage";
        _rewards.Add(new RewardData(image, description, _stats.IncreaseCriticalDamage));
        description = "Increase Sword Damage";
        _rewards.Add(new RewardData(image, description, _stats.IncreaseWeaponDamage));
        description = "Increase Armor Value";
        _rewards.Add(new RewardData(image, description, _stats.IncreaseArmorValue));

        foreach (ItemNames itemName in Enum.GetValues(typeof(ItemNames)))
        {
            description = "Increase " + itemName.ToString() + " Resourse";
            _rewards.Add(new RewardData(image, description, _stats.IncreaseResource, itemName));
            description = "Increase " + itemName.ToString() + " Effect";
            _rewards.Add(new RewardData(image, description, _stats.IncreaseEffectDuration, itemName));
        }
    }
}