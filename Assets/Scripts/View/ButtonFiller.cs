using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonFiller : MonoBehaviour
{
    private Image _filler;

    private void Awake()
    {
        _filler = transform.Find("FillerImage").GetComponent<Image>();
        if (_filler == null)
            Debug.LogError("ButtonFiller: no FillerImage RectTransform found");
    }

    public void FillImage(int counter)
    {
        counter %= 10;
        _filler.fillAmount = (1f - (float) counter / 10f);
    }
}