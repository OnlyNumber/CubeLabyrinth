using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocalizedText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;
    
    private string key;

    private void Start()
    {
        Init();
        Localize();
        LocalizationManager.OnLanguageChange += OnLanguageChange;

    }

    private void OnLanguageChange()
    {
        Localize();
    }

    private void Init()
    {
        key = text.text;
    }

    public void Localize(string newKey = null)
    {
        Debug.Log(gameObject.name);

        if(text == null)
        {

            Debug.Log("text == null");
            Init();
        }

        if (newKey != null)
        {
            key = newKey;
        }

        text.text = LocalizationManager.GetTranslate(key);

    }


}
