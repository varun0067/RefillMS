using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using RefillMSProject.DTO;
using RefillMSProject.Models;
using RefillMSProject.RefillRepository;

namespace RefillMSProject.RefillService
{
    public class RefillService:IRefillService
    {
        private readonly IRefillRepository _repository;

        public RefillService(IRefillRepository repository)
        {
            _repository = repository;
        }

        public bool CreateRefillOrders(RefillOrderDTO refillOrder)
        {
            return _repository.CreateRefillOrders(refillOrder);
        }

        public List<RefillOrder> GetRefillOrders(int subscriptionId)
        {
            return _repository.GetRefillOrders(subscriptionId);
        }
        public List<RefillOrder> GetRefillDuesAsOfDate(int subscriptionId, DateTime fromDate)
        {
            return _repository.GetRefillDuesAsOfDate(subscriptionId, fromDate);
        }
        public bool RequestAdhocRefill(int subscriptionId, int refillId)
        {
            return _repository.RequestAdhocRefill(subscriptionId, refillId);
        }
        public bool CheckPendingPaymentStatus(int subscriptionId)
        {
            return _repository.CheckPendingPaymentStatus(subscriptionId);
        }
    }
}
