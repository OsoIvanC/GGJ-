using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;

    public int playerMovements = 10; 

    public Tile activeTile;

    public List<Tile> activeNeighbors = new List<Tile>();

    public LayerMask tileMask;
    public LayerMask obstacleMask;

    public float speed;

    public bool isMoving;

    public bool canMove;

    public float rayDistance;

    public GameObject obstacle;

    [Header("UI")]
    public GameObject[] arrowsUI;
    public TMP_Text playerMoves;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        activeTile = GetActiveTile();

        if(activeTile != null)
            UpdateArrowsUI(activeTile._Neighbors);

        //Time.timeScale = 0;

        //playerMoves.text = $"Movements Left {playerMovements}";
    }


    private void Update()
    {
        //Debug.DrawRay(transform.position + new Vector3(0.5f, 0.25f, 0.5f), -transform.up, Color.red);
        //Debug.DrawRay(transform.position + new Vector3(0.5f, 0.25f, 0.5f), transform.forward, Color.red);

    }


    public void UpdateArrowsUI(Neighbors n)
    {

        arrowsUI[0].SetActive((n.North != null) ? true : false) ;
        arrowsUI[1].SetActive((n.South != null) ? true : false);
        arrowsUI[2].SetActive((n.West != null) ? true : false);
        arrowsUI[3].SetActive((n.East != null) ? true : false);

    }

    


    public void Move(Vector3 pos)
    {

        //if (Timer.instance.Pause)
        //    return;

        if (playerMovements <= 0)
            return;
        
        if (isMoving)
            return;

        //Debug.Log("moving");

        //Debug.Log(pos);


        if (!CanMove(pos))
            return;


        isMoving = true;

        CheckPull(pos);

        Tween t = transform.DOMove(pos, speed, true).SetEase(Ease.InOutSine);
       
        t.Complete( isMoving = false);

        t.Complete(activeTile = GetActiveTile());

        UpdateArrowsUI(activeTile._Neighbors);

        playerMovements--;

        GameController.instance.RandomizeObstacles();
        
        Debug.Log($"Movimientos del jugador : {playerMovements} ");
        
        //UpdateArrowsUI(activeTile._Neighbors);

    }

    
    public Tile GetActiveTile()
    {
        RaycastHit hit;

        Tile tile = null;

        //Debug.DrawRay(transform.position + new Vector3(0.5f, 0.5f, 0.5f), -transform.up, Color.red);

        if( Physics.Raycast(transform.position + new Vector3(0.5f,0.5f,0.5f), -transform.up, out hit, 2f, tileMask))
        {
            //Debug.Log(hit.collider.name);

            if(hit.collider.CompareTag("Tile"))
            {
                tile = hit.collider.GetComponent<Tile>();

                GetActiveNeighbors(tile);

            }
        }

        //Set Obstacles

        foreach (Tile t in tile._Neighbors.GetNeighbors())
        {
           t.SetObstacle();
           //t.UpdateTile();
        }

        return tile;
    }

    
    public bool CanMove(Vector3 dir)
    {
        RaycastHit hit;


        //Debug.DrawRay(transform.position + new Vector3(0.5f, 0.5f, 0.5f), dir - transform.position , Color.red,5);

        if (Physics.Raycast(transform.position + new Vector3(0.5f, 0.2f, 0.5f), dir - transform.position, out hit, 1f, obstacleMask))
        {
            Vector3 hitDir = hit.normal;

            Debug.Log(hitDir);

            Debug.Log(-hitDir);

            Debug.DrawRay(transform.position + new Vector3(0.5f, 0.2f, 0.5f), hitDir, Color.blue,5);

            if (hit.collider != null)
            {
                //Debug.Log("Cant Move");
                
                //return false;

                Obstacle obs = hit.collider.GetComponent<Obstacle>();

                if (obs.CanMove(-hitDir))
                {
                    obs.Move(-hitDir);
                    return true;
                }
                else
                    return false;
            }
           
        }
       
        Debug.Log("Can Move");

        return true;
       // Debug.DrawRay(rayPoint.position, transform.TransformDirection(Vector3.forward) * rayDistance);
    }


    public void CheckPull(Vector3 dir)
    {
        RaycastHit hit;


        Debug.DrawRay(transform.position + new Vector3(0.5f, 0.5f, 0.5f), -(dir - transform.position) , Color.green,5);

        if (Physics.Raycast(transform.position + new Vector3(0.5f, 0.2f, 0.5f), -(dir - transform.position), out hit, 1f, obstacleMask))
        {
            Vector3 hitDir = hit.normal;

            Debug.Log(hitDir);

            Debug.Log(-hitDir);

            Debug.DrawRay(transform.position + new Vector3(0.5f, 0.2f, 0.5f), hitDir, Color.blue, 5);

            if (hit.collider != null)
            {
                //Debug.Log("Cant Move");

                //return false;

                Obstacle obs = hit.collider.GetComponent<Obstacle>();

                if (obs.CanMove(hitDir))
                {
                    obs.Move(hitDir);
                    
                }
             
            }

        }

        Debug.Log("Can Move");

        
        // Debug.DrawRay(rayPoint.position, transform.TransformDirection(Vector3.forward) * rayDistance);
    }



    private void LateUpdate()
    {
        //playerMoves.text = $"Movements Left {playerMovements}";
    }

    void GetActiveNeighbors(Tile t)
    {
        if(activeNeighbors.Count != 0)
        {
            foreach (Tile tile in activeNeighbors)
            {
                tile.isNeighbor = false;

                tile.SetObstacle();
                tile.obstacle = null;

                tile.UpdateTile();
            }
        }

        activeNeighbors.Clear();

        activeNeighbors = t._Neighbors.GetNeighbors();

        foreach (Tile tile in activeNeighbors)
        {
            tile.isNeighbor = true;

            tile.SetObstacle();

            tile.UpdateTile();
        }
    }
}
