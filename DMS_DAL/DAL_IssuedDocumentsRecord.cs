using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace DMS_DAL
{
    public class DAL_IssuedDocumentsRecord : DAL
    {

        public DataTable usp_Setup_Crud_IssuedDocumentsRecord(int OperationType, int? DocumentTypeID, int? ApplicationID, string Education ,bool IsActive, int UserId, string UserIP)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_IssuedDocumentsRecord_CRUD"))
                {
                    OpenConnection(true);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OperationType", SqlDbType.Int).Value = OperationType;
                    cmd.Parameters.Add("@DocumentTypeID", SqlDbType.Int).Value = DocumentTypeID;
                    cmd.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = ApplicationID;
                    cmd.Parameters.Add("@Education", SqlDbType.VarChar).Value = Education;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = IsActive;
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
                    cmd.Parameters.Add("@UserIP", SqlDbType.VarChar).Value = UserIP;

                    dt = GetData(cmd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error occured during sp_DuplicateDocumentsRecord_CRUD : {0}", ex.Message), ex);
            }
            return dt;
        }
    }
}
