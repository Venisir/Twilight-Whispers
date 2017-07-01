using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Text _timer;

    void Start()
    {

    }

    void Update()
    {
        if(LevelManager.Instance.IsDay())
            _timer.text = LocalizationManager.Instance.GetText("_TIME_TO_DAY") + ": " + Utils.TimeFormattedStringFromSeconds(LevelManager.Instance.RemainingTime());
        else
            _timer.text = LocalizationManager.Instance.GetText("_TIME_TO_NIGHT") + ": " + Utils.TimeFormattedStringFromSeconds(LevelManager.Instance.RemainingTime());
    }
}
