using Newtonsoft.Json.Linq;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ImageVraag : MonoBehaviour
{
    public TextMeshProUGUI VraagTxt;
    public TextMeshProUGUI AntwoordATxt;
    public TextMeshProUGUI AntwoordBTxt;
    public TextMeshProUGUI AntwoordCTxt;
    public TextMeshProUGUI vraagNummerTxt;
    public TextMeshProUGUI totaalTxt;
    public GameObject Antwoord_A;
    public GameObject Antwoord_B;
    public GameObject Antwoord_C;
    public GameObject Volgende;
    public GameObject Vorige;
    public GameObject ArrowL;
    public GameObject ArrowR;
    public GameObject Inleveren;
    public Image Afbeelding;
    public Sprite image;
    public static string Type;
    public static string Img;
    public static int vraagCountString;
    public int Terug;

    private void Start()
    {
        CheckVraag();
        totaalTxt.text = StateNameController.aantVragen.ToString();
    }

    //Reset de knoppen naar standaard zodat niks geselecteerd is en als je bijvoorbeeld van de laatste vraag 1 vraag terug gaat dat de inleveren knop ook weer weg gaat.
    public void VraagReset()
    {
        ArrowL.SetActive(true);
        Vorige.SetActive(true);
        Volgende.SetActive(false);
        ArrowR.SetActive(false);
        Inleveren.SetActive(false);
        Antwoord_A.GetComponent<Toggle>().interactable = true;
        Antwoord_B.GetComponent<Toggle>().interactable = true;
        Antwoord_C.GetComponent<Toggle>().interactable = true;
        Antwoord_A.GetComponent<Toggle>().isOn = false;
        Antwoord_B.GetComponent<Toggle>().isOn = false;
        Antwoord_C.GetComponent<Toggle>().isOn = false;
    }

    //Kijkt wat voor type vraag het is via het tekst bestand. Als het een normale vraag is (zonder afbeelding) gaat de applicatie naar die specifieke scene toe,
    //als het een image is blijft de applicatie in deze scene en ook dus dit script om verder te gaan.
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
                SceneManager.LoadScene(1);
                break;

            case "image":
                StateNameController.vraagCount++;
                Image();
                break;
        }
        Opgeslagen();
    }

    //Deze functie word uitgevoerd als je op de vorige knop drukt.
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
                StateNameController.vraagCount = Terug;
                SceneManager.LoadScene(1);
                break;

            case "image":
                StateNameController.vraagCount--;
                Image();
                break;
        }
        Opgeslagen();
    }

    //Dit is de functie die het meeste werk doet en er voor zorgt dat de vraag, antwoorden en afbeelding er komen te staan.
    public void Image()
    {
        VraagReset();
        string readFromFilePath = Application.streamingAssetsPath + "/Vragen/" + "vragen" + ".txt";
        string vragenTxt = File.ReadAllText(readFromFilePath);
        JObject vragenJson = JObject.Parse(vragenTxt);
        string[] antwoordenJson = new string[4];
        string vraagJson = (string)vragenJson["vragen"][StateNameController.vraagCount.ToString()]["vraag"];
        for (int i = 0; i < 3; i++)
        {
            antwoordenJson[i] = (string)vragenJson["vragen"][StateNameController.vraagCount.ToString()][$"antwoord_{i + 1}"];
        }
        Img = (string)vragenJson["vragen"][StateNameController.vraagCount.ToString()]["image"];
        Type = (string)vragenJson["vragen"][StateNameController.vraagCount.ToString()]["type"];
        image = Resources.Load<Sprite>(Img);
        Afbeelding.sprite = image;
        AntwoordATxt.text = antwoordenJson[0];
        AntwoordBTxt.text = antwoordenJson[1];
        AntwoordCTxt.text = antwoordenJson[2];
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

    //Deze functie slaat het geselecteerde antwoord op en maakt er een variabele van.
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
    }
}