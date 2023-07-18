/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

public class EmailSender : MonoBehaviour
{
    public TMP_InputField To;
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
        string subject = "Dit is een test titel";
        string body = "Hier komt de tekst te staan";
        SendEmail(recipient, subject, body);
        Debug.Log("Email sent");
    }
}
*/