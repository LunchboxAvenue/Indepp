using Indepp.DAL;
using Indepp.Models;
using Indepp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Indepp.HelperMethods
{
    public class ViewBagHelperMethods
    {
        public IEnumerable<Place> PopulatePlaceDropdown(PlaceContext context)
        {
            var places = context.Places.ToList();
            places.Insert(0, new Place() { ID = 0, Name = "None" });
            return places.Select(p => new Place()
            {
                ID = p.ID,
                Name = p.Name
            });
        }

        public List<string> PopulatePlaceCategories()
        {
            return new List<string>() { "Coffee", "Food", "Farm", "CraftShop", "Fashion" };
        }

        public IEnumerable<ArticleAndBlogPost> GetRecentPosts(PlaceContext context, int amount)
        {
            var articles = context.Articles.Select(a => new ArticleAndBlogPost
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

            var blogPosts = context.BlogPosts.Select(bp => new ArticleAndBlogPost
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

            var articlesAndBlogPosts = articles.Union(blogPosts).OrderByDescending(abp => abp.PostedOn).Take(amount);

            return articlesAndBlogPosts;
        }

        public IEnumerable<PlaceContributor> GetTopContributors(PlaceContext context, int contributors)
        {
            var topContributors = context.Places
                .Where(p => p.UserEmail != null)
                .GroupBy(x => new { x.UserName })
                .Select(group => new { Name = group.Key, Count = group.Count() })
                .OrderByDescending(x => x.Count)
                .Take(contributors)
                .AsQueryable();

            var topContrbitorsList = topContributors.Select(c => new PlaceContributor { UserName = c.Name.UserName ?? "Anonymous", PlacesContributed = c.Count });

            return topContrbitorsList;
        }
    }
}