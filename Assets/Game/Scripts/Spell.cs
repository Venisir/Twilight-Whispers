using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField]
    private float _duration = 1.5f;

    [SerializeField]
    private ParticleSystem _particles;

    private float _speed = 2.0f;
    private float _damage = 100.0f;
    private bool dying = false;
    private Vector3 _direction = Vector3.zero;
    
    void Update()
    {
        _duration -= Time.deltaTime;

        if (_direction != Vector3.zero)
        {
            this.transform.Translate(_direction * _speed * Time.deltaTime);
        }

        if (_duration <= 0.0f && !dying)
        {
            dying = true;

            // Destroy particles and enemy go
            _particles.enableEmission = false;
            _particles.startSize = 0f;

            foreach (ParticleSystem ps in _particles.GetComponentsInChildren<ParticleSystem>())
                ps.enableEmission = false;
            
            Destroy(this.gameObject, 2.5f);
        }
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }

    public void SetSpeed(float s)
    {
        _speed = s;
    }

    public void SetDamage(float d)
    {
        _damage = d;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().Damage(_damage);

            _particles.transform.parent = null;
            _particles.transform.localScale = Vector3.one;
            Destroy(_particles.gameObject, _particles.duration);

            Destroy(this.gameObject);
        }
    }
}
