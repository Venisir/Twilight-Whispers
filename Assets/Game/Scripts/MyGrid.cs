using System.Collections.Generic;
using UnityEngine;

public class MyGrid : MonoBehaviour
{
    [SerializeField]
    private GameObject hexPrefab;
    
    [SerializeField]
    private int gridWidth = 11;

    [SerializeField]
    private int gridHeight = 11;

    [SerializeField]
    private float gap = 0.0f;
    
    private float hexWidth = 1.732f;
    private float hexHeight = 2.0f;

    private Vector3 startPos;
    private MyTile[,] tileGrid;

    public void Start()
    {
        AddGap();
        CalcStartPos();
        CreateGrid();
    }

    private void AddGap()
    {
        hexWidth += hexWidth * gap;
        hexHeight += hexHeight * gap;
    }

    private void CalcStartPos()
    {
        float offset = 0;
        if (gridHeight / 2 % 2 != 0)
            offset = hexWidth / 2;

        float x = -hexWidth * (gridWidth / 2) - offset;
        float z = hexHeight * 0.75f * (gridHeight / 2);

        startPos = new Vector3(x, 0, z);
    }

    private Vector3 CalcWorldPos(Vector2 gridPos)
    {
        float offset = 0;
        if (gridPos.y % 2 != 0)
            offset = hexWidth / 2;

        float x = startPos.x + gridPos.x * hexWidth + offset;
        float z = startPos.z - gridPos.y * hexHeight * 0.75f;

        return new Vector3(x, transform.position.y, z);
    }

    private void CreateGrid()
    {
        tileGrid = new MyTile[gridWidth, gridHeight];

        //For each column
        for (int y = 0; y < gridHeight; y++)
        {
            //For each row
            for (int x = 0; x < gridWidth; x++)
            {
                GameObject hex = Instantiate(hexPrefab) as GameObject;
                hex.GetComponent<MyTile>().x = x;
                hex.GetComponent<MyTile>().y = y;
                
                Vector2 gridPos = new Vector2(x, y);
                hex.transform.position = CalcWorldPos(gridPos);
                hex.transform.parent = this.transform;
                hex.name = "Hexagon" + x + "|" + y;
                
                tileGrid[x, y] = hex.GetComponent<MyTile>();
            }
        }
    }

    public void BlockNeighbours(MyTile tile)
    {
        Vector2[] oddrDirectionsEven = { new Vector2(+1,  0), new Vector2( 0, -1), new Vector2(-1, -1),
                                         new Vector2(-1,  0), new Vector2(-1, +1), new Vector2( 0, +1)
                                       };
        
        Vector2[] oddrDirectionsOdd = { new Vector2(+1,  0), new Vector2(+1, -1), new Vector2( 0, -1),
                                         new Vector2(-1,  0), new Vector2( 0, +1), new Vector2(+1, +1)
                                       };

        if (tile.y % 2 == 1)
        {
            foreach (Vector2 v2 in oddrDirectionsOdd)
            {
                int auxx = tile.x + (int)v2.x;
                int auxy = tile.y + (int)v2.y;

                if (auxx >= 0 && auxx < gridWidth &&
                    auxy >= 0 && auxy < gridHeight &&
                    tileGrid[auxx, auxy] != null)
                {
                    tileGrid[auxx, auxy].SetOccuped(true);
                }
            }
        }
        else
        {
            foreach (Vector2 v2 in oddrDirectionsEven)
            {
                int auxx = tile.x + (int)v2.x;
                int auxy = tile.y + (int)v2.y;

                if (auxx >= 0 && auxx < gridWidth &&
                    auxy >= 0 && auxy < gridHeight &&
                    tileGrid[auxx, auxy] != null)
                {
                    tileGrid[auxx, auxy].SetOccuped(true);
                }
            }
        }
    }

    public void ClearGrid()
    {
        Debug.Log("Cleaning grid...");
        foreach (MyTile tile in tileGrid)
        {
            DestroyImmediate(tile.gameObject, false);
        }
        tileGrid = null;
    }

    public MyTile GetTileAt(Vector2 gridPos)
    {
        if (gridPos.x < gridWidth && gridPos.y < gridHeight && tileGrid[(int)gridPos.x, (int)gridPos.y] != null)
            return tileGrid[(int)gridPos.x, (int)gridPos.y];
        return null;
    }

    public int GetHeight()
    {
        return gridHeight;
    }
    
    public int GetWidth()
    {
        return gridWidth;
    }
}