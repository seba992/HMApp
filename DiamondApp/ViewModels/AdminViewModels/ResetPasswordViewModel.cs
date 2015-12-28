using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using DiamondApp.Model;
using DiamondApp.Tools.MvvmClasses;

namespace DiamondApp.ViewModels.AdminViewModels
{
    public class ResetPasswordViewModel : ObservableObject
    {
        private DiamondDBEntities _ctx;
        public RelayCommand _resetPasswordCommand;
        private List<Users> _userList;
        private Users _userProp;

        public ResetPasswordViewModel()
        {
            _ctx = new DiamondDBEntities();
            SelectAllUsers();
        }

        public Users UserProp
        {
            get { return _userProp; }
            set
            {
                _userProp = value;
                RaisePropertyChanged("UserProp");
            }
        }

        public void SelectAllUsers()
        {
            var q = (from s in _ctx.Users
                     orderby s.Surname
                     select s ).ToList();

            UsersList = q;
        }


        public List<Users> UsersList
        {
            get { return _userList; }
            set
            {
                _userList = value;
                RaisePropertyChanged("UsersList");
            }
        }

        public ICommand ResetPasswordCommand
        {
            get
            {
                if (_resetPasswordCommand == null)
                {
                    _resetPasswordCommand = new RelayCommand(param => this.ResetPasswordExecute(), param => this.CanResetPasswordExecute());
                }
                return _resetPasswordCommand;
            }
        }

        private bool CanResetPasswordExecute()
        {
            if (_userProp == null)
                return false;
            return true;
        }

        private void ResetPasswordExecute()
        {
            try
            {
                Users userUpdate = (from user in _ctx.Users
                                    where user.Id == _userProp.Id // int selected!!! you know what i want up to date
                                    select user).First();

                userUpdate.FirstLogin = "t";

                _ctx.SaveChanges();

                Xceed.Wpf.Toolkit.MessageBox.Show("Hasło użytkownika zostało zresetowane!", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.MainWindow.Hide();
            }
            catch (Exception ex)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Wystąpił błąd! Hasło użytkownika nie zostało zresetowane.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}
