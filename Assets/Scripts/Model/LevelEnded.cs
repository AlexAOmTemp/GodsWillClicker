using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelEnded : MonoBehaviour
{
    [SerializeField] private GameObject _endLevelPanel;
    [SerializeField] private TextMeshProUGUI _winLoseText;
    [SerializeField] private GameObject _rewardPanel;
    public void OnLevelEnded(bool win)
    {

    }
    void GameLost ()
    {
        _rewardPanel.SetActive(false);
        _winLoseText.SetText("You Lose");
        
    }
}
