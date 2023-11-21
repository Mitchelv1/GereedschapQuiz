using UnityEngine;
using UnityEngine.UI;

public class AfsluitenScript : MonoBehaviour
{
    public GameObject AfsluitenPopup;
    public GameObject StartBtn;
    public GameObject AfsluitenBtn;

    //Deze functie opent een pop up als je op de afsluiten knop klikt.
    public void OpenStop()
    {
        AfsluitenBtn.GetComponent<Button>().interactable = false;
        StartBtn.GetComponent<Button>().interactable = false;
        AfsluitenPopup.SetActive(true);
    }

    //Deze functie sluit de applicatie als je op Ja klikt bij de pop up.
    public void Afsluiten()
    {
        Application.Quit();
    }

    //Deze functie sluit de pop up als je op Nee klikt bij de pop up
    public void SluitStop()
    {
        AfsluitenBtn.GetComponent<Button>().interactable = true;
        StartBtn.GetComponent<Button>().interactable = true;
        AfsluitenPopup.SetActive(false);
    }
}