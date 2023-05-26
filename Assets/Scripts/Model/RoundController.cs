using UnityEngine;
public class RoundController : MonoBehaviour
{
    #region Editor Fields
    [SerializeField] private RewardGenerator _rewardGenerator;
    [SerializeField] private LevelEndPanel _endLevelPanel;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _enemy;
    #endregion
    
    #region Public Events
    public delegate void NewRoundStarted(int stage);
    public event NewRoundStarted NewRoundIsStarted;
    #endregion
    
    #region Private Fields
    private int _currentStage = 1;
    private float _roundTime = 0;
    #endregion
   
    public void StartRound()
    {
        Debug.Log($"RoundTime {_roundTime}, Stage {_currentStage}");
        _roundTime = 0;
        _player.GetComponent<PlayerBaseStats>().PlayerInit();
        _enemy.GetComponent<DemonsGenerator>().GenerateDemon(_currentStage);

        NewRoundIsStarted?.Invoke(_currentStage);
        Pause.UnpauseGame();
    }
    
    #region Private Methods
    private void Awake()
    {
        _player.GetComponent<CombatSystem>().IsDead += onAngelDead;
        _enemy.GetComponent<CombatSystem>().IsDead += onDeamonDead;
        _rewardGenerator.RewardIsChoosen += onRewardChoosen;
        _endLevelPanel.TryAgainIsClicked += onTryAgainClicked;
    }
    private void Start()
    {
        StartRound();
    }
    private void onAngelDead()
    {
        Pause.PauseGame();
        _endLevelPanel.LevelLost(_currentStage);
    }
    private void onTryAgainClicked()
    {
        StartRound();
    }
    private void onDeamonDead()
    {
        Pause.PauseGame();
        _rewardGenerator.generate(_currentStage);
    }
    private void onRewardChoosen()
    {
        _currentStage++;
        StartRound();
    }
    private void Update()
    {
        _roundTime += Time.deltaTime;
    }
    #endregion
}
