using Indepp.Models;
using Indepp.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Indepp.HelperMethods
{
    public class DynamicFilteringMethods
    {
        public IQueryable<Place> SortPlaces(IQueryable<Place> places, string sortOrder)
        {
            switch (sortOrder)
            {
                case "name_desc":
                    places = places.OrderByDescending(p => p.Name);
                    break;
                case "id_desc":
                    places = places.OrderByDescending(p => p.ID);
                    break;
                case "ID":
                    places = places.OrderBy(p => p.ID);
                    break;
                default:
                    places = places.OrderBy(p => p.Name);
                    break;
            }

            return places;
        }

        public IQueryable<Place> FilterPlaces(IQueryable<Place> places, PlaceFilter filter)
        {
            if (!String.IsNullOrEmpty(filter.Name))
                places = places.Where(p => p.Name.Contains(filter.Name));
            if (!String.IsNullOrEmpty(filter.Country))
                places = places.Where(p => p.Address.Country.Contains(filter.Country));
            if (!String.IsNullOrEmpty(filter.City))
                places = places.Where(p => p.Address.City.Contains(filter.City));

            return places;
        }

        public IPagedList<Place> PlaceList(IQueryable<Place> places, int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return places.ToPagedList(pageNumber, pageSize);
        }
    }
}