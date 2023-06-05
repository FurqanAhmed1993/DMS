using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS_DAL
{
    public class DAL_IssuanceRegister : DAL
    {
        public DataTable usp_Setup_Crud_IssuanceRegister(int OperationType, int? ApplicationId, string Receiver_Name, string Receiver_Address, string Receiver_CNIC, DateTime? Issuance_Date, int? UserId, string UserIP)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_Setup_IssuanceRegister"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OperationType", SqlDbType.Int).Value = OperationType;
                    cmd.Parameters.Add("@ApplicationId", SqlDbType.Int).Value = ApplicationId;
                    cmd.Parameters.Add("@Receiver_Name", SqlDbType.VarChar).Value = Receiver_Name;
                    cmd.Parameters.Add("@Receiver_Address", SqlDbType.VarChar).Value = Receiver_Address;
                    cmd.Parameters.Add("@Receiver_CNIC", SqlDbType.VarChar).Value = Receiver_CNIC;
                    cmd.Parameters.Add("@Issuance_Date", SqlDbType.DateTime).Value = Issuance_Date;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
                    cmd.Parameters.Add("@UserIP", SqlDbType.VarChar).Value = UserIP;
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during usp_Setup_Taluka : {0}", ex.Message), ex);
            }
            return dt;
        }
    }
}
