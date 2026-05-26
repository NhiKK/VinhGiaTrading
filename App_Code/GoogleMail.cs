using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.IO;

public class GoogleMail
{
    private int _port;
    private String _email, _password, _smtp;
    private bool _ssl;
    public String Smtp
    {
        get { return _smtp; }
        set { _smtp = value; }
    }

    public String Password
    {
        get { return _password; }
        set { _password = value; }
    }

    public String Email
    {
        get { return _email; }
        set { _email = value; }
    }

    public int Port
    {
        get { return _port; }
        set { _port = value; }
    }

    public bool Ssl
    {
        get { return _ssl; }
        set { _ssl = value; }
    }

    public GoogleMail()
    { 
    
    }

    public GoogleMail(String email, String password, String smtp, int port)
    {
        this._email = email;
        this._password = password;
        this._smtp = smtp;
        this._port = port;
        this._ssl = false;
    }

    public GoogleMail(String email, String password, String smtp, int port, bool ssl)
    {
        this._email = email;
        this._password = password;
        this._smtp = smtp;
        this._port = port;
        this._ssl = ssl;
    }

    public void Send(String to, String subject, String body, params object[] args)
    {
        String from = _email;
        this.Send(from, to, "", "", subject, body, "", args);
    }

    public void Send(String from, String to, String subject, String body, params object[] args)
    {
        this.Send(from, to, "", "", subject, body, "", args);
    }

    public void Send(String from, String to, String cc, String subject, String body, String attachments, params object[] args)
    {
        this.Send(from, to, cc, "", subject, body, attachments, args);
    }

    public void Send(String from, String to, String cc, String bcc, String subject, String body, String attachments, params object[] args)
    {
        if (Regex.IsMatch(body, "^([cCdDeEfFgGhHiIjI]:).+"))
        {
            body = File.ReadAllText(body);
        }
        body = String.Format(body, args);

        MailMessage mail = new MailMessage();
        mail.From = new MailAddress(from);
        mail.ReplyTo = new MailAddress(from);
        
        to = to.Replace(" ", "");
        string[] tos = to.Split(";,".ToArray());
        foreach (string item in tos)
        {
            mail.To.Add(new MailAddress(item));    
        }
        mail.Subject = subject;
        mail.Body = body;
        mail.IsBodyHtml = true;

        if (!String.IsNullOrEmpty(cc))
        {
            mail.CC.Add(cc);
        }
        if (!String.IsNullOrEmpty(bcc))
        {
            mail.Bcc.Add(bcc);
        }
        if (!String.IsNullOrEmpty(attachments))
        {
            String[] fileNames = attachments.Split(";,".ToCharArray());
            foreach (String fileName in fileNames)
            {
                if (fileName.Trim().Length > 0)
                {
                    mail.Attachments.Add(new Attachment(fileName.Trim()));
                }
            }
        }

        ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
        SmtpClient client = new SmtpClient(_smtp, _port);
        client.EnableSsl = _ssl;
        client.UseDefaultCredentials = false;
        client.Credentials = new NetworkCredential(_email, _password);
        client.Send(mail);
    }
}
