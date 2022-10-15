using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    [SerializeField] private float forceMagnitude;
    

    //Para que funcione el Script Los Objetos tienen que tener Constrains en Todos los Rotation en X,Y,Z
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rigidbody = hit.collider.attachedRigidbody; //Obtienes el Rigidbody con el que has colisionado
        
        if(rigidbody!=null)
        {
            Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
            forceDirection.y = 0; //Solo para no afectar en y
            forceDirection.Normalize();

            rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse); //Con esto aplicamos fuerza desde una posición específica (la del personaje hacia el obstaculos)
        }
    }
}
