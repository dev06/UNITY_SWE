using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
public class Email : MonoBehaviour {



	public static void Send(string email, string message)
	{
		MailMessage mail = new MailMessage();

		mail.From = new MailAddress(email);
		mail.To.Add(email);
		mail.Subject = "Thank you for your purchase | Order Receipt";
		mail.Body = message;

		SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
		smtpServer.Port = 587;
		smtpServer.Credentials = new System.Net.NetworkCredential(email, "kennesawowl123") as ICredentialsByHost;
		smtpServer.EnableSsl = true;
		ServicePointManager.ServerCertificateValidationCallback =
		    delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{ return true; };
		smtpServer.Send(mail);
		Debug.Log("success");
	}
}
