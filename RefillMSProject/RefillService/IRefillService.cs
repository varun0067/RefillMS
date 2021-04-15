using RefillMSProject.DTO;
using RefillMSProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefillMSProject.RefillService
{
    public interface IRefillService
    {
        public bool CreateRefillOrders(RefillOrderDTO refillOrder);
        public List<RefillOrder> GetRefillOrders(int subscriptionId);
        public List<RefillOrder> GetRefillDuesAsOfDate(int subscriptionId,DateTime fromDate);

        public bool RequestAdhocRefill(int subscriptionId, int refillId);

        public bool CheckPendingPaymentStatus(int subscriptionId);
        //public int GetRefillStatus(int SubscriptionID);
        //public int GetRefillDuesAsOfDate(int id, DateTime date);

        //public int RequestAdhocRefill(int id,string location);
    }
}
