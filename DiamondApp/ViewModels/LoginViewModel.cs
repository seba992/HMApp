using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DiamondApp.EntityModel;
using DiamondApp.Tools;
using DiamondApp.Views;
using Microsoft.Practices.ServiceLocation;

namespace DiamondApp.ViewModels
{
    public class LoginViewModel : ObservableObject
    {
        private DiamondDBEntities _ctx;
        private RelayCommand<PasswordBox> _loginCommand;
        // public Action CloseAction { get; set; } <- read on google to correct close the window

        private int _userId;
        private string _userType;
        private string _userLogin;
        private bool _allowToLog;

        public LoginViewModel()
        {
            _ctx = new DiamondDBEntities();
            // komentarz w celu wydajniejszych testów - LAG ON FIRST QUERRY
//            users = (from s in _ctx.Users
//                select s).ToList();
        }

        public string UserLogin
        {
            get { return _userLogin; }
            set
            {
                _userLogin = value;
                RaisePropertyChanged("UserLogin");
                _loginCommand.RaiseCanExecuteChanged(); // sprawdz czy user nacisnal przycisk logowania
                // działa - odkomentować gdy wersja beta :D
//                var q = (from s in _ctx.Users
//                    where _userLogin == s.Login
//                    select s).SingleOrDefault();
//                if (q != null && q.FirstLogin.ToUpper() == "T")
//                    MessageBox.Show("Wymagana zmiana hasła! Wprowadź swoje nowe hasło celu jego ustawienia.");
            }
        }

        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        
        public ICommand LoginCommand
        {
            // jesli zostal nacisniety przycisk logowania wykonaj ta operacje
            get
            {
                if (_loginCommand == null)
                {
                    _loginCommand = new RelayCommand<PasswordBox>((t) => LoginExecute(t), (t) => CanLoginExecute(t));
                }
                return _loginCommand;
            }          
        }

        // sprawdza czy login nie jest pusty
        private bool CanLoginExecute(PasswordBox arg)
        {
            if (string.IsNullOrEmpty(_userLogin))
                return false;
            return true;
        }

        // login logic
        private void LoginExecute(PasswordBox passBox)
        {
            try
            {
                var userToLogin = (from u in _ctx.Users
                                  where u.Login == _userLogin
                                  select u);

                if (userToLogin.Count() == 1 && userToLogin.SingleOrDefault().FirstLogin.ToUpper() == "T") // jesli jest jeden i tylko jeden user
                { 
                    //.Password = ShaConverter.sha256_hash(passBox.Password);
                    userToLogin.First().Password = passBox.Password;
                    userToLogin.First().FirstLogin = "f";
                    UserId = userToLogin.First().Id;
                    _userType = userToLogin.First().AccountPrivileges.AccountType;
                    _ctx.SaveChanges();
                    _allowToLog = true;
                }
                //.Password == ShaConverter.sha256_hash(passBox.Password))
                else if (userToLogin.Count() == 1 && userToLogin.First().Login == _userLogin && userToLogin.First().Password == passBox.Password)
                {
                    _userType = userToLogin.First().AccountPrivileges.AccountType;
                    UserId = userToLogin.First().Id;
                    _allowToLog = true;
                }
                if (_allowToLog)
                {
                    // if typ konta to je wlacz
                    if (_userType.ToUpper() == "A")
                    {
                        AdminMainView adminMainView = new AdminMainView(_userId);
                        adminMainView.Show();
                        Application.Current.MainWindow.Hide();
                    }
                    else if(_userType.ToUpper() == "S")
                    {
                        UserMainView userMainView = new UserMainView(_userId);
                        userMainView.Show();
                        Application.Current.MainWindow.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Błędny typ konta usera");      // TO DELETE
                    }
                }
                else
                {
                    MessageBox.Show("Podana nazwa użytkownika i/lub hasło jest niepoprawne!" +
                                    "Spróbuj ponownie!");
                    UserLogin = string.Empty;
                    passBox.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show("Błąd połączenia z bazą danych. Skontaktuj się z administratorem.");
            }   
        }
    }
}
