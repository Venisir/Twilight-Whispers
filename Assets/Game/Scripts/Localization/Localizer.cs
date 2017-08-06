using UnityEngine;
using UnityEngine.UI;

public class Localizer : MonoBehaviour
{
    public string m_key;
    public bool ManualText = false;

    private Text LL_label;

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
            if (!ManualText)
            {
                LL_label.text = LocalizationManager.Instance.GetText(m_key);
            }
        }
    }
}