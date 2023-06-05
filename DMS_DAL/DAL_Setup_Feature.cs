using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace DMS_DAL
{
    public class DAL_Setup_Feature : DAL
    {
        public DataTable usp_Feature_Get(int? FeatureId, string Feature, bool? IsActive)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_Feature_Get"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@FeatureId", SqlDbType.Int).Value = FeatureId;
                    cmd.Parameters.Add("@Feature", SqlDbType.VarChar).Value = Feature;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = IsActive;
                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during usp_Feature_Get : {0}", ex.Message), ex);
            }
            return dt;
        }

    }
}
