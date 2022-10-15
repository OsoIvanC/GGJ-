using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;

    public Tile activeTile;

    public List<Tile> activeNeighbors = new List<Tile>();

    public LayerMask tileMask;

    public float speed;

    public bool isMoving;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        activeTile = GetActiveTile();
    }


    private void Update()
    {
        Debug.DrawRay(transform.position + new Vector3(0.5f, 0.5f, 0.5f), -transform.up, Color.red);
    }

    public void Move(Vector3 pos)
    {
        Debug.Log("moving");

        if (isMoving)
            return;
       
        isMoving = true;
       
        Tween t = transform.DOMove(pos, speed, true).SetEase(Ease.InOutSine);
       
        t.Complete( isMoving = false);
        
        t.Complete(activeTile = GetActiveTile());
        
       
    }
    Tile GetActiveTile()
    {
        RaycastHit hit;

        Tile tile = null;

        //Debug.DrawRay(transform.position + new Vector3(0.5f, 0.5f, 0.5f), -transform.up, Color.red);

        if( Physics.Raycast(transform.position + new Vector3(0.5f,0.5f,0.5f), -transform.up, out hit, 1f, tileMask))
        {

            Debug.Log(hit.collider.name);

            if(hit.collider.CompareTag("Tile"))
            {
                tile = hit.collider.GetComponent<Tile>();

                GetActiveNeighbors(tile);
            }
        }
        
        return tile;
    }

    //private void Update()
    //{
    //    RaycastHit hit;

    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //    if (Physics.Raycast(ray, out hit))
    //    {
    //        if (!hit.collider.GetComponent<Tile>())
    //            return;

    //        if (!hit.collider.GetComponent<Tile>().isNeighbor)
    //            return;
            
    //        Debug.Log(hit.collider.name);
    //    }
    //}

    void GetActiveNeighbors(Tile t)
    {
        if(activeNeighbors.Count != 0)
        {
            foreach (Tile tile in activeNeighbors)
            {
                tile.isNeighbor = false;
                tile.ChangeColor();
            }
        }

        activeNeighbors.Clear();

        activeNeighbors = t._Neighbors.GetNeighbors();

        foreach (Tile tile in activeNeighbors)
        {
            tile.isNeighbor = true;
            tile.ChangeColor();
        }
    }
}
