using UnityEngine;
using TMPro;
using UnityEngine.UI;

//Dit script is alleen voor het laatste scherm waar je een naam en email invoerd, en als je op TAB drukt dat je dan naar de volgende box gaat.
public class TabInputField : MonoBehaviour
{
    public TMP_InputField NaamInput; // 0
    public TMP_InputField EmailInput; // 1
    public GameObject OpslaanBtn;
    public int InputSelected;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            InputSelected--;
            if (InputSelected < 0) InputSelected = 1;
            SelectInputField();
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            InputSelected++;
            if (InputSelected > 1) InputSelected = 0;
            SelectInputField();
        }

        void SelectInputField()
        {
            switch (InputSelected)
            {
                case 0: NaamInput.Select();
                    break;
                case 1: EmailInput.Select();
                    break;  
            }
        }

        if (NaamInput.text == "" || EmailInput.text == "")
        {
            OpslaanBtn.GetComponent<Button>().interactable = false;
        }
        else
        {
            OpslaanBtn.GetComponent<Button>().interactable = true;
        }
    }
    public void NaamSelected() => InputSelected = 0;
    public void EmailSelected() => InputSelected = 1;
}