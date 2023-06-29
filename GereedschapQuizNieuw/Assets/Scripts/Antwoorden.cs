using Newtonsoft.Json.Linq;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Antwoorden : MonoBehaviour
{
    public GameObject Antwoord_A;
    public GameObject Antwoord_B;
    public GameObject Antwoord_C;
    public GameObject Antwoord_D;
    public GameObject Volgende;
    public GameObject ArrowR;
    public GameObject InleverenBtn;
    public static string checkVraag;
    public void CheckAntwoord()
    {
        string filePath = Path.Combine("Assets", "vragen.txt");
        string vragenTxt = File.ReadAllText(filePath);
        JObject vragenJson = JObject.Parse(vragenTxt);
        checkVraag = (string)vragenJson["vragen"][StateNameController.vraagCount.ToString()]["laatste"];
        StateNameController.AGoed[StateNameController.vraagCount] = (string)vragenJson["vragen"][StateNameController.vraagCount.ToString()]["goed"];
        if (checkVraag == "ja")
        {
            StateNameController.laatsteVraag = true;
        }
    }
    public void AntwoordA()
    {
        Interactable();
        Antwoord_A.GetComponent<Toggle>().interactable = false;
        StateNameController.saveantwoord[StateNameController.vraagCount - 1] = "A";
        CheckAntwoord();
        Popup();
    }
    public void AntwoordB()
    {
        Interactable();
        Antwoord_B.GetComponent<Toggle>().interactable = false;
        StateNameController.saveantwoord[StateNameController.vraagCount - 1] = "B";
        CheckAntwoord();
        Popup();
    }
    public void AntwoordC()
    {
        Interactable();
        Antwoord_C.GetComponent<Toggle>().interactable = false;
        StateNameController.saveantwoord[StateNameController.vraagCount - 1] = "C";
        CheckAntwoord();
        Popup();
    }
    public void AntwoordD()
    {
        Interactable();
        Antwoord_D.GetComponent<Toggle>().interactable = false;
        StateNameController.saveantwoord[StateNameController.vraagCount - 1] = "D";
        CheckAntwoord();
        Popup();
    }

    public void Popup()
    {
        Time.timeScale = 1;
        if (StateNameController.laatsteVraag == true)
        {
            InleverenBtn.SetActive(true);
        }
        else
        {
            Volgende.SetActive(true);
            ArrowR.SetActive(true);
        }
    }
    public void Inleveren()
    {
        int i = 0;
        int b = 1;
        while (i < StateNameController.vraagCount)
        {
            if (StateNameController.saveantwoord[i] == StateNameController.AGoed[b])
            {
                StateNameController.Goed++;
            }
            i++;
            b++;
        }
    }
    public void Interactable()
    {
        Antwoord_A.GetComponent<Toggle>().interactable = true;
        Antwoord_B.GetComponent<Toggle>().interactable = true;
        Antwoord_C.GetComponent<Toggle>().interactable = true;
        Antwoord_D.GetComponent<Toggle>().interactable = true;
    }
}