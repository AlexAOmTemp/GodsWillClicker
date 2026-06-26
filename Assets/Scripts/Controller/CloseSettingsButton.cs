using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{
    [SerializeField] private GameObject _settingsPanel;

    private bool _vision = false;

    private void Start()
    {
        var button = this.GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        _settingsPanel.SetActive(true);
        Pause.PauseGame();
    }
}