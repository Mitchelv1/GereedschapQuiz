using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    public string naam;
    public int score = StateNameController.Goed;
    public string tijdString = StateNameController.tijd;
    public float tijd = StateNameController.timeValue;

    public User(string naam)
    {
        this.naam = naam;
    }
}
