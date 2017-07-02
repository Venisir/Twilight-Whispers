using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{

    private float _speed;
    private float _damage;

    void Start()
    {

    }

    void Update()
    {

    }

    public void SetDirection()
    {
        //_objective = objective;
        //_objectivePosition = new Vector3(objective.transform.position.x, objective.transform.position.y, objective.transform.position.z);
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
