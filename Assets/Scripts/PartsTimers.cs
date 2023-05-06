using UnityEngine;

public class PartsTimers : MonoBehaviour
{
    private readonly static float _timeConstant = 60f; //tempConstant units of resource generate 1 click in second  
    private float[] _partTimer = new float[5];
    private float[] _currentTime = new float[5];
    public delegate void CounterReady(int index);
    public event CounterReady CounterIsReady;

    public void Init(DemonParts parts)
    {
        for (int i = 0; i < _currentTime.Length; i++)
            _currentTime[i] = 0;
        PartsCounterUpdated(parts);
    }
    public void PartsCounterUpdated(DemonParts parts)
    {
        for (int i = 0; i < _partTimer.Length; i++)
            _partTimer[i] = 0;

        if (parts.Blood > 0)
            _partTimer[0] = _timeConstant / (float)parts.Blood / 10f;
        if (parts.Armor > 0)
            _partTimer[1] = _timeConstant / (float)parts.Armor;
        if (parts.Wings > 0)
            _partTimer[2] = _timeConstant / (float)parts.Wings;
        if (parts.Weapons > 0)
            _partTimer[3] = _timeConstant / (float)parts.Weapons;
        if (parts.Horns > 0)
            _partTimer[4] = _timeConstant / (float)parts.Horns;
    }
    void Update()
    {
        for (int i = 0; i < _currentTime.Length; i++)
        {
            if (_partTimer[i] == 0)
                continue;
            else
            {
                _currentTime[i] += Time.deltaTime;
                if (_currentTime[i] >= _partTimer[i])
                {
                    _currentTime[i] = 0;
                    CounterIsReady?.Invoke(i);
                }
            }
        }
    }
}
