using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallCenter.Models
{
    public class WorkPlatformModels
    {
        public int AccountNumber { get; set; }
        public string ClientReference { get; set; }
        public string ClientID { get; set; }
        public string Name { get; set; }
        public float AssignAmount { get; set; }
        public float TotalReceived { get; set; }
        public float OtherDue { get; set; }
        public float TotalDue { get; set; }
        public string Desk { get; set; }
        public string Status { get; set; }
        public DateTime PalacementDate { get; set; }
        public DateTime LastWorkDate { get; set; }
    }
}