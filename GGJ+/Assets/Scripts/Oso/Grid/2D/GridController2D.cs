using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController2D : MonoBehaviour
{
    public static GridController2D instance;


    [SerializeField]
    int h, w;

    [SerializeField]
    GameObject tilePrefab;

    [SerializeField]
    GameObject playerPrefab;

    public Tile2D[,] grid;

    public List<Tile2D> tiles = new List<Tile2D>();

    private void Awake()
    {

        instance = this;

        grid = new Tile2D[h, w];
    }

    private void Start()
    {
        CreateGrid();
    }

    public void CreateGrid()
    {
        for (int i = 0; i < h; i++)
        {
            for (int j = 0; j < w; j++)
            {
                GameObject tile = Instantiate(tilePrefab, new Vector2(i, j) - new Vector2(h/2,w/2), Quaternion.identity, this.transform);


                if (i == 0 && j == 0)
                {
                    Instantiate(playerPrefab, new Vector2(i, j) - new Vector2(h / 2, w / 2), Quaternion.identity);
                }

                tile.AddComponent<Tile2D>();

                Tile2D t = tile.GetComponent<Tile2D>();
                grid[i, j] = t;

            }
        }

        SetNeighbors();
    }

    public void SetNeighbors()
    {

        print(grid.GetLength(0));
        print(grid.GetLength(1));

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                grid[i, j]._Neighbors.North = (j + 1 < grid.GetLength(0)) ? grid[i, j + 1] : null;
                grid[i, j]._Neighbors.South = (j - 1 > 0) ? grid[i, j - 1] : null;
                grid[i, j]._Neighbors.West = (i - 1 > 0) ? grid[i - 1, j] : null;
                grid[i, j]._Neighbors.East = (i + 1 < grid.GetLength(1)) ? grid[i + 1, j] : null;

            }
        }
    }
}
