using Newtonsoft.Json.Linq;
using System.IO;
using Firebase.Database;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class Antwoorden : MonoBehaviour
{
    public GameObject Antwoord_A;
    public GameObject Antwoord_B;
    public GameObject Antwoord_C;
    public GameObject Volgende;
    public GameObject ArrowR;
    public GameObject InleverenBtn;
    public static string checkVraag;
    private DatabaseReference dbReference;

    //Kijkt of de vraag de laatste vraag is en start een coroutine om het goede antwoord te krijgen en een variabel er van te maken.
    public void CheckAntwoord()
    {
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        string readFromFilePath = Application.streamingAssetsPath + "/Vragen/" + "vragen" + ".txt";
        string vragenTxt = File.ReadAllText(readFromFilePath);
        JObject vragenJson = JObject.Parse(vragenTxt);
        checkVraag = (string)vragenJson["vragen"][StateNameController.vraagCount.ToString()]["laatste"];
        StartCoroutine(GetGoed((string goed) =>
        {
            StateNameController.AGoed[StateNameController.vraagCount] = goed;
        }));
        if (checkVraag == "ja")
        {
            StateNameController.laatsteVraag = true;
        }
    }

    //Deze functie word uitgevoerd als er op het eerste antwoord word geklikt.
    public void AntwoordA()
    {
        Interactable();
        Antwoord_A.GetComponent<Toggle>().interactable = false;
        StateNameController.saveantwoord[StateNameController.vraagCount - 1] = "A";
        CheckAntwoord();
        Popup();
    }

    //Deze functie word uitgevoerd als er op het tweede antwoord word geklikt.
    public void AntwoordB()
    {
        Interactable();
        Antwoord_B.GetComponent<Toggle>().interactable = false;
        StateNameController.saveantwoord[StateNameController.vraagCount - 1] = "B";
        CheckAntwoord();
        Popup();
    }

    //Deze functie word uitgevoerd als er op het derde antwoord word geklikt.
    public void AntwoordC()
    {
        Interactable();
        Antwoord_C.GetComponent<Toggle>().interactable = false;
        StateNameController.saveantwoord[StateNameController.vraagCount - 1] = "C";
        CheckAntwoord();
        Popup();
    }

    //Dit zet de volgende knop aan zodat je pas naar de volgende vraag kan gaan als je op een antwoord heb gedrukt. Als het de laatste vraag is komt er de inleveren knop.
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

    //Bij het klikken op de inleveren knop word deze functie uitgevoerd en worden de antwoorden gecontroleerd met de goede antwoorden.
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
        StateNameController.timerOff = true;
    }

    public void Interactable()
    {
        Antwoord_A.GetComponent<Toggle>().interactable = true;
        Antwoord_B.GetComponent<Toggle>().interactable = true;
        Antwoord_C.GetComponent<Toggle>().interactable = true;
    }

    //Dit is de coroutine die het goede antwoord krijgt uit de database.
    public IEnumerator GetGoed(Action<string> onCallback)
    {
        var goedData = dbReference.Child("vragen").Child(StateNameController.vraagCount.ToString()).Child("goed").GetValueAsync();

        yield return new WaitUntil(predicate: () => goedData.IsCompleted);

        if (goedData != null)
        {
            DataSnapshot snapshot = goedData.Result;

            onCallback.Invoke(snapshot.Value.ToString());
        }
    }
}