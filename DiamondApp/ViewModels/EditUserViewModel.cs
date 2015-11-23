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
using DiamondApp.DataGridObjectClasses;

namespace DiamondApp.ViewModels
{
    public class EditUserViewModel : ObservableObject
    {
        private DiamondDBEntities _ctx;
        private List<User> _userListGrid;

        public EditUserViewModel()
        {
            _ctx = new DiamondDBEntities();
            SelectAllUsers();
        }

        public List<User> UsersListGrid
        {
            get { return _userListGrid; }
            set
            {
                SelectAllUsers();
                _userListGrid = value;
                RaisePropertyChanged("UsersListGrid");
            }
        }

        public void SelectAllUsers()
        {
            var myQuerry = (from user in _ctx.Users
                            select new User
                            {
                                UserId = user.Id,
                                UserName = user.Name,
                                UserSurname = user.Surname,
                                UserPhoneNumber = user.PhoneNum,
                                UserEmail = user.Email,
                                UserPosition = user.Position,
                                UserAccountType = user.AccountType == 1 ? "Administrator" : "Użytkownik",
                                UserLogin = user.Login,

                            }).ToList();

            _userListGrid = myQuerry;
        }
    }
}
