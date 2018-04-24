using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CallCenter.Models
{
    public class InvoiceModels
    {
        [Key]
        public int Id { get; set; }
        public int AccountNumber { get; set; }
        public string Invoice { get; set; }
        public string Status { get; set; }
        public float Amount { get; set; }
        public float Due { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}