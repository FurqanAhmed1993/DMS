using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO.Compression;
using DMS_Utilities;

public class Base : System.Web.UI.Page
{
    public string LoginId
    {
        get { return GetCookie("Sanofi_LoginId"); }
        set { SaveCookie("Sanofi_LoginId", value); }
    }
    public string FullName
    {
        get { return GetCookie("Sanofi_fullname"); }
        set { SaveCookie("Sanofi_fullname", value); }
    }
    public string EmailAddress
    {
        get { return GetCookie("Sanofi_emailaddress"); }
        set { SaveCookie("Sanofi_emailaddress", value); }
    }
    public int RoleId
    {
        get
        {
            if (string.IsNullOrEmpty(GetCookie("Sanofi_RoleId")))
                return 0;
            else
                return int.Parse(GetCookie("Sanofi_RoleId"));
        }
        set { SaveCookie("Sanofi_RoleId", value.ToString()); }
    }
    public int UserId
    {
        get
        {
            if (string.IsNullOrEmpty(GetCookie("Sanofi_UserId")))
                return 0;
            else
                return int.Parse(GetCookie("Sanofi_UserId"));
        }
        set { SaveCookie("Sanofi_UserId", value.ToString()); }
    }
    public int DistributerId
    {
        get
        {
            if (string.IsNullOrEmpty(GetCookie("Sanofi_DistributerId")))
                return 0;
            else
                return int.Parse(GetCookie("Sanofi_DistributerId"));
        }
        set { SaveCookie("Sanofi_DistributerId", value.ToString()); }
    }
    public int _EducatorId
    {
        get
        {
            if (string.IsNullOrEmpty(GetCookie("Sanofi_EducatorId")))
                return 0;
            else
                return int.Parse(GetCookie("Sanofi_EducatorId"));
        }
        set { SaveCookie("Sanofi_EducatorId", value.ToString()); }
    }

    public bool IsSuperAdmin
    {
        get
        {
            if (string.IsNullOrEmpty(GetCookie("Sanofi_IsSuperAdmin")))
                return false;
            else
                return bool.Parse(GetCookie("Sanofi_IsSuperAdmin"));
        }
        set { SaveCookie("Sanofi_IsSuperAdmin", value.ToString()); }
    }
    public bool IsAdmin
    {
        get
        {
            if (string.IsNullOrEmpty(GetCookie("Sanofi_IsAdmin")))
                return false;
            else
                return bool.Parse(GetCookie("Sanofi_IsAdmin"));
        }
        set { SaveCookie("Sanofi_IsAdmin", value.ToString()); }
    }
    public bool IsSubAdmin
    {
        get
        {
            if (string.IsNullOrEmpty(GetCookie("Sanofi_IsSubAdmin")))
                return false;
            else
                return bool.Parse(GetCookie("Sanofi_IsSubAdmin"));
        }
        set { SaveCookie("Sanofi_IsSubAdmin", value.ToString()); }
    }
    public bool IsSupervisor
    {
        get
        {
            if (string.IsNullOrEmpty(GetCookie("Sanofi_IsSupervisor")))
                return false;
            else
                return bool.Parse(GetCookie("Sanofi_IsSupervisor"));
        }
        set { SaveCookie("Sanofi_IsSupervisor", value.ToString()); }
    }
    public bool IsAgent
    {
        get
        {
            if (string.IsNullOrEmpty(GetCookie("Sanofi_IsAgent")))
                return false;
            else
                return bool.Parse(GetCookie("Sanofi_IsAgent"));
        }
        set { SaveCookie("Sanofi_IsAgent", value.ToString()); }
    }
    public bool IsDistributer
    {
        get
        {
            if (string.IsNullOrEmpty(GetCookie("Sanofi_IsDistributer")))
                return false;
            else
                return bool.Parse(GetCookie("Sanofi_IsDistributer"));
        }
        set { SaveCookie("Sanofi_IsDistributer", value.ToString()); }
    }
    public bool IsCommunicationManager
    {
        get
        {
            if (string.IsNullOrEmpty(GetCookie("Sanofi_IsCommunicationManager")))
                return false;
            else
                return bool.Parse(GetCookie("Sanofi_IsCommunicationManager"));
        }
        set { SaveCookie("Sanofi_IsCommunicationManager", value.ToString()); }
    }
    public bool IsSupplyChain
    {
        get
        {
            if (string.IsNullOrEmpty(GetCookie("Sanofi_IsSupplyChain")))
                return false;
            else
                return bool.Parse(GetCookie("Sanofi_IsSupplyChain"));
        }
        set { SaveCookie("Sanofi_IsSupplyChain", value.ToString()); }
    }
    public bool IsDCM
    {
        get
        {
            if (string.IsNullOrEmpty(GetCookie("Sanofi_IsDCM")))
                return false;
            else
                return bool.Parse(GetCookie("Sanofi_IsDCM"));
        }
        set { SaveCookie("Sanofi_IsDCM", value.ToString()); }
    }
    public string UserIP
    {
        get { return Context.Request.UserHostAddress + " -- " + CommonObjects.GetMAC(); }
    }
    private bool ValidateError(string errorMessage)
    {
        // exclude common backend exceptions
        if (errorMessage == "File does not exist.")
            return false;
        else
            return true;
    }
    private Exception SetExceptionText()
    {
        Exception ex = HttpContext.Current.Server.GetLastError();
        if (ex != null)
        {
            if (ex.GetBaseException() != null)
            {
                ex = ex.GetBaseException();
                // lblError.Text = ex.Message;
            }
        }
        return ex;
    }
    public void SaveCookie(string strKey, string strValue)
    {
        if (HttpContext.Current.Request.Cookies[strKey] != null)
        {
            HttpContext.Current.Request.Cookies[strKey].Value = strValue;
        }
        else
        {
            HttpCookie cookie = new HttpCookie(strKey);
            cookie.Value = strValue;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
    public string GetCookie(string strKey)
    {
        if (HttpContext.Current.Request.Cookies[strKey] != null)
        {
            return HttpContext.Current.Request.Cookies[strKey].Value;
        }
        else
            return null;
    }
    public void ExpireCookie()
    {
        HttpRequest req = HttpContext.Current.Request;
        HttpResponse res = HttpContext.Current.Response;
        int count = req.Cookies.Count;
        for (int i = 0; i < count; i++)
        {
            HttpCookie cookie = new HttpCookie(req.Cookies[i].Name);
            cookie.Expires = DateTime.Now.AddDays(-1);
            res.Cookies.Add(cookie);
        }
    }
    public string GetSecurityKey()
    {
        string key = GetCookie("Sanofi_sesk");
        if (key != null)
            return key;
        else
        {
            key = Guid.NewGuid().ToString();
            SaveCookie("Sanofi_sesk", key);
            return key;
        }
    }

}

public static class UIExtensions
{
    public static void EnableCompression(this HttpResponse response)
    {
        response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
        response.AddHeader("Content-Encoding", "gzip");
    }
}