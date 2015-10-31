using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DiamondApp.EntityModel;
using DiamondApp.Tools;
using Microsoft.Practices.ServiceLocation;

namespace DiamondApp.ViewModels
{
    class AdminViewModel : ObservableObject
    {
        private DiamondDBEntities _ctx;
        public List<Proposition> propositionList;
        public List<Users> usersList;
        private int _userId;
        private string _tag;

        public AdminViewModel()
        {
            _ctx = new DiamondDBEntities();
            MyMethod();

        }

        public AdminViewModel(int userId)
        {
            _ctx = new DiamondDBEntities();
            _userId = userId;
            MyMethod();
        }

#region Properties

        public List<Proposition> PropositionsList
        {
            get
            {
                return propositionList;
            }
            set
            {
                propositionList = value;
                RaisePropertyChanged("PropositionsList");
            }
        }


        public List<Users> UsersList
        {
            get { return usersList; }
            set
            {
                usersList = value ;
                UserList();
                RaisePropertyChanged("UsersList");
               
            }

        }

        private void UserList()
        {
            var q = (from s in _ctx.Users
                select s).ToList();
            usersList = q;
        }

        #endregion

        private void FillPropositions()
        {
            // wybierz wszystkie propozycje obecnie zalogowanego uzytkownika

            //var q = (from a in _ctx.Proposition
            //         where a.Id_user == a.Users.Id
            //         where a.Id_user == _userId
            //         select a).ToList();


            // wybierz wszystkie propozycje userow

            var q = (from a in _ctx.Proposition
                     select a).ToList();

            propositionList = q;

            MessageBox.Show(_userId.ToString());

        }

        private void MyMethod()
        {
            var myQuerry = (from s in _ctx.Proposition
                            select s).ToList();
            propositionList = myQuerry;
        }
    }
}
