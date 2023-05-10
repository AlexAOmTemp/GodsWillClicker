using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonsGenerator : MonoBehaviour
{
    [SerializeField] private Demon _demon;
    private Stats _stats = new Stats();
    private DemonParts _demonParts = new DemonParts();
    private Counters _availabilityCounters = new Counters();
    private ActiveEffects _effects = new ActiveEffects();
    public void GenerateDemon(int stage)
    {
        _stats.SetDefault();
        _stats.ClickDelay = 0.4f;
        _stats.Life = 250 + stage;
        _stats.Damage = 10;
        _demonParts.Blood = 60;
        _demonParts.Armor = 30;
        _demonParts.Weapons = 20;
        _demonParts.Wings = 10;
        _demonParts.Horns = 5;
        _demon.Init(_stats, _demonParts, _availabilityCounters, _effects);

    }
}
