using UnityEngine;
using System.Collections.Generic;
public class CombatSystem : MonoBehaviour
{
    [SerializeField] private Animations _animations;
    [SerializeField] private CombatSystem _enemy;
    [SerializeField] private PartsTimers _partsTimers;
    [SerializeField] private GuiController _guiController;
    [SerializeField] bool isPlayer;
    #region Audio
    [Header("Audio")]
    [SerializeField] private AudioSource _receiveFleshDamageSound;
    [SerializeField] private AudioSource _evasonSound;
    [SerializeField] private AudioSource _receiveArmorDamageSound;
    [SerializeField] private AudioSource _armorSound;
    [SerializeField] private AudioSource _wingSound;
    [SerializeField] private AudioSource _swordSound;
    [SerializeField] private AudioSource _nimbusSound;
    [SerializeField] private AudioSource _wrathSound;
    #endregion
    #region Public Events
    public delegate void Death();
    public event Death IsDead;
    public delegate void AvailabilityChanged(Counters availabilityCounters);
    public event AvailabilityChanged AvailabilityIsChanged;

    #endregion
    #region Private Variables
    private ActiveEffects _effects = new ActiveEffects();
    private DemonParts _parts = new DemonParts();
    private Stats _stats = new Stats();
    private Counters _counters;
    private Counters _availabilityCounters = new Counters();
    #endregion

    void Awake()
    {
        _partsTimers.CounterIsReady += onTimerReady;
    }
    public void Init(Stats stats, DemonParts parts, Counters availabilityCounters, ActiveEffects effects)
    {
        testGetDamage();
        _stats = stats;
        _stats.calculateStats();
        _parts = parts;
        _availabilityCounters = availabilityCounters;
        _counters = new Counters();
        _effects = effects;
        _partsTimers.Init(_parts);
        updateGui();
    }
    public void OnButtonClick(int index)
    {
        switch (index)
        {
            case 0:
                // chanse to click punch button extra times. 
                //Every 100% makes 1 warranty extra click
                //For example 125% = double click and 25% chance for triple click
                int times = 1;
                if (_stats.ExtraFillingChance > 0)
                {
                    int add = _stats.ExtraFillingChance % 100;
                    times += _stats.ExtraFillingChance / 100;
                    if (checkProc((float)add) == true)
                        times++;
                }
                for (int i = 0; i < times; i++)
                    onPunchButtonClick();
                break;
            case 1:
                onArmorButtonClick();
                break;
            case 2:
                onWingButtonClick();
                break;
            case 3:
                onSwordButtonClick();
                break;
            case 4:
                onNimbusButtonClick();
                break;
            case 5:
                OnWrathButtonClick();
                break;
            default:
                Debug.LogError($"OnButtonClick wrong index {index}");
                break;
        }
        AvailabilityIsChanged?.Invoke(_availabilityCounters);
        updateGui();
    }
    public void GetDamage(int damage, bool isCrit, int swordDamage)
    {
        bool wingHit = false;
        bool armorHit = false;
        if (_effects.WingsHits > 0)
        {
            wingHit = true;
            if (checkProc(_stats.EvasonChance) == true)
            {
                _effects.WingsHits--;
                if (_effects.WingsHits == 0)
                    _animations.ActivateBuff(Buffs.Wings, false);

                if (isCrit == true) //crit makes half of damage if evason is sucsessed
                    damage /= 2;
                else    //non crit makes no damage
                {
                    ifSetPlaySound(_evasonSound);
                    updateGui();
                    return;
                }
            }
        }
        if (_effects.ArmorLayers > 0)
        {
            armorHit = true;
            _effects.ArmorLayers--;
            if (_effects.ArmorLayers == 0)
                _animations.ActivateBuff(Buffs.Armor, false);

            //sword makes double damage to armor
            int armorAfterSword = _stats.ArmorValue - swordDamage * 2;
            if (armorAfterSword >= 0)
                damage -= armorAfterSword;
            else
                damage += (int)(armorAfterSword / (-2));

            if (damage <= 0)//all damage is blocked
            {
                ifSetPlaySound(_receiveArmorDamageSound);
                updateGui();
                return;
            }
        }
        _stats.Life -= damage;
        ifSetPlaySound(_receiveFleshDamageSound);
        if (checkProc(_enemy.GetDropChance()))
            _enemy.AddPart(generatePart(wingHit, armorHit));
        if (_stats.Life <= 0)
            IsDead?.Invoke();
        updateGui();
    }
    public void AddPart(Parts part)
    {
        switch (part)
        {
            case Parts.Blood:
                _parts.Blood++;
                break;
            case Parts.Armor:
                _parts.Armor++;
                break;
            case Parts.Weapons:
                _parts.Weapons++;
                break;
            case Parts.Wings:
                _parts.Wings++;
                break;
            case Parts.Horns:
                _parts.Horns++;
                break;
        }
        _partsTimers.PartsCounterUpdated(_parts);
    }
    public float GetDropChance()
    {
        return _stats.DropChance;
    }
    private void onPunchButtonClick()
    {
        _counters.Punch++;
        punchCounterChanged();
    }
    private void onArmorButtonClick()
    {
        _availabilityCounters.Armor -= 1;
        _counters.Armor += 1;
        _effects.ArmorLayers += 3;
        if (_counters.Armor % 10 == 0)
            _availabilityCounters.Swords += 1;
        _animations.ActivateBuff(Buffs.Armor, true);
        ifSetPlaySound(_armorSound);
    }
    private void onSwordButtonClick()
    {
        _availabilityCounters.Swords -= 1;
        _counters.Swords += 1;
        _effects.WeaponHits += 3;
        if (_counters.Swords % 10 == 0)
            _availabilityCounters.Wings += 1;
        _animations.ActivateBuff(Buffs.Sword, true);
        ifSetPlaySound(_swordSound);
    }
    private void onWingButtonClick()
    {
        _availabilityCounters.Wings -= 1;
        _counters.Wings += 1;
        _effects.WingsHits += 3;
        if (_counters.Wings % 10 == 0)
            _availabilityCounters.Nimbus += 1;
        _animations.ActivateBuff(Buffs.Wings, true);
        ifSetPlaySound(_wingSound);
    }
    private void onNimbusButtonClick()
    {
        _availabilityCounters.Nimbus -= 1;
        _counters.Nimbus += 1;
        _effects.NimbusHits += 3;
        if (_counters.Nimbus % 10 == 0)
            _availabilityCounters.Wrath += 1;
        _animations.ActivateBuff(Buffs.Nimbus, true);
        ifSetPlaySound(_nimbusSound);
    }
    private void OnWrathButtonClick()
    {
        _availabilityCounters.Wrath -= 1;
        _counters.Wrath += 1;
        _effects.WrathHits += 1;
        _animations.ActivateBuff(Buffs.Wrath, true);
        ifSetPlaySound(_wrathSound);
    }

    private void makePunch()
    {
        _animations.Attack();
        bool isCrit = false;
        int damage = _stats.Damage;
        int swordDamage = 0;
        if (_effects.WeaponHits > 0)
        {
            _effects.WeaponHits--;
            if (_effects.WeaponHits == 0)
                _animations.ActivateBuff(Buffs.Sword, false);
            swordDamage = _stats.WeaponDamage;
        }
        if (_effects.NimbusHits > 0)
        {
            if (checkProc(_stats.CriticalChance) == true)
            {
                _effects.NimbusHits--;
                if (_effects.NimbusHits == 0)
                    _animations.ActivateBuff(Buffs.Nimbus, false);

                damage = (int)(damage * _stats.CriticalDamage / 100);
                swordDamage = (int)(swordDamage * _stats.CriticalDamage / 100);
                isCrit = true;
            }
        }
        _enemy.GetDamage(damage, isCrit, swordDamage);
    }
    private Parts generatePart(bool wing, bool armor)
    {
        List<Parts> random = new List<Parts>();

        if (wing == true)
            random.Add(Parts.Wings);
        if (_effects.NimbusHits > 0)
            random.Add(Parts.Horns);
        if (_effects.WeaponHits > 0)
            random.Add(Parts.Weapons);
        if (armor == true)
            random.Add(Parts.Armor);
        random.Add(Parts.Blood);
        int RndGen = Random.Range(0, random.Count - 1);
        return (random[RndGen]);
    }
    private void punchCounterChanged()
    {
        if (_counters.Punch % 10 == 0 && _counters.Punch != 0)
            makePunch();
        if (_counters.Punch % 100 == 0 && _counters.Punch != 0)
            _availabilityCounters.Armor += 1;
    }
    private void onTimerReady(int index)
    {
        switch (index)
        {
            case 0:
                OnButtonClick(index);
                break;
            case 1:
                _availabilityCounters.Armor++;
                break;
            case 2:
                _availabilityCounters.Wings++;
                break;
            case 3:
                _availabilityCounters.Swords++;
                break;
            case 4:
                _availabilityCounters.Nimbus++;
                break;
            default:
                Debug.LogError($"onTimerReady wrong index {index}");
                break;
        }
        AvailabilityIsChanged?.Invoke(_availabilityCounters);
    }
    private void updateGui()
    {
        if (isPlayer == true)
            _guiController.UpdatePlayerGui(_stats, _counters, _availabilityCounters, _parts, _effects);
        else
            _guiController.UpdateEnemyGui(_stats, _effects);
    }
    private bool checkProc(float chance)
    {
        float rand = Random.Range(0f, 99f);
        if (rand > chance)
            return false;
        else
            return true;
    }
    private void ifSetPlaySound(AudioSource sound)
    {
        if (sound != null)
            sound.Play();
    }
    private void testGetDamage()
    {
        _effects.WingsHits = 10;
        _effects.ArmorLayers = 0;
        _stats.Life = 10;
        _stats.EvasonRating = int.MaxValue;
        _stats.calculateStats();
        Debug.Log($"chance {_stats.EvasonChance}");
        GetDamage(2, false, 0);
        Debug.Log($"WingsHits = {_effects.WingsHits}(9),Life = {_stats.Life}(10)");
        GetDamage(2, true, 0);
        Debug.Log($"WingsHits = {_effects.WingsHits}(8),Life = {_stats.Life}(9)");
        _effects.ArmorLayers = 5;
        _stats.ArmorValue = 10;
        _effects.WingsHits = 0;
        _stats.Life = 20;
        GetDamage(15, false, 0);
        Debug.Log($"ArmorLayers = {_effects.ArmorLayers}(4),Life = {_stats.Life}(15)");
        GetDamage(5, false, 10);
        Debug.Log($"ArmorLayers = {_effects.ArmorLayers}(3),Life = {_stats.Life}(5)");
        GetDamage(1, false, 5);
        Debug.Log($"ArmorLayers = {_effects.ArmorLayers}(2),Life = {_stats.Life}(4)");
        GetDamage(1, false, 4);
        Debug.Log($"ArmorLayers = {_effects.ArmorLayers}(1),Life = {_stats.Life}(4)");
        GetDamage(8, false, 0);
        Debug.Log($"ArmorLayers = {_effects.ArmorLayers}(0),Life = {_stats.Life}(4)");
        GetDamage(2, false, 0);
        Debug.Log($"Life = {_stats.Life}(2)");
    }
}
