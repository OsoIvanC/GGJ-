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
    float Gravity = -7.5f;

    [Header("YouWinStuff")]
    public GameObject YouWinScreen;
    public GameObject PauseIcon;
    Timer time;
    [SerializeField] private GameObject Timer;

    [Header("Sounds")]
    private AudioSource AudioShido;
    public AudioClip PauseJingle;
    public AudioClip Winner;

    private CharacterController characterController;
    Vector3 velocity;

    private void Awake()
    {
        //YOU WIN
        time = Timer.GetComponent<Timer>();
        AudioShido = GetComponent<AudioSource>();

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

        if(Input.GetKeyDown(KeyCode.Space))
        {
            //Añadir el Time.TimeScale=0
            Debug.Log("Win");
            AudioShido.clip = Winner;
            AudioShido.Play();
            time.Pause = !time.Pause;
            YouWinScreen.SetActive(true);
            PauseIcon.SetActive(!true);
        }
    }

    public void PauseMenu()
    {
        AudioShido.clip = PauseJingle;
        AudioShido.Play();
        time.Pause = !time.Pause;
    }
}

