using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    public float timeValue = 0;
    public static float timeCount;
    public static bool timerOff = StateNameController.timerOff;
    private bool isUpdateEnabled = false;
    public TMP_Text firstMinute;
    public TMP_Text secondMinute;
    public TMP_Text firstSecond;
    public TMP_Text secondSecond;
    public Scene scene;

    void Start()
    {
        timerOff = false;
        scene = SceneManager.GetActiveScene();
        GameObject go = GameObject.Find("TimerManager");
        if (go)
        {
            DontDestroyOnLoad(go.gameObject);
        }
    }

    void Update()
    {
        if (isUpdateEnabled)
        {
            if (scene.buildIndex != 0)
            {
                firstMinute = GameObject.Find("firstMinute").GetComponent<TMP_Text>();
                secondMinute = GameObject.Find("secondMinute").GetComponent<TMP_Text>();
                firstSecond = GameObject.Find("firstSecond").GetComponent<TMP_Text>();
                secondSecond = GameObject.Find("secondSecond").GetComponent<TMP_Text>();
                DisplayTime(timeValue);
                if (!timerOff)
                {
                    if (timeValue <= 3600)
                    {
                        timeValue += Time.deltaTime;
                    }
                    else
                    {
                        //Laad eindscherm scene tijd over?
                    }
                }
            }
        }
    }

    public void EnableUpdate()
    {
        isUpdateEnabled = true;
    }

    public void Inleveren()
    {
        timerOff = true;
        SceneManager.LoadScene("Eind-leerlingen");
        StateNameController.timerOff = timerOff;
    }

    public void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        string currentTime = string.Format("{0:00}{1:00}", minutes, seconds);
        firstMinute.text = currentTime[0].ToString();
        secondMinute.text = currentTime[1].ToString();
        firstSecond.text = currentTime[2].ToString();
        secondSecond.text = currentTime[3].ToString();
    }
}
