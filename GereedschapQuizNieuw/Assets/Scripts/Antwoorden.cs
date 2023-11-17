using Newtonsoft.Json.Linq;
using System.IO;
using TMPro;
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
/*    public GameObject Antwoord_D;*/
    public GameObject Volgende;
    public GameObject ArrowR;
    public GameObject InleverenBtn;
    public static string checkVraag;

    private DatabaseReference dbReference;
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
/*    public void AntwoordD()
    {
        Interactable();
        Antwoord_D.GetComponent<Toggle>().interactable = false;
        StateNameController.saveantwoord[StateNameController.vraagCount - 1] = "D";
        CheckAntwoord();
        Popup();
    }*/

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
        StateNameController.timerOff = true;
    }

    public void Interactable()
    {
        Antwoord_A.GetComponent<Toggle>().interactable = true;
        Antwoord_B.GetComponent<Toggle>().interactable = true;
        Antwoord_C.GetComponent<Toggle>().interactable = true;
/*        Antwoord_D.GetComponent<Toggle>().interactable = true;*/
    }

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