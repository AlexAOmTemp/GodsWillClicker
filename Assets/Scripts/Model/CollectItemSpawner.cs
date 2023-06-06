using UnityEngine;

public class CollectItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _collectItemPrefab;
    [SerializeField] private GameObject _collectItemsFolder;
    [SerializeField] private RectTransform _demonTransform;
    [SerializeField] private RectTransform _groundTransform;
    [SerializeField] private CombatSystem _enemyCombatSystem;
    private CombatSystem _playerCombatSystem;

    void Start ()
    {
        _enemyCombatSystem.ResorceIsGenerated+=CreateItem;
        _playerCombatSystem = this.GetComponent<CombatSystem>();
    }
    void CreateItem(ItemNames name)
    {
        var prefab = Instantiate (_collectItemPrefab, _demonTransform.position, 
            Quaternion.identity, _collectItemsFolder.transform);
        prefab.GetComponent<RectTransform>().localScale = new Vector3(-1,1,1);
        prefab.GetComponent<CollectFall>().Init(_groundTransform);
        prefab.GetComponent<CollectItemButton>().Init(name,_playerCombatSystem);
    }
}
