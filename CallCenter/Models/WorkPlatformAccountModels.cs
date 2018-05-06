using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallCenter.Models
{
    public class WorkPlatformAccountModels
    {
        public IEnumerable<PhoneModels> Phones { get; set; }
        public IEnumerable<NotesModels> Notes { get; set; }
        public IEnumerable<InvoiceModels> Invoices { get; set; }
        public AddressModels Address { get; set; }
        public WorkPlatformModels Account { get; set; }
        public NotesModels Notes2 { get; set; }
    }
}