using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLocalize : MonoBehaviour
{
    [SerializeField]
    private LocalizedText text;



    public void LocalizeText()
    {
        text.Localize("Text2_key");
    }
}
