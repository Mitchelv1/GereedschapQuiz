using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject StoppenPopup;
    public GameObject Antwoord_A;
    public GameObject Antwoord_B;
    public GameObject Antwoord_C;
    public GameObject Volgende;
    public GameObject Vorige;
    public GameObject ArrowL;
    public GameObject ArrowR;
    public GameObject Inleveren;
    
    //Deze functie word momenteel alleen gebruikt als je op Start klikt op het menu, hier mee ga je naar de volgende scene die in volgorde staan.
    public void Verder()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    //Deze functie word uitgevoerd als je op de knop Scorebord klikt in het menu.
    public void Scorebord()
    {
        SceneManager.LoadScene(4);
        StateNameController.isUpdateEnabled = false;
    }

    //Deze functie reset all variabelen naar 0 of standaard, zodat het mogelijk is om de quiz nog een keer te maken nadat je hem al 1 keer hebt gemaakt.
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
        StateNameController.nul = 0;
        StateNameController.rank = 0;
        SceneManager.LoadScene(0);
    }

    //Deze functie maakt alle antwoorden leeg die ingevuld zijn in een vorige quiz. 
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

    //Deze functie word uitgevoerd nadat je op de stoppen knop links boven van de quiz klikt. Hiermee kan je de quiz stoppen.
    public void OpenStop()
    {
        Antwoord_A.GetComponent<Toggle>().interactable = false;
        Antwoord_B.GetComponent<Toggle>().interactable = false;
        Antwoord_C.GetComponent<Toggle>().interactable = false;
        Volgende.GetComponent<Button>().interactable = false;
        Vorige.GetComponent<Button>().interactable = false;
        ArrowL.GetComponent<Button>().interactable = false;
        ArrowR.GetComponent<Button>().interactable = false;
        Inleveren.GetComponent<Button>().interactable = false;
        StoppenPopup.SetActive(true);
    }

    //Deze functie sluit de stoppen pop up.
    public void SluitStop()
    {
        Antwoord_A.GetComponent<Toggle>().interactable = true;
        Antwoord_B.GetComponent<Toggle>().interactable = true;
        Antwoord_C.GetComponent<Toggle>().interactable = true;
        Volgende.GetComponent<Button>().interactable = true;
        Vorige.GetComponent<Button>().interactable = true;
        ArrowL.GetComponent<Button>().interactable = true;
        ArrowR.GetComponent<Button>().interactable = true;
        Inleveren.GetComponent<Button>().interactable = true;
        StoppenPopup.SetActive(false);
    }
}