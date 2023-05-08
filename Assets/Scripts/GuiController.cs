using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GuiController : MonoBehaviour
{
    #region Serialized 
    [Header("Buttons")]
    [SerializeField] private Button _armorButton;
    [SerializeField] private Button _wingButton;
    [SerializeField] private Button _swordButton;
    [SerializeField] private Button _nimbusButton;
    [SerializeField] private Button _wrathButton;

    [Header("Button Counters")]
    [SerializeField] private TMP_Text _punchCounter;
    [SerializeField] private TMP_Text _armorCounter;
    [SerializeField] private TMP_Text _wingCounter;
    [SerializeField] private TMP_Text _swordCounter;
    [SerializeField] private TMP_Text _nimbusCounter;
    [SerializeField] private TMP_Text _wrathCounter;

    [Header("Resource Counters")]
    [SerializeField] private TMP_Text _bloodResourse;
    [SerializeField] private TMP_Text _armorResourse;
    [SerializeField] private TMP_Text _wingResourse;
    [SerializeField] private TMP_Text _weaponResourse;
    [SerializeField] private TMP_Text _hornResourse;

    [Header("Player Active Effect Counters")]
    [SerializeField] private TMP_Text _playerArmorEffects;
    [SerializeField] private TMP_Text _playerWingEffects;
    [SerializeField] private TMP_Text _playerNimbusEffects;
    [SerializeField] private TMP_Text _playerWeaponEffects;
    [SerializeField] private TMP_Text _playerWrathEffects;

    [Header("Enemy Active Effect Counters")]
    [SerializeField] private TMP_Text _enemyArmorEffects;
    [SerializeField] private TMP_Text _enemyWingEffects;
    [SerializeField] private TMP_Text _enemyNimbusEffects;
    [SerializeField] private TMP_Text _enemyWeaponEffects;
    [SerializeField] private TMP_Text _enemyWrathEffects;

    [Header("Life bar")]
    [SerializeField] private TMP_Text _playerLife;
    [SerializeField] private TMP_Text _enemyLife;
    [SerializeField] RectTransform _healthBarCurrent;
    [SerializeField] RectTransform _healthBar;
    #endregion
    private int _angelLife;
    private int _demonLife;
    public void UpdatePlayerGui(Stats stats, Counters counters, Counters availabilityCounters, DemonParts parts, ActiveEffects effects)
    {
        updateCounters(counters);
        updateParts(parts);
        updateActiveEffectsOf(true, effects, stats);
        updateAvailability(availabilityCounters);
    }
    public void UpdateEnemyGui(Stats stats, ActiveEffects effects)
    {
        updateActiveEffectsOf(false, effects, stats);
    }
    private void updateCounters(Counters counters)
    {
        _punchCounter.SetText(counters.Punch.ToString());
        _armorCounter.SetText(counters.Armor.ToString());
        _wingCounter.SetText(counters.Wings.ToString());
        _swordCounter.SetText(counters.Swords.ToString());
        _nimbusCounter.SetText(counters.Nimbus.ToString());
        _wrathCounter.SetText(counters.Wrath.ToString());
    }
    private void updateParts(DemonParts parts)
    {
        _bloodResourse.SetText(parts.Blood.ToString());
        _armorResourse.SetText(parts.Armor.ToString());
        _wingResourse.SetText(parts.Wings.ToString());
        _weaponResourse.SetText(parts.Weapons.ToString());
        _hornResourse.SetText(parts.Horns.ToString());
    }
    private void updateActiveEffectsOf(bool player, ActiveEffects effects, Stats stats)
    {
        if (player)
        {
            _playerLife.SetText(stats.Life.ToString());
            _playerArmorEffects.SetText(effects.ArmorLayers.ToString());
            _playerWingEffects.SetText(effects.WingsHits.ToString());
            _playerWeaponEffects.SetText(effects.WeaponHits.ToString());
            _playerNimbusEffects.SetText(effects.NimbusHits.ToString());
            _playerWrathEffects.SetText(effects.WrathHits.ToString());
            updateAngelLife(stats.Life);
        }
        else
        {
            _enemyLife.SetText(stats.Life.ToString());
            _enemyArmorEffects.SetText(effects.ArmorLayers.ToString());
            _enemyWingEffects.SetText(effects.WingsHits.ToString());
            _enemyWeaponEffects.SetText(effects.WeaponHits.ToString());
            _enemyNimbusEffects.SetText(effects.NimbusHits.ToString());
            _enemyWrathEffects.SetText(effects.WrathHits.ToString());
            updateDemonLife(stats.Life);
        }
    }
    private void updateAngelLife(int angelLife)
    {
        _angelLife = angelLife;
        var sum = _angelLife + _demonLife;
        updateLife((float)_angelLife / (float)sum);
    }
    private void updateDemonLife(int demonLife)
    {
        _demonLife = demonLife;
        var sum = _angelLife + _demonLife;
        updateLife((float)_angelLife / (float)sum);
    }
    private void updateAvailability(Counters availibleCounter)
    {
        updateButtonAvailability(availibleCounter.Armor, _armorButton);
        updateButtonAvailability(availibleCounter.Wings, _wingButton);
        updateButtonAvailability(availibleCounter.Swords, _swordButton);
        updateButtonAvailability(availibleCounter.Nimbus, _nimbusButton);
        updateButtonAvailability(availibleCounter.Wrath, _wrathButton);
    }
    private void updateButtonAvailability(int value, Button button)
    {
        if (value > 0)
        {
            if (button.interactable == false)
                button.interactable = true;
        }
        else
            button.interactable = false;
    }
    private void updateLife(float lifeProportion)
    {
        _healthBarCurrent.localScale = new Vector3(lifeProportion, 1, 1);
    }
}
