using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MovingText : MonoBehaviour
{
    private RectTransform _startPosition;
    private float _endPosition;
    private RectTransform _rectTransform;
    private TMP_Text _text;
    private float _speed;
    private bool _isSet;
    void Start()
    {
        _rectTransform = this.GetComponent<RectTransform>();
        _text = this.GetComponent<TextMeshProUGUI>();
    }
    public void PlayText(RectTransform startPosition, float endPositionY, float speed, string text, Color color)
    {
        _endPosition= endPositionY;
        _rectTransform.position = startPosition.position;
        _speed=speed;
        _text.SetText(text);
        _text.color = color;
        _isSet = true;
    }

    void Update()
    {
        if (_isSet == true)
        {
            if (_rectTransform.position.y < _endPosition)
                _rectTransform.Translate(Vector3.up * _speed * Time.deltaTime);
            else
                Destroy(this);
        }
    }
}
