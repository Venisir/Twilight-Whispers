using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTile : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletTowerPrefab;

    [SerializeField]
    private GameObject laserTowerPrefab;

    [SerializeField]
    private GameObject portalPrefab;

    private Tower tower = null;
    private Portal portal = null;
    public int y, x;

    void Start()
    {
        /*
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        Vector2[] uvs = new Vector2[vertices.Length];

        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
        }
        mesh.uv = uvs;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        */
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

    public void CreateTower(GameData.Towers type)
    {
        GameObject hex = null;

        switch (type)
        {
            case GameData.Towers.Bullets:
                hex = Instantiate(bulletTowerPrefab) as GameObject;
                break;
            case GameData.Towers.Lasers:
                hex = Instantiate(laserTowerPrefab) as GameObject;
                break;
        }

        hex.transform.parent = this.transform;
        hex.transform.localPosition = Vector3.zero;

        tower = hex.GetComponent<Tower>();
    }

    public void CreatePortal()
    {
        GameObject hex = Instantiate(portalPrefab) as GameObject;
        hex.transform.parent = this.transform;
        hex.transform.localPosition = Vector3.zero;

        portal = hex.GetComponent<Portal>();
        LevelManager.Instance.AddPortal(portal);
    }

    public bool Occuped()
    {
        return !(tower == null && portal == null);
    }
}
