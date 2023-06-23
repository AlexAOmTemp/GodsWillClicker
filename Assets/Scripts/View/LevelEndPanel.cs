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
    private GameObject _levelTextObject;
    private GameObject _winPanel;
    private GameObject _losePanel;
    private GameObject _rewardPanel;
    private GameObject _rewardLayout;
    private Button _tryAgainButton;
    private GameObject[] _generatedButtons = new GameObject[_MAX_REWARD_BUTTONS_COUNT];
    private bool _isInit = false;
    #endregion

    #region Public Methods
    public void LevelLost(int level)
    {
        levelEnded(false, level);
    }
    public void LevelWon(int level, RewardData[] rewards)
    {
        levelEnded(true, level);
        setRewardButtons(rewards);
    }
    public void Init()
    {
        if (_isInit == false)
        {
            //this.gameObject.SetActive(true);
            var framePanel = this.transform.Find("FramePanel");
            _levelTextObject = framePanel.transform.Find("LevelText").gameObject;
            _levelText = _levelTextObject.GetComponent<TextMeshProUGUI>();
            _winPanel = framePanel.transform.Find("WinPanel").gameObject;
            _losePanel = framePanel.transform.Find("LosePanel").gameObject;
            _tryAgainButton = _losePanel.transform.Find("TryAgainButton").GetComponent<Button>();
            _rewardPanel = framePanel.transform.Find("RewardPanel").gameObject;
            _rewardLayout = _rewardPanel.transform.Find("RewardLayout").gameObject;
            _tryAgainButton.onClick.AddListener(onTryAgainClicked);
            generateRewardButtons();
            _isInit = true;
            this.gameObject.SetActive(false);
        }
    }
    #endregion

    #region Private Methods
    private void onTryAgainClicked()
    {
        this.gameObject.SetActive(false);
        TryAgainIsClicked?.Invoke();
    }
    private void levelEnded(bool isWon, int level)
    {
        this.gameObject.SetActive(true);
        _levelTextObject.SetActive(true);
        _winPanel.gameObject.SetActive(isWon);
        _losePanel.SetActive(!isWon);
        _levelText.SetText(_LEVEL_STRING + level.ToString());
    }
    private void setRewardButtons(RewardData[] rewards)
    {
        Debug.Log($"LevelEndPanel: set reward buttons {rewards.Length}");
        //hide unused buttons
        for (int i = rewards.Length; i < _MAX_REWARD_BUTTONS_COUNT; i++)
            _generatedButtons[i].SetActive(false);
        //show and set used buttons
        for (int i = 0; i < rewards.Length; i++)
        {
            _generatedButtons[i].SetActive(true);
            var image = _generatedButtons[i].GetComponent<Image>();
            image.sprite = rewards[i].RewardImage;
            _generatedButtons[i].GetComponentInChildren<TextMeshProUGUI>().SetText(rewards[i].Description);
        }
    }
    private void generateRewardButtons()
    {
        for (int i = 0; i < _MAX_REWARD_BUTTONS_COUNT; i++)
        {
            var buttonObject = Instantiate(_buttonPrefab,
                Vector3.zero, Quaternion.identity, _rewardLayout.transform);
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
        _rewardPanel.SetActive(false);
    }
    #endregion
}
