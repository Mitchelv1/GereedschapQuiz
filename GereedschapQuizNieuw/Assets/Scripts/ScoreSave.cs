using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*using Firebase.Database;*/
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Runtime.InteropServices;
using System;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

public class ScoreSave : MonoBehaviour
{
    public TMP_InputField naamInput;
    public GameObject OpslaanPopup;
    public TextMeshProUGUI ScoresavedTxt;
    public TextMeshProUGUI SavingTxt;
    public GameObject VerderBtn;
    public GameObject OpslaanBtn;
    private string userID;
    public Text debugTxt;
    public TMP_InputField To;
    /*private DatabaseReference dbReference;*/

    void Start()
    {
        if (!PlayerPrefs.HasKey("userID"))
        {
            Guid myGUID = Guid.NewGuid();
            PlayerPrefs.SetString("userID", myGUID.ToString());
        }
        userID = PlayerPrefs.GetString("userID", null);
        /*userID = SystemInfo.deviceUniqueIdentifier;*/
        /*dbReference = FirebaseDatabase.DefaultInstance.RootReference;*/
    }
    [DllImport("__Internal")]
    public static extern void GetJSON(string path, string objectName, string callback, string fallback);

    [DllImport("__Internal")]
    public static extern void PostJSON(string path, string value, string objectName, string callback, string fallback);
    public void CreateUser()
    {
        OpslaanBtn.GetComponent<Button>().interactable = false;
        GetJSON("/users/" + userID + "/naam", gameObject.name, "CheckUserRequest", "OnRequestFailed");
/*        dbReference.Child("users").Child(userID).GetValueAsync().ContinueWith(task =>
        {
            DataSnapshot snapshot = task.Result;

            if (!snapshot.Exists)
            {
                User newUser = new User(naamInput.text);
                string json = JsonUtility.ToJson(newUser);

                dbReference.Child("users").Child(userID).SetRawJsonValueAsync(json);
            }
            else if (snapshot.Exists)
            {
                dbReference.Child("users").Child(userID).Child("score").GetValueAsync().ContinueWith(task =>
                {
                    DataSnapshot snapshot = task.Result;
                    int scoreValue = int.Parse(snapshot.Value.ToString());
                    if (StateNameController.Goed > scoreValue)
                    {
                        User newUser = new User(naamInput.text);
                        string json = JsonUtility.ToJson(newUser);

                        dbReference.Child("users").Child(userID).SetRawJsonValueAsync(json);
                    }
                    if (StateNameController.Goed == scoreValue)
                    {
                        dbReference.Child("users").Child(userID).Child("tijd").GetValueAsync().ContinueWith(task =>
                        {
                            DataSnapshot snapshot = task.Result;
                            float tijdValue = float.Parse(snapshot.Value.ToString());
                            if (StateNameController.timeValue < tijdValue)
                            {
                                User newUser = new User(naamInput.text);
                                string json = JsonUtility.ToJson(newUser);

                                dbReference.Child("users").Child(userID).SetRawJsonValueAsync(json);
                            }
                        });
                    }
                });
            }
        });*/
        StartCoroutine(OpslaanDelay());
    }

    /*public void CheckUser() => GetJSON("/users/"+userID+"/naam", gameObject.name, "CheckUserRequest", "OnRequestFailed");*/
    private void CheckUserRequest(string data)
    {
        if (data == "null")
        {
            User newUser = new User(naamInput.text);
            string json = JsonUtility.ToJson(newUser);
            PostJSON("/users/"+userID +"/", json, gameObject.name, "PostEmpty", "OnRequestFailed");
        }
        else if (data != "null")
        {
            GetJSON("/users/" + userID + "/score", gameObject.name, "CheckScoreRequest", "OnRequestFailed");
        }
    }
    private void PostEmpty(string data)
    {
    }
    private void CheckScoreRequest(string data)
    {
        if (StateNameController.Goed > int.Parse(data))
        {
            User newUser = new User(naamInput.text);
            string json = JsonUtility.ToJson(newUser);
            PostJSON("/users/" + userID + "/", json, gameObject.name, "PostEmpty", "OnRequestFailed");
        }
        if (StateNameController.Goed == int.Parse(data))
        {
            GetJSON("/users/" + userID + "/tijd", gameObject.name, "CheckTijdRequest", "OnRequestFailed");
        }
    }
    private void CheckTijdRequest(string data)
    {
        if (StateNameController.timeValue < float.Parse(data))
        {
            User newUser = new User(naamInput.text);
            string json = JsonUtility.ToJson(newUser);
            PostJSON("/users/" + userID + "/", json, gameObject.name, "PostEmpty", "OnRequestFailed");
        }
    }
    private void OnRequestFailed(string error)
    {
        debugTxt.text = error;
    }
    IEnumerator OpslaanDelay()
    {
        OpslaanPopup.SetActive(true);
        WaitForSecondsRealtime dotinterval = new WaitForSecondsRealtime(0.75f);
        for (int i = 0; i < 1; i++)
        {
            SavingTxt.text = "Opslaan";
            yield return dotinterval;
            SavingTxt.text = "Opslaan.";
            yield return dotinterval;
            SavingTxt.text = "Opslaan..";
            yield return dotinterval;
            SavingTxt.text = "Opslaan...";
            yield return dotinterval;
        }
        SavingTxt.gameObject.SetActive(false);
        ScoresavedTxt.gameObject.SetActive(true);
        VerderBtn.GetComponent<Button>().interactable = true;
        GetUsers();
    }
    public void GetUsers()
    {
/*        dbReference.Child("users").OrderByChild("score").LimitToLast(15).GetValueAsync().ContinueWith(task =>
        {
            DataSnapshot snapshot = task.Result;
            List<UserData> users = new List<UserData>();

            foreach (var childSnapshot in snapshot.Children)
            {
                string userID = childSnapshot.Key;
                int score = Convert.ToInt32(childSnapshot.Child("score").Value);
                float tijd = Convert.ToSingle(childSnapshot.Child("tijd").Value);
                string naam = Convert.ToString(childSnapshot.Child("naam").Value);
                string tijdstring = Convert.ToString(childSnapshot.Child("tijdString").Value);
                UserData user = new UserData(userID, score, tijd, naam, tijdstring);
                users.Add(user);
            }

            users.Sort((x, y) =>
            {
                int scoreComparison = y.score.CompareTo(x.score);
                if (scoreComparison != 0)
                {
                    return scoreComparison;
                }
                else
                {
                    return x.tijd.CompareTo(y.tijd);
                }
            });

            for (int i = 0; i < users.Count && i < 15; i++)
            {
                StateNameController.rank++;
                StateNameController.naamUser[StateNameController.rank - 1] = users[i].naam;
                StateNameController.scoreUser[StateNameController.rank - 1] = users[i].score.ToString();
                StateNameController.tijdUser[StateNameController.rank - 1] = users[i].tijdstring;
            }
        });*/
    }

    public class UserData
    {
        public string userID;
        public int score;
        public float tijd;
        public string naam;
        public string tijdstring;

        public UserData(string userID, int score, float tijd, string naam, string tijdstring)
        {
            this.userID = userID;
            this.score = score;
            this.tijd = tijd;
            this.naam = naam;
            this.tijdstring = tijdstring;
        }
    }

    public void Scorebord()
    {
        OpslaanPopup.SetActive(false);
        SavingTxt.gameObject.SetActive(true);
        ScoresavedTxt.gameObject.SetActive(false);
        VerderBtn.GetComponent<Button>().interactable = false;
        OpslaanBtn.GetComponent<Button>().interactable = true;
        SceneManager.LoadScene(4);
        StateNameController.isUpdateEnabled = false;
    }

    public void SendEmail(string recipient, string subject, string body)
    {
        string senderEmail = "gereedschapquiz@gmail.com";
        string senderPassword = "djwu vydp ibme xiay";

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("", senderEmail));
        message.To.Add(new MailboxAddress("", recipient));
        message.Subject = subject;
        message.Body = new TextPart("plain")
        {
            Text = body
        };

        using (var client = new SmtpClient())
        {
            client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            client.Authenticate(senderEmail, senderPassword);
            client.Send(message);
            client.Disconnect(true);
        }
    }

    public void OnSendEmailButtonClicked()
    {
        string recipient = To.text;
        string subject = "Resultaten Gereedschap Quiz";
        string body = "Je hebt zojuist de Gereedschap Quiz gemaakt! Hieronder staan de resultaten. \n\n" + "Naam: " + naamInput.text + "\n" + "Score: " + StateNameController.Goed + "/" + StateNameController.aantVragen + "\n" + "Tijd besteed: " + StateNameController.tijd;
        SendEmail(recipient, subject, body);
        Debug.Log("Email sent");
    }
}
