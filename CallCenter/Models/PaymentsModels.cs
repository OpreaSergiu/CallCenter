using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CallCenter.Models
{
    public class PaymentsModels
    {
        [Key]
        public int Id { get; set; }
        public int AccountNumber { get; set; }
        public string ClientID { get; set; }
        public string ClientReference { get; set; }
        public string Invoice { get; set; }
        public string Amount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public bool Approve { get; set; }
    }
}