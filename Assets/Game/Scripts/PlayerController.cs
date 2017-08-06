using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.0f;

    //TODO
    [SerializeField]
    private float _hp = 100.0f;
    
    [SerializeField]
    private float _manaGenerationMultiplier = 1f;

    [SerializeField]
    private float _attackDelay = 0.5f;

    [SerializeField]
    private float _bulletSpeed = 10f;

    [SerializeField]
    private float _bulletDamage = 7f;

    [SerializeField]
    private GameObject m_spell;

    [SerializeField]
    private GameObject _meteorPrefab;
    
    [SerializeField]
    private Animation _animation;

    [SerializeField]
    private bool _asdwMovement;

    [NonSerialized]
    private GameData.PlayerStates _state;

    private float _mana = 0f;
    private float _currentAttackDelay;
    private bool _movementEnabled;
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
        _animation.CrossFade(GameConstants.ANIM_IDLE);
        _movementEnabled = true;
    }

    void Update()
    {
        AddMana((Time.deltaTime * _manaGenerationMultiplier));

        if (!_movementEnabled)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out m_HitInfo))
        {
            this.transform.LookAt(new Vector3(m_HitInfo.point.x, this.transform.position.y, m_HitInfo.point.z));
        }

        if (!_animation.isPlaying)
        {
            _state = GameData.PlayerStates.Idle;
            _animation.CrossFade(GameConstants.ANIM_IDLE);
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
        if (EventSystem.current.IsPointerOverGameObject() || !_movementEnabled)
            return;

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
                    if (!_animation.IsPlaying(GameConstants.ANIM_RUN))
                        _animation.CrossFade(GameConstants.ANIM_RUN);
                }
            }
            else
            {
                if (_state != GameData.PlayerStates.Attacking)
                    if (!_animation.IsPlaying(GameConstants.ANIM_IDLE))
                        _animation.CrossFade(GameConstants.ANIM_IDLE);
            }
        }
        else
        {
            //Movimiento 2 (click a posición)
            if (Input.GetMouseButton(0) && _state != GameData.PlayerStates.Attacking)
            {
                _rigibody.AddForce(transform.forward * _speed);
                if (!_animation.IsPlaying(GameConstants.ANIM_RUN))
                    _animation.CrossFade(GameConstants.ANIM_RUN);
            }
            else
            {
                if (_state != GameData.PlayerStates.Attacking)
                    if (!_animation.IsPlaying(GameConstants.ANIM_IDLE))
                        _animation.CrossFade(GameConstants.ANIM_IDLE);
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

    public void PlayMeteorSwarm()
    {
        if (_mana >= 100f)
        {
            StartCoroutine(MeteorSwarm());
            _mana = 0;
            UIController.Instance.SetMana(_mana);
        }
    }

    public void AddMana(float f)
    {
        _mana = Mathf.Clamp(_mana + f, 0f, 100f);
        UIController.Instance.SetMana(_mana);
    }

    private IEnumerator MeteorSwarm()
    {
        _movementEnabled = false;
        UIController.Instance.SetPauseButtons(true);

        GameObject meteorGO = PoolManager.Spawn(_meteorPrefab);
        Destroy(meteorGO, 10.0f);

        _animation.CrossFade(GameConstants.ANIM_UNLIMITED_POWER);

        while (_animation.IsPlaying(GameConstants.ANIM_UNLIMITED_POWER))
        {
            yield return null;
        }

        _movementEnabled = true;
        UIController.Instance.SetPauseButtons(false);
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