using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiamondApp.DataGridFillingObjects
{
    public class AdminProposition
    {
        public int PropositionId 
        { get; set; }
        public string UserFirstName
        { get; set; }
        public string UserSurname
        { get; set; }
        public string CompanyName
        { get; set; }
        [Column(TypeName = "Date")]
        public DateTime UpdateDate
        { get; set; }
        public string Status 
        { get; set; }
        public string CustomerFullName
        { get; set; }

        public string UserFullName
        {
            get { return UserFirstName + " " + UserSurname; }
        }
    }
}
