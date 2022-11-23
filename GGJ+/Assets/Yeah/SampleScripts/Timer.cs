using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    [SerializeField] private Image uiFill;
    [SerializeField] private Text uiText;

    public static Timer instance;

    public int Duration;

    public bool Pause;

    public GameObject YouLose; //Canvas de Game Over
    public GameObject PauseIcon;//Icono de Pausa
    private int remainingDuration;

    [Header("Sounds")]
    [SerializeField] AudioSource AudioShido;
    public AudioClip Loser;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        Being(Duration);
        //YOU WIN
        //AudioShido = GetComponent<AudioSource>();
    }

    private void Being(int Second)
    {
        remainingDuration = Second;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (remainingDuration >= 0)
        {
            if (!Pause)
            {
                //Time.timeScale = 1;
                uiText.text = $"{remainingDuration / 60:00}:{remainingDuration % 60:00}";
                uiFill.fillAmount = Mathf.InverseLerp(0, Duration, remainingDuration);
                remainingDuration--;
                yield return new WaitForSeconds(1f);
            }
            //Time.timeScale = 0;
            yield return null;
        }
       
        OnEnd();
    }

    public void OnEnd()
    {
        Pause = !Pause;
        AudioShido.clip = Loser;
        AudioShido.Play();
        AudioShido.loop = false;
        YouLose.SetActive(true);
        PauseIcon.SetActive(false);
        print("Termino");

    }


    public void PauseMenu()
    {
        Pause = !Pause;
    }
}
