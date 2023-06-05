using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS_DAL
{
    public class DAL_DeputyCommissioner : DAL
    {
        public DataTable usp_Setup_Crud_DeputyCommisioner(int OperationType, int? CommisionerId, string CommisionerName, DateTime? FromDate , DateTime? ToDate,  DateTime? DomicileApprovalDate ,int? UserId, string UserIP)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_Setup_DeputyCommisioner"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OperationType", SqlDbType.Int).Value = OperationType;
                    cmd.Parameters.Add("@CommisionerId", SqlDbType.Int).Value = CommisionerId;
                    cmd.Parameters.Add("@CommisionerName", SqlDbType.VarChar).Value = CommisionerName;
                    cmd.Parameters.Add("@FromDate", SqlDbType.Date).Value = FromDate;
                    cmd.Parameters.Add("@ToDate", SqlDbType.Date).Value = ToDate;
                    cmd.Parameters.Add("@DomicileApprovalDate", SqlDbType.DateTime).Value = DomicileApprovalDate;
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

        public DataTable usp_SetupDeputyCommisioner_IsTransection_Exist(string OperationType, int? Id)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_Setup_DeputyCommisioner"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OperationType", SqlDbType.VarChar).Value = OperationType;
                    cmd.Parameters.Add("@CommisionerId", SqlDbType.Int).Value = Id;
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during usp_Setup_DeliveryDaysDate : {0}", ex.Message), ex);
            }
            return dt;
        }


    }
}
