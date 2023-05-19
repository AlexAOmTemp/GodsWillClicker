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
        _punch.onClick.AddListener(delegate { buttonHandler(ItemNames.Punch); });
        _armor.onClick.AddListener(delegate { buttonHandler(ItemNames.Armor); });
        _sword.onClick.AddListener(delegate { buttonHandler(ItemNames.Sword); });
        _wings.onClick.AddListener(delegate { buttonHandler(ItemNames.Wings); });
        _nimbus.onClick.AddListener(delegate { buttonHandler(ItemNames.Nimbus); });
        _wrath.onClick.AddListener(delegate { buttonHandler(ItemNames.Wrath); });
    }
    void buttonHandler(ItemNames name)
    {
        Debug.Log("click");
        _playerCombatSystem.OnButtonClick(name);
    }

}

