using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    public static float timeCount;
    public TMP_Text firstMinute;
    public TMP_Text secondMinute;
    public TMP_Text firstSecond;
    public TMP_Text secondSecond;
    public Scene scene;

    void Start()
    {
        StateNameController.timerOff = false;
        scene = SceneManager.GetActiveScene();
        GameObject go = GameObject.Find("TimerManager");
        if (go)
        {
            DontDestroyOnLoad(go.gameObject);
        }
    }

    //Deze functie is een timer, elke seconde word de tijd veranderd en de timer gaat ook mee als de scene word verwisseld.
    //De timer word gestart op het moment dat je op start klikt en gaat door tot dat de timer word weggehaald bij het resultaten scherm.
    //Als de tijd 1 uur bereikt word de quiz gestopt en word je terug gestuurd naar het menu.

    void Update()
    {
        if (StateNameController.isUpdateEnabled)
        {
            if (scene.buildIndex != 0)
            {
                firstMinute = GameObject.Find("firstMinute").GetComponent<TMP_Text>();
                secondMinute = GameObject.Find("secondMinute").GetComponent<TMP_Text>();
                firstSecond = GameObject.Find("firstSecond").GetComponent<TMP_Text>();
                secondSecond = GameObject.Find("secondSecond").GetComponent<TMP_Text>();
                DisplayTime(StateNameController.timeValue);
                if (!StateNameController.timerOff)
                {
                    if (StateNameController.timeValue <= 3600)
                    {
                        StateNameController.timeValue += Time.deltaTime;
                    }
                    else
                    {
                        GameObject go = GameObject.Find("TimerManager");
                        if (go)
                        {
                            Destroy(go.gameObject);
                        }
                        int i = 0;
                        while (i < StateNameController.vraagCount)
                        {
                            StateNameController.saveantwoord[i] = "";
                            i++;
                        }
                        StateNameController.vraagCount = 0;
                        StateNameController.Goed = 0;
                        StateNameController.checkVraag = "";
                        StateNameController.timeValue = 0;
                        StateNameController.laatsteVraag = false;
                        StateNameController.isUpdateEnabled = false;
                        StateNameController.timerOff = false;
                        StateNameController.nul = 0;
                        StateNameController.rank = 0;
                        SceneManager.LoadScene(0);
                    }
                }
            }
        }
    }

    //Word uitgevoerd als je op start en start dan de Update functie.
    public void EnableUpdate()
    {
        StateNameController.isUpdateEnabled = true;
    }

    //Dit zet de variabeles in een formaat zodat het er mooi uit ziet.
    public void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        string currentTime = string.Format("{0:00}{1:00}", minutes, seconds);
        StateNameController.tijd = string.Format("{0:00}:{1:00}", minutes, seconds);
        firstMinute.text = currentTime[0].ToString();
        secondMinute.text = currentTime[1].ToString();
        firstSecond.text = currentTime[2].ToString();
        secondSecond.text = currentTime[3].ToString();
    }
}