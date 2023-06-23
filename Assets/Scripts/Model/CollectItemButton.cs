using UnityEngine;
using UnityEngine.UI;

public class CollectItemButton : MonoBehaviour
{
    // Start is called before the first frame update
    AbilityNames _name;
    CombatSystem _playerCombatSystem;
    public void Init (AbilityNames name, CombatSystem playerCombatSystem)
    {
        _name = name;
        _playerCombatSystem = playerCombatSystem;
        this.GetComponent<Button>().onClick.AddListener(onButtonClick);
    }

    private void onButtonClick()
    {
        _playerCombatSystem.AddPart(_name);
        Destroy(this.gameObject);
    }
}
