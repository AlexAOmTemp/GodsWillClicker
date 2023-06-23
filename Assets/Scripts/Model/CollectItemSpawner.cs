using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CollectItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _collectItemPrefab;
    [SerializeField] private GameObject _collectItemsFolder;
    [SerializeField] private RectTransform _demonTransform;
    [SerializeField] private RectTransform _groundTransform;
    [SerializeField] private CombatSystem _enemyCombatSystem;

    [SerializeField] private Sprite _bloodIcon;
    [SerializeField] private Sprite _armorIcon;
    [SerializeField] private Sprite _swordIcon;
    [SerializeField] private Sprite _wingsIcon;
    [SerializeField] private Sprite _hornsIcon;

    private CombatSystem _playerCombatSystem;
    private List<Sprite> _icons = new List<Sprite>();

    void Start ()
    {
        _enemyCombatSystem.ResorceIsGenerated+=CreateItem;
        _playerCombatSystem = this.GetComponent<CombatSystem>();
        _icons.Add(_bloodIcon);
        _icons.Add(_armorIcon);
        _icons.Add(_swordIcon);
        _icons.Add(_wingsIcon);
        _icons.Add(_hornsIcon);
    }
    void CreateItem(AbilityNames name)
    {
        var prefab = Instantiate (_collectItemPrefab, _demonTransform.position, 
            Quaternion.identity, _collectItemsFolder.transform);
        prefab.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
        prefab.GetComponent<CollectFall>().Init(_groundTransform);
        prefab.GetComponent<CollectItemButton>().Init(name,_playerCombatSystem);
        prefab.transform.Find("Icon").GetComponent<Image>().sprite = _icons[(int)name];

    }
}
