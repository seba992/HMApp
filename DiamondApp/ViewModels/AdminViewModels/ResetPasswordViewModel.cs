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
using DiamondApp.ViewModels;
using DiamondApp.Views;

namespace DiamondApp.ViewModels
{
    public class ResetPasswordViewModel : ObservableObject
    {
        private DiamondDBEntities _ctx;
        public RelayCommand _resetPasswordCommand;
        private List<Users> _userList;
        private string _userLogin;

        public ResetPasswordViewModel()
        {
            _ctx = new DiamondDBEntities();
            SelectAllUsers();
        }

        public string UserLogin
        {
            get { return _userLogin; }
            set { _userLogin = value; }
        }

        public void SelectAllUsers()
        {
            var q = (from s in _ctx.Users
                     select s).ToList();

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
            if (string.IsNullOrEmpty(_userLogin))
                return false;
            return true;
        }

        private void ResetPasswordExecute()
        {
            try
            {
                Users userUpdate = (from user in _ctx.Users
                                    where user.Login == _userLogin // int selected!!! you know what i want up to date
                                    select user).First();

                userUpdate.FirstLogin = "t";

                _ctx.SaveChanges();

                MessageBox.Show("Hasło użytkownika zostało zresetowane!");
                Application.Current.MainWindow.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wystąpił błąd!");
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
