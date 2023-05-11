using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;

public class LocalizationManager : MonoBehaviour
{
    public static int SelectedLanguage { get; private set; }

    public static event Action OnLanguageChange;

    private static Dictionary<string, List<string>> _localization;

    [SerializeField]
    private TextAsset _localizationFile;

    // Start is called before the first frame update
    void Awake()
    {
        if(_localization == null)
        LoadLocalization();
    }

    public void SetLanguage(int language)
    {
        SelectedLanguage = language;
        OnLanguageChange?.Invoke();
    }

    private void LoadLocalization()
    {
        _localization = new Dictionary<string, List<string>>();

        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(_localizationFile.text);

        foreach (XmlNode key in xmlDocument["Keys"].ChildNodes)
        {
            string keyStr = key.Attributes["Name"].Value;

            var values = new List<string>();

            foreach(XmlNode translate in key["Translate"].ChildNodes)
            {
                values.Add(translate.InnerText);
            }

            _localization[keyStr] = values;
        }
    }

    public static string GetTranslate(string key, int languageId = -1)
    {
        Debug.Log(key);

        if(languageId == -1)
        {
            languageId = SelectedLanguage;
        }

        foreach (var item in _localization.Keys)
        {
            Debug.Log(item);
        }

        if(_localization.ContainsKey(key))
        {
            return _localization[key][languageId];
        }

        return key;

    }

}
