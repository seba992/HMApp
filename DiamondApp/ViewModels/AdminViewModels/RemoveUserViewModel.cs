using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using DiamondApp.Model;
using DiamondApp.Tools.MvvmClasses;

namespace DiamondApp.ViewModels.AdminViewModels
{
    public class RemoveUserViewModel : ObservableObject
    {
        private DiamondDBEntities _ctx;
        public RelayCommand _removeUserCommand;
        private List<Users> _userList;
        private Users _userProp;

        public RemoveUserViewModel()
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
                     select s).ToList();

            UsersList = q;
        }

        public List<Users> UsersList
        {
            get { return _userList; }
            set
            {
                _userList = value;
                RaisePropertyChanged("UsersProp");
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
            if (_userProp == null)
                return false;
            return true;
        }

        private void RemoveUserExecute()
        {
            //MessageBox.Show(_userProp.Id.ToString());
            try
            {
                
                var order = (from o in _ctx.Users
                             where o.Id == _userProp.Id
                             select o).First();
                _ctx.Users.Remove(order);
                _ctx.SaveChanges();
                Xceed.Wpf.Toolkit.MessageBox.Show("Konto użytkownika zostało usunięte!", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.MainWindow.Hide();
            }
            catch (Exception ex)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Wystąpił błąd! Konto użytkownika nie zostało usunięte.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
        }

    }
}
