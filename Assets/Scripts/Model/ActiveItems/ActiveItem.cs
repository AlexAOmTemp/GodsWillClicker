using UnityEngine;
public abstract class ActiveItem
{
    #region Private Fields
    public delegate void ChangeCounters(CountersSet counters);
    public event ChangeCounters CountersIsChanged;
    public delegate void ResourceCountChanged(int newValue);
    public event ResourceCountChanged ResourceCountIsChanged;
    protected BuffItem _next;
    protected CountersSet _counters;
    protected int _oldClickValue;
    #endregion
    public void Init(BuffItem next = null)
    {
        _next = next;
        CountersIsChanged?.Invoke(_counters);
    }
    public void SetCounters(CountersSet counters)
    {
        _counters = counters;
        _oldClickValue=0;
        ResourceCountIsChanged?.Invoke(_counters.Resource);
        CountersIsChanged?.Invoke(_counters);
    }
    public abstract void OnButtonClick();
    public abstract void OnTimerFinished(int value);
    public void AddResource()
    {
        _counters.Resource++;
        ResourceCountIsChanged?.Invoke(_counters.Resource);
        CountersIsChanged?.Invoke(_counters);
    }
    public abstract void UpdateAvailibleCounter(int value);
    protected void countersChanged()
    {
        CountersIsChanged?.Invoke(_counters);
    }
}
