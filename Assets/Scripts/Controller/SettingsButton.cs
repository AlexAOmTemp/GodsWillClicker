using UnityEngine;
using UnityEngine.UI;

public class CloseSettingsButton : MonoBehaviour
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
        _settingsPanel.SetActive(false);
        Pause.UnpauseGame();
    }
}
