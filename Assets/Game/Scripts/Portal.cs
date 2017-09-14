using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private List<Enemy> _enemies;
    
    void Start()
    {
        _enemies = new List<Enemy>();
    }
    
    void Update()
    {

    }

    public void DestroyPortal()
    {
        _enemies.RemoveAll(item => item == null);
        _enemies.RemoveAll(item => item.GetState() != GameData.EnemyStates.Walking);

        foreach (Enemy enemy in _enemies)
        {
            enemy.CallRecalculateDestination();
        }

        List<ParticleSystem> particleSystems = this.GetComponentsInChildren<ParticleSystem>().ToList();
        foreach (ParticleSystem ps in particleSystems)
            ps.enableEmission = false;

        Destroy(this.gameObject, 2.5f);
        Destroy(this);
    }

    public void AddEnemy(Enemy en)
    {
        _enemies.Add(en);
    }

    public void RemoveEnemy(Enemy en)
    {
        _enemies.Remove(en);
    }
}
