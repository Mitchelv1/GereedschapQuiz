using Newtonsoft.Json.Linq;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NormalVraag : MonoBehaviour
{
    public TextMeshProUGUI VraagTxt;
    public TextMeshProUGUI AntwoordATxt;
    public TextMeshProUGUI AntwoordBTxt;
    public TextMeshProUGUI AntwoordCTxt;
/*    public TextMeshProUGUI AntwoordDTxt;*/
    public TextMeshProUGUI vraagNummerTxt;
    public TextMeshProUGUI totaalTxt;
    public GameObject Antwoord_A;
    public GameObject Antwoord_B;
    public GameObject Antwoord_C;
/*    public GameObject Antwoord_D;*/
    public GameObject Volgende;
    public GameObject Vorige;
    public GameObject ArrowL;
    public GameObject ArrowR;
    public GameObject Inleveren;
    public static string Type;
    public static string vindLaatste;
    public static int vraagCountString;
    public static int aantVragen = 1;
    public int Terug;

    private void Start()
    {
        CheckVraag();
        if (StateNameController.checkVragen != false)
        {
            AantalVragen();
        }
        totaalTxt.text = StateNameController.aantVragen.ToString();
    }

    public void AantalVragen()
    {
        string readFromFilePath = Application.streamingAssetsPath + "/Vragen/" + "vragen" + ".txt";
        string vragenTxt = File.ReadAllText(readFromFilePath);
        JObject vragenJson = JObject.Parse(vragenTxt);
        while ((string)vragenJson["vragen"][aantVragen.ToString()]["laatste"] != "ja")
        {
            aantVragen++;
        }
        StateNameController.aantVragen = aantVragen;
        StateNameController.checkVragen = false;
    }
    public void VraagReset()
    {
        if (StateNameController.vraagCount == 1)
        {
            Vorige.SetActive(false);
            ArrowL.SetActive(false);
        }
        else
        {
            Vorige.SetActive(true);
            ArrowL.SetActive(true);
        }
        Volgende.SetActive(false);
        ArrowR.SetActive(false);
        Inleveren.SetActive(false);
        Antwoord_A.GetComponent<Toggle>().interactable = true;
        Antwoord_B.GetComponent<Toggle>().interactable = true;
        Antwoord_C.GetComponent<Toggle>().interactable = true;
/*        Antwoord_D.GetComponent<Toggle>().interactable = true;*/
        Antwoord_A.GetComponent<Toggle>().isOn = false;
        Antwoord_B.GetComponent<Toggle>().isOn = false;
        Antwoord_C.GetComponent<Toggle>().isOn = false;
/*        Antwoord_D.GetComponent<Toggle>().isOn = false;*/
    }
    public void CheckVraag()
    {
        VraagReset();
        string readFromFilePath = Application.streamingAssetsPath + "/Vragen/" + "vragen" + ".txt";
        string vragenTxt = File.ReadAllText(readFromFilePath);
        JObject vragenJson = JObject.Parse(vragenTxt);
        vraagCountString = StateNameController.vraagCount + 1;
        Type = (string)vragenJson["vragen"][vraagCountString.ToString()]["type"];
        switch (Type)
        {
            case "normal":
                StateNameController.vraagCount++;
                Normaal();
                break;

            case "image":
                SceneManager.LoadScene(2);
                break;
        }
        Opgeslagen();
    }

    public void VorigeVraag()
    {
        VraagReset();
        if (StateNameController.laatsteVraag == true)
        {
            StateNameController.laatsteVraag = false;
        }
        string readFromFilePath = Application.streamingAssetsPath + "/Vragen/" + "vragen" + ".txt";
        string vragenTxt = File.ReadAllText(readFromFilePath);
        JObject vragenJson = JObject.Parse(vragenTxt);
        vraagCountString = StateNameController.vraagCount - 1;
        Terug = StateNameController.vraagCount - 2;
        Type = (string)vragenJson["vragen"][vraagCountString.ToString()]["type"];
        switch (Type)
        {
            case "normal":
                StateNameController.vraagCount--;
                Normaal();
                break;

            case "image":
                StateNameController.vraagCount = Terug;
                SceneManager.LoadScene(2);
                break;
        }
        Opgeslagen();
    }

    public void Normaal()
    {
        VraagReset();
        string readFromFilePath = Application.streamingAssetsPath + "/Vragen/" + "vragen" + ".txt";
        string vragenTxt = File.ReadAllText(readFromFilePath);
        JObject vragenJson = JObject.Parse(vragenTxt);
        Type = (string)vragenJson["vragen"][StateNameController.vraagCount.ToString()]["type"];
        string[] antwoordenJson = new string[3];
        string vraagJson = (string)vragenJson["vragen"][StateNameController.vraagCount.ToString()]["vraag"];
        for (int i = 0; i < 3; i++)
        {
            antwoordenJson[i] = (string)vragenJson["vragen"][StateNameController.vraagCount.ToString()][$"antwoord_{i + 1}"];
        }
        AntwoordATxt.text = antwoordenJson[0];
        AntwoordBTxt.text = antwoordenJson[1];
        AntwoordCTxt.text = antwoordenJson[2];
/*        AntwoordDTxt.text = antwoordenJson[3];*/
        VraagTxt.text = vraagJson;
        if (StateNameController.vraagCount < 10)
        {
            vraagNummerTxt.text = "0" + StateNameController.vraagCount.ToString();
        }
        else
        {
            vraagNummerTxt.text = StateNameController.vraagCount.ToString();

        }
    }

    public void Opgeslagen()
    {
        if (StateNameController.saveantwoord[StateNameController.vraagCount - 1] == "A")
        {
            Antwoord_A.GetComponent<Toggle>().isOn = true;
        }
        if (StateNameController.saveantwoord[StateNameController.vraagCount - 1] == "B")
        {
            Antwoord_B.GetComponent<Toggle>().isOn = true;
        }
        if (StateNameController.saveantwoord[StateNameController.vraagCount - 1] == "C")
        {
            Antwoord_C.GetComponent<Toggle>().isOn = true;
        }
/*        if (StateNameController.saveantwoord[StateNameController.vraagCount - 1] == "D")
        {
            Antwoord_D.GetComponent<Toggle>().isOn = true;
        }*/
    }
}
