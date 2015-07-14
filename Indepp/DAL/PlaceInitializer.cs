using Indepp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Indepp.DAL
{
    public class PlaceInitializer : System.Data.Entity.DropCreateDatabaseAlways<PlaceContext>
    {
        protected override void Seed(PlaceContext context)
        {
            var address = new Address { City = "Leeds", Country = "United Kingdom", Couty = "West Yorkshire" };
            var article = new Article { Name = "Best Coffee in UK!", Description = "This is the best place to drink coffee in UK." };
            var description = new PlaceDescription { Description = "Temporary Missing" };

            var places = new List<Place> 
            {
                new Place {ID = 1, Name = "La Bottega Milanese", Category = "Coffee", Address = address, Article = article, Description = description },
                new Place {ID = 2, Name = "Test Coffee", Category = "Coffee", Address = address, Article = article, Description = description },
                new Place {ID = 3, Name = "Good Coffee", Category = "Coffee", Address = address, Article = article, Description = description },
                new Place {ID = 4, Name = "Shit Coffee", Category = "Coffee", Address = address, Article = article, Description = description },
                new Place {ID = 5, Name = "The Old Black Cat", Category = "Coffee", Address = address, Article = article, Description = description },
                new Place {ID = 6, Name = "Name me what you like", Category = "Coffee", Address = address, Article = article, Description = description },
                new Place {ID = 7, Name = "Burning hell", Category = "Coffee", Address = address, Article = article, Description = description },
                new Place {ID = 8, Name = "Sweet lords coffee", Category = "Coffee", Address = address, Article = article, Description = description },
                new Place {ID = 9, Name = "Belgrave Music Hall and Canteen", Category = "Food", Address = address, Article = article, Description = description },
                new Place {ID = 10, Name = "JUST FOOD", Category = "Food", Address = address, Article = article, Description = description },
                new Place {ID = 11, Name = "Yorkshire Farm", Category = "Farms", Address = address, Article = article, Description = description },
                new Place {ID = 12, Name = "Leeds Farm", Category = "Farms", Address = address, Article = article, Description = description },
                new Place {ID = 11, Name = "The Light", Category = "CraftShops", Address = address, Article = article, Description = description },
                new Place {ID = 12, Name = "The Dark", Category = "CraftShops", Address = address, Article = article, Description = description }
            };

            places.ForEach(p => context.Places.Add(p));
            context.SaveChanges();


            var blogPosts = new List<BlogPost>
            {
                new BlogPost {ID = 1, Title = "Welcome to our website", ShortDescription = "Hello and welcome. We pleased to announce, that ...", Description = "pam", PostedOn = new DateTime(2015, 7, 12, 22, 37, 15)},
                new BlogPost {ID = 2, Title = "First thing to do in the mornings", ShortDescription = "It feels good to start your day with a great cup of coffee...", Description = "param", PostedOn = new DateTime(2015, 7, 12, 23, 0 , 0)}
            };

            blogPosts.ForEach(bp => context.BlogPosts.Add(bp));
            context.SaveChanges();
        }
    }
}