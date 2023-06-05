using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DMS_BAL;
using DMS_Utilities;
//using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Net;
using System.Threading;

public class SMS_Templete : Base
{
    public static string SMS_API_Key = System.Configuration.ConfigurationManager.AppSettings["SMS_API_Key"];
    public static string SMS_sender = System.Configuration.ConfigurationManager.AppSettings["SMS_sender"];
    public static string SMS_Toll_Free_Number = System.Configuration.ConfigurationManager.AppSettings["SMS_Toll_Free_Number"];

    //public static void Order_SMS(int OrderMasterId)
    //{

    //    Thread sms = new Thread(delegate ()
    //    {
    //        Send_SMS(OrderMasterId);
    //    });
    //    sms.IsBackground = true;
    //    sms.Start();
    //}
    public static void Send_Consolidate_SMS(string PhoneNumber,string Message)
    {

        Thread sms = new Thread(delegate ()
        {
            Send_Consolidated_SMS(PhoneNumber, Message);
        });
        sms.IsBackground = true;
        sms.Start();
    }
    public static void Send_Consolidated_SMS(string PhoneNumber, string Message)
    {
        if (PhoneNumber != "" && Message != "")
        {
            try
            {
                string urlData = String.Empty;
                WebClient wc = new WebClient();
                PhoneNumber = PhoneNumber.Length == GenericConstants.MobileNoLength ? PhoneNumber.Remove(0, 1) : PhoneNumber;
                string APi = "http://sms.maplesolpk.com/sms/api.php?key=" + SMS_API_Key + "&to=" + PhoneNumber + "&sender=" + SMS_sender + "&msg=" + Message + "";
                urlData = wc.DownloadString(APi);
                WriteFile("", PhoneNumber, Message, "API Executed Successfully: " + urlData);
            }
            catch (Exception ex1)
            {
                WriteFile("", PhoneNumber, Message, "API Not Executed Successfully" + " : Exception : " + ex1.ToString());
            }
        }
        else
        {
            WriteFile("", PhoneNumber, Message, "Phone Number or message not found");
        }
    }

 
    private static void WriteFile(string OrderNumber, string MobileNo, string Order_Status, string APIResponse)
    {
        OrderNumber = " ---- [ Order #: " + OrderNumber + " ]";
        MobileNo = " ---- [ Mobile #: " + MobileNo + " ]";
        Order_Status = " ---- [ OrderStatus: " + Order_Status + " ]";
        APIResponse = " ---- [ Log: " + APIResponse + " ]";

        DateTime dateTime = DateTime.Now;
        string SMSLog = CommonObjects.GetFileUploadPath(GenericConstants.SMSLog);
        if (!Directory.Exists(SMSLog))
            Directory.CreateDirectory(SMSLog);
        string Year = SMSLog + "/" + dateTime.ToString("yyyy");
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
            sw.WriteLine(DateTime.Now.ToString() + OrderNumber + MobileNo + Order_Status + APIResponse);
        }
    }

}