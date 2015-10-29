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
using Indepp.ViewModels;
using Indepp.Filters;
using Indepp.HelperMethods;

namespace Indepp.Controllers
{
    public class HomeController : Controller
    {
        private PlaceContext Context;

        public HomeController(PlaceContext context)
        {
            Context = context;
        }

        // GET: Home
        public ActionResult Index(int? page)
        {
            ViewBag.PageTitle = "Home";

            var articles = Context.Articles.Select(a => new ArticleAndBlogPost
            {
                ID = a.ID,
                Title = a.Title,
                ShortDescription = a.ShortDescription,
                Description = a.Description,
                PostedOn = a.PostedOn,
                PlaceID = a.PlaceID,
                IsArticle = true,
                Place = a.Place
            });

            var blogPosts = Context.BlogPosts.Select(bp => new ArticleAndBlogPost
            {
                ID = bp.ID,
                Title = bp.Title,
                ShortDescription = bp.ShortDescription,
                Description = bp.Description,
                PostedOn = bp.PostedOn,
                PlaceID = bp.PlaceID,
                IsArticle = false,
                Place = null
            });

            var articlesAndBlogPosts = articles.Union(blogPosts).OrderByDescending(abp => abp.PostedOn);


            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(articlesAndBlogPosts.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult About() 
        {
            ViewBag.PageTitle = "About";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.PageTitle = "Contact";
            return View();
        }

        [HttpPost]
        [Throttle(Message = "You must wait {n} minutes before you can send us another email.", Seconds = 120)]
        public ActionResult Contact(EmailMessage emailMessage)
        {
            ViewBag.PageTitle = "Contact";

            if (ModelState.IsValid)
            {
                // Add Recipients
                /*
                var recipients = new List<string>()
                { 
                    @"Artur Smulko <artursmulko@gmail.com>" 
                };

                // Using SendGrid to send emails
                var messageToIndepp = new SendGridMessage();
                messageToIndepp.From = new MailAddress(emailMessage.Email, emailMessage.Name);
                messageToIndepp.AddTo(recipients);
                messageToIndepp.Subject = emailMessage.Subject;
                messageToIndepp.Text = emailMessage.Message;

                // Credentials
                var username = ConfigurationManager.AppSettings["SENDGRID_USER"];
                var password = ConfigurationManager.AppSettings["SENDGRID_PASSWORD"];
                var credentials = new NetworkCredential(username, password);
                var transportWeb = new Web(credentials);

                // Send email
                transportWeb.DeliverAsync(messageToIndepp);
                */
                TempData.Add("MessageSent", "Your email has been sent");

                return RedirectToAction("Contact");
            }
            else
            {
                return View(emailMessage);
            }
        }

        [HttpGet]
        public ActionResult PlaceMap()
        {
            return View();
        }

        public JsonResult GetPlaceLocations(string showCoffee, string showFood, string showFarm, string showCraftShop, string showFashion, string placeName = "")
        {
            var places = Context.Places.ToList().Select(p => new PlaceMap
            {
                Name = p.Name,
                Category = p.Category,
                Longitude = p.Address.Longitude,
                Latitude = p.Address.Latitude,
                WorkingHours = p.WorkingHours.Select(wh => new WorkingHourView
                    { 
                        Day = wh.Day,
                        OpenTime = wh.OpenTime.HasValue ? wh.OpenTime.Value.ToString(@"hh\:mm") : "",
                        CloseTime = wh.CloseTime.HasValue ? wh.CloseTime.Value.ToString(@"hh\:mm") : ""
                    }).ToList()
            }).Where(p => p.Latitude != null && p.Longitude != null);

            var sortedPlaces = places
                .Where(p => p.Category == showCoffee || p.Category == showFood
                        || p.Category == showFarm || p.Category == showCraftShop || p.Category == showFashion)
                .Where(p => p.Name.Contains(placeName));

            return Json(sortedPlaces, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BlogPost(int? id)
        {
            ViewBag.RecentPosts = new ViewBagHelperMethods().GetRecentPosts(Context, 5);

            var blogPost = Context.BlogPosts.FirstOrDefault(b => b.ID == id);

            return View(blogPost);
        }
    }
}