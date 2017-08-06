using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIController : Singleton<UIController>
{
    [SerializeField]
    private Text _timer;

    [SerializeField]
    private Text _tutorial;

    [SerializeField]
    private Text _portalsToLose;

    [SerializeField]
    private Text _waves;

    [SerializeField]
    private List<SpawnButton> _towerButtons;

    private void Start()
    {
        StartCoroutine(ShowTutorial());
    }

    void Update()
    {
        if (LevelManager.Instance.IsDay())
            _timer.text = LocalizationManager.Instance.GetText("_TIME_TO_NIGHT") + ": " + Utils.TimeFormattedStringFromSeconds(LevelManager.Instance.RemainingTime());
        else
            _timer.text = LocalizationManager.Instance.GetText("_TIME_TO_DAY") + ": " + Utils.TimeFormattedStringFromSeconds(LevelManager.Instance.RemainingTime());
    }

    public void SetRemainingPortals(int score)
    {
        _portalsToLose.text = score.ToString();
    }

    public void SetWaves(int score)
    {
        _waves.text = score.ToString();
    }

    public void SetButtons(bool b)
    {
        foreach (SpawnButton sb in _towerButtons)
            sb.SetEnabled(b);
    }

    public void SetPauseButtons(bool b)
    {
        foreach (SpawnButton sb in _towerButtons)
            sb.SetPaused(b);
    }

    private IEnumerator ShowTutorial()
    {
        _tutorial.gameObject.SetActive(true);
        _tutorial.text = LocalizationManager.Instance.GetText("_TUTORIAL_1");

        yield return new WaitForSeconds(6);
        _tutorial.text = string.Format(LocalizationManager.Instance.GetText("_TUTORIAL_2"), LevelManager.Instance.GetPortalsToLose());

        yield return new WaitForSeconds(6);
        _tutorial.gameObject.SetActive(false);
    }
}
