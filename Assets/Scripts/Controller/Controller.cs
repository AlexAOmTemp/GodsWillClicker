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

    [SerializeField] private CombatSystem _playerCombatSystem;

    public void Awake()
    {
        _punch.onClick.AddListener(delegate { ButtonHandler(AbilityNames.Punch); });
        _armor.onClick.AddListener(delegate { ButtonHandler(AbilityNames.Armor); });
        _sword.onClick.AddListener(delegate { ButtonHandler(AbilityNames.Sword); });
        _wings.onClick.AddListener(delegate { ButtonHandler(AbilityNames.Wings); });
        _nimbus.onClick.AddListener(delegate { ButtonHandler(AbilityNames.Nimbus); });
        _wrath.onClick.AddListener(delegate { ButtonHandler(AbilityNames.Wrath); });
    }

    private void ButtonHandler(AbilityNames name)
    {
        _playerCombatSystem.OnButtonClick(name);
    }
}