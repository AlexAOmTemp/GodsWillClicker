using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectFall : MonoBehaviour
{
    private float _speed;
    private Vector3 _target;
    private float _arcHeight;
    private Vector3 _startPosition;
    private float _stepScale;
    private float _progress;
    private bool _isArrived = false;
    private bool _isInitialized = false;
    private float _timeBeforeDeath = 2.0f;

    public void Init(RectTransform groundTransform)
    {
        _startPosition = this.transform.position;
        this.transform.position = _startPosition;
        _target = new Vector3(Random.Range(_startPosition.x, groundTransform.position.x),
            groundTransform.position.y, groundTransform.position.z);
        _arcHeight = Random.Range(40f, 100f);
        _speed = Random.Range(60f, 120f);
        float distance = Vector3.Distance(_startPosition, _target);

        // This is one divided by the total flight duration, to help convert it to 0-1 progress.
        _stepScale = _speed / distance;
        RoundController.NewRoundIsStarted += selfDestroy;
        _isInitialized = true;
    }

    private void Update()
    {
        if (!_isInitialized)
            return;

        if (_isArrived == false)
        {
            _progress = Mathf.Min(_progress + Time.deltaTime * _stepScale, 1.0f);
            float parabola = 1.0f - 4.0f * (_progress - 0.5f) * (_progress - 0.5f);
            Vector3 nextPos = Vector3.Lerp(_startPosition, _target, _progress);
            nextPos.y += parabola * _arcHeight;
            transform.position = nextPos;
            if (_progress == 1.0f)
                _isArrived = true;
        }

        if (_isArrived)
        {
            _timeBeforeDeath -= Time.deltaTime;
            if (_timeBeforeDeath <= 0)
                Destroy(this.gameObject);
        }
    }

    private void selfDestroy(int stage)
    {
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        RoundController.NewRoundIsStarted -= selfDestroy;
    }
}