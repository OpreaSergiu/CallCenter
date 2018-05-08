using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CallCenter.Models
{
    public class LoginLogsModels
    {
        [Key]
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public string UserRole { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm}")]
        public DateTime? LoginDate { get; set; }
    }
}