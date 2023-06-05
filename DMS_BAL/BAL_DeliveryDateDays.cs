using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS_BAL
{
    public class BAL_DeliveryDateDays : DMS_DAL.DAL_DeliveryDateDays
    {
        DataTable dt = new DataTable();
        public DataTable usp_Setup_DeliveryDateDays(int OperationType ,int? DeliveryDateDayId, int? DeliveryDateDays, int? UserId, string UserIP)
        {
            dt = usp_Setup_Crud_DeliveryDateDays(OperationType, DeliveryDateDayId, DeliveryDateDays, UserId, UserIP);
            return dt;
        }

        public DataTable usp_IsTransection_Exist_DeliveryDateDays(int OperationType,int Id)
        {
            dt = usp_SetupDeliveryDateDays_IsTransection_Exist(OperationType, Id);
            return dt;
        }
    }
}
