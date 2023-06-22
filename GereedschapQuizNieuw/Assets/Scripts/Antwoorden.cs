using Newtonsoft.Json.Linq;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Antwoorden : MonoBehaviour
{
    /*    public TextMeshProUGUI GoedTxt;
        public TextMeshProUGUI FoutTxt;*/
/*    public TextMeshProUGUI TestTxt;*/
    public GameObject Antwoord_A;
    public GameObject Antwoord_B;
    public GameObject Antwoord_C;
    public GameObject Antwoord_D;
/*    public GameObject CanvasPopup;*/
    public GameObject Volgende;
    public GameObject InleverenBtn;
    /*    string Resultaat;*/

    public static string checkVraag = StateNameController.checkVraag;
    public static int Goed = StateNameController.Goed;
    public static int Fout = StateNameController.Fout;
    public static string AGoed;
/*    public static int vraagCount = StateNameController.vraagCount;*/
    public void CheckAntwoord()
    {
        string filePath = Path.Combine("Assets", "vragen.txt");
        string vragenTxt = File.ReadAllText(filePath);
        JObject vragenJson = JObject.Parse(vragenTxt);
        /*int vraagCountString = StateNameController.vraagCount;*/
        /*int vraagCountString = 1;*/
/*        int checkVolgende = vraagCount + 1;*/

/*        TestTxt.text = StateNameController.vraagCount.ToString();*/
        checkVraag = (string)vragenJson["vragen"][StateNameController.vraagCount.ToString()]["laatste"];
        AGoed = (string)vragenJson["vragen"][StateNameController.vraagCount.ToString()]["goed"];
        if (checkVraag == "ja")
        {
            StateNameController.laatsteVraag = true;
/*          vraagCount = 0;
            StateNameController.vraagCount = vraagCount;*/
        }
    }
    public void AntwoordA()
    {
        CheckAntwoord();
        if (AGoed == "A")
        {
            Antwoord_A.GetComponent<Image>().color = new Color32(11, 212, 0, 255);
/*            Resultaat = "Goed";*/
            Goed++;
        }
        else
        {
            Antwoord_A.GetComponent<Image>().color = new Color32(255, 64, 64, 255);
            Fout++;
        }
        Popup();
    }
    public void AntwoordB()
    {
        CheckAntwoord();
        if (AGoed == "B")
        {
            Antwoord_B.GetComponent<Image>().color = new Color32(11, 212, 0, 255);
/*            Resultaat = "Goed";*/
            Goed++;
        }
        else
        {
            Antwoord_B.GetComponent<Image>().color = new Color32(255, 64, 64, 255);
            Fout++;
        }
        Popup();
    }
    public void AntwoordC()
    {
        CheckAntwoord();
        if (AGoed == "C")
        {
            Antwoord_C.GetComponent<Image>().color = new Color32(11, 212, 0, 255);
/*            Resultaat = "Goed";*/
            Goed++;
        }
        else
        {
            Antwoord_C.GetComponent<Image>().color = new Color32(255, 64, 64, 255);
            Fout++;
        }
        Popup();
    }
    public void AntwoordD()
    {
        CheckAntwoord();
        if (AGoed == "D")
        {
            Antwoord_D.GetComponent<Image>().color = new Color32(11, 212, 0, 255);
/*            Resultaat = "Goed";*/
            Goed++;
        }
        else
        {
            Antwoord_D.GetComponent<Image>().color = new Color32(255, 64, 64, 255);
            Fout++;
        }
        Popup();
    }

    public void Popup()
    {
        Time.timeScale = 1;
        Antwoord_A.GetComponent<Button>().enabled = false;
        Antwoord_B.GetComponent<Button>().enabled = false;
        Antwoord_C.GetComponent<Button>().enabled = false;
        Antwoord_D.GetComponent<Button>().enabled = false;
        if (StateNameController.laatsteVraag == true)
        {
            InleverenBtn.SetActive(true);
        }
        else
        {
            Volgende.SetActive(true);
        }
        /*CanvasPopup.SetActive(true);*/
        /*        if (Resultaat == "Goed")
                {
                    GoedTxt.gameObject.SetActive(true);
                    FoutTxt.gameObject.SetActive(false);
                    Resultaat = "";
                }
                else
                {
                    FoutTxt.gameObject.SetActive(true);
                    GoedTxt.gameObject.SetActive(false);
                }*/
    }
}