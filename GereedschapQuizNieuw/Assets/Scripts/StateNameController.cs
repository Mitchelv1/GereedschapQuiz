using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateNameController : MonoBehaviour
{
    public static int Goed = 0;
    public static int vraagCount;
    public static float timeValue;
    public static bool timerOff = false;
    public static bool checkVragen = true;
    public static string checkVraag;
    public static bool laatsteVraag = false;
    public static int aantVragen;
    public static bool isUpdateEnabled = false;
    public static bool isReloadEnabled = true;
    public static string[] saveantwoord = new string[99];
    public static string[] AGoed = new string[99];
    public static string[] naamUser = new string[15];
    public static string[] scoreUser = new string[15];
    public static string[] tijdUser = new string[15];
    public static float[] tijdFloat = new float[15];
    public static string tijd;
    public static int nul = 0;
    public static int rank = 0;
}
