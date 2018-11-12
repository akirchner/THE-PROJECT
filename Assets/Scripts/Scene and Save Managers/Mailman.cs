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

    void Send()
    {
        MailMessage mail = new MailMessage();

        mail.From = new MailAddress("zetagamesmailman@gmail.com");
        mail.To.Add("zetagames18@gmail.com");
        mail.Subject = "New Level Code";
        mail.Body = "---Insert Code Here---";

        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 587;
        smtpServer.Credentials = new System.Net.NetworkCredential("zetagamesmailman@gmail.com", "#fluxisarealforce") as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };
        smtpServer.Send(mail);

        Debug.Log("Sent mail!");
    }
}