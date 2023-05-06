
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    [SerializeField] private CombatSystem _enemy;
    [SerializeField] private PartsTimers _partsTimers;
    [SerializeField] private GuiController _guiController;
    [SerializeField] private AudioSource _receiveFleshDamageSound;
    [SerializeField] private AudioSource _receiveArmorDamageSound;
    [SerializeField] private AudioSource _armorSound;
    [SerializeField] private AudioSource _wingSound;
    [SerializeField] private AudioSource _swordSound;
    [SerializeField] private AudioSource _nimbusSound;
    [SerializeField] private AudioSource _wrathSound;
    [SerializeField] bool isPlayer;
    private ActiveEffects _effects = new ActiveEffects();
    private DemonParts _parts = new DemonParts();
    private Counters _counters;
    private Counters _availabilityCounters = new Counters();
    public delegate void Death();
    public event Death IsDead;
    public delegate void AvailabilityChanged(Counters availabilityCounters);
    public event AvailabilityChanged AvailabilityIsChanged;

    void Awake()
    {
        _partsTimers.CounterIsReady += onTimerReady;
    }
    public void Init(DemonParts parts, Counters availabilityCounters, ActiveEffects effects)
    {
        _parts = parts;
        _availabilityCounters = availabilityCounters;
        _counters = new Counters();
        _effects = effects;
        _partsTimers.Init(_parts);
        updateGui();
    }
    public void OnPunchButtonClick()
    {
        _counters.Punch++;
        punchCounterChanged();
    }
    public void OnArmorButtonClick()
    {
        _availabilityCounters.Armor -= 1;
        _counters.Armor += 1;
        _effects.ArmorLayers += 3;
        if (_counters.Armor % 10 == 0)
            _availabilityCounters.Wings += 1;
        if (_armorSound != null)
            _armorSound.Play();
    }
    public void OnWingButtonClick()
    {
        _availabilityCounters.Wings -= 1;
        _counters.Wings += 1;
        _effects.WingsHits += 3;
        if (_counters.Wings % 10 == 0)
            _availabilityCounters.Swords += 1;
        if (_wingSound != null)
            _wingSound.Play();
    }
    public void OnSwordButtonClick()
    {
        _availabilityCounters.Swords -= 1;
        _counters.Swords += 1;
        _effects.WeaponHits += 3;
        if (_counters.Swords % 10 == 0)
            _availabilityCounters.Nimbus += 1;
        if (_swordSound != null)
            _swordSound.Play();
    }
    public void OnNimbusButtonClick()
    {
        _availabilityCounters.Nimbus -= 1;
        _counters.Nimbus += 1;
        _effects.NimbusHits += 3;
        if (_counters.Nimbus % 10 == 0)
            _availabilityCounters.Wrath += 1;
        if (_nimbusSound != null)
            _nimbusSound.Play();
    }
    public void OnWrathButtonClick()
    {
        _availabilityCounters.Wrath -= 1;
        _counters.Wrath += 1;
        _effects.WrathHits += 1;
        if (_wrathSound != null)
            _wrathSound.Play();
    }
    private void makePunch(int damage)
    {
        int multiple = 1;
        if (_effects.WrathHits > 0)
        {
            multiple += 100;
            _effects.NimbusHits--;
        }
        if (_effects.NimbusHits > 0)
        {
            multiple += 10;
            _effects.NimbusHits--;
        }
        if (_effects.WingsHits > 0)
        {
            multiple += 3;
            _effects.WingsHits--;
        }
        if (_effects.WeaponHits > 0)
        {
            multiple += 5;
            _effects.WeaponHits--;
        }
        _enemy.GetDamage(damage * multiple);
    }
    public void GetDamage(int damage)
    {
        if (_effects.ArmorLayers > damage)
        {
            _effects.ArmorLayers -= damage;
            if (_receiveArmorDamageSound != null)
                _receiveArmorDamageSound.Play();
        }
        else
        {
            damage -= _effects.ArmorLayers;
            _effects.ArmorLayers = 0;
            _effects.Life -= damage;
            if (_receiveFleshDamageSound != null)
                _receiveFleshDamageSound.Play();
            if (_effects.Life <= 0)
                IsDead?.Invoke();
        }
        updateGui();
    }
    private void punchCounterChanged()
    {
        if (_counters.Punch % 10 == 0 && _counters.Punch != 0)
            makePunch(_effects.Damage);
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
    public void OnButtonClick(int index)
    {
        switch (index)
        {
            case 0:
                OnPunchButtonClick();
                break;
            case 1:
                OnArmorButtonClick();
                break;
            case 2:
                OnWingButtonClick();
                break;
            case 3:
                OnSwordButtonClick();
                break;
            case 4:
                OnNimbusButtonClick();
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
    private void updateGui()
    {
        if (isPlayer == true)
            _guiController.UpdatePlayerGui(_counters, _availabilityCounters, _parts, _effects);
        else
            _guiController.UpdateEnemyGui(_effects);
    }
}
