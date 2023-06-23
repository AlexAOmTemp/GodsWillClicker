using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class GuiController : MonoBehaviour
{
    #region Serialized 
    [Header("DamageText")]
    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject _damageTextPrefab;
    [SerializeField] private GameObject _battleFieldPanel;
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

    [Header("Awailability Counters")]
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

    [Header("Player Current Effect Duration")]
    [SerializeField] private TMP_Text _playerArmorEffects;
    [SerializeField] private TMP_Text _playerWingEffects;
    [SerializeField] private TMP_Text _playerNimbusEffects;
    [SerializeField] private TMP_Text _playerWeaponEffects;
    [SerializeField] private TMP_Text _playerWrathEffects;

    [Header("Enemy Current Effect Duration")]
    [SerializeField] private TMP_Text _enemyArmorEffects;
    [SerializeField] private TMP_Text _enemyWingEffects;
    [SerializeField] private TMP_Text _enemyNimbusEffects;
    [SerializeField] private TMP_Text _enemyWeaponEffects;
    [SerializeField] private TMP_Text _enemyWrathEffects;

    [Header("Life bar")]
    [SerializeField] private TMP_Text _playerLifeText;
    [SerializeField] private TMP_Text _enemyLifeText;
    [SerializeField] RectTransform _enemyHealthFiller;
    [SerializeField] RectTransform _playerHealthFiller;
    [SerializeField] RectTransform _healthBar;
    #endregion

    #region Private Variables
    private int _playerMaxLife;
    private int _enemyMaxLife;
    private Dictionary<AbilityNames, GuiItem> _playerGui = new Dictionary<AbilityNames, GuiItem>();
    private Dictionary<AbilityNames, GuiItem> _enemyGui = new Dictionary<AbilityNames, GuiItem>();

    #endregion

    #region Public Methods
    public void SetMaxLife(bool isPlayer, int maxLife)
    {
        if (isPlayer == true)
        {
            _playerMaxLife = maxLife;
            _playerLifeText.SetText(maxLife.ToString());
        }
        else
        {
            _enemyMaxLife = maxLife;
            _enemyLifeText.SetText(maxLife.ToString());
        }
    }
    public void UpdateLife(bool isPlayer, int life)
    {
        if (isPlayer == true)
            updatePlayerLife(life);
        else
            updateEnemyLife(life);
    }
    public GuiItem GetGuiItem(bool isPlayer, AbilityNames name)
    {
        if (isPlayer)
            return _playerGui[name];
        else
            return _enemyGui[name];
    }
    public void GenerateDamageText(bool player, string damage, bool sword, bool critical)
    {
        var text = Instantiate(_damageTextPrefab, Vector3.zero, Quaternion.identity, _battleFieldPanel.transform);
        var movingText = text.GetComponent<MovingText>();
        Color color = Color.white;
        if (sword == true)
            color = Color.yellow;
        if (critical == true)
            color = Color.red;
        Vector3 startPosition = _enemyTextStartPosition.position;
        if (player == true)
            startPosition = _playerTextStartPosition.position;
        movingText.PlayText(startPosition, _textFinishPosition.position.y, 1f, damage, color);
    }
    #endregion

    #region Private Methods
    private void Awake()
    {
        initGui();
        Debug.Log($"Gui initialized");
    }
    private void initGui()
    {
        GuiItem gui = new GuiItem(_punchButton, null, _bloodResourse, null);
        _playerGui.Add(AbilityNames.Punch, gui);
        gui = new GuiItem(_armorButton, _armorCounter, _armorResourse, _playerArmorEffects);
        _playerGui.Add(AbilityNames.Armor, gui);
        gui = new GuiItem(_swordButton, _swordCounter, _weaponResourse, _playerWeaponEffects);
        _playerGui.Add(AbilityNames.Sword, gui);
        gui = new GuiItem(_wingButton, _wingCounter, _wingResourse, _playerWingEffects);
        _playerGui.Add(AbilityNames.Wings, gui);
        gui = new GuiItem(_nimbusButton, _nimbusCounter, _hornResourse, _playerNimbusEffects);
        _playerGui.Add(AbilityNames.Nimbus, gui);
        gui = new GuiItem(_wrathButton, _wrathCounter, null, _playerWrathEffects);
        _playerGui.Add(AbilityNames.Wrath, gui);

        gui = new GuiItem(null, null, null, null);
        _enemyGui.Add(AbilityNames.Punch, gui);
        gui = new GuiItem(null, null, null, _enemyArmorEffects);
        _enemyGui.Add(AbilityNames.Armor, gui);
        gui = new GuiItem(null, null, null, _enemyWeaponEffects);
        _enemyGui.Add(AbilityNames.Sword, gui);
        gui = new GuiItem(null, null, null, _enemyWingEffects);
        _enemyGui.Add(AbilityNames.Wings, gui);
        gui = new GuiItem(null, null, null, _enemyNimbusEffects);
        _enemyGui.Add(AbilityNames.Nimbus, gui);
        gui = new GuiItem(null, null, null, _enemyWrathEffects);
        _enemyGui.Add(AbilityNames.Wrath, gui);
    }
    private void updatePlayerLife(int playerLife)
    {
        float scale = (float)playerLife / (float)_playerMaxLife;
        _playerHealthFiller.localScale = new Vector3(-scale, 1, 1);
        _playerLifeText.SetText(playerLife.ToString());
    }
    private void updateEnemyLife(int enemyLife)
    {
        float scale = (float)enemyLife / (float)_enemyMaxLife;
        _enemyHealthFiller.localScale = new Vector3(scale, 1, 1);
        _enemyLifeText.SetText(enemyLife.ToString());
    }
    #endregion
}