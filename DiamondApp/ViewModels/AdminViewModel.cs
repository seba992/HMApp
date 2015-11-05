using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using DiamondApp.DataGridObjectClasses;
using DiamondApp.EntityModel;
using DiamondApp.Tools;
using Microsoft.Practices.ServiceLocation;

namespace DiamondApp.ViewModels
{
    public class AdminViewModel : ObservableObject
    {
        private DiamondDBEntities _ctx;
        public List<AdminPropositionList> propositionList;
        public List<Users> usersList;
        private int _userId;
        private string _tag;



        public AdminViewModel()
        {
            _ctx = new DiamondDBEntities();
            SelectPropositions();

        }

        public AdminViewModel(int userId)
        {
            _ctx = new DiamondDBEntities();
            _userId = userId;
            SelectPropositions();
        }

#region Properties

        public List<AdminPropositionList> PropositionsList
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
                SelectUsers();
                RaisePropertyChanged("UsersList");
               
            }

        }

        public ICommand CreateNewPropositionCommand
        {
            get
            {
                if (_createNewPropositionCommand == null)
                {
                    _createNewPropositionCommand = new RelayCommand(CreateNewPropositionExecute, CanCreateNewPropositionExecute);
                }
                return _createNewPropositionCommand;
            }
        }


        #endregion

#region Commands

        private ICommand _createNewPropositionCommand;
        private bool CanCreateNewPropositionExecute(object arg)
        {
           // throw new System.NotImplementedException();
            return true;
        }

        private void CreateNewPropositionExecute(object obj)
        {
            //throw new System.NotImplementedException();
        }

#endregion


#region Methods

        private void SelectUsers()
        {
            var q = (from s in _ctx.Users
                     select s).ToList();
            usersList = q;
        }

        private void SelectPropositions()
        {
            var myQuerry = (from prop in _ctx.Proposition
                from user in _ctx.Users
                where prop.Id == user.Id
                orderby prop.UpdateDate, user.Name
                select new AdminPropositionList
                {
                    UserFirstName = user.Name,
                    UserSurname = user.Surname,
                    CustomerFirstName = prop.PropClient.CustomerName,
                    CustomerSurname = prop.PropClient.CustomerSurname,
                    CompanyName = prop.PropClient.CompanyName,
                    UpdateDate = prop.UpdateDate,
                    Status = prop.Status
                }).ToList();
            propositionList = myQuerry;
        }
#endregion
        
    }
}
