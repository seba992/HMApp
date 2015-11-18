//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DiamondApp.EntityModel
{
    using System;
    using System.Collections.ObjectModel;
    
    public partial class Proposition
    {
        public Proposition()
        {
            this.PropHallEquipment = new ObservableCollection<PropHallEquipment>();
            this.PropMenuMerge = new ObservableCollection<PropMenuMerge>();
            this.PropPaymentSuggestions = new ObservableCollection<PropPaymentSuggestions>();
            this.PropMenuPosition = new ObservableCollection<PropMenuPosition>();
            this.PropAccomodation = new ObservableCollection<PropAccomodation>();
            this.PropExtraServices = new ObservableCollection<PropExtraServices>();
        }
    
        public int Id { get; set; }
        public int Id_user { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public string Status { get; set; }
    
        public virtual PropExtraServicesDiscount PropExtraServicesDiscount { get; set; }
        public virtual ObservableCollection<PropHallEquipment> PropHallEquipment { get; set; }
        public virtual PropHallEquipmentDiscount PropHallEquipmentDiscount { get; set; }
        public virtual ObservableCollection<PropMenuMerge> PropMenuMerge { get; set; }
        public virtual Users Users { get; set; }
        public virtual ObservableCollection<PropPaymentSuggestions> PropPaymentSuggestions { get; set; }
        public virtual PropReservationDetails PropReservationDetails { get; set; }
        public virtual PropClient PropClient { get; set; }
        public virtual ObservableCollection<PropMenuPosition> PropMenuPosition { get; set; }
        public virtual PropAccomodationDiscount PropAccomodationDiscount { get; set; }
        public virtual ObservableCollection<PropAccomodation> PropAccomodation { get; set; }
        public virtual ObservableCollection<PropExtraServices> PropExtraServices { get; set; }
    }
}
