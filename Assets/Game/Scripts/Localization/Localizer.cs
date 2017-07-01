using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// This class updates the text of UI Label with the international text.
/// </summary>
public class Localizer : MonoBehaviour
{
    public string m_key;
    public bool ManualText = false;
    public bool ManualFont = false;

    private Text LL_label;
    // Use this for initialization
    void Awake()
    {
        LL_label = GetComponent<Text>();
    }

    void Start()
    {
        LocalizationManager lm = LocalizationManager.Instance;
        lm.AddLabel(this);
        if (LL_label)
        {
            Font newFont = lm.GetFont();
            if (newFont != null && !ManualFont)
                LL_label.font = newFont;
            if (!ManualText)
            {
                LL_label.text = lm.GetText(m_key);
            }
        }
    }

    public void Restart()
    {
        if (LL_label)
        {
            Font newFont = LocalizationManager.Instance.GetFont();
            if (newFont != null && !ManualFont)
                LL_label.font = newFont;
            if (!ManualText)
            {
                LL_label.text = LocalizationManager.Instance.GetText(m_key);
            }
        }
    }
}