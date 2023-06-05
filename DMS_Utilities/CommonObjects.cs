using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Configuration;
using Newtonsoft.Json;
using System.IO;
using System.Data;
using OfficeOpenXml;
using System.Security.Cryptography;
using System.Reflection;

namespace DMS_Utilities
{
    public static class CommonObjects
    {
        public static string GetCongifValue(string ConfigKey)
        {
            return ConfigurationManager.AppSettings[ConfigKey].ToString();
        }
        public static string Serialize(object obj)
        {
            string result = JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            return result;
        }
        public static object Deserialize(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type);
        }
        public static string GetHash(string value)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(value, "md5");
        }
        public static void BindDropDown(DropDownList ddlGeneral, Object dataSource, string dataTextField, string dataValueField, bool hasSelectItem, bool hasOtherItem)
        {
            ddlGeneral.Items.Clear();
            ddlGeneral.DataSource = dataSource;
            ddlGeneral.DataTextField = dataTextField;
            ddlGeneral.DataValueField = dataValueField;
            ddlGeneral.DataBind();

            if (hasSelectItem == true)
            {
                ddlGeneral.Items.Insert(0, new ListItem("-Select-", "0"));
            }

            if (hasOtherItem == true)
            {
                ddlGeneral.Items.Add(new ListItem("-OTHER-", "-100"));
            }
        }
        public static void BindDropDownAll(DropDownList ddlGeneral, Object dataSource, string dataTextField, string dataValueField, bool hasSelectItem)
        {
            ddlGeneral.Items.Clear();
            ddlGeneral.DataSource = dataSource;
            ddlGeneral.DataTextField = dataTextField;
            ddlGeneral.DataValueField = dataValueField;
            ddlGeneral.DataBind();
            if (hasSelectItem == true)
            {
                ddlGeneral.Items.Insert(0, new ListItem("All", "0"));
            }

        }
        public static void BindCheckBoxList(CheckBoxList cbl, Object dataSource, string dataTextField, string dataValueField, bool hasAllItem = false, bool hasAllItemsSelected = false)
        {
            cbl.Items.Clear();
            cbl.DataSource = dataSource;
            cbl.DataTextField = dataTextField;
            cbl.DataValueField = dataValueField;
            cbl.DataBind();

            if (hasAllItem == true)
            {
                cbl.Items.Insert(0, new ListItem("All", "0"));
                //cbl.Items[0].Selected = true;
            }
            if (hasAllItemsSelected == true)
            {
                foreach (ListItem li in cbl.Items)
                {
                    li.Selected = true;
                }
            }
        }
        public static void BindRadioButtonList(RadioButtonList cbl, Object dataSource, string dataTextField, string dataValueField)
        {
            cbl.DataSource = dataSource;
            cbl.DataTextField = dataTextField;
            cbl.DataValueField = dataValueField;
            cbl.DataBind();

            if (cbl.Items.Count > 0)
            {
                cbl.Items[0].Selected = true;
            }
        }
        public static string getSubstring(string content, int length)
        {
            string substr = content;
            if (content.Length > length)
            {
                substr = content.Substring(0, length) + "...";
            }

            return substr;
        }
        public static string GetCommaSeparatedCBLValues(CheckBoxList cbl, bool breakOnAllSelected = false)
        {
            bool isAllItemSelected = false;
            string value = "";
            foreach (ListItem li in cbl.Items)
            {
                if (li.Selected || isAllItemSelected)
                {
                    if (li.Value == "0")
                    {
                        isAllItemSelected = true;
                        if (breakOnAllSelected)
                        {
                            break;
                        }
                    }
                    value += li.Value + ",";
                }
            }
            return value.Length > 1 ? value : null;
        }
        public static string ExportToExcel(Repeater rpt, string FileName, HttpResponse Response)
        {
            try
            {
                //TextWriter tw;// = new TextWriter();
                //HttpResponse Response = new HttpResponse();// = new HttpResponse();

                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=" + FileName + ".xls");
                Response.ContentType = "application/vnd.ms-excel";
                Response.Charset = "";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                rpt.RenderControl(htw);
                string style = @"<style> TD.exceltext { mso-number-format:\@; } </style> ";
                Response.Write(style);
                //Response.Write(CreateExcelCSS());

                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        private static string CreateExcelCSS()
        {
            string css = @"<style> 
            table {
                /*background: none repeat scroll 0 0 #fff;*/
                border: 1px #585858 solid;
                /*margin: 5px 0 0 5px;*/
                width: 100%;
            }

            .gridline a {
                color: #ffffff;
                font-family: Calibri;
                font-size: 13px;
                text-decoration: none;
                margin: 10px auto;
            }

            th {
                color: #eee;
                font-weight: normal;
                padding: 5px 5px 10px;
                font: 15px;
                background-color: Red;
                text-align: center;
            }

            .gridline td {
                padding: 2px 2px 2px 2px;
                text-transform: capitalize;
                border-bottom: 1px #585858 solid;
                border-right: 1px #585858 solid;
                text-align: center;
            }

            tr:nth-child(even) {
                background: #E0EDF3;
            }

            tr:nth-child(odd) {
                background: #FFF;
            }
        </style>";
            return css;
        }
        public static void SetRepeaterFooter(Repeater rpt)
        {
            RepeaterItem item = (RepeaterItem)rpt.Controls[rpt.Controls.Count - 1];
            if (item.ItemType == ListItemType.Footer)
            {
                for (int i = 1; i <= item.Controls.Count; i++)
                {
                    System.Web.UI.HtmlControls.HtmlInputHidden hfFooter = (System.Web.UI.HtmlControls.HtmlInputHidden)item.FindControl("hf" + i);
                    if (hfFooter != null)
                    {
                        string strClass = Convert.ToString(hfFooter.Attributes["class"]);
                        Label lbl = (Label)item.FindControl(strClass);
                        if (lbl != null)
                        {
                            lbl.Text = hfFooter.Value;
                        }
                    }
                }
            }
        }
        public static string GetFileUploadPath(string FolderName)
        {
            string FilePath = AppDomain.CurrentDomain.BaseDirectory + FolderName;
            return FilePath;
        }
        public static string GetMAC()
        {
            string macAddresses = "";

            foreach (System.Net.NetworkInformation.NetworkInterface nic in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up)
                {
                    macAddresses += nic.GetPhysicalAddress().ToString();
                    break;
                }
            }
            return macAddresses;
        }
        public static void ExportToExcel(string FileName, DataTable dt)
        {
            try
            {
                using (ExcelPackage pck = new ExcelPackage())
                {
                    ExcelWorksheet ws1 = pck.Workbook.Worksheets.Add("Sheet1");
                    ws1.Cells["A1"].LoadFromDataTable(dt, true);
                    ws1.Cells.AutoFitColumns();
                    if (pck.Workbook.Worksheets.Count > 0)
                    {
                        Byte[] fileBytes = pck.GetAsByteArray();
                        HttpContext.Current.Response.ClearContent();
                        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + FileName.Trim() + ".xlsx");
                        HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        HttpContext.Current.Response.BinaryWrite(fileBytes);
                        HttpContext.Current.Response.End();
                        pck.Save();
                    }
                }
            }
            catch
            {

            }
        }
        public static string HashChecksum(string Message)
        {
            string key = ConfigurationManager.AppSettings["CheckSumKey"].ToString();


            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

            byte[] keyByte = encoding.GetBytes(key);

            HMACSHA256 hmacsha256 = new HMACSHA256(keyByte);

            byte[] messageBytes = encoding.GetBytes(Message);

            byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);

            var tempHash = ByteToString(hashmessage);
            return tempHash;
        }

        public static string ByteToString(byte[] buff)
        {
            string sbinary = "";

            for (int i = 0; i < buff.Length; i++)
            {
                sbinary += buff[i].ToString("X2"); // hex format
            }
            return (sbinary);
        }

        public static Dictionary<string, object> GetRepsonse(bool IsSuccess, string ResponseCode, string ResponseMessage, dynamic data = null, string Token = null)
        {
            Dictionary<string, object> objResponse = new Dictionary<string, object>();
            objResponse.Add(ResponseKeys.Response, IsSuccess);
            objResponse.Add(ResponseKeys.ResponseCode, ResponseCode);
            objResponse.Add(ResponseKeys.ResponseMessage, ResponseMessage);
            objResponse.Add(ResponseKeys.Data, data);
            return objResponse;
        }
        public static string CreateRandomPassword(int length = 10)
        {
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            Random random = new Random();
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public static List<T> ConvertToList<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
            var properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name.ToLower()))
                    {
                        try
                        {
                            pro.SetValue(objT, row[pro.Name]);
                        }
                        catch (Exception ex) { }
                    }
                }
                return objT;
            }).ToList();
        }
        public static List<Menu> GetMenus()
        {
            List<Menu> routes = new List<Menu>();
            routes.Add(
                new Menu()
                {
                    path = "/dashboard",
                    name = "Dashboard",
                    icon = "nc-icon nc-bank",
                    component = "Dashboard",
                    layout = "/admin",
                    SubMenus = new List<SubMenu>()
                    {

                    }
                }
                );

            routes.Add(
            new Menu()
            {
                path = "/Security",
                name = "Security",
                icon = "nc-icon nc-caps-small",
                component = "Security",
                layout = "/admin",
                SubMenus = new List<SubMenu>()
                {
                    new SubMenu()
                       {
                        path = "/Roles",
                        name = "Roles",
                        icon = "nc-icon nc-bank",
                        component = "Roles",
                        layout = "/admin",
                       },
                    new SubMenu()
                       {
                        path = "/RolesAccess",
                        name = "RolesAccess",
                        icon = "nc-icon nc-bank",
                        component = "RolesAccess",
                        layout = "/admin",
                       },
                    new SubMenu()
                       {
                        path = "/MenuList",
                        name = "MenuList",
                        icon = "nc-icon nc-bank",
                        component = "MenuList",
                        layout = "/admin",
                       }
                }
            }
            );
            routes.Add(
            new Menu()
            {
                path = "/Setups",
                name = "Setups",
                icon = "nc-icon nc-caps-small",
                component = "Setups",
                layout = "/admin",
                SubMenus = new List<SubMenu>()
                {
                    new SubMenu()
                       {
                        path = "/SetupUsers",
                        name = "User Profile",
                        icon = "nc-icon nc-bank",
                        component = "SetupUsers",
                        layout = "/admin",
                       },
                    new SubMenu()
                       {
                        path = "/Beneficiary",
                        name = "Beneficiary",
                        icon = "nc-icon nc-bank",
                        component = "Beneficiary",
                        layout = "/admin",
                       },
                    new SubMenu()
                       {
                        path = "/Category",
                        name = "Category",
                        icon = "nc-icon nc-bank",
                        component = "Category",
                        layout = "/admin",
                       },
                    new SubMenu()
                       {
                        path = "/Company",
                        name = "Company",
                        icon = "nc-icon nc-bank",
                        component = "Company",
                        layout = "/admin",
                       },
                    new SubMenu()
                       {
                        path = "/CompanyFamily",
                        name = "CompanyFamily",
                        icon = "nc-icon nc-bank",
                        component = "CompanyFamily",
                        layout = "/admin",
                       },
                    new SubMenu()
                       {
                        path = "/CompanyLocation",
                        name = "CompanyLocation",
                        icon = "nc-icon nc-bank",
                        component = "CompanyLocation",
                        layout = "/admin",
                       },
                    new SubMenu()
                       {
                        path = "/Frequency",
                        name = "Frequency",
                        icon = "nc-icon nc-bank",
                        component = "Frequency",
                        layout = "/admin",
                       },
                    new SubMenu()
                       {
                        path = "/FundCategory",
                        name = "FundCategory",
                        icon = "nc-icon nc-bank",
                        component = "FundCategory",
                        layout = "/admin",
                       },
                    new SubMenu()
                       {
                        path = "/PaymentType",
                        name = "PaymentType",
                        icon = "nc-icon nc-bank",
                        component = "PaymentType",
                        layout = "/admin",
                       },
                }
            }
            );
            routes.Add(
            new Menu()
            {
                path = "/ApplicantList",
                name = "ApplicantList",
                icon = "nc-icon nc-caps-small",
                component = "ApplicantList",
                layout = "/admin",
                SubMenus = new List<SubMenu>()
                {

                }
            }
            );
            return routes;
        }

        public static string GetFileUploadFinalPath(string AttachmentsFolderName, string AttachmentsFolderPath, string OrignalFilename, out string FileName)
        {
            string FilePathGuid = string.Empty;
            FileName = null;
            AttachmentsFolderName = GetFileUploadPath(GenericConstants.AttachmentsFolderName);

            if (!Directory.Exists(AttachmentsFolderName))
                Directory.CreateDirectory(AttachmentsFolderName);

            string path = GetFileUploadPath(AttachmentsFolderPath);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            if (path != "")
            {
                Random RandomNumber = new Random();
                string FileRandom = RandomNumber.Next(0, 999999).ToString();
                FileName = FileRandom + "_" + OrignalFilename;
                FileName = Regex.Replace(FileName, @"[^0-9a-zA-Z.]+", "");
                FilePathGuid = GetFileUploadPath(AttachmentsFolderPath + FileName);
            }
            return FilePathGuid;
        }
    }
}

