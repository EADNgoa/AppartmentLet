﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GoaQuickTrips
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QuickTripsEntities : DbContext
    {
        public QuickTripsEntities()
            : base("name=QuickTripsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BookedCustomer> BookedCustomers { get; set; }
        public virtual DbSet<Price> Prices { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Config> Configs { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Apartment> Apartments { get; set; }
        public virtual DbSet<BookingDetail> BookingDetails { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<ReviewDetail> ReviewDetails { get; set; }
        public virtual DbSet<MasterAmenity> MasterAmenities { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
    }
}
