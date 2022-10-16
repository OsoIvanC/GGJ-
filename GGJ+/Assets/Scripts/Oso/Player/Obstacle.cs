using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    LayerMask mask;

    [SerializeField]
    bool isMoving;

    [SerializeField]
    float speed;

    public bool isMovable;

    public bool CanMove(Vector3 dir)
    {
        if(!isMovable)
            return false;

        RaycastHit hit;

        Debug.DrawRay(transform.position + new Vector3(0.5f, 0.2f, 0.5f), dir, Color.yellow, 5);

        if (Physics.Raycast(transform.position + new Vector3(0.5f, 0.2f, 0.5f), dir, out hit, 1, mask))
        {
            if(hit.collider != null)
                return false;
        }

        return true;
        // Debug.DrawRay(rayPoint.position, transform.TransformDirection(Vector3.forward) * rayDistance);
    }

   
    public void Move(Vector3 pos)
    {
        //Debug.Log("moving");


        if (!isMovable)
            return;
        
        if (isMoving)
            return;

        isMoving = true;

        Tween t = transform.DOMove(transform.position + pos, speed, true).SetEase(Ease.InOutSine);

        t.Complete(isMoving = false);

        //t.Complete(activeTile = GetActiveTile());


    }
}
