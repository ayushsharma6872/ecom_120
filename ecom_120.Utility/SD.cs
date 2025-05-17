using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecom_120.Utility
{
    public static class SD
    {
        //Role
        public const string Role_Admin = "Admin";
        public const string Role_Company = "Company";
        public const string Role_Employee = "Employee";
        public const string Role_Individual = "Individual";

        //Session
        public const string Ss_CartSessionCount = "Cart Count Session";

        public static double GetPriceBasedOnQuantity(double quantity, double price, double price50, double price100)
        {
            if (quantity < 50)
                return price;
            else if (quantity < 100)
                return price50; return price100;
        }
        //Order Status
        public const string OrderStatusPending = "Pending";
        public const string OrderStatusApproved = "Approved";
        public const string OrderStatusInProgress = "Processing";
        public const string OrderStatusShipped = "Shipped";
        public const string OrderStatusCancelled = "Cancelled";
        public const string OrderStatusRefunded = "Refunded";
        // Payment Status
        public const string PaymentStatusPending = "Pending";
        public const string PaymentStatusApproved = "Approved";
        public const string PaymentStatusDelayPayment = "PaymentSatatusDelay";
        public const string PaymentStatusRejected = "Rejected";

    }

    //this is for stored procedure

    //public static class SD
    //{
    //    //coverType
    //    public const string SP_CreateCoverType = "SP_CreateCoverType";
    //    public const string SP_UpdateCoverType = "SP_UpdateCoverType";
    //    public const string SP_DeleteCoverType = "SP_DeleteCoverType";
    //    public const string SP_GetCoverType = "SP_GetCoverType";
    //    public const string SP_GetCoverTypes = "SP_GetCoverTypes";

    //}
}
