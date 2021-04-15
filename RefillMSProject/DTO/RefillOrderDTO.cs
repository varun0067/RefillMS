using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefillMSProject.DTO
{
    public enum Occurrence
    {
        Weekly, Monthly
    };
    public class RefillOrderDTO
    {
        public DateTime RefillDate { get; set; }
        public int SubscriptionId { get; set; }
        public int DosagePerDay { get; set; }
        public int CourseInWeeks { get; set; }
        public string Location { get; set; }
        public double CostPerUnit { get; set; }
        public Occurrence RefillOccurrence { get; set; }
    }
}
