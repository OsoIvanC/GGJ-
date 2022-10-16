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

    public Obstacle obstacle;

    public LayerMask obstacleMask;


    public void ChangeColor()
    {
        Color color = (isNeighbor) ? Color.green : Color.white;
        transform.GetComponentInChildren<Renderer>().material.SetColor("_Color",color) ;
    }

    public Obstacle GetObstacle()
    {

        if (!isNeighbor)
            return null;

        RaycastHit hit;

        if (Physics.Raycast(transform.position,transform.up, out hit,1, obstacleMask))
        {
            return hit.collider.GetComponent<Obstacle>();
        }

        return null;
    }


    public void UpdateTile()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {

        RaycastHit hit;
        
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);

        if (Physics.Raycast(ray, out hit))
        {
            
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



