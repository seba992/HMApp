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
        private string _userName;
        private string _userSurname;
        private string _userPhoneNumber;
        private string _userEmail;
        private string _userPosition;
        private int _userAccountType;

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

        public string UserLogin
        {
            get 
            {
                return  Validations.FirstLetterToLowerCase(UserName) + "." + Validations.FirstLetterToLowerCase(UserSurname);  
            }
        }

        public string UserPassword
        {
            get { return _userPassword; }
            set { _userPassword = ShaConverter.sha256_hash(value); }
        }

        public int UserAccountType
        {
            get { return _userAccountType; }
            set { _userAccountType = value; }
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
            if (string.IsNullOrEmpty(_userName) || string.IsNullOrEmpty(_userSurname) || _userAccountType == 0)
                return false;
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
                    AccountType = _userAccountType,
                    Login = UserLogin,
                    Password = "",
                    FirstLogin = "f"
                };

                _ctx.Users.Add(addUser);
                _ctx.SaveChanges();
                MessageBox.Show("Konto użytkownika zostało utworzone!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            } 
        }
    }
}
