using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCollider : MonoBehaviour
{
    [SerializeField]
    private Portal parent;

    void Start()
    {
        parent = transform.GetComponentInParent<Portal>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().Scape();

            LevelManager.Instance.RemovePortal(parent);
            parent.DestroyPortal();
        }
    }
}
