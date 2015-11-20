using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondApp.DataGridObjectClasses
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
