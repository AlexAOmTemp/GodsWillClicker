using UnityEngine;
using System.Collections;

public class ResourceTimer : MonoBehaviour
{
    public delegate void CountFinish(int value);
    public event CountFinish CountIsFinished;

    #region Private Fields
    private readonly static float _timeConstant = 6f; //tempConstant units of resource generate 1/10 of click per second  
    const float GAME_UPDATE_TICK = 0.2f;
    private float _timeBeforeClick;
    private float _currentTime;
    private int _IncrementValue; // for optimization increase value of generated click parts
    #endregion

    #region Public Methods
   
    public void NewRoundStarted(int value)
    {
        _currentTime = 0;
        _IncrementValue = 1;
        ResourceCountChanged(value);
    }
    public void ResourceCountChanged(int value) //resourse value changed
    {
        calculateTime(value);
    }
    #endregion

    #region Private Methods
    private void Awake()
    {
        StartCoroutine(SlowUpdate());
        /*if (oneTime == false)
        {
            for (int i = 0; i < 400; i++)
                calculateTime(i, 0);
            oneTime = true;
        }*/ //tmp for calculateTime debugging
    }
    IEnumerator SlowUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(GAME_UPDATE_TICK);
            if (_timeBeforeClick > 0) //is timeBeforeClick set?
            {
                _currentTime += GAME_UPDATE_TICK;
                if (_currentTime >= _timeBeforeClick)
                {
                    _currentTime -= _timeBeforeClick;
                    CountIsFinished.Invoke(_IncrementValue);
                }
            }
        }
    }
    //working but need rework, while have to be removed.
    private void calculateTime(int value)
    {
        _timeBeforeClick = 0;
        _IncrementValue = 1;
        while (true)
        {
            _timeBeforeClick = _timeConstant / ((float)value / _IncrementValue);
            if (_timeBeforeClick < GAME_UPDATE_TICK)
                _IncrementValue++;
            else
                break;
        }
        //Debug.Log($"Res Quantity ={part}, value = {_IncrementValue}, time =  {_timeBeforeClick}");
    }
    #endregion
}
