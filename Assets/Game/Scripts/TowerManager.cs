using UnityEngine;

public class TowerManager : Singleton<TowerManager>
{
    [SerializeField]
    private GameObject fireTowerPrefab;

    [SerializeField]
    private GameObject magicTowerPrefab;

    [SerializeField]
    private GameObject laserTowerPrefab;

    public GameObject GetTower(GameData.Towers type)
    {
        switch (type)
        {
            case GameData.Towers.Fire:
                return fireTowerPrefab;

            case GameData.Towers.Magic:
                return magicTowerPrefab;

            case GameData.Towers.Laser:
                return laserTowerPrefab;
        }
        return null;
    }
}
