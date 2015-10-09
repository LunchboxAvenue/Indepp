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
                case "country_desc":
                    places = places.OrderByDescending(p => p.Address.Country);
                    break;
                case "country_asc":
                    places = places.OrderBy(p => p.Address.Country);
                    break;
                case "city_desc":
                    places = places.OrderByDescending(p => p.Address.City);
                    break;
                case "city_asc":
                    places = places.OrderBy(p => p.Address.City);
                    break;
                default:
                    places = places.OrderBy(p => p.Name);
                    break;
            }

            return places;
        }

        public IQueryable<Place> FilterPlaces(IQueryable<Place> places, PlaceFilter filter)
        {
            int dayOfTheWeek = (int)DateTime.Now.DayOfWeek;
            if (!String.IsNullOrEmpty(filter.Name))
                places = places.Where(p => p.Name.Contains(filter.Name));
            if (!String.IsNullOrEmpty(filter.Country))
                places = places.Where(p => p.Address.Country.Contains(filter.Country));
            if (!String.IsNullOrEmpty(filter.City))
                places = places.Where(p => p.Address.City.Contains(filter.City));
            if (!String.IsNullOrEmpty(filter.OpenTime.ToString()))
                places = places.Where(p => p.WorkingHours.Any(wh => (int)wh.Day == dayOfTheWeek && filter.OpenTime.Value.CompareTo(wh.OpenTime.Value) != -1));
            if (!String.IsNullOrEmpty(filter.CloseTime.ToString()))
                places = places.Where(p => p.WorkingHours.Any(wh => (int)wh.Day == dayOfTheWeek && wh.CloseTime.Value.CompareTo(filter.CloseTime.Value) != -1));
            if (!String.IsNullOrEmpty(filter.Category))
                places = places.Where(p => p.Category.Contains(filter.Category));
                
            return places;
        }

        public IPagedList<Place> PlaceList(IQueryable<Place> places, int? page, int pageSize = 3)
        {
            int pageNumber = (page ?? 1);

            return places.ToPagedList(pageNumber, pageSize);
        }

        public bool FilterCheck(PlaceFilter filter, PlaceFilter currentPlaceFilter)
        {
            if (filter.Name != null || filter.City != null || filter.Country != null || filter.OpenTime.ToString() != null || filter.CloseTime.ToString() != null || filter.Category != null)
                if (filter.Name != currentPlaceFilter.Name || filter.City != currentPlaceFilter.City || filter.Country != currentPlaceFilter.Country || filter.OpenTime != currentPlaceFilter.OpenTime || filter.CloseTime != currentPlaceFilter.CloseTime || filter.Category != currentPlaceFilter.Category)
                    return true;
                else
                    return false;
            else
                return false;
        }
    }
}