using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public static GridController instance;


    [SerializeField]
    int h,w;

    [SerializeField]
    GameObject tilePrefab;

    [SerializeField]
    GameObject playerPrefab;

    public Tile[,] grid;

    public List<Tile> tiles = new List<Tile>();

    public GameObject PauseIcon;
    public GameObject winScreen;

    public AudioSource source;
    public AudioClip winClip;


    private void Awake()
    {

        instance = this;

        grid = new Tile[h,w];
    }

    private void Start()
    {
        CreateGrid();
    }

    public void Win()
    {
        //Añadir el Time.TimeScale=0
        Debug.Log("Win");

        Timer.instance.Pause = !Timer.instance.Pause;

        source.clip = winClip;
        source.Play();

        source.loop = false;

        winScreen.SetActive(true);
        PauseIcon.SetActive(false);
    }

    public void Lose()
    {
        Timer.instance.OnEnd();
    }

    public void CreateGrid()
    {
        for(int i = 0; i < h; i++)
        {
            for (int j = 0; j < w; j++)
            {

                if(i==0 && j == 0)
                {
                    Instantiate(playerPrefab, new Vector3(i, 0 , j), Quaternion.identity);
                }

                GameObject tile = Instantiate(tilePrefab,new Vector3(i,0,j),Quaternion.identity,this.transform);

                tile.name = "Tile [ " + i + ',' + j + ']';
               
                ///tile.AddComponent<Tile>();

                Tile t = tile.GetComponent<Tile>();
                grid[i, j] = t;

            }
        }

        SetNeighbors();
    }

    public void SetNeighbors()
    {

        //print(grid.GetLength(0));
        //print(grid.GetLength(1));

        for (int i = 0; i  <  grid.GetLength(0) ; i++)
        {
            for (int j = 0; j <  grid.GetLength(1) ; j++)
            {
                grid[i, j]._Neighbors.North = (j + 1 < grid.GetLength(1)) ? grid[i , j + 1] : null;
                grid[i, j]._Neighbors.South = (j - 1 >= 0) ? grid[i , j - 1] : null ;

                grid[i, j]._Neighbors.West = (i - 1 >= 0) ? grid[i - 1, j] : null;
                grid[i, j]._Neighbors.East = (i + 1 < grid.GetLength(0)) ? grid[i + 1, j] : null;
                

            }
        }
    }

}
