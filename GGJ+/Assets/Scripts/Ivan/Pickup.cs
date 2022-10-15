using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    //Es necesario que en el Inspector este asignador un empty object de donde salga el Ray Point (Sirve para visualizar el Raycast), referenciar al jugador mismo, y elegir la Layer con la cuál el jugador interactuará con los objetos.
    //Los Cubos deben de contar con Rigidbody
    [SerializeField] private Transform rayPoint;
    [SerializeField] private float rayDistance;
    [SerializeField] private GameObject grabbedObj;

    [SerializeField] private LayerMask layerIndex;

    Movement moveP1;
    [SerializeField] private GameObject player;

    void Start()
    {
        moveP1 = player.GetComponent<Movement>();

    }

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayDistance, layerIndex))
        {

           if (hit.collider.CompareTag("Obj") && Input.GetButtonDown("F") && grabbedObj==null) //Si el objeto tiene la tag del player, el jugador presiona el Button F y no tiene otro objeto agarrado entonces:
           {
                
                grabbedObj = hit.collider.gameObject; //Cuál objeto esta agarrando
                grabbedObj.GetComponent<Rigidbody>().isKinematic = true;
                grabbedObj.transform.SetParent(transform);


                moveP1.speed = moveP1.slowspeed; //Cambia la velocidad del jugador.
                    Debug.Log("Awa");
           }
           else if(Input.GetButtonDown("F"))
            {
                moveP1.speed = moveP1.normalspeed;
                grabbedObj.GetComponent<Rigidbody>().isKinematic = false;
                grabbedObj.transform.SetParent(null);
                grabbedObj = null;
            }
            
        }
        Debug.DrawRay(rayPoint.position,transform.TransformDirection(Vector3.forward) * rayDistance);
    }
}
