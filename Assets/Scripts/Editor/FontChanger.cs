using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
public class FontChanger 
{
    
    [MenuItem("Tools/Change Fonts")]
    private static void changeFonts()
    {
        var fonts = Transform.FindAnyObjectByType<FontPack>();
        FontGroup[] textObjects = (FontGroup[])Transform.FindObjectsByType(typeof(FontGroup), FindObjectsSortMode.None);
        foreach (var textObject in textObjects)
        {
            if (textObject.Number >= fonts.fonts.Count)
                textObject.Number = 0;
            textObject.GetComponent<TMP_Text>().font = fonts.fonts[textObject.Number];
        }
    }
    [MenuItem("Tools/Add Font Group To All TMP_Text")]
    private static void addFontGroup()
    {
        TMP_Text[] textObjects = (TMP_Text[])Transform.FindObjectsByType(typeof(TMP_Text), FindObjectsSortMode.None);
        foreach (var textObject in textObjects)
        {
            FontGroup fontGroup;
            if (textObject.TryGetComponent<FontGroup>(out fontGroup) == false)
            {
                textObject.gameObject.AddComponent<FontGroup>();
            }
        }
    }

}
