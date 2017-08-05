using UnityEngine;

public class MyTile : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletTowerPrefab;

    [SerializeField]
    private GameObject laserTowerPrefab;

    [SerializeField]
    private GameObject portalPrefab;

    private Tower _tower = null;
    private Portal _portal = null;
    public int y, x;

    void Start()
    {

    }

    void Update()
    {

    }

    public static Vector3 Corner(Vector3 origin, float radius, int corner)
    {
        float angle = 60 * corner;
        angle += 30;
        angle *= Mathf.PI / 180;
        return new Vector3(origin.x + radius * Mathf.Cos(angle), 0.5f, origin.z + radius * Mathf.Sin(angle));
    }

    public void CreateTower(GameObject tower)
    {
        GameObject hex = null;

        hex = Instantiate(tower);

        hex.transform.parent = this.transform;
        hex.transform.localPosition = Vector3.zero;

        _tower = tower.GetComponent<Tower>();
    }

    public void CreatePortal()
    {
        GameObject hex = Instantiate(portalPrefab) as GameObject;
        hex.transform.parent = this.transform;
        hex.transform.localPosition = Vector3.zero;

        _portal = hex.GetComponent<Portal>();
        LevelManager.Instance.AddPortal(_portal);
    }

    public bool Occuped()
    {
        return !(_tower == null && _portal == null);
    }
}
