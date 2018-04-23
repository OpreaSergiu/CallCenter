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
    }
}