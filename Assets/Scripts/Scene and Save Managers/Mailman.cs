using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class Mailman : MonoBehaviour
{
    private void Start()
    {
        
    }

    void Send(string subject, string body)
    {
        MailMessage mail = new MailMessage();

        mail.From = new MailAddress("zetagamesmailman@gmail.com");
        mail.To.Add("zetagames18@gmail.com");
        mail.Subject = subject;
        mail.Body = body;

        SmtpClient smtpServer = new SmtpClient();
        smtpServer.Host = "smtp.sendgrid.net";
        smtpServer.Port = 587;
        smtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpServer.Credentials = new System.Net.NetworkCredential("apikey", "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") as ICredentialsByHost;
        smtpServer.EnableSsl = false;
        ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };

        try {
            smtpServer.Send(mail);
        }  
        catch (Exception ex) {
            Debug.Log("Exception while smtpServer.Send(mail): " + ex.ToString());            
        }   
    }

    public void SubmitReport()
    {
        //Send("Bug Report " + UnityEngine.Random.Range(1, 1000000) + " - " + SystemInfo.operatingSystem, GameProperties.bugDescription);
        Send("Bug Report " + UnityEngine.Random.Range(1, 1000000), GameProperties.bugDescription);
        GameProperties.bugDescription = "";
    }

    public void SendLevel()
    {
        Send("Level Code " + UnityEngine.Random.Range(1, 1000000), GameProperties.levelcode);
    }
}