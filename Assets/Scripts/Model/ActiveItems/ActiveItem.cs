using UnityEngine;

public abstract class ActiveItem
{
    #region Private Fields
    public delegate void ChangeCounters(CountersSet counters);
    public event ChangeCounters CountersIsChanged;
    protected ResourceTimer _resourceTimer;
    protected BuffItem _next;
    protected CountersSet _counters;
    protected int _oldClickValue;
    #endregion
    public void Init(ResourceTimer timer, BuffItem next = null)
    {
        _next = next;
        if (_next != null)
        {
            _resourceTimer = timer;
            _resourceTimer.CountIsFinished += onTimerFinished;
        }
        CountersIsChanged?.Invoke(_counters);
    }
    public void SetCounters(CountersSet counters)
    {
        _counters = counters;
        if (_resourceTimer != null)
            _resourceTimer.ResourceCountChanged(_counters.Resource);
        CountersIsChanged?.Invoke(_counters);
    }
    public void NewRoundStarted()
    {
        if (_resourceTimer != null)
            _resourceTimer.NewRoundStarted(_counters.Resource);
    }
    public abstract void OnButtonClick();
    protected abstract void onTimerFinished(int value);
    public void AddResource()
    {
        _counters.Resource++;
        if (_resourceTimer != null)
            _resourceTimer.ResourceCountChanged(_counters.Resource);
        CountersIsChanged?.Invoke(_counters);
    }
    public abstract void UpdateAvailibleCounter(int value);
    protected void countersChanged()
    {
        CountersIsChanged?.Invoke(_counters);
    }
}
