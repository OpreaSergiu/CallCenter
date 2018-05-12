using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CallCenter.Models
{
    public class UserClientIdModels
    {
        [Key]
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public string ClientID { get; set; }
    }
}