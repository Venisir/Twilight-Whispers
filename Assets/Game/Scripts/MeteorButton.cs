using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorButton : MonoBehaviour
{
    [SerializeField]
    private GameObject _meteorPrefab;

    public void Meteor()
    {
        GameObject meteorGO = PoolManager.Spawn(_meteorPrefab);
        Destroy(meteorGO, 5.0f);
    }
}
