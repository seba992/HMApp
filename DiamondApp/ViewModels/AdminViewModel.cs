using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private List<AdminProposition> _propositionList;
        private AddNewProposition _addNewProposition; 
        public List<Users> usersList;
        private int _userId;
        private string _testString;



        public AdminViewModel()
        {
            _ctx = new DiamondDBEntities();
            SelectAllPropositions();

        }

        public AdminViewModel(int userId)
        {
            _ctx = new DiamondDBEntities();
            _userId = userId;
            SelectAllPropositions();
            CacheMethodWhichAllowRunsAdminWindowOnCreateNewPropositionTabControl();         // CACHE
        }

#region Properties

        public List<AdminProposition> PropositionsList
        {
            get
            {
                return _propositionList;
            }
            set
            {
                _propositionList = value;
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

        public AddNewProposition AddNewProposition
        {
            get { return _addNewProposition; }
            set
            {
                _addNewProposition = value;
                RaisePropertyChanged("AddNewProposition");
            }
        }

        public string TestString
        {
            get { return _testString; }
            set { _testString = value; }
        }

        #endregion

#region Commands

        private ICommand _createNewPropositionCommand;
        private bool CanCreateNewPropositionExecute(object arg)
        {
            return true;
        }
        private void CreateNewPropositionExecute(object obj)
        {
            var querry = (from user in _ctx.Users
                where user.Id == _userId
                select new AddNewProposition
                {
                    UpdateDate = DateTime.Today,
                    UserFirstName = user.Name,
                    UserSurname = user.Surname,
                    UserPhoneNum = user.PhoneNum,
                    UserEmail = user.Email
                }).SingleOrDefault();
            _addNewProposition = querry;
        }



#endregion


#region Methods

        private void SelectUsers()
        {
            var q = (from s in _ctx.Users
                     select s).ToList();
            usersList = q;
        }

        private void CacheMethodWhichAllowRunsAdminWindowOnCreateNewPropositionTabControl()
        {
            var querry = (from user in _ctx.Users
                where user.Id == _userId
                select new AddNewProposition
                {
                    UpdateDate = DateTime.Today,
                    UserFirstName = user.Name,
                    UserSurname = user.Surname,
                    UserPhoneNum = user.PhoneNum,
                    UserEmail = user.Email
                }).SingleOrDefault();
            _addNewProposition = querry;
            TestString = _addNewProposition.OurNip;
        }
        private void SelectAllPropositions()
        {
            var myQuerry = (from prop in _ctx.Proposition
                from user in _ctx.Users
                where prop.Id == user.Id
                orderby prop.UpdateDate, user.Name
                select new AdminProposition
                {
                    UserFirstName = user.Name,
                    UserSurname = user.Surname,
                    CustomerFirstName = prop.PropClient.CustomerName,
                    CustomerSurname = prop.PropClient.CustomerSurname,
                    CompanyName = prop.PropClient.CompanyName,
                    UpdateDate = prop.UpdateDate,
                    Status = prop.Status
                }).ToList();
            _propositionList = myQuerry;
        }
#endregion
        
    }
}
