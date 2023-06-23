using UnityEngine;

public class RewardGenerator : MonoBehaviour
{
    public delegate void RewardChoosen();
    public event RewardChoosen RewardIsChoosen;

    [SerializeField] private LevelEndPanel _endLevelPanel;
    [SerializeField] private RewardsFiller _rewardFiller;
    private RewardData[] _rewards;

    public void generate(int stage)
    {
        _endLevelPanel.LevelWon(stage, tempRandomGenerator(stage));
    }

    private void Awake()
    {
        _endLevelPanel.RewardIsChoosed += onRewardChoose;
    }
    private RewardData[] tempRandomGenerator(int stage)
    {
        _rewards = _rewardFiller.GetRewards(stage);
        return _rewards;
    }
    private void onRewardChoose(int rewardId)
    {
        Debug.Log($"RewardGenerator: Rewards choosen {rewardId}");
        _rewards[rewardId].InvokeEffect();
        RewardIsChoosen?.Invoke();
    }

}
