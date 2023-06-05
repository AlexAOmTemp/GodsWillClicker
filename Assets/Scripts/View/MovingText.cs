using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MovingText : MonoBehaviour
{
    private float _endPosition;
    private RectTransform _rectTransform;
    private TMP_Text _text;
    private float _speed;
    private bool _isSet;

    public void PlayText(Vector3 startPosition, float endPositionY, float speed, string text, Color color)
    {
        _endPosition= endPositionY;
        _rectTransform.position = startPosition;
        _speed=speed;
        _text.SetText(text);
        _text.color = color;
        _isSet = true;
    }
    void Awake()
    {
        _rectTransform = this.GetComponent<RectTransform>();
        if (_rectTransform == null)
            Debug.LogError("MovingText: no RectTransform found");
        _text = this.GetComponent<TextMeshProUGUI>();
        if (_text == null)
            Debug.LogError("MovingText: no TextMeshProUGUI found");
        RoundController.NewRoundIsStarted += selfDestroy;
    }
    void Update()
    {
        if (_isSet == true)
        {
            if (_rectTransform.position.y < _endPosition)
                _rectTransform.position += Vector3.up * 50 * Time.deltaTime;
            else
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
