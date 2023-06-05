using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS_DAL
{
    public class DAL_DeliveryDateDays : DAL
    {
        public DataTable usp_Setup_Crud_DeliveryDateDays(int OperationType, int? DeliveryDateDayId, int? DeliveryDateDays, int? UserId, string UserIP)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_Setup_DeliveryDaysDate"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OperationType", SqlDbType.Int).Value = OperationType;
                    cmd.Parameters.Add("@DeliveryDateDayId", SqlDbType.Int).Value = DeliveryDateDayId;
                    cmd.Parameters.Add("@DeliveryDateDays", SqlDbType.VarChar).Value = DeliveryDateDays;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
                    cmd.Parameters.Add("@UserIP", SqlDbType.VarChar).Value = UserIP;
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during usp_Setup_Complaint_Category : {0}", ex.Message), ex);
            }
            return dt;
        }

        public DataTable usp_SetupDeliveryDateDays_IsTransection_Exist(int OperationType, int? Id)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_Setup_DeliveryDaysDate"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OperationType", SqlDbType.Int).Value = OperationType;
                    cmd.Parameters.Add("@DeliveryDateDayId", SqlDbType.Int).Value = Id;
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
