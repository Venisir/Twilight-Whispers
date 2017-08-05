using UnityEngine;

public class Meteor : MonoBehaviour {

    [SerializeField]
    private float _damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
            other.GetComponent<Enemy>().Damage(_damage);
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
            other.gameObject.GetComponent<Enemy>().Damage(_damage);
    }
}