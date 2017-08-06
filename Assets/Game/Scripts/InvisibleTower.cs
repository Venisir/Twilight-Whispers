using UnityEngine;

public class InvisibleTower : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _currentMesh;

    [SerializeField]
    private Material _availableMaterial;

    [SerializeField]
    private Material _blockedMaterial;

    //0: available
    //1: blocked
    private int _currentState = 0; 
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

                if (tile.IsOccuped())
                {
                    //Red Material
                    if (_currentState == 0)
                    {
                        _currentState = 1;
                        _currentMesh.material = _blockedMaterial;
                    }
                }
                else
                {
                    //Blue material
                    if (_currentState == 1)
                    {
                        _currentState = 0;
                        _currentMesh.material = _availableMaterial;
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        UIController.Instance.SetPauseButtons(false);
                        tile.CreateTower(TowerManager.Instance.GetTower(_type));

                        Destroy(this.gameObject);
                        return;
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