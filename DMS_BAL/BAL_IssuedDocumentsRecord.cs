using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DMS_BAL
{
    public class BAL_IssuedDocumentsRecord : DMS_DAL.DAL_IssuedDocumentsRecord
    {
        DataTable dt = new DataTable();

        public DataTable usp_Setup_IssuedDocumentsRecord(int OperationType, int? DocumentTypeID, int? ApplicationID, string Education ,bool IsActive, int UserId, string UserIP)
        {
            dt = usp_Setup_Crud_IssuedDocumentsRecord(OperationType, DocumentTypeID, ApplicationID, Education, IsActive, UserId, UserIP);
            return dt;
        }

    }
}
