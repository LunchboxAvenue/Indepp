using Indepp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Net;
using System.Net.Mail;
using SendGrid;
using System.Configuration;

namespace Indepp.Controllers
{
    public class HomeController : Controller
    {
        private PlaceContext db = new PlaceContext();

        // GET: Home
        public ActionResult Index(int? page)
        {
            ViewBag.PageTitle = "Home";

            var blogPosts = db.BlogPosts.OrderByDescending(pb => pb.ID);
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(blogPosts.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult About() 
        {
            ViewBag.PageTitle = "About";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.PageTitle = "Contact";
            ViewBag.MessageSent = false;
            return View();
        }

        [HttpPost]
        public ActionResult Contact(string name, string email, string subject, string message)
        {
            ViewBag.PageTitle = "Contact";

            // Add Recipients
            /*
            var recipients = new List<string>()
            { 
                @"Artur Smulko <artursmulko@gmail.com>" 
            };

            // Using SendGrid to send emails
            var messageToIndepp = new SendGridMessage();
            messageToIndepp.From = new MailAddress(email, name);
            messageToIndepp.AddTo(recipients);
            messageToIndepp.Subject = subject;
            messageToIndepp.Text = message;

            // Credentials
            var username = ConfigurationManager.AppSettings["SENDGRID_USER"];
            var password = ConfigurationManager.AppSettings["SENDGRID_PASSWORD"];
            var credentials = new NetworkCredential(username, password);
            var transportWeb = new Web(credentials);

            // Send email
            transportWeb.DeliverAsync(messageToIndepp);
            */

            ViewBag.MessageSent = true;
            return View();
        }
    }
}