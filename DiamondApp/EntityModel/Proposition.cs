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
            this.PropAccomodation = new ObservableCollection<PropAccomodation>();
            this.PropExtraServices = new ObservableCollection<PropExtraServices>();
            this.PropHallEquipment = new ObservableCollection<PropHallEquipment>();
            this.PropMenuPosition = new ObservableCollection<PropMenuPosition>();
            this.PropPaymentSuggestions = new ObservableCollection<PropPaymentSuggestions>();
        }
    
        public int Id { get; set; }
        public int Id_user { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
    
        public virtual ObservableCollection<PropAccomodation> PropAccomodation { get; set; }
        public virtual ObservableCollection<PropExtraServices> PropExtraServices { get; set; }
        public virtual ObservableCollection<PropHallEquipment> PropHallEquipment { get; set; }
        public virtual ObservableCollection<PropMenuPosition> PropMenuPosition { get; set; }
        public virtual Users Users { get; set; }
        public virtual ObservableCollection<PropPaymentSuggestions> PropPaymentSuggestions { get; set; }
    }
}
