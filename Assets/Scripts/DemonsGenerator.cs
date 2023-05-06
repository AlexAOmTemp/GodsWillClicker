using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonsGenerator : MonoBehaviour
{
    [SerializeField] private Demon _demon;
    private DemonParts GeneratedDemonParts = new DemonParts();
    private Counters GeneratedCounters = new Counters();
    private ActiveEffects GeneratedEffects = new ActiveEffects();
    public void GenerateDemon(int stage)
    {
        GeneratedEffects.ClickDelay = 0.4f;
        GeneratedEffects.Life = 200 + stage;
        GeneratedEffects.Damage = 1;
        GeneratedDemonParts.Blood = 60;
        GeneratedDemonParts.Armor = 30;
        GeneratedDemonParts.Weapons = 20;
        GeneratedDemonParts.Wings = 10;
        GeneratedDemonParts.Horns = 5;
        _demon.Init(GeneratedDemonParts, GeneratedCounters, GeneratedEffects);

    }
}
