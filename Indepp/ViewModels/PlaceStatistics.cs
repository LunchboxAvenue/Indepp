using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Indepp.ViewModels
{
    public class PlaceStatistics
    {
        public int CoffeePlaces { get; set; }
        public int FoodPlaces { get; set; }
        public int FarmPlaces { get; set; }
        public int CraftShopPlaces { get; set; }
        public int FashionPlaces { get; set; }

        public int TotalPlaces { get; set; }
        public int UserContributedPlaces { get; set; }
        public int ReviewedPlaces { get; set; }

        public int TotalArticles { get; set; }
        public int LinkedArticles { get; set; }
        public int TotalBlogPosts { get; set; }

        public string BestContributorEmail { get; set; }
    }
}