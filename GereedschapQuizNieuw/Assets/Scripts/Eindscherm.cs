using UnityEngine;
using TMPro;

public class Eindscherm : MonoBehaviour
{
    public TextMeshProUGUI Goed;
    public TextMeshProUGUI Totaal;

    //Dit is meer om het wat mooier te maken, als het aantal goed onder de 10 is komt er een 0 voor te staan zodat het mooi gecentreerd is.
    void Start()
    {
        if (StateNameController.Goed < 10)
        {
            Goed.text = "0" + StateNameController.Goed.ToString();
        }
        else
        {
            Goed.text = StateNameController.Goed.ToString();
        }
        Totaal.text = StateNameController.vraagCount.ToString();
    }
}