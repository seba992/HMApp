//------------------------------------------------------------------------------
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
    using System.Collections.ObjectModel;
    
    public partial class PropReservationDetails
    {
        public int Id_proposition { get; set; }
        public Nullable<System.DateTime> StartData { get; set; }
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.DateTime> EndData { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }
        public Nullable<int> PeopleNumber { get; set; }
        public string Hall { get; set; }
        public string HallSetting { get; set; }
    
        public virtual Proposition Proposition { get; set; }
    }
}
