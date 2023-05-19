using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchItem : ActiveItem
{
    private CombatSystem _combat;
    public void SetCombat(CombatSystem combat)
    {
        _combat = combat;
    }
    override public void OnButtonClick()
    {
        UpdateAvailibleCounter(1);
    }
    override public void UpdateAvailibleCounter(int value)
    {
        _counters.Clicks += value;
        int newClickValue = _counters.Clicks / 10;
        int diff = newClickValue - _oldClickValue;
        if (diff > 0)
        {
            _combat.MakeFewPunches(diff);
            if (_next != null)
                _next.UpdateAvailibleCounter(diff);
        }
        _oldClickValue = newClickValue;
        countersChanged();
    }
    override protected void onTimerFinished(int value)
    {
        for (int i = 0; i < value; i++)
            OnButtonClick();
    }
}
