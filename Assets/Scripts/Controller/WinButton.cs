using UnityEngine;
using UnityEngine.UI;

public class WinButton : MonoBehaviour
{
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _rewardPanel;
    [SerializeField] private GameObject _levelText;

    void Start()
    {
        var button = this.GetComponent<Button>();
        button.onClick.AddListener(onButtonClick);
    }

    private void onButtonClick()
    {
        _winPanel.SetActive(false);
        _levelText.SetActive(false);
        _rewardPanel.SetActive(true);
    }
}
