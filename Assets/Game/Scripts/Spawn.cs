using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _spawnItems;

    [SerializeField]
    private Transform _spawnPoint;

    [SerializeField]
    private ParticleSystem _particles;

    [SerializeField]
    private float _spawnDelay = 5.0f;

    private bool _enabled = true;
    private float _currentTime = 0.0f;

    public void Start()
    {
        _currentTime = 0;
        _enabled = false;
    }

    public void Update()
    {
        if (_enabled)
        {
            _currentTime -= Time.deltaTime;
            if (_currentTime <= 0.0f)
            {
                _particles.Play();

                StartCoroutine(SpawnAfterWait(1.0f));
                _currentTime = _spawnDelay;
            }
        }
    }

    private IEnumerator SpawnAfterWait(float s)
    {
        AudioManager.Instance.PlaySFX("spawn");

        yield return new WaitForSeconds(s);
        
        GameObject enemyGO = PoolManager.Spawn(_spawnItems[Random.Range(0, _spawnItems.Count)]);

        Enemy enemy = enemyGO.GetComponent<Enemy>();
        
        enemy.transform.localPosition = _spawnPoint.position;
        enemy.ChooseBestDestination();
    }

    public void SetEnabled(bool b)
    {
        _enabled = b;
        StopAllCoroutines();
    }
}
