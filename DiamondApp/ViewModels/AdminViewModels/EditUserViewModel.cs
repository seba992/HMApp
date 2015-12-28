using System.Collections.Generic;
using System.Linq;
using DiamondApp.DataGridFillingObjects;
using DiamondApp.Model;
using DiamondApp.Tools.MvvmClasses;

namespace DiamondApp.ViewModels.AdminViewModels
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
                            orderby user.Surname
                            select new User
                            {
                                UserId = user.Id,
                                UserName = user.Name,
                                UserSurname = user.Surname,
                                UserPhoneNumber = user.PhoneNum,
                                UserEmail = user.Email,
                                UserPosition = user.Position,
                                UserAccountType = user.AccountType == 1 ? "Administrator" : "Sprzedawca",
                                UserLogin = user.Login,

                            }).ToList();

            _userListGrid = myQuerry;
        }
    }
}
