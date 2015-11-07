using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiamondApp.Tools;

namespace DiamondApp.DataGridObjectClasses
{
    public class AddNewProposition : ObservableObject
    {
        private DateTime _updateDate;
        private DateTime _validityPropDate;

        public string CompanyName
        {
            get { return "Zmienić tą durną nazwę " + Environment.NewLine + " nic innego nie przyszło mi do głowy "+Environment.NewLine+" :("; }
            set { }
        }

        public string OurNip
        {
            get { return "12323434545"; }
            set { }
        }

        public string UserFullname
        {
            get { return UserFirstName + " " + UserSurname; }
            set { }
        }

        public DateTime UpdateDate
        {
            get { return _updateDate; }
            set
            {
                _updateDate = value;
                ValidityPropDate = value.AddDays(4);
            }
        }

        public DateTime ValidityPropDate
        {
            get { return _validityPropDate; }
            set
            {
                _validityPropDate = value;
                RaisePropertyChanged("ValidityPropDate");
            }
        }

        public string UserFirstName { get; set; }
        public string UserSurname { get; set; }
        public string UserPhoneNum { get; set; }
        public string UserEmail { get; set; }
        public bool IsCreated { get; set; }


    }
}
