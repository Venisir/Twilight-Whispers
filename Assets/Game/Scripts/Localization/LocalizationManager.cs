using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class LocalizationManager : Singleton<LocalizationManager>
{
    [System.Serializable]
    public class FontData
    {
        public GameData.Languages name = GameData.Languages.English;
        public Font font;
        public Material fontMaterial;
        public TextAsset localizations;
    }

    public List<FontData> font;
    private int[] m_langIndexs;
    private int m_keyIndex;

    private Dictionary<string, string> m_localization;
    private List<Dictionary<string, string>> m_localizations;
    private int currentFont = -1;
    private List<Localizer> m_iLabels;

    public void ChangeLanguage(GameData.Languages newLanguage)
    {
        SetLanguageFont(newLanguage.ToString().ToLowerInvariant());
        LoadLanguage((GameData.Languages)currentFont);
        int count = m_iLabels.Count;
        for (int i = 0; i < count; i++)
        {
            m_iLabels[i].Restart();
        }
    }

    public string GetText(string key)
    {
        string text = null;

        if (m_localization.TryGetValue(key, out text))
        {
            return text;
        }

        return key;
    }

    public Font GetFont()
    {
        if (currentFont == -1)
            return null;

        return font[currentFont].font;
    }

    public Material GetFontMaterial()
    {
        if (currentFont == -1)
            return null;

        return font[currentFont].fontMaterial;
    }

    public void RestartLabelLists()
    {
        m_iLabels = new List<Localizer>();
    }

    public void AddLabel(Localizer label)
    {
        m_iLabels.Add(label);
    }

    public override void Awake()
    {
        base.Awake();

        RestartLabelLists();
        m_localization = new Dictionary<string, string>();

        GameData.Languages lang = GameData.Languages.English;

        for (int i = 0; i < font.Count; i++)
        {
            if (font[i].name.ToString().ToLower() == Application.systemLanguage.ToString().ToLower())
            {
                lang = (GameData.Languages)i;
            }
        }

        if (PlayerPrefs.GetInt("Language") != -1)
        {
            lang = (GameData.Languages)PlayerPrefs.GetInt("Language");
        }

        ChangeLanguage(lang);
    }

    private void SetLanguageFont(string newLanguage)
    {
        for (int i = 0; i < font.Count; i++)
        {
            if (newLanguage.Equals(font[i].name.ToString().ToLowerInvariant()))
            {
                currentFont = i;
                PlayerPrefs.SetInt("Language", currentFont);
                break;
            }
        }
    }

    private void LoadLanguage(GameData.Languages newLanguage)
    {
        //m_localization.Clear();

        if (m_localizations == null)
            ReadLocalizations();

        m_localization = m_localizations[(int)newLanguage];
    }

    private void ReadLocalizations()
    {
        m_localizations = new List<Dictionary<string, string>>();

        foreach (FontData fontData in font)
        {
            TextAsset textAsset = fontData.localizations;

            TinyTsvReader tsvDoc = new TinyTsvReader(textAsset.text);
            if (tsvDoc == null)
                return;

            Dictionary<string, string> localizationAux = new Dictionary<string, string>();

            string fs = textAsset.text;
            string[] fLines = Regex.Split(fs, "\n");

            for (int i = 0; i < fLines.Length; i++)
            {
                string valueLine = fLines[i];
                string[] values = Regex.Split(valueLine, " = ");

                string s;
                if (!localizationAux.TryGetValue(values[0], out s))
                    localizationAux.Add(values[0], values[1]);
                else
                    Debug.Log("Duplicated key! " + values[0]);
            }
            m_localizations.Add(localizationAux);
        }
    }
}