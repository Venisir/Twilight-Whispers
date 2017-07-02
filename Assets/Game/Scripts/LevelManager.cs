using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    private PlayerController player1;

    //TODO
    [SerializeField]
    private List<string> _colorsNames;

    //TODO
    [SerializeField]
    private List<Material> _colorsMaterials;

    [SerializeField]
    private Light _sun;

    [SerializeField]
    private float _dayTime;

    [SerializeField]
    private float _nightTime;

    [SerializeField]
    private MyGrid grid;

    [SerializeField]
    private List<Portal> _portals;

    [SerializeField]
    private int _portalsToLose;

    private int _dayScore, _nightScore;

    private RaycastHit m_HitInfo = new RaycastHit();
    private Dictionary<string, Material> colorMaterials;
    private float _timer;
    private bool _day;
    private int _difficulty = 4;

    public override void Awake()
    {
        base.Awake();

        colorMaterials = new Dictionary<string, Material>();

        if (_colorsNames.Count != _colorsMaterials.Count)
            Debug.LogError("Error: discrepancias entre colores al inicializar");

        for (int i = 0; i < _colorsNames.Count; i++)
        {
            colorMaterials.Add(_colorsNames[i], _colorsMaterials[i]);
        }
    }

    private void Start()
    {
        _day = false;
        _timer = 0.0f;

        _dayScore = 0;
        _nightScore = 0;

        UIController.Instance.SetScore(_portalsToLose.ToString());
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            //SwitchPlayers();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out m_HitInfo))
            {
                MyTile tile = m_HitInfo.transform.GetComponent<MyTile>();
                if (tile != null && !tile.Occuped())
                {
                    tile.CreateTower(GameData.Towers.Bullets);
                    //grid.FindNeighbours(tile);
                }
            }
        }
        if (Input.GetMouseButtonDown(2))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out m_HitInfo))
            {
                MyTile tile = m_HitInfo.transform.GetComponent<MyTile>();
                if (tile != null && !tile.Occuped())
                {
                    tile.CreateTower(GameData.Towers.Lasers);
                    //grid.FindNeighbours(tile);
                }
            }
        }
        //if (Input.GetMouseButtonDown(2))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    if (Physics.Raycast(ray.origin, ray.direction, out m_HitInfo))
        //    {
        //        MyTile tile = m_HitInfo.transform.GetComponent<MyTile>();
        //        if (tile != null && !tile.Occuped())
        //        {
        //            tile.CreatePortal();
        //            //grid.FindNeighbours(tile);
        //        }
        //    }
        //}

        _timer -= Time.deltaTime;

        if (_timer <= 0f)
        {
            // Fin del día o noche

            if (_day)
            {
                
            }

            if (!_day)
            {
                SpawnEnemies(false);
                KillEnemies();
                KillPortals();
            }

            // Aumentamos las puntuaciones
            if (_day)
                _dayScore++;
            else
                _nightScore++;

            // Comienzo del nuevo timer
            _day = !_day;
            _timer = _day ? _dayTime : _nightTime;

            if (_day)
            {
                StartCoroutine(SpawnPortals());
                //TowerTime
            }

            if (!_day)
            {
                SpawnEnemies(true);
                //Fight
            }
        }


        // 0 -> min value (TODO?)

        if (_day)
            _sun.intensity = _timer / ((_day ? _dayTime : _nightTime) - 0);
        else
            _sun.intensity = 1 - (_timer / ((_day ? _dayTime : _nightTime) - 0));
    }

    public bool IsDay()
    {
        return _day;
    }

    public List<Portal> GetPortals()
    {
        return _portals;
    }

    public float RemainingTime()
    {
        return _timer;
    }

    public void SpawnEnemies(bool b)
    {
        GameObject[] spawns = GameObject.FindGameObjectsWithTag("Spawn");

        foreach (GameObject spawn in spawns)
        {
            spawn.GetComponent<Spawn>().SetEnabled(b);
        }
    }

    public void KillEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().Die();
        }
    }

    public IEnumerator SpawnPortals()
    {
        MyTile tile;

        for (int i = 0; i < _difficulty; i++)
        {
            do
            {
                int x = UnityEngine.Random.Range(0, grid.GetHeight());
                int y = UnityEngine.Random.Range(0, grid.GetWidth());

                tile = grid.GetTileAt(new Vector2(x, y));

            } while (tile == null || tile.Occuped());

            tile.CreatePortal();
            yield return Utils.WaitForRealSeconds(1.0f);
        }
    }

    public void KillPortals()
    {
        //GameObject[] portals = GameObject.FindGameObjectsWithTag("Portal");

        foreach (Portal portal in _portals)
        {
            portal.DestroyPortal();
        }

        _portals.Clear();
    }

    public void AddPortal(Portal p)
    {
        _portals.Add(p);
    }

    public void RemovePortal(Portal p)
    {
        _portals.Remove(p);
    }

    //public void SwitchPlayers()
    //{
    //    player1.SwitchPlayerId();
    //    player2.SwitchPlayerId();
    //}

    public Material GetMaterial(string color)
    {
        Material m = null;
        colorMaterials.TryGetValue(color, out m);
        return m;
    }

    public void DecrementPortalScore()
    {
        _portalsToLose--;

        UIController.Instance.SetScore(_portalsToLose.ToString());

        if (_portalsToLose <= 0)
        {
            //TODO
            //Game Over
            UIController.Instance.SetScore(LocalizationManager.Instance.GetText("_YOU_LOSE"));
        }
    }    
}
