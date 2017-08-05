using UnityEngine;

public class InvisibleTower : MonoBehaviour
{
    private RaycastHit m_HitInfo = new RaycastHit();

    private GameData.Towers _type;

    void Start()
    {

    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out m_HitInfo))
        {
            MyTile tile = m_HitInfo.transform.GetComponent<MyTile>();
            if (tile != null)
            {
                this.transform.position = tile.transform.position;

                if (Input.GetMouseButtonDown(0))
                {
                    if (!tile.Occuped())
                    {
                        tile.CreateTower(TowerManager.Instance.GetTower(_type));
                        
                        Destroy(this.gameObject);
                    }
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void SetType(GameData.Towers type)
    {
        _type = type;
    }
}