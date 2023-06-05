using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DMS_Utilities;

public class Email
{
    public static string SenderEmailAddress = System.Configuration.ConfigurationManager.AppSettings["SenderEmailAddress"];
    public static string SenderEmailPassword = System.Configuration.ConfigurationManager.AppSettings["SenderEmailPassword"];
    public static string SenderSMTPServer = System.Configuration.ConfigurationManager.AppSettings["SenderSMTPServer"];
    public static string Port = System.Configuration.ConfigurationManager.AppSettings["Port"];
    public static string clientEnableSsl = System.Configuration.ConfigurationManager.AppSettings["clientEnableSsl"];
    public static string Displayname = System.Configuration.ConfigurationManager.AppSettings["Displayname"];
    public static string ReplyTo = System.Configuration.ConfigurationManager.AppSettings["ReplyTo"];
	public static string CallCenterEmailAddress = System.Configuration.ConfigurationManager.AppSettings["CallCenterEmailAddress"];


    public static void SendMail(string to, string subject, string msg, string cc, string Attachment)
    {
        if (SenderSMTPServer != "" && SenderSMTPServer != null && SenderEmailAddress != "" && SenderEmailAddress != null && Port != "" && Port != null)
        {
            Thread email = new Thread(delegate ()
            {
                SendMail(to, subject, msg, cc, Attachment, "");
            });

            email.IsBackground = true;
            email.Start();
        }
    }
    public static void SendMail(string to, string subject, string msg, string cc, string Attachment, string var)
    {
        to = "fahad.iqbal@sybrid.com;haris.shamsi@sybrid.com";
        //to = "MTahir.Nazir@sanofi.com";
        cc = "";
        if (string.IsNullOrEmpty(to) == false)
        {
            if (SenderSMTPServer != "" && SenderSMTPServer != null && SenderEmailAddress != "" && SenderEmailAddress != null && Port != "" && Port != null)
            {
                string BCC = System.Configuration.ConfigurationManager.AppSettings["BCC"];
                string EmailsAddress = subject + " : " + to + (cc != "" ? (";" + cc + "") : "") + (BCC != "" ? (";" + BCC + "") : "");
                try
                {
                    MailMessage message = new MailMessage();
                    string[] addresses = to.Split(';');
                    foreach (string address in addresses)
                    {
                        if (address != "")
                        {
                            message.To.Add(new MailAddress(address));
                        }
                    }
                    if (cc != "")
                    {
                        string[] cc_address = cc.Split(';');
                        foreach (string address in cc_address)
                        {
                            if (address != "")
                            {
                                message.CC.Add(new MailAddress(address));
                            }
                        }
                    }
                    if (BCC != "")
                    {
                        string[] BCC_address = BCC.Split(';');
                        foreach (string address in BCC_address)
                        {
                            if (address != "")
                            {
                                message.Bcc.Add(new MailAddress(address));
                            }
                        }
                    }


                    if (Attachment != string.Empty)
                    {
                        string[] Attachment_ = Attachment.Split(';');
                        foreach (string file in Attachment_)
                        {
                            if (file != "")
                            {
                                System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(file);
                                message.Attachments.Add(attach);
                            }
                        }
                    }

                    message.From = new MailAddress(CallCenterEmailAddress, Displayname);
                    message.Subject = subject;
                    message.Body = msg;
                    message.IsBodyHtml = true;
                    SmtpClient client = new SmtpClient();
                    client.Port = Convert.ToInt32(Port);
                    client.Host = SenderSMTPServer;
                    System.Net.NetworkCredential objNetworkCredential = new System.Net.NetworkCredential(SenderEmailAddress, SenderEmailPassword);
                    client.EnableSsl = Convert.ToBoolean(clientEnableSsl);
                    client.UseDefaultCredentials = false;
                    client.Credentials = objNetworkCredential;
                    client.Send(message);
                    WriteFile("Success - ", "", EmailsAddress);
                }
                catch (Exception ex)
                {
                    WriteFile("Error   - ", ex.ToString(), EmailsAddress);
                }
            }
        }
    }
    private static void WriteFile(string Type, string error, string EmailsAddress)
    {
        DateTime dateTime = DateTime.Now;
        string EmailLog = CommonObjects.GetFileUploadPath(GenericConstants.EmailLog);
        if (!Directory.Exists(EmailLog))
            Directory.CreateDirectory(EmailLog);
        string Year = EmailLog + "/" + dateTime.ToString("yyyy");
        if (!Directory.Exists(Year))
            Directory.CreateDirectory(Year);
        string Month = Year + "/" + dateTime.ToString("MMM");
        if (!Directory.Exists(Month))
            Directory.CreateDirectory(Month);
        string Date = dateTime.ToString(GenericConstants.DateFormat1_);
        string LogFileName = Month + "/" + Date + ".txt";

        if (!System.IO.File.Exists(LogFileName))
        {
            // Create a file to write to. 
            using (System.IO.StreamWriter sw = System.IO.File.CreateText(LogFileName))
            {

            }
        }
        // This text is always added, making the file longer over time 
        // if it is not deleted. 
        using (System.IO.StreamWriter sw = System.IO.File.AppendText(LogFileName))
        {
            sw.WriteLine(Type + DateTime.Now.ToString() + " < " + EmailsAddress + " > " + " : " + error);
        }
    }

}