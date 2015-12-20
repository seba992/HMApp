using System;
using DiamondApp.Tools.MvvmClasses;

namespace DiamondApp.DataGridFillingObjects
{
    public class AddNewProposition : ObservableObject
    {
        private DateTime _updateDate;
        private DateTime _validityPropDate;

        public string CompanyName
        {
            get { return "Sieć hotelowa Gliwice *****" + Environment.NewLine + "ul. Inżynierska 5 " + Environment.NewLine + "55-555 Nibylandia"; }
            set { }
        }

        public string OurNip
        {
            get { return "1234567890"; }
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
