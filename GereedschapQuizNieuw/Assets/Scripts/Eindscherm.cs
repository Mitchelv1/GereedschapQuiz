using UnityEngine;
using TMPro;

public class Eindscherm : MonoBehaviour
{
    public TextMeshProUGUI Goed;
    public TextMeshProUGUI Totaal;

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
