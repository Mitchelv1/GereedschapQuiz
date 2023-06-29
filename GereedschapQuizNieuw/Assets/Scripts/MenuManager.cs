using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Verder()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Menu()
    {
        GameObject go = GameObject.Find("TimerManager");
        if (go)
        {
            Destroy(go.gameObject);
        }
        Nul();
        StateNameController.Goed = 0;
        StateNameController.Fout = 0;
        StateNameController.checkVraag = "";
        StateNameController.laatsteVraag = false;
        StateNameController.timerOff = false;
        SceneManager.LoadScene(0);
    }

    public void Nul()
    {
        int i = 0;
        while (i < StateNameController.vraagCount)
        {
            StateNameController.saveantwoord[i] = "";
            i++;
        }
        StateNameController.vraagCount = 0;
    }
}
