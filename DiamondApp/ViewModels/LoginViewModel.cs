using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DiamondApp.Model;
using DiamondApp.Tools.Converters;
using DiamondApp.Tools.MvvmClasses;
using DiamondApp.Tools.Validators;
using DiamondApp.Views;

namespace DiamondApp.ViewModels
{
    public class LoginViewModel : ObservableObject
    {
        private readonly DiamondDBEntities _ctx;
        private RelayCommand<PasswordBox> _loginCommand;

        private int _userId;
        private string _userType;
        private string _userLogin;
        private bool _allowToLog;
        private bool _isWrongPassword;

        public LoginViewModel()
        {
            _ctx = new DiamondDBEntities();
//             komentarz w celu wydajniejszych testów - LAG ON FIRST QUERRY
//            var users = (from s in _ctx.Users
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
                var q = (from s in _ctx.Users
                    where _userLogin == s.Login
                    select s).SingleOrDefault();
                if (q != null && q.FirstLogin.ToUpper() == "T")
                    Xceed.Wpf.Toolkit.MessageBox.Show("Wymagana zmiana hasła! Wprowadź swoje nowe hasło celu jego ustawienia.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information); 
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
                    _loginCommand = new RelayCommand<PasswordBox>(LoginExecute, CanLoginExecute);
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

                // jeżeli w bazie jest tylko jeden użytkownik o podanej nazwie użytkownika oraz jest to jego pierwsze logowanie
                if (userToLogin.Count() == 1 && userToLogin.SingleOrDefault().FirstLogin.ToUpper() == "T") 
                {
                    if (PasswordValidator.ValidatePassword(passBox.Password))
                    {
                        //.Password = passBox.Password;
                        userToLogin.First().Password = ShaConverter.sha256_hash(passBox.Password);    // przypisz do konta użytkownika wpisane przez niego hasło
                        userToLogin.First().FirstLogin = "f";   // zmień tryb logowania
                        UserId = userToLogin.First().Id;    // przypisz Id użytkownika w celu umożliwienia jego jednoznacznej identyfikacji
                        _userType = userToLogin.First().AccountPrivileges.AccountType;  // uzyskaj typ konta użytkownika znajdujący się w bazie danych
                        _ctx.SaveChanges(); // zapisz zmiany
                        _allowToLog = true; // umożliwienie zalogowania się
                        _isWrongPassword = false;
                    }
                    else
                    {
                        Xceed.Wpf.Toolkit.MessageBox.Show("Hasło musi zawierac przynajmniej 8 znaków w tym przynajmniej jedną dużą literę, małą literę oraz cyfrę. " + 
                            Environment.NewLine + "Spróbuj ponownie!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                        _isWrongPassword = true;
                    }
                }
                //.Password == passBox.Password)
                // jeśli w bazie jest tylko jeden użytkownik o podanej nazwie użytkownika oraz podana nazwa konta oraz przypisane do niego hasło jest poprawne
                else if (userToLogin.Count() == 1 && userToLogin.First().Login == _userLogin && userToLogin.First().Password == ShaConverter.sha256_hash(passBox.Password))
                {
                    _userType = userToLogin.First().AccountPrivileges.AccountType;
                    UserId = userToLogin.First().Id;
                    _allowToLog = true;
                }
                // jeżli użytkownik otrzymał dostęp do logowania
                if (_allowToLog && _isWrongPassword == false)
                {
                    // w zależności od typu konta uruchom okno główne
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
                        Xceed.Wpf.Toolkit.MessageBox.Show("Błędny typ konta usera", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    if (_isWrongPassword)
                    {
                        passBox.Clear();
                    }
                    else
                    {
                        Xceed.Wpf.Toolkit.MessageBox.Show("Podana nazwa użytkownika i/lub hasło jest niepoprawne!" + Environment.NewLine +
                                        "Spróbuj ponownie!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                        UserLogin = string.Empty;
                        passBox.Clear();
                    }

                }
            }
            catch (Exception ex)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Błąd połączenia z bazą danych. Skontaktuj się z administratorem.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                
                //MessageBox.Show(ex.ToString());
                //MessageBox.Show("Błąd połączenia z bazą danych. Skontaktuj się z administratorem.");
            }   
        }
    }
}
