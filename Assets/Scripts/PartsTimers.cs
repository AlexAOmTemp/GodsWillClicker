using UnityEngine;
using System.Collections;
public class PartsTimers : MonoBehaviour
{
    [SerializeField] private RoundController _roundController;
    private CombatSystem _combatSystem;
    private readonly static float _timeConstant = 60f; //tempConstant units of resource generate 1 click in second  
    private float[] _partTimer = new float[5];
    private float[] _currentTime = new float[5];
    private int[] _IncrementValue = new int[5]; // for optimization increase value of clicks 
    public delegate void CounterReady(int index);
    public event CounterReady CounterIsReady;
    static bool oneTime = false;
    void Awake()
    {
        _combatSystem = this.GetComponent<CombatSystem>();
        _combatSystem.PartsCounterIsChanged += PartsCounterUpdated;
        _roundController.NewRoundHasStarted += onNewRoundStarted;
        StartCoroutine(SlowUpdate());
        /*if (oneTime == false)
        {
            for (int i = 0; i < 400; i++)
                calculateTime(i, 0);
            oneTime = true;
        }*/ //tmp for calculateTime debugging

    }
    private void onNewRoundStarted(int stage)
    {
        for (int i = 0; i < _currentTime.Length; i++)
            _currentTime[i] = 0;
        for (int i = 0; i < _IncrementValue.Length; i++)
            _IncrementValue[i] = 1;
    }
    public void PartsCounterUpdated(DemonParts parts) //resourse value changed
    {
        for (int i = 0; i < _partTimer.Length; i++)
            _partTimer[i] = 0;
        if (parts.Blood > 0)
            calculateTime(parts.Blood, 0);
        if (parts.Armor > 0)
            calculateTime(parts.Armor, 1);
        if (parts.Weapons > 0)
            calculateTime(parts.Weapons, 2);
        if (parts.Wings > 0)
            calculateTime(parts.Wings, 3);
        if (parts.Horns > 0)
            calculateTime(parts.Horns, 4);

    }
    const float GAME_UPDATE_TICK = 0.2f;
    IEnumerator SlowUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(GAME_UPDATE_TICK);
            for (int i = 0; i < _currentTime.Length; i++)
            {
                if (_partTimer[i] == 0) //not set no action needed
                    continue;
                else
                {
                    _currentTime[i] += GAME_UPDATE_TICK;
                    if (_currentTime[i] >= _partTimer[i])
                    {
                        _currentTime[i] -= _partTimer[i];
                        _combatSystem.OnTimerReady(i, _IncrementValue[i]);
                    }
                }
            }
        }
    }
    //working but need rework, while have to be removed.
    private void calculateTime(int part, int index)
    {
        _IncrementValue[index] = 1;
        while (true)
        {
            _partTimer[index] = _timeConstant / ((float)part / _IncrementValue[index]) / 10f;
            if (_partTimer[index] < GAME_UPDATE_TICK)
                _IncrementValue[index]++;
            else
                break;
        }
        Debug.Log($"Res Quantity ={part}, value = {_IncrementValue[index]}, time =  {_partTimer[index]}");
    }
}
