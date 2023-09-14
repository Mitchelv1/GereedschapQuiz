using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;


public class Test : MonoBehaviour
{
    public Text text;
    public Text statusText;
    public Text IDText;

    [DllImport("__Internal")]
    public static extern void GetJSON(string path, string objectName, string callback, string fallback);

    [DllImport("__Internal")]
    public static extern void PostJSON(string path, string value, string objectName, string callback, string fallback);
    private void Start()
    {
/*        string userID = PlayerPrefs.GetString("userID", null);
        if (userID == null)
        {
            System.Guid myGUID = System.Guid.NewGuid();
            PlayerPrefs.SetString("userID", myGUID.ToString());
        }*/
        if (!PlayerPrefs.HasKey("userID"))
        {
            Guid myGUID = Guid.NewGuid();
            PlayerPrefs.SetString("userID", myGUID.ToString());
        }  
        string userID = PlayerPrefs.GetString("userID", null);
        IDText.text = userID;
        GetJSON("/users/user1/naam", gameObject.name, "OnRequestSuccess", "OnRequestFailed");
        /*GetJSON("/users/fb582db73a5153b3a804455e50d9523381dd0d4b/naam", gameObject.name, "OnRequestSuccess", "OnRequestFailed");*/
        /*AddName();*/
    }

    public void NewUser() => PostJSON("/users/user1", "", gameObject.name, "DisplayInfo", "DisplayErrorObject");

    public void AddName() => PostJSON("/users/user1/naam", "Mitchel2", gameObject.name, "DisplayName", "DisplayErrorObject");

    private void OnRequestSuccess(string data)
    {
        if (data != "null")
        {
            text.color = Color.green;
            string replacedData = data.Replace("\"", "");
            text.text = replacedData;
        }
        else
        {
            text.color = Color.white;
            text.text = "Data is null or does not exist";
        }
    }

    private void OnRequestFailed(string error)
    {
        text.color = Color.red;
        text.text = error;
    }

    public void DisplayName(string info)
    {
        /*naamText.text = info;*/
    }
    public void DisplayInfo(string info)
    {
        statusText.text = info;
    }
    public class FirebaseError
    {
        public string code;
        public string message;
        public string details;
    }
    public void DisplayErrorObject(string error)
    {
        var parsedError = JsonUtility.FromJson<FirebaseError>(error);
        DisplayError(parsedError.message);
    }

    public void DisplayError(string error)
    {
        statusText.text = error;
    }
}
