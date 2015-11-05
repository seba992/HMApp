using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using DiamondApp.EntityModel;
using DiamondApp.Tools;
using DiamondApp;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices;

namespace DiamondApp.ViewModels
{
    class AddUserViewModel : ObservableObject
    {
        private DiamondDBEntities _ctx;

        public RelayCommand _addUserCommand;
        private string _userPassword;

        private string _userName { get; set; }
        private string _userSurname { get; set; }
        private string _userPhoneNumber { get; set; }
        private string _userEmail { get; set; }
        private string _userPosition { get; set; }
        private int _userType { get; set; }
        private string _userLogin { get; set; }

        public string UserPassword
        {
            get { return _userPassword; }
            set { _userPassword = ShaConverter.sha256_hash(value); }
        }


        public AddUserViewModel()
        {
            _ctx = new DiamondDBEntities();
        }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public string UserSurname
        {
            get { return _userSurname; }
            set { _userSurname = value; }
        }

        public string UserPhoneNumber
        {
            get { return _userPhoneNumber; }
            set { _userPhoneNumber = value; }
        }

        public string UserEmail
        {
            get { return _userEmail; }
            set { _userEmail = value; }
        }

        public string UserPosition
        {
            get { return _userPosition; }
            set { _userPosition = value; }
        }

        public ICommand AddUserCommand
        {
            get
            {
                if (_addUserCommand == null)
                {
                    _addUserCommand = new RelayCommand(param => this.AddUserExecute(), param => this.CanAddUserExecute());
                }
                return _addUserCommand;
            }
        }

        private bool CanAddUserExecute()
        {
            return true;
        }

        private void AddUserExecute()
        {

            try {
                var addUser = new Users
                {
                    Name = _userName,
                    Surname = _userSurname,
                    PhoneNum = _userPhoneNumber,
                    Email = _userEmail,
                    Position = _userPosition,
                    AccountType = 1,
                    Login = "krzysiek", //ShaConverter.sha256_hash() !!!!! nie dziala
                    Password = "zbyszek",
                    FirstLogin = "t"
                };

                _ctx.Users.Add(addUser);
                _ctx.SaveChanges();
                MessageBox.Show("udalo sie");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            } 
        }
    }
}
