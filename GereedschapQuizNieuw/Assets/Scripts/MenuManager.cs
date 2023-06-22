using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Verder()
    {
/*        StateNameController.Goed = Antwoorden.Goed;
        StateNameController.Fout = Antwoorden.Fout;*/
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Menu()
    {
        GameObject go = GameObject.Find("TimerManager");
        if (go)
        {
            Destroy(go.gameObject);
        }
        StateNameController.Goed = 0;
        StateNameController.Fout = 0;
        StateNameController.vraagCount = 0;
        StateNameController.checkVraag = "";
        StateNameController.laatsteVraag = false;
        StateNameController.timerOff = false;
        SceneManager.LoadScene(0);
    }
}
