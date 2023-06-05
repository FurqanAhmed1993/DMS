using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS_BAL
{
    public class BAL_IssuanceRegister : DMS_DAL.DAL_IssuanceRegister
    {
        DataTable dt = new DataTable();
        public DataTable usp_Setup_IssuanceRegister(int OperationType, int? ApplicationId, string Receiver_Name, string Receiver_Address, string Receiver_CNIC, DateTime? Issuance_Date, int? UserId, string UserIP)
        {
            dt = usp_Setup_Crud_IssuanceRegister(OperationType, ApplicationId, Receiver_Name, Receiver_Address, Receiver_CNIC, Issuance_Date, UserId, UserIP);
            return dt;
        }
    }
}
