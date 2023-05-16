using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GuiController : MonoBehaviour
{
    #region Serialized 
    [Header("DamageText")]
    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject _damageTextPrefab;
    [SerializeField] private RectTransform _playerTextStartPosition;
    [SerializeField] private RectTransform _enemyTextStartPosition;
    [SerializeField] private RectTransform _textFinishPosition;
    [Header("Buttons")]
    [SerializeField] private Button _punchButton;
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
    #region Private Variables
    private int _angelLife;
    private int _demonLife;
    private ButtonFiller _punchFiller;
    private ButtonFiller _armorFiller;
    private ButtonFiller _swordFiller;
    private ButtonFiller _wingFiller;
    private ButtonFiller _nimbusFiller;
    private ButtonFiller _wrathFiller;
    #endregion

    void Awake()
    {
        _punchFiller = _punchButton.GetComponent<ButtonFiller>();
        _armorFiller = _armorButton.GetComponent<ButtonFiller>();
        _swordFiller = _swordButton.GetComponent<ButtonFiller>();
        _wingFiller = _wingButton.GetComponent<ButtonFiller>();
        _nimbusFiller = _nimbusButton.GetComponent<ButtonFiller>();
        _wrathFiller = _wrathButton.GetComponent<ButtonFiller>();
    }
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
    public void GenerateDamageText(bool player, string damage, bool sword, bool critical)
    {
        var text = Instantiate(_damageTextPrefab, Vector3.zero, Quaternion.identity, _canvas.transform);
        var movingText = text.GetComponent<MovingText>();
        Color color = Color.grey;
        if (sword == true)
            color = Color.yellow;
        if (critical == true)
            color = Color.red;
        Vector3 startPosition = _enemyTextStartPosition.position;
        if (player == true)
            startPosition = _playerTextStartPosition.position;
        movingText.PlayText(startPosition, _textFinishPosition.position.y, 1f, damage, color);
    }
    #region Private Methods 
    private void updateCounters(Counters counters)
    {
        _punchFiller.FillImage(counters.Punch);
        _armorFiller.FillImage(counters.Armor);
        _swordFiller.FillImage(counters.Swords);
        _wingFiller.FillImage(counters.Wings);
        _nimbusFiller.FillImage(counters.Nimbus);
        _wrathFiller.FillImage(counters.Wrath);
        /*_punchCounter.SetText(counters.Punch.ToString());
        _armorCounter.SetText(counters.Armor.ToString());
        _wingCounter.SetText(counters.Wings.ToString());
        _swordCounter.SetText(counters.Swords.ToString());
        _nimbusCounter.SetText(counters.Nimbus.ToString());
        _wrathCounter.SetText(counters.Wrath.ToString());*/

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
        updateButtonAvailability(availibleCounter.Armor, _armorButton, _armorCounter);
        updateButtonAvailability(availibleCounter.Wings, _wingButton, _wingCounter);
        updateButtonAvailability(availibleCounter.Swords, _swordButton, _swordCounter);
        updateButtonAvailability(availibleCounter.Nimbus, _nimbusButton, _nimbusCounter);
        updateButtonAvailability(availibleCounter.Wrath, _wrathButton, _wrathCounter);
    }
    private void updateButtonAvailability(int value, Button button, TMP_Text counter)
    {
        counter.SetText(value.ToString());
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
    #endregion
}