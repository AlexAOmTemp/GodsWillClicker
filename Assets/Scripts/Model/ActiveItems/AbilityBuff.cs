using UnityEngine;

public class AbilityBuff : Ability
{
    public delegate void PlayAnimation(bool activate);
    public event PlayAnimation AnimationIsPlaying;
    private AudioSource _sound;
    public void SetSound(AudioSource sound)
    {
        _sound = sound;
    }
    override public void OnButtonClick()
    {
        _counters.ButtonAwailability--;
        _counters.CurrentEffectDuration += _counters.AddEffectDurationOnActivation;
        AnimationIsPlaying?.Invoke(true);
        Sounds.IfSetPlaySound(_sound);
        if (_next != null)
            _next.UpdateAvailibleCounter(1);
        countersChanged();
    }
    override public void UpdateAvailibleCounter(int value)
    {
        _counters.Clicks += value;
        int newClickValue = _counters.Clicks / 10;
        int diff = newClickValue - _oldClickValue;
        if (diff > 0)
        {
            _counters.ButtonAwailability += diff;
        }
        _oldClickValue = newClickValue;
        countersChanged();

    }
    override public void OnTimerFinished(int value)
    {
        UpdateAvailibleCounter(value);
    }
    public void BuffChargeUsed()
    {
        _counters.CurrentEffectDuration--;
        if (_counters.CurrentEffectDuration == 0)
            AnimationIsPlaying?.Invoke(false);
        countersChanged();
    }
    public bool IsActive()
    {
        if (_counters.CurrentEffectDuration > 0)
            return true;
        return false;
    }
    public bool IsAwailible()
    {
        if (_counters.ButtonAwailability > 0)
            return true;
        return false;
    }
}
