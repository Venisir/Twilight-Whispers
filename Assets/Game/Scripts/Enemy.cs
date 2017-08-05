using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UI;
using System;

//[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameData.GameColors _color;

    [SerializeField]
    private float _speed = 1.0f;

    [SerializeField]
    private float _hp = 100.0f;

    [SerializeField]
    private Slider _hpBar;

    [SerializeField]
    private ParticleSystem _particleSystem;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private GameObject _thinkingGO;

    [NonSerialized]
    private GameData.EnemyStates _state;

    private NavMeshAgent m_Agent;
    private RaycastHit m_HitInfo = new RaycastHit();
    private float deadTime = 1f;

    void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_Agent.speed = _speed;

        _hpBar.maxValue = _hp;
        _hpBar.value = _hp;
    }

    private void Start()
    {
        _thinkingGO.SetActive(false);
        _state = GameData.EnemyStates.Walking;
    }

    void Update()
    {
        if (m_Agent != null && m_Agent.destination == null && _state == GameData.EnemyStates.Walking)
        {
            StartCoroutine(RecalculateDestination());
        }

        if (_state != GameData.EnemyStates.Walking)
        {
            switch (_state)
            {
                case GameData.EnemyStates.Dying:
                    if (deadTime > 0)
                        deadTime -= Time.deltaTime;
                    else
                        transform.localPosition -= transform.up * Time.deltaTime * 0.5f;
                    break;

                case GameData.EnemyStates.Scaping:

                    //TODO
                    if (transform.localScale.x > 0f)
                    {
                        transform.localScale -= Vector3.one * Time.deltaTime * 1f;
                        transform.localPosition -= (-transform.up) * Time.deltaTime * 1.5f;
                    }
                    break;
            }
        }
    }

    public void CallRecalculateDestination()
    {
        StartCoroutine(RecalculateDestination());
    }

    public IEnumerator RecalculateDestination()
    {
        _animator.Play(GameConstants.ANIM_IDLE);
        _thinkingGO.SetActive(true);

        m_Agent.enabled = false;
        yield return new WaitForSeconds(1.5f);
        m_Agent.enabled = true;

        ChooseBestDestination();
        _thinkingGO.SetActive(false);
    }

    public void ChooseBestDestination()
    {
        List<Portal> list = LevelManager.Instance.GetPortals();

        Portal closest = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Portal p in list)
        {
            float dist = Vector3.Distance(p.transform.position, currentPos);
            if (dist < minDist)
            {
                closest = p;
                minDist = dist;
            }
        }

        if (closest != null)
        {
            closest.AddEnemy(this);
            SetDestination(closest.transform.position);
        }
    }

    public GameData.EnemyStates GetState()
    {
        return _state;
    }

    public void SetDestination(Vector3 v)
    {
        if (m_Agent != null)
        {
            m_Agent.destination = v;
            _animator.Play(GameConstants.ANIM_WALK);
        }
    }

    public void Damage(float damage)
    {
        _hp -= damage;
        _hpBar.value = _hp;

        if (_hp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        _animator.Play(GameConstants.ANIM_DEAD);
        _state = GameData.EnemyStates.Dying;
        DestroyStuff();
    }

    public void Scape()
    {
        _animator.Play(GameConstants.ANIM_JUMP);
        _state = GameData.EnemyStates.Scaping;
        DestroyStuff();
    }

    private void DestroyStuff()
    {
        try
        {
            StopAllCoroutines();

            _particleSystem.enableEmission = false;
            _particleSystem.transform.parent = null;
            _hpBar.transform.parent = null;
            _thinkingGO.SetActive(false);

            Destroy(_particleSystem.gameObject, 2.5f);
            Destroy(_hpBar.gameObject, 2.5f);

            Destroy(m_Agent);
            Destroy(this.GetComponent<SphereCollider>());
            Destroy(this.gameObject, 2.5f);
            //Destroy(this);
        }
        catch (Exception e)
        {
            Debug.LogWarning("Error destroying enemy: " + e);
        }
    }
}
