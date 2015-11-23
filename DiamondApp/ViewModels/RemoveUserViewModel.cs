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
    public class RemoveUserViewModel : ObservableObject
    {
        private DiamondDBEntities _ctx;
        public RelayCommand _removeUserCommand;
        private List<Users> _userList;
        private string _userLogin;

        public RemoveUserViewModel()
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

        public ICommand RemoveUserCommand
        {
            get
            {
                if (_removeUserCommand == null)
                {
                    _removeUserCommand = new RelayCommand(param => this.RemoveUserExecute(), param => this.CanRemoveUserExecute());
                }
                return _removeUserCommand;
            }
        }

        private bool CanRemoveUserExecute()
        {
            if (string.IsNullOrEmpty(_userLogin))
                return false;
            return true;
        }

        private void RemoveUserExecute()
        {
            try
            {

                var order = (from o in _ctx.Users
                             where o.Login == _userLogin
                             select o).First();
                _ctx.Users.Remove(order);
                _ctx.SaveChanges();
                MessageBox.Show("Konto użytkownika zostało usunięte!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wystąpił błąd!");
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
