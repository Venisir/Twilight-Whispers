using System;
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
    private GameObject m_spell;
    
    [SerializeField]
    private Animation _animation;
    
    [NonSerialized]
    private GameData.PlayerStates _state;

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
        _state = GameData.PlayerStates.Idle;
        _animation.CrossFade("Idle");
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out m_HitInfo))
        {
            this.transform.LookAt(new Vector3(m_HitInfo.point.x, this.transform.position.y, m_HitInfo.point.z));
        }

        if(!_animation.isPlaying)
        {
            _state = GameData.PlayerStates.Idle;
            _animation.CrossFade("Idle");
        }


        _currentAttackDelay -= Time.deltaTime;

        if (Input.GetMouseButton(1) && _state != GameData.PlayerStates.Attacking)
        {
            //if (_currentAttackDelay <= 0.0f)
            //{
                Debug.Log("Disparando");
                //_currentAttackDelay = _attackDelay;

                _animation.CrossFade("Staff Swing");

                _state = GameData.PlayerStates.Attacking;
                StartCoroutine(Shoot());
            //}
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (moveHorizontal != 0 || moveVertical != 0)
        {
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            if (_state == GameData.PlayerStates.Walking || _state == GameData.PlayerStates.Idle)
            {
                rb.AddForce(movement.normalized * _speed);
                if (!_animation.IsPlaying("Run"))
                    _animation.CrossFade("Run");
            }

            Debug.Log(Input.GetAxis("Horizontal") + "   " + Input.GetAxis("Vertical") + " - Force: " + (movement.normalized * _speed));
        }
        else
        {
            if(_state != GameData.PlayerStates.Attacking)
                if (!_animation.IsPlaying("Idle"))
                    _animation.CrossFade("Idle");
        }
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.5f);

        GameObject spellGO = Instantiate(m_spell) as GameObject;

        Spell spell = spellGO.GetComponent<Spell>();

        spell.SetSpeed(_bulletSpeed);
        spell.SetDamage(_bulletDamage);
        spell.SetDirection(this.transform.forward);

        spellGO.transform.localPosition = this.transform.position + Vector3.forward;
        //spellGO.transform.LookAt(Vector3.back);
    }

    public GameData.PlayerStates GetState()
    {
        return _state;
    }
}