using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonOnClick : MonoBehaviour
{
    public Image imageTemplate;
    public TextMeshProUGUI textTempalte;

    public void ChangeSprite(Sprite sprite)
    {
        imageTemplate.sprite = sprite;
    }

    public void ChangeText(string text)
    {
        textTempalte.text = text;
    }
}
