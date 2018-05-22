using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace CallCenter.Models
{
    public class SurveyModels
    {
        [Key]
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public DateTime PostDate { get; set; }
        public string Suggestions { get; set; }
        public int Rating { get; set; }
    }
}