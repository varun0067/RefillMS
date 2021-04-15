using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using RefillMSProject.DTO;
using RefillMSProject.Models;
using RefillMSProject.RefillRepository;

namespace RefillMSProject.RefillRepository
{
    public class RefillRepository : IRefillRepository
    {
        private DBHelper _dbHelper;
        int refillNumber;
        public RefillRepository(DBHelper dBHelper)
        {
            refillNumber = 5000;
            _dbHelper = dBHelper;
        }
        public bool CreateRefillOrders(RefillOrderDTO refillOrder)
        {
            List<RefillOrder> refills = new List<RefillOrder>();
            try 
            {
                if (refillOrder.RefillOccurrence == Occurrence.Weekly)
                {

                    int days = 0;
                    for (int i = 1; i <= refillOrder.CourseInWeeks; i++)
                    {
                        RefillOrder refill = new RefillOrder();

                        refill.RefillOrderID = refillNumber;
                        refill.RefillDate = refillOrder.RefillDate.AddDays(days);
                        refill.SubscriptionID = refillOrder.SubscriptionId;
                        refill.Quantity = refillOrder.DosagePerDay * 7;
                        refill.Location = refillOrder.Location;
                        refill.Price = (refillOrder.DosagePerDay * 7) * refillOrder.CostPerUnit;
                        refill.Status = RefilStatus.Pending;
                        refillNumber++;
                        days += 7;
                        refills.Add(refill);
                    }
                    _dbHelper.RefillOrders.AddRange(refills);

                }
                else if (refillOrder.RefillOccurrence == Occurrence.Monthly)
                {
                    if (refillOrder.CourseInWeeks % 4 == 0)
                    {
                        int rep = (refillOrder.CourseInWeeks / 4);

                        int days = 0;
                        for (int i = 1; i <= rep; i++)
                        {
                            RefillOrder refill = new RefillOrder();

                            refill.RefillOrderID = refillNumber;
                            refill.RefillDate = refillOrder.RefillDate.AddDays(days);
                            refill.SubscriptionID = refillOrder.SubscriptionId;
                            refill.Quantity = refillOrder.DosagePerDay * 28;
                            refill.Location = refillOrder.Location;
                            refill.Price = (refillOrder.DosagePerDay * 28) * refillOrder.CostPerUnit;
                            refill.Status = RefilStatus.Pending;
                            refillNumber++;
                            days += 28;

                            refills.Add(refill);
                        }
                    }
                    else
                    {
                        int rep = (refillOrder.CourseInWeeks / 4);

                        int days = 0;
                        for (int i = 1; i <= rep; i++)
                        {
                            RefillOrder refill = new RefillOrder();

                            refill.RefillOrderID = refillNumber;
                            refill.RefillDate = refillOrder.RefillDate.AddDays(days);
                            refill.SubscriptionID = refillOrder.SubscriptionId;
                            refill.Quantity = refillOrder.DosagePerDay * 28;
                            refill.Location = refillOrder.Location;
                            refill.Price = (refillOrder.DosagePerDay * 28) * refillOrder.CostPerUnit;
                            refill.Status = RefilStatus.Pending;
                            refillNumber++;
                            days += 28;

                            refills.Add(refill);
                        }

                        int remWeekly = refillOrder.CourseInWeeks - (rep * 4);

                        for (int i = 1; i <= remWeekly; i++)
                        {
                            RefillOrder refill1 = new RefillOrder();

                            refill1.RefillOrderID = refillNumber;
                            refill1.RefillDate = refillOrder.RefillDate.AddDays(days);
                            refill1.SubscriptionID = refillOrder.SubscriptionId;
                            refill1.Quantity = refillOrder.DosagePerDay * 7;
                            refill1.Location = refillOrder.Location;
                            refill1.Price = (refillOrder.DosagePerDay * 7) * refillOrder.CostPerUnit;
                            refill1.Status = RefilStatus.Pending;
                            refillNumber++;
                            days += 7;
                            refills.Add(refill1);
                        }
                        _dbHelper.RefillOrders.AddRange(refills);
                    }
                }
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
        public List<RefillOrder> GetRefillOrders(int subscriptionId)
        {
            List<RefillOrder> refillOrders = (from RefillOrder order in _dbHelper.RefillOrders
                                              where order.SubscriptionID == subscriptionId
                                              select order).ToList();
            return refillOrders;
        }
        public List<RefillOrder> GetRefillDuesAsOfDate(int subscriptionId, DateTime fromDate)
        {
            List<RefillOrder> refillDueOrders = (from RefillOrder order in _dbHelper.RefillOrders
                                              where (order.SubscriptionID == subscriptionId && order.RefillDate<fromDate && order.Status==RefilStatus.Pending)
                                              select order).ToList();

            return refillDueOrders;
        }
        public bool RequestAdhocRefill(int subscriptionId, int refillId)
        {
            try
            {
                RefillOrder refillOrder = (from RefillOrder order in _dbHelper.RefillOrders
                                           where order.SubscriptionID == subscriptionId && order.RefillOrderID == refillId
                                           select order).FirstOrDefault();
                if(refillOrder!=null)
                {
                    refillOrder.Status = RefilStatus.Completed;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool CheckPendingPaymentStatus(int subscriptionId)
        {
            try
            {
                List<RefillOrder> refillOrders = (from RefillOrder order in _dbHelper.RefillOrders
                                           where order.SubscriptionID == subscriptionId
                                           select order).ToList();

                if (refillOrders != null)
                {
                    foreach(RefillOrder r in refillOrders)
                    {
                        if (r.Status == RefilStatus.Pending)
                            return false;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        //DBHelper dBHelper=new DBHelper();
        //public int GetRefillStatus(int subscriptionID)
        //{

        //    var refillItem = (dBHelper.RefillOrders.Where(p => p.SubscriptionID == subscriptionID)).SingleOrDefault();
        //    if (refillItem == null)
        //        return 0;
        //    var res = refillItem.Status;
        //        return res;



        //}
        //public int GetDrugID(int id)
        //{
        //    var refillItem = (dBHelper.RefillOrders.Where(p => p.SubscriptionID == id)).SingleOrDefault();
        //    if (refillItem == null)
        //        return 0;
        //    else
        //        return refillItem.DrugID;
        //}
        //public int GetQty(int id)
        //{
        //    var refillItem = (dBHelper.RefillOrders.Where(p => p.SubscriptionID == id)).SingleOrDefault();
        //    if (refillItem == null)
        //        return 0;
        //    else
        //        return refillItem.Quantity;
        //}

        //public int GetRefillOrderID(int id)
        //{
        //    var refillItem = (dBHelper.RefillOrders.Where(p => p.SubscriptionID == id)).SingleOrDefault();
        //    if (refillItem == null)
        //        return 0;
        //    return refillItem.RefillOrderID;
        //}


    }
}
