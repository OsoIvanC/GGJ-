using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    [SerializeField] private Image uiFill;
    [SerializeField] private Text uiText;

    public static Timer instance;  //Singelton

    public int Duration;

    public bool Pause;

    private int remainingDuration;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;

       
    }
    void Start()
    {
        Being(Duration);
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
                uiText.text = $"{remainingDuration / 60:00}:{remainingDuration % 60:00}";
                uiFill.fillAmount = Mathf.InverseLerp(0, Duration, remainingDuration);
                remainingDuration--;
                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }
       
        OnEnd();
    }

    private void OnEnd()
    {
        print("Termino");
    }

    public void PauseMenu()
    {
        Pause = !Pause;       
    }
}
