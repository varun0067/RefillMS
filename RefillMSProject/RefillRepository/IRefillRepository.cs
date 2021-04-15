using RefillMSProject.DTO;
using RefillMSProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefillMSProject.RefillRepository
{
    public interface IRefillRepository
    {
        public bool CreateRefillOrders(RefillOrderDTO refillOrder);
        public List<RefillOrder> GetRefillOrders(int subscriptionId);
        public List<RefillOrder> GetRefillDuesAsOfDate(int subscriptionId, DateTime fromDate);
        public bool RequestAdhocRefill(int subscriptionId, int refillId);

        public bool CheckPendingPaymentStatus(int subscriptionId);
        //public int GetRefillStatus(int subscriptionID);
        //public int GetQty(int id);
        //public int GetRefillOrderID(int id);
        //public int GetDrugID(int id);

    }
}
