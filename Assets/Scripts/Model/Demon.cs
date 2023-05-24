using UnityEngine;
using System;
public class Demon : MonoBehaviour
{
    [SerializeField] private CombatSystem _combat;
    private CountersPack _counters = new CountersPack();
    private float _clickDelay;
    private float _currentTime;
    void Awake()
    {
        _combat = this.GetComponent<CombatSystem>();
    }
    public void StartRound(Stats stats, CountersPack counters)
    {
        _clickDelay = stats.ClickDelay;
        _currentTime = 0;
        _combat.Init( stats, counters);
    }
    public void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= _clickDelay)
        {
            _currentTime = 0;
            //check if we can use buf 
            for (int i=  Enum.GetNames(typeof(ItemNames)).Length-1; i>=1; i--)
            {
                if (_combat.ActiveItems.IsAwailible( (ItemNames)i ) )
                {
                    _combat.OnButtonClick( (ItemNames)i );
                    break;
                }
            }
            //or just attack
            _combat.OnButtonClick(ItemNames.Punch);
        }
    }
}
