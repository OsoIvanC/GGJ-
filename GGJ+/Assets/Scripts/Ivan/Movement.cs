using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 20;
    public float normalspeed;
    public float slowspeed;
    public bool move;
    [Header("References")]
    private AudioSource AudioShido;
    float Gravity = -7.5f;


    private CharacterController characterController;
    Vector3 velocity;

    private void Awake()
    {
        normalspeed = speed;
        
        move = true;

    }
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {

        {
            velocity.y += Gravity * Time.deltaTime;
            characterController.Move(velocity);
        }
    }
    void FixedUpdate()
    {


        Move();

    }

    void Move()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        characterController.Move(transform.rotation * (move.normalized * Time.deltaTime * speed));
    }
}

