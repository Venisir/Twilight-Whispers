using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particles;

    private Enemy _objective;
    private Vector3 _objectivePosition;
    private float _speed;
    private float _damage;

    private Vector3 _objectivePositionAux;

    void Start()
    {

    }

    void Update()
    {
        if (_objective != null)
            _objectivePositionAux = _objective.transform.position;
        else
            _objectivePositionAux = _objectivePosition;

        this.transform.position = Vector3.MoveTowards(this.transform.position, _objectivePositionAux, _speed * Time.deltaTime);

        if (Vector3.Distance(_objectivePositionAux, this.transform.position) <= 0.1f)
        {
            // Destroy particles and enemy go
            _particles.enableEmission = false;
            _particles.transform.parent = null; // detach particle system
            _particles.transform.localScale = Vector3.one; // detach particle system
            Destroy(_particles.gameObject, _particles.duration);
            Destroy(this.gameObject);

            // Damage
            _objective.Damage(_damage);
        }
    }

    public void SetObjective(Enemy objective)
    {
        _objective = objective;
        _objectivePosition = new Vector3(objective.transform.position.x, objective.transform.position.y, objective.transform.position.z);
    }

    public void SetSpeed(float s)
    {
        _speed = s;
    }

    public void SetDamage(float d)
    {
        _damage = d;
    }
}
