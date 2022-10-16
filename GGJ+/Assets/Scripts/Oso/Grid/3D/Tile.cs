using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public struct Neighbors
{
    public Tile North;
    public Tile South;
    public Tile East;
    public Tile West;

    public List<Tile> GetNeighbors()
    {
        List<Tile> neighbors = new List<Tile>();

        if(North != null)
            neighbors.Add(North);
        if(South != null)
            neighbors.Add(South);
        if(East != null)
            neighbors.Add(East);
        if (West != null)
            neighbors.Add(West);

        return neighbors;
    }

}



public class Tile : MonoBehaviour,IPointerClickHandler
{
    public Neighbors _Neighbors;
 
    public bool isNeighbor;

    public bool isGoal;

    public Obstacle obstacle;

    public LayerMask obstacleMask;
    public LayerMask tileMask;
    public LayerMask goalMask;

    public void ChangeColor()
    {
        Color color = (isNeighbor) ? Color.green : Color.white;
        transform.GetComponentInChildren<Renderer>().material.SetColor("_Color",color) ;
    }

    private void Awake()
    {
        SetWinTile();
    }

    public void SetObstacle()
    {
        if (!isNeighbor)
            return;

        Debug.DrawRay(transform.position + new Vector3(0.5f,-0.2f,0.5f), transform.up, Color.red, 0.5f);

        RaycastHit hit;

        if (Physics.Raycast(transform.position + new Vector3(0.5f, -0.2f , 0.5f) , transform.up, out hit,1, obstacleMask))
        {
            //Debug.Log("Hay obstaculo");
            obstacle = hit.collider.GetComponent<Obstacle>();
        }
        
    }

    void SetWinTile()
    {
       
        Debug.DrawRay(transform.position + new Vector3(0.5f, -0.2f, 0.5f), transform.up, Color.red, 0.5f);
        RaycastHit hit;

        if (Physics.Raycast(transform.position + new Vector3(0.5f, -0.2f, 0.5f), transform.up, out hit, 1, goalMask))
        {
           isGoal = true;
        }

        
    }

    public void UpdateTile()
    {

        SetObstacle();

        Color color;        
        
        color = isNeighbor ? Color.green : Color.white;

        transform.GetComponentInChildren<Renderer>().material.SetColor("_Color", color);

        //if (obstacle == null)
        //    return;

        //color = (obstacle.isMovable) ? new Color(255, 120, 0) : Color.red;
       
        //transform.GetComponentInChildren<Renderer>().material.SetColor("_Color", color);

    }

    public void OnPointerClick(PointerEventData eventData)
    {

        RaycastHit hit;
        
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);

        if (Physics.Raycast(ray, out hit,Mathf.Infinity, tileMask))
        {
            
            Debug.Log(hit.collider.name);

            if (!hit.collider.GetComponent<Tile>())
                return;

            if (!hit.collider.GetComponent<Tile>().isNeighbor)
                return;

            Tile t = hit.collider.GetComponent<Tile>();

            Vector3 pos = t.GetComponentInChildren<Transform>().position;

            //PlayerController.instance.canMove = PlayerController.instance.CanMove(pos);
            //Debug.Log(pos);

            PlayerController.instance.Move(pos);
        }
    }
}



