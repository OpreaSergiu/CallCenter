using CallCenter.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CallCenter.Controllers
{
    [Authorize(Roles = "User")]
    public class SurveyController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            string user_name = User.Identity.GetUserName();
            string redirectUrl = "/WorkPlatform/Index/";

            var check = db.SurveyModels.Where(s => s.UserEmail == user_name).SingleOrDefault();
            var user_desk = db.UserDeskModels.Where(s => s.UserEmail == user_name).SingleOrDefault().Desk;
            var closed_accounts = db.WorkPlatformModels.Where(s => s.Status == "CLOSED").Where(d => d.Desk == user_desk).Count();

            if(check == null)
            {
                if(closed_accounts >= 3)
                {
                    return View();
                }
                else
                {
                    return Redirect(redirectUrl);
                }
            }
            else
            {
                return Redirect(redirectUrl);
            }
        }

        [HttpPost]
        public ActionResult Create(int Rating, string Suggestions)
        {
            string user_name = User.Identity.GetUserName();

            SurveyModels surveyModels = new SurveyModels();

            surveyModels.PostDate = DateTime.Now;
            surveyModels.UserEmail = user_name;
            surveyModels.Rating = Rating;
            surveyModels.Suggestions = Suggestions;

            db.SurveyModels.Add(surveyModels);
            db.SaveChanges();

            string redirectUrl = "/WorkPlatform/Index/";
            return Redirect(redirectUrl);
        }
    }
}