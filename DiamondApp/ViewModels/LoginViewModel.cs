using System;
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

        private bool _allowToLog = false;

        public LoginViewModel()
        {
            _ctx = new DiamondDBEntities();
        }

        public string UserLogin
        {
            get { return _userLogin; }
            set
            {
                _userLogin = value;
                RaisePropertyChanged("UserLogin");
                _loginCommand.RaiseCanExecuteChanged(); // sprawdz czy user nacisnal przycisk logowania
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
                var userList = (from u in _ctx.Users
                                select new { u.Id, u.Login, u.Password, u.AccountPrivileges.AccountType }
                ).ToList();
                if (userList.Any())
                {
                    foreach (var user in userList)
                    {
                        //(user.Password == ShaConverter.sha256_hash(passBox.Password))\
                        if (user.Login == _userLogin && user.Password == passBox.Password)
                        {

                            UserId = user.Id;
                            _userType = user.AccountType;
                            Application.Current.MainWindow.Hide();

//                            MessageBox.Show("Otworz nowe okno \n" +
//                                            "Zamknij obecne");
                            _allowToLog = true;
                            break;
                        }
                    }
                }

                if (_allowToLog)
                {
                    // Application.Current.AdminMainView

                    // if typ konta to je wlacz
                    if (_userType.ToUpper() == "A")
                    {
                        AdminMainView adminMainView = new AdminMainView(_userId);
                        adminMainView.Show();
                    }
                    else if(_userType.ToUpper() == "S")
                    {
                        UserMainView userMainView = new UserMainView(_userId);
                        userMainView.Show();
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
                // MessageBox.Show("Błąd połączenia z bazą danych. Skontaktuj się z administratorem.");
            }   
        }
    }
}
