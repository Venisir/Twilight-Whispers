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
    private float _attackDelay = 0.5f;

    [SerializeField]
    private float _bulletSpeed = 10f;

    [SerializeField]
    private float _bulletDamage = 7f;

    [SerializeField]
    private GameObject m_spell;

    [SerializeField]
    private Animation _animation;

    [SerializeField]
    private bool _asdwMovement;

    [NonSerialized]
    private GameData.PlayerStates _state;

    private float _currentAttackDelay;
    private Rigidbody _rigibody;
    private AudioSource _audioSource;
    private RaycastHit m_HitInfo = new RaycastHit();

    void Awake()
    {
        _currentAttackDelay = _attackDelay;
    }

    private void Start()
    {
        _rigibody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
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

        if (!_animation.isPlaying)
        {
            _state = GameData.PlayerStates.Idle;
            _animation.CrossFade("Idle");
        }

        _currentAttackDelay -= Time.deltaTime;

        if (Input.GetMouseButton(1) && _state != GameData.PlayerStates.Attacking)
        {
            _audioSource.Play();
            _animation.CrossFade("Staff Swing");

            _state = GameData.PlayerStates.Attacking;
            StartCoroutine(Shoot());
        }
    }

    void FixedUpdate()
    {
        if (_asdwMovement == true)
        {
            // Movimiento 1 (ASDW)
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            if (moveHorizontal != 0 || moveVertical != 0)
            {
                Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

                if (_state == GameData.PlayerStates.Walking || _state == GameData.PlayerStates.Idle)
                {
                    _rigibody.AddForce(movement.normalized * _speed);
                    if (!_animation.IsPlaying("Run"))
                        _animation.CrossFade("Run");
                }
            }
            else
            {
                if (_state != GameData.PlayerStates.Attacking)
                    if (!_animation.IsPlaying("Idle"))
                        _animation.CrossFade("Idle");
            }
        }
        else
        {
            //Movimiento 2 (click a posición)
            if (Input.GetMouseButton(0) && _state != GameData.PlayerStates.Attacking)
            {
                _rigibody.AddForce(transform.forward * _speed);
                if (!_animation.IsPlaying("Run"))
                    _animation.CrossFade("Run");
            }
            else
            {
                if (_state != GameData.PlayerStates.Attacking)
                    if (!_animation.IsPlaying("Idle"))
                        _animation.CrossFade("Idle");
            }
        }
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.5f);
        
        GameObject spellGO = PoolManager.Spawn(m_spell);

        Spell spell = spellGO.GetComponent<Spell>();

        spell.SetSpeed(_bulletSpeed);
        spell.SetDamage(_bulletDamage);
        spell.SetDirection(this.transform.forward);

        spellGO.transform.localPosition = this.transform.position + Vector3.forward;
    }

    public GameData.PlayerStates GetState()
    {
        return _state;
    }

    public void SwitchASDWMovement()
    {
        _asdwMovement = !_asdwMovement;
    }
}