using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonFiller : MonoBehaviour
{

    private Image _filler;
    //private RectTransform _buttonRect;
    //private float _fillByOneCount;
    void Awake()
    {
        _filler = transform.Find("FillerImage").GetComponent<Image>();
        if (_filler == null)
            Debug.LogError("ButtonFiller: no FillerImage RectTransform found");
        //_buttonRect = this.GetComponent<RectTransform>();
        //if (_buttonRect == null)
          //  Debug.LogError("ButtonFiller: no Button RectTransform found");

        //StartCoroutine(WaitUntilEndOfFrame());
    }
   /* IEnumerator WaitUntilEndOfFrame()
    {
        yield return new WaitForEndOfFrame();
        _fillByOneCount = _buttonRect.rect.height / 10f;
        StopCoroutine(WaitUntilEndOfFrame());
    }*/
    public void FillImage(int counter)
    {
        counter%=10;
        _filler.fillAmount = (1f -  (float)counter/10f);
        //_fillerRect.offsetMax = new Vector2(0, -_fillByOneCount * counter);
    }

}
