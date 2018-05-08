using CallCenter.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CallCenter
{
    public class Context : DbContext
    {
        public DbSet<WorkPlatformModels> WorkPlatformModels { get; set; }
        public DbSet<PhoneModels> PhoneModels { get; set; }
        public DbSet<NotesModels> NotesModels { get; set; }
        public DbSet<InvoiceModels> InvoiceModels { get; set; }
        public DbSet<AddressModels> AddressModels { get; set; }
        public DbSet<UserDeskModels> UserDeskModels { get; set; }
        public DbSet<PaymentsModels> PaymentsModels { get; set; }
        public DbSet<LoginLogsModels> LoginLogsModels { get; set; }
    }
}