using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateNameController : MonoBehaviour
{
    public static int Goed = 0;
    public static int Fout = 0;
    public static int vraagCount;
    public static float timeValue;
    public static bool timerOff = false;
    public static bool checkVragen = true;
    public static string checkVraag;
    public static bool laatsteVraag = false;
    public static int aantVragen;
    public static bool isUpdateEnabled = false;
    public static string[] saveantwoord = new string[99];
    public static string[] AGoed = new string[99];
}
