using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS_BAL
{
    public class BAL_DuplicateDocumentsRecord : DMS_DAL.DAL_DuplicateDocumentsRecord
    {
        DataTable dt = new DataTable();

        public DataTable usp_Setup_DuplicateDocumentsRecord(int OperationType, int? DocumentTypeID, int? ApplicationID,  bool IsActive, int UserId ,string UserIP)
        {
            dt = usp_Setup_Crud_DuplicateDocumentsRecord(OperationType, DocumentTypeID, ApplicationID,  IsActive, UserId, UserIP);
            return dt;
        }
    }
}
