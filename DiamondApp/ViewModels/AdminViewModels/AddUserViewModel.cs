using System;
using System.Windows;
using System.Windows.Input;
using DiamondApp.Model;
using DiamondApp.Tools.Converters;
using DiamondApp.Tools.MvvmClasses;

namespace DiamondApp.ViewModels.AdminViewModels
{
    public class AddUserViewModel : ObservableObject
    {
        private DiamondDBEntities _ctx;

        public RelayCommand _addUserCommand;
       
        private string _userPassword;
        private string _userName;
        private string _userSurname;
        private string _userPhoneNumber;
        private string _userEmail;
        private string _userPosition;
        private string _userAccountType;

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
                return UserName.ToLower() + "." + UserSurname.ToLower() ;  
            }
        }

        public string UserPassword
        {
            get { return _userPassword; }
            set { _userPassword = ShaConverter.sha256_hash(value); }
        }

        public string UserAccountType
        {
            get { return _userAccountType; }
            set { _userAccountType = value; }
        }

        public int UserAccontType2()
        { 
            if(UserAccountType=="Administrator")
                return 1;
            else
                return 2;
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
            if (string.IsNullOrEmpty(_userName) || string.IsNullOrEmpty(_userSurname)
                || string.IsNullOrEmpty(_userPhoneNumber) || string.IsNullOrEmpty(_userEmail) || string.IsNullOrEmpty(_userPosition)
                || string.IsNullOrEmpty(_userAccountType))
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
                    AccountType = UserAccontType2(),
                    Login = UserLogin,
                    Password = "",
                    FirstLogin = "t"
                };

                _ctx.Users.Add(addUser);
                _ctx.SaveChanges();
                Xceed.Wpf.Toolkit.MessageBox.Show("Konto użytkownika zostało utworzone!", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.MainWindow.Hide();
            }
            catch (Exception ex)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Wystąpił błąd! Konto użytkownika nie zostało utworzone", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            } 
        }
    }
}
