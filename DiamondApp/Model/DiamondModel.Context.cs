﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DiamondApp.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DiamondDBEntities : DbContext
    {
        public DiamondDBEntities()
            : base("name=DiamondDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<PropAccomodation_Dictionary> PropAccomodation_Dictionary { get; set; }
        public virtual DbSet<PropExtraServices_Dictionary> PropExtraServices_Dictionary { get; set; }
        public virtual DbSet<PropExtraServicesDiscount> PropExtraServicesDiscount { get; set; }
        public virtual DbSet<PropHallEquipment> PropHallEquipment { get; set; }
        public virtual DbSet<PropHallEquipmentDiscount> PropHallEquipmentDiscount { get; set; }
        public virtual DbSet<PropHallEquipmnet_Dictionary_First> PropHallEquipmnet_Dictionary_First { get; set; }
        public virtual DbSet<PropHallEquipmnet_Dictionary_Second> PropHallEquipmnet_Dictionary_Second { get; set; }
        public virtual DbSet<PropMenuMerge_Dictionary_First> PropMenuMerge_Dictionary_First { get; set; }
        public virtual DbSet<PropMergeTypes_Dictionary> PropMergeTypes_Dictionary { get; set; }
        public virtual DbSet<PropPaymentSuggestions> PropPaymentSuggestions { get; set; }
        public virtual DbSet<PropPaymentSuggestions_Dictionary_First> PropPaymentSuggestions_Dictionary_First { get; set; }
        public virtual DbSet<PropPaymentSuggestions_Dictionary_Fourth> PropPaymentSuggestions_Dictionary_Fourth { get; set; }
        public virtual DbSet<PropPaymentSuggestions_Dictionary_Second> PropPaymentSuggestions_Dictionary_Second { get; set; }
        public virtual DbSet<PropPaymentSuggestions_Dictionary_Third> PropPaymentSuggestions_Dictionary_Third { get; set; }
        public virtual DbSet<PropReservationDetails> PropReservationDetails { get; set; }
        public virtual DbSet<PropReservationDetails_Dictionary_HallCapacity> PropReservationDetails_Dictionary_HallCapacity { get; set; }
        public virtual DbSet<PropReservationDetails_Dictionary_HallPrices> PropReservationDetails_Dictionary_HallPrices { get; set; }
        public virtual DbSet<PropMenuMerge> PropMenuMerge { get; set; }
        public virtual DbSet<AccountPrivileges> AccountPrivileges { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Proposition> Proposition { get; set; }
        public virtual DbSet<PropClient> PropClient { get; set; }
        public virtual DbSet<PropReservationDetails_Dictionary_HallSettings> PropReservationDetails_Dictionary_HallSettings { get; set; }
        public virtual DbSet<PropMenuGastronomicThings_Dictionary_First> PropMenuGastronomicThings_Dictionary_First { get; set; }
        public virtual DbSet<VatList> VatList { get; set; }
        public virtual DbSet<PropAccomodationDiscount> PropAccomodationDiscount { get; set; }
        public virtual DbSet<PropAccomodation> PropAccomodation { get; set; }
        public virtual DbSet<PropExtraServices> PropExtraServices { get; set; }
        public virtual DbSet<PropMenuPosition> PropMenuPosition { get; set; }
        public virtual DbSet<PropositionStates_Dictionary> PropositionStates_Dictionary { get; set; }
    }
}