using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    [SerializeField] private Button _punch;
    [SerializeField] private Button _armor;
    [SerializeField] private Button _sword;
    [SerializeField] private Button _wings;
    [SerializeField] private Button _nimbus;
    [SerializeField] private Button _wrath;
    [SerializeField] private Button _settings;

    [SerializeField] private CombatSystem _playerCombatSystem;

    public void Awake()
    {
        _punch.onClick.AddListener(delegate { buttonHandler(AbilityNames.Punch); });
        _armor.onClick.AddListener(delegate { buttonHandler(AbilityNames.Armor); });
        _sword.onClick.AddListener(delegate { buttonHandler(AbilityNames.Sword); });
        _wings.onClick.AddListener(delegate { buttonHandler(AbilityNames.Wings); });
        _nimbus.onClick.AddListener(delegate { buttonHandler(AbilityNames.Nimbus); });
        _wrath.onClick.AddListener(delegate { buttonHandler(AbilityNames.Wrath); });
    }
    void buttonHandler(AbilityNames name)
    {
        _playerCombatSystem.OnButtonClick(name);
    }

}

