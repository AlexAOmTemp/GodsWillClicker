using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{
    [SerializeField] private GameObject _settingsPanel;
    private bool _vision = false;
    // Start is called before the first frame update
    void Start()
    {
        var button = this.GetComponent<Button>();
        button.onClick.AddListener(onButtonClick);
    }

    private void onButtonClick()
    {
        if (_vision == true)
        {
            _vision = false;
            _settingsPanel.SetActive(false);
            Pause.UnpauseGame();
        }
        else
        {
            _vision = true;
            _settingsPanel.SetActive(true);
            Pause.PauseGame();
        }
    }
}
