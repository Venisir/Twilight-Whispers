using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusZone : MonoBehaviour
{
    [SerializeField]
    private Tower _tower;

    [SerializeField]
    private SphereCollider _collider;
    
    void Start()
    {

    }

    void Update()
    {

    }

    public void SetRadius(float r)
    {
        _collider.radius = r;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
            _tower.AddEnemy(other.GetComponent<Enemy>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
            _tower.RemoveEnemy(other.GetComponent<Enemy>());
    }
}
