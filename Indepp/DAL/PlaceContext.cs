﻿using Indepp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Indepp.DAL
{
    public class PlaceContext : DbContext
    {
        public PlaceContext() : base("PlaceContext")
        {
        }

        public DbSet<Place> Places { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<PlaceDescription> PlaceDescriptions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}