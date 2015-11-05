using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondApp.DataGridObjectClasses
{
    public class AddNewProposition
    {
        public string CompanyName
        {
            get { return "Zmienić tą durną nazwę \\n nic innego nie przyszło mi do głowy \\n :("; }
            set { }
        }

        public string OurNip
        {
            get { return "12323434545"; }
            set { }
        }

        public DateTime UpdateDate { get; set; }
        public string UserFirstName { get; set; }
        public string UserSurname { get; set; }
        public string UserPhoneNum { get; set; }
        public string UserEmail { get; set; }

        public string UserFullname
        {
            get { return UserFirstName + " " + UserSurname; }
        }
    }
}
