using System;
using System.Collections.Generic;
using UnityEngine;

public class Timers : MonoBehaviour
{
    [SerializeField] private GameObject _resourceTimerPrefab;
    private Dictionary<AbilityNames, ResourceTimer> _resourceTimers = new Dictionary<AbilityNames, ResourceTimer>();

    public void SetCountFinishDelegate(AbilityNames name, ResourceTimer.CountFinish onCountFinish)
    {
        _resourceTimers[name].CountIsFinished += onCountFinish;
    }
    public ResourceTimer GetTemer(AbilityNames name)
    {
        return _resourceTimers[name];
    }
    void Awake()
    {
        foreach (AbilityNames name in Enum.GetValues(typeof(AbilityNames)))
        {
            var timer = Instantiate(_resourceTimerPrefab, Vector3.zero, Quaternion.identity, this.transform);
            var resourceTimer = timer.GetComponent<ResourceTimer>();
            RoundController.NewRoundIsStarted += resourceTimer.NewRoundStarted;
            _resourceTimers.Add(name, resourceTimer);
        }
    }
    void OnDestroy()
    {
        foreach (var resourceTimer in _resourceTimers.Values)
            RoundController.NewRoundIsStarted -= resourceTimer.NewRoundStarted;
    }

}
