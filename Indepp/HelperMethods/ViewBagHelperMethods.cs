using Indepp.DAL;
using Indepp.Models;
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
    }
}