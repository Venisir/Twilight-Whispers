using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private float _radius = 10f;

    [SerializeField]
    private float _attackDelay = 0.5f;

    [SerializeField]
    private float _bulletSpeed = 10f;

    [SerializeField]
    private float _bulletDamage = 7f;

    [SerializeField]
    private float _laserDamage = 7f;

    [SerializeField]
    private RadiusZone m_radiusZone;

    [SerializeField]
    private GameObject m_bullet;

    [SerializeField]
    private ParticleSystem m_laser;

    [SerializeField]
    private GameObject m_light;

    [SerializeField]
    private GameData.Towers _type;

    private List<Enemy> _enemies;
    private AudioSource _audioSource;
    private float _currentAttackDelay;

    // Use this for initialization
    void Start()
    {
        _enemies = new List<Enemy>();
        _audioSource = GetComponent<AudioSource>();
        _currentAttackDelay = _attackDelay;

        m_radiusZone.SetRadius(_radius);
    }

    // Update is called once per frame
    void Update()
    {
        _currentAttackDelay -= Time.deltaTime;
        if (_currentAttackDelay <= 0.0f)
        {
            _enemies.RemoveAll(item => item == null);
            _enemies.RemoveAll(item => item.GetState() != GameData.EnemyStates.Walking);

            if (_enemies.Count > 0)
            {
                Debug.Log("TowerAttack " + _enemies[0]);
                _currentAttackDelay = _attackDelay;
                Shoot();
            }
        }
    }

    public void AddEnemy(Enemy en)
    {
        _enemies.Add(en);
    }

    public void RemoveEnemy(Enemy en)
    {
        _enemies.Remove(en);
    }

    private void Shoot()
    {
        switch (_type)
        {
            case (GameData.Towers.Bullets):

                _audioSource.Play();
                //GameObject bulletGO = Instantiate(m_bullet) as GameObject;
                GameObject bulletGO = PoolManager.Spawn(m_bullet);

                Bullet bullet = bulletGO.GetComponent<Bullet>();

                bullet.SetSpeed(_bulletSpeed);
                bullet.SetDamage(_bulletDamage);
                bullet.SetObjective(_enemies[0]);

                bulletGO.transform.localPosition = m_light.transform.position;

                break;

            case (GameData.Towers.Lasers):

                _audioSource.Play();
                m_laser.transform.LookAt(_enemies[0].gameObject.transform.position, Vector3.back);
                m_laser.Play();
                m_laser.loop = false;


                _enemies[0].Damage(_laserDamage);

                break;
        }
    }
}
