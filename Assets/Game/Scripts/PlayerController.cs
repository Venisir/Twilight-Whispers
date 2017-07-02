using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.0f;

    [SerializeField]
    private float _hp = 100.0f;

    [SerializeField]
    private Slider _hpBar;

    [SerializeField]
    private float _attackDelay = 0.5f;

    [SerializeField]
    private float _bulletSpeed = 10f;

    [SerializeField]
    private float _bulletDamage = 7f;

    [SerializeField]
    private GameObject m_bullet;

    [SerializeField]
    private ParticleSystem m_laser;

    private float _currentAttackDelay;   
    private Rigidbody rb;
    private RaycastHit m_HitInfo = new RaycastHit();

    void Awake()
    {
        //_hpBar.maxValue = _hp;
        //_hpBar.value = _hp;

        _currentAttackDelay = _attackDelay;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out m_HitInfo))
        {
            this.transform.LookAt(new Vector3(m_HitInfo.point.x, this.transform.position.y, m_HitInfo.point.z));
        }


        _currentAttackDelay -= Time.deltaTime;

        if (Input.GetMouseButton(1))
        {
            Debug.Log("Disparando");

            if (_currentAttackDelay <= 0.0f)
            {
                //_enemies.RemoveAll(item => item == null);
                //_enemies.RemoveAll(item => item.GetState() != GameData.EnemyStates.Walking);
                //
                //if (_enemies.Count > 0)
                //{
                //    Debug.Log("TowerAttack " + _enemies[0]);
                //    _currentAttackDelay = _attackDelay;
                //    Shoot();
                //}
            }
            Shoot();
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        
        rb.AddForce(movement.normalized * _speed);


        Debug.Log(Input.GetAxis("Horizontal") + "   " + Input.GetAxis("Vertical") + " - Force: " + (movement.normalized * _speed));
    }

    private void Shoot()
    {
        GameObject spellGO = Instantiate(m_bullet) as GameObject;

        Spell spell = spellGO.GetComponent<Spell>();

        spell.SetSpeed(_bulletSpeed);
        spell.SetDamage(_bulletDamage);
        //spell.SetDirection(this.transform - Input.Mo);

        spellGO.transform.localPosition = this.transform.position + Vector3.forward;

        //m_laser.transform.LookAt(_enemies[0].gameObject.transform.position, Vector3.back);
    }
}