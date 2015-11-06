using System;
using System.Collections.Generic;
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
        public string CustomerFirstName
        { get; set; }
        public string CustomerSurname
        { get; set; }
        public string CompanyName
        { get; set; }
        public DateTime? UpdateDate
        { get; set; }
        public string Status 
        { get; set; }

        public string UserFullName
        {
            get { return UserFirstName + " " + UserSurname; }
        }

        public string CustomerFullName
        {
            get { return CustomerFirstName + " " + CustomerSurname; }
        }

        
    }
}
