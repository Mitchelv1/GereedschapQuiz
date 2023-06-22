using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI Score;

    private void Start()
    {
        int Goed = StateNameController.Goed;
        int Fout = StateNameController.Fout;
        int Vragen = Goed + Fout;
        Score.text = Goed + "/" + Vragen;
    }
}
