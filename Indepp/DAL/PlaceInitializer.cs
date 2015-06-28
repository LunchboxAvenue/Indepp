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
                new Place {ID = 1, Name = "La Bottega Milanese", Category = "Coffee Shop", Address = address, Article = article, Description = description },
                new Place {ID = 2, Name = "Test Coffee", Category = "Coffee Shop", Address = address, Article = article, Description = description },
                new Place {ID = 3, Name = "Good Coffee", Category = "Coffee Shop", Address = address, Article = article, Description = description },
                new Place {ID = 4, Name = "Shit Coffee", Category = "Coffee Shop", Address = address, Article = article, Description = description },
                new Place {ID = 5, Name = "The Old Black Cat", Category = "Coffee Shop", Address = address, Article = article, Description = description },
                new Place {ID = 6, Name = "Name me what you like", Category = "Coffee Shop", Address = address, Article = article, Description = description },
                new Place {ID = 7, Name = "Burning hell", Category = "Coffee Shop", Address = address, Article = article, Description = description },
                new Place {ID = 8, Name = "Sweet lords coffee", Category = "Coffee Shop", Address = address, Article = article, Description = description },
            };

            places.ForEach(p => context.Places.Add(p));
            context.SaveChanges();
        }
    }
}