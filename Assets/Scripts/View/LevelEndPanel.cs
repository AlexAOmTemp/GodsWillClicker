using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//todo change awake to init
public class LevelEndPanel : MonoBehaviour
{
    #region Public Events
    public delegate void TryAgain();
    public event TryAgain TryAgainIsClicked;
    public delegate void RewardChoosed(int rewardID);
    public event RewardChoosed RewardIsChoosed;
    #endregion

    #region Private Constants and Editor fields
    private const string _LEVEL_STRING = "Level ";
    private const int _MAX_REWARD_BUTTONS_COUNT = 10;
    [SerializeField] private GameObject _buttonPrefab;
    #endregion

    #region Private Fields
    private TextMeshProUGUI _levelText;
    private GameObject _winPanel;
    private GameObject _losePanel;
    private GameObject _rewardPanel;
    private Button _tryAgainButton;
    private GameObject[] _generatedButtons = new GameObject[_MAX_REWARD_BUTTONS_COUNT];
    #endregion

    #region Public Methods
    public void LevelLost(int level)
    {
        levelEnded(false, level);
    }
    public void LevelWon(int level, List<RewardGui> rewards)
    {
        levelEnded(true, level);
        setRewardButtons(rewards);
    }
    #endregion

    #region Private Methods
    private void Awake()
    {
        this.gameObject.SetActive(false);
        _levelText = this.transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
        _winPanel = this.transform.Find("WinPanel").gameObject;
        _losePanel = this.transform.Find("LosePanel").gameObject;
        _tryAgainButton = _losePanel.transform.Find("TryAgainButton").GetComponent<Button>();
        _rewardPanel = _winPanel.transform.Find("RewardPanel").gameObject;
        _tryAgainButton.onClick.AddListener(onTryAgainClicked);
        generateRewardButtons();
    }
    private void onTryAgainClicked()
    {
        this.gameObject.SetActive(false);
        TryAgainIsClicked?.Invoke();
    }
    private void levelEnded(bool isWon, int level)
    {
        this.gameObject.SetActive(true);
        _winPanel.gameObject.SetActive(isWon);
        _losePanel.SetActive(!isWon);
        _levelText.SetText(_LEVEL_STRING + level.ToString());
    }
    private void setRewardButtons(List<RewardGui> rewards)
    {
        Debug.Log($"LevelEndPanel: set reward buttons {rewards.Count}");
        //hide unused buttons
        for (int i = rewards.Count; i < _MAX_REWARD_BUTTONS_COUNT; i++)
            _generatedButtons[i].SetActive(false);
        //show and set used buttons
        for (int i = 0; i < rewards.Count; i++)
        {
            _generatedButtons[i].SetActive(true);
            var image = _generatedButtons[i].GetComponent<Image>();
            image = rewards[i].RewardImage;
            _generatedButtons[i].GetComponentInChildren<TextMeshProUGUI>().SetText(rewards[i].Description);
        }
    }
    private void generateRewardButtons()
    {
        for (int i = 0; i < _MAX_REWARD_BUTTONS_COUNT; i++)
        {
            var buttonObject = Instantiate(_buttonPrefab,
                Vector3.zero, Quaternion.identity, _rewardPanel.transform);
            buttonObject.SetActive(false);
            buttonObject.name = "Button" + i.ToString();
            int a = i;
            buttonObject.GetComponent<Button>().onClick.AddListener(
                () => rewardButtonHandler(a));
            _generatedButtons[i] = buttonObject;
        }
        Debug.Log("LevelEndPanel: Buttons Generated"); 
    }
    private void rewardButtonHandler(int button)
    {
        Debug.Log($"Button clicked {button}");
        this.gameObject.SetActive(false);
        RewardIsChoosed?.Invoke(button);
    }
    #endregion
}
