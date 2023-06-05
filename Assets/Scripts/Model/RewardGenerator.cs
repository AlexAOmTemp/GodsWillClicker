using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardGenerator : MonoBehaviour
{
    public delegate void RewardChoosen();
    public event RewardChoosen RewardIsChoosen;

    [SerializeField] private LevelEndPanel _endLevelPanel;
    [SerializeField] private Image _tempImage;

    private List<RewardData> _rewards = new List<RewardData>();

    public void generate(int stage)
    {
        _endLevelPanel.LevelWon(stage, tempRandomGenerator());
    }

    private void Awake()
    {
        _endLevelPanel.RewardIsChoosed += onRewardChoose;
    }
    private List<RewardData> tempRandomGenerator()
    {
        List<RewardData> rewards = new List<RewardData>();
        int count = Random.Range(1, 6);
        for (int i = 0; i < count; i++)
        {
            //RewardGui gui = new RewardGui(_tempImage, "Reward " + i.ToString());
            //rewards.Add(gui);
        }
        Debug.Log ($"RewardGenerator: Rewards generated {rewards.Count}");
        return rewards;
    }
    private void onRewardChoose(int rewardId)
    {
         Debug.Log ($"RewardGenerator: Rewards choosen {rewardId}");
        RewardIsChoosen?.Invoke();
    }

}
