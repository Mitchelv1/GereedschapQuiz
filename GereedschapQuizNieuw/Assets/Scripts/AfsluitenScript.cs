using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AfsluitenScript : MonoBehaviour
{
    public GameObject AfsluitenPopup;
    public GameObject StartBtn;
    public GameObject AfsluitenBtn;

    public void OpenStop()
    {
        AfsluitenBtn.GetComponent<Button>().interactable = false;
        StartBtn.GetComponent<Button>().interactable = false;
        AfsluitenPopup.SetActive(true);
    }

    public void Afsluiten()
    {
        Application.Quit();
    }

    public void SluitStop()
    {
        AfsluitenBtn.GetComponent<Button>().interactable = true;
        StartBtn.GetComponent<Button>().interactable = true;
        AfsluitenPopup.SetActive(false);
    }
}
