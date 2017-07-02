using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnButton : MonoBehaviour
{
    [SerializeField]
    private GameData.Towers _towerType;

    [SerializeField]
    private GameObject _locked;

    [SerializeField]
    private Button _button;

    private bool _spawning;

    public void SetEnabled(bool b)
    {
        _locked.SetActive(!b);
        _button.interactable = b;

        if (_spawning)
        {
            CancelConstruction();
        }
    }

    public void SpawnTower()
    {
        GameObject towerGO = PoolManager.Spawn("InvisibleTower");
        towerGO.GetComponent<InvisibleTower>().SetType(_towerType);

        _spawning = true;
    }

    private void CancelConstruction()
    {
        _spawning = false;
    }
}
