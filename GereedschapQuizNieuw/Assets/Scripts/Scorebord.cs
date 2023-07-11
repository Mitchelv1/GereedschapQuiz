using UnityEngine;
using TMPro;

public class Scorebord : MonoBehaviour
{
    public TextMeshProUGUI[] naam;
    public TextMeshProUGUI[] score;
    public TextMeshProUGUI[] tijd;
    public TextMeshProUGUI check1;

    void Start()
    {
        ChangeTxt();
    }

    private void Update()
    {
        ChangeTxt();
    }

    public void ChangeTxt() 
    {
        for (int i = StateNameController.testint; i < StateNameController.rank; i++)
        {
            naam[i].text = StateNameController.naamUser[i];
            score[i].text = StateNameController.scoreUser[i];
            tijd[i].text = StateNameController.tijdUser[i];
        }
    }
}
