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
    
    public partial class PropHallEquipmentDiscount
    {
        public int Id_proposition { get; set; }
        public Nullable<float> StandardPrice { get; set; }
        public Nullable<float> Discount { get; set; }
    
        public virtual Proposition Proposition { get; set; }
    }
}