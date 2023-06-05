using System;
using System.Collections.Generic;
using UnityEngine;

public class Timers : MonoBehaviour
{
    [SerializeField] private GameObject _resourceTimerPrefab;
    private Dictionary<ItemNames, ResourceTimer> _resourceTimers = new Dictionary<ItemNames, ResourceTimer>();

    public void SetCountFinishDelegate(ItemNames itemName, ResourceTimer.CountFinish onCountFinish)
    {
        _resourceTimers[itemName].CountIsFinished += onCountFinish;
    }
    public ResourceTimer GetTemer(ItemNames itemName)
    {
        return _resourceTimers[itemName];
    }
    void Awake()
    {
        foreach (ItemNames itemName in Enum.GetValues(typeof(ItemNames)))
        {
            var timer = Instantiate(_resourceTimerPrefab, Vector3.zero, Quaternion.identity, this.transform);
            var resourceTimer = timer.GetComponent<ResourceTimer>();
            RoundController.NewRoundIsStarted += resourceTimer.NewRoundStarted;
            _resourceTimers.Add(itemName, resourceTimer);
        }
    }
    void OnDestroy()
    {
        foreach (var resourceTimer in _resourceTimers.Values)
            RoundController.NewRoundIsStarted -= resourceTimer.NewRoundStarted;
    }

}
