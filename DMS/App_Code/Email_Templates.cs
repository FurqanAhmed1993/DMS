using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DMS_BAL;
using DMS_Utilities;
//using CrystalDecisions.CrystalReports.Engine;
using System.IO;

public class Email_Templates : Base
{
    public static string Displayname = System.Configuration.ConfigurationManager.AppSettings["Displayname"];
    public static string ApplicationURL = System.Configuration.ConfigurationManager.AppSettings["ApplicationURL"];
    public static string CallCenterEmailAddress = System.Configuration.ConfigurationManager.AppSettings["CallCenterEmailAddress"];


}