using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    private PlayerController player1;

    [SerializeField]
    private MyGrid grid;

    [SerializeField]
    private Light _sun;

    [SerializeField]
    private float _dayTime;

    [SerializeField]
    private float _nightTime;
    
    [SerializeField]
    private int _portalsToLose;

    private List<Portal> _portals;

    private int _dayScore, _nightScore;
    private bool init;
    private bool _finished = false;

    private RaycastHit m_HitInfo = new RaycastHit();
    private Dictionary<string, Material> colorMaterials;
    private float _timer;
    private bool _day;
    //TODO
    private int _difficulty = 4;

    public override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        _portals = new List<Portal>();

        _day = true;
        _timer = _dayTime;
        _dayScore = 0;
        _nightScore = 0;

        UIController.Instance.SetRemainingPortals(_portalsToLose);
        UIController.Instance.SetWaves(0);
    }

    private void Update()
    {
        if (_finished)
            return;

        if (!init)
        {
            StartCoroutine(SpawnPortals());
            UIController.Instance.SetButtons(true);

            init = true;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }

        _timer -= Time.deltaTime;

        if (_timer <= 0f)
        {
            // Aumentamos las puntuaciones
            if (_day)
                _dayScore++;
            else
                _nightScore++;
            
            // Fin del día o noche
            if (_day)
            {

            }

            if (!_day)
            {
                SpawnEnemies(false);
                KillEnemies();
                KillPortals();
                UIController.Instance.SetWaves(_nightScore);
            }

            // Comienzo del nuevo timer
            _day = !_day;
            _timer = _day ? _dayTime : _nightTime;

            if (_day)
            {
                StartCoroutine(SpawnPortals());
                //TowerTime
                UIController.Instance.SetButtons(true);
            }

            if (!_day)
            {
                UIController.Instance.SetButtons(false);
                SpawnEnemies(true);
                //Fight
            }
        }
        
        if (_day)
            _sun.intensity = _timer / ((_day ? _dayTime : _nightTime) - 0);
        else
            _sun.intensity = 1 - (_timer / ((_day ? _dayTime : _nightTime) - 0));
    }

    public PlayerController GetPlayer()
    {
        return player1;
    }

    public MyGrid GetGrid()
    {
        return grid;
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

    public void AddPortal(Portal p)
    {
        _portals.Add(p);
    }

    public void RemovePortal(Portal p)
    {
        _portals.Remove(p);
    }

    public int GetPortalsToLose()
    {
        return _portalsToLose;
    }

    public void DecrementPortalScore()
    {
        _portalsToLose--;

        UIController.Instance.SetRemainingPortals(_portalsToLose);

        if (_portalsToLose <= 0)
        {
            //Game Over
            _finished = true;
            player1.Die();

            UIController.Instance.GameOver();
        }

        if (_portals.Count == 0)
        {
            _timer = 0;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Menu");
    }

    private void SpawnEnemies(bool b)
    {
        GameObject[] spawns = GameObject.FindGameObjectsWithTag("Spawn");

        foreach (GameObject spawn in spawns)
        {
            spawn.GetComponent<Spawn>().SetEnabled(b);
        }
    }

    private void KillEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().Die();
        }
    }

    private IEnumerator SpawnPortals()
    {
        MyTile tile;

        for (int i = 0; i < _difficulty; i++)
        {
            do
            {
                //TODO improve
                int x = Random.Range(0, grid.GetHeight());
                int y = Random.Range(0, grid.GetWidth());

                tile = grid.GetTileAt(new Vector2(x, y));

            } while (tile == null || tile.IsOccuped());

            tile.CreatePortal();
            yield return Utils.WaitForRealSeconds(1.0f);
        }
    }

    private void KillPortals()
    {
        foreach (Portal portal in _portals)
        {
            portal.DestroyPortal();
        }

        _portals.Clear();
    }
}
