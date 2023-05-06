using UnityEngine;

public class Demon : MonoBehaviour
{
    [SerializeField] private CombatSystem _combat;
    private Counters _availabilityCounters = new Counters();
    private float _clickDelay;
    private float _currentTime;
    void Awake()
    {
        _combat = this.GetComponent<CombatSystem>();
        _combat.AvailabilityIsChanged+= onAvailabilityChanged;
    }
    public void Init(DemonParts parts, Counters availabilityCounters, ActiveEffects effects)
    {
        _clickDelay = effects.ClickDelay;
        _combat.Init( parts, availabilityCounters, effects);
    }
    public void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= _clickDelay)
        {
            _currentTime = 0;
            if (_availabilityCounters.Wrath > 0)
                _combat.OnButtonClick(5);
            else if (_availabilityCounters.Nimbus > 0)
                _combat.OnButtonClick(4);
            else if (_availabilityCounters.Swords > 0)
                _combat.OnButtonClick(3);
            else if (_availabilityCounters.Wings > 0)
                _combat.OnButtonClick(2);
            else if (_availabilityCounters.Armor > 0)
                _combat.OnButtonClick(1);
            else
                _combat.OnButtonClick(0);
        }
    }
    private void onAvailabilityChanged (Counters availabilityCounters)
    {
        _availabilityCounters = availabilityCounters;
    }
}
