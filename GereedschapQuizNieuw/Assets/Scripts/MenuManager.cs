using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject StoppenPopup;
    public GameObject Antwoord_A;
    public GameObject Antwoord_B;
    public GameObject Antwoord_C;
/*    public GameObject Antwoord_D;*/
    public GameObject Volgende;
    public GameObject Vorige;
    public GameObject ArrowL;
    public GameObject ArrowR;
    public GameObject Inleveren;
    public void Verder()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Scorebord()
    {
        SceneManager.LoadScene(4);
        StateNameController.isUpdateEnabled = false;
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
        StateNameController.checkVraag = "";
        StateNameController.timeValue = 0;
        StateNameController.laatsteVraag = false;
        StateNameController.isUpdateEnabled = false;
        StateNameController.timerOff = false;
        StateNameController.testint = 0;
        StateNameController.rank = 0;
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

    public void OpenStop()
    {
        Antwoord_A.GetComponent<Toggle>().interactable = false;
        Antwoord_B.GetComponent<Toggle>().interactable = false;
        Antwoord_C.GetComponent<Toggle>().interactable = false;
/*        Antwoord_D.GetComponent<Toggle>().interactable = false;*/
        Volgende.GetComponent<Button>().interactable = false;
        Vorige.GetComponent<Button>().interactable = false;
        ArrowL.GetComponent<Button>().interactable = false;
        ArrowR.GetComponent<Button>().interactable = false;
        Inleveren.GetComponent<Button>().interactable = false;
        StoppenPopup.SetActive(true);
    }

    public void SluitStop()
    {
        Antwoord_A.GetComponent<Toggle>().interactable = true;
        Antwoord_B.GetComponent<Toggle>().interactable = true;
        Antwoord_C.GetComponent<Toggle>().interactable = true;
/*        Antwoord_D.GetComponent<Toggle>().interactable = true;*/
        Volgende.GetComponent<Button>().interactable = true;
        Vorige.GetComponent<Button>().interactable = true;
        ArrowL.GetComponent<Button>().interactable = true;
        ArrowR.GetComponent<Button>().interactable = true;
        Inleveren.GetComponent<Button>().interactable = true;
        StoppenPopup.SetActive(false);
    }
}
