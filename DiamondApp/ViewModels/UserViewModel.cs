using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using DiamondApp.EntityModel;
using DiamondApp.Tools;
using Microsoft.Practices.ServiceLocation;
using DiamondApp.DataGridObjectClasses;

namespace DiamondApp.ViewModels
{
    public class UserViewModel : ObservableObject
    {
        private DiamondDBEntities _ctx;
        private int _userId;
        // public List<Proposition> propositionList;
        private int _selectedPropositionId;
        private List<AdminProposition> _propositionList;
        private ICommand _createNewPropositionCommand;
        private AddNewProposition _addNewProposition;
        private ICommand _savePropositionCommand;
        private Proposition _proposition;
        //private int _currentPropositionId;
        private ICommand _showPropositionsCommand;
     


        public UserViewModel()
        {
            _ctx = new DiamondDBEntities();
            SelectAllPropositions();
        }
    
        public UserViewModel(int userId) :base()
        {
            _userId = userId;
            _ctx = new DiamondDBEntities();
            SelectAllPropositions();
            PropDefaultSeller();
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
        public int UserId
         {
              get { return _userId; }
              set { _userId = value; }
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

         public ICommand SavePropositionCommand
         {
             get
             {
                 if (_savePropositionCommand == null)
                 {
                     _savePropositionCommand = new RelayCommand(SavePropositionExecute, CanSavePropositionExecute);
                 }
                 return _savePropositionCommand;
             }
         }

         public Proposition Proposition
         {
             get { return _proposition; }
             set
             {
                 _proposition = value;
                 RaisePropertyChanged("AddNewProposition");
             }
         }

         public ICommand ShowPropositionsCommand
         {
             get
             {
                 if (_showPropositionsCommand == null)
                 {
                     _showPropositionsCommand = new RelayCommand(ShowPropositionExecute, CanShowPropositionExecute);
                 }
                 return _showPropositionsCommand;
             }

         }
#endregion 
    
#region Method
        private void SelectAllPropositions()
        {
            var myQuerry = (from prop in _ctx.Proposition 
                            from user in _ctx.Users
                            where user.Id ==_userId
                           where prop.Id_user == UserId
                            select new AdminProposition
                            {
                                PropositionId = prop.Id,   
                                UserFirstName = user.Name,
                                UserSurname = user.Surname,
                                CustomerFirstName = prop.PropClient.CustomerName,
                                CustomerSurname = prop.PropClient.CustomerSurname,
                                CompanyName = prop.PropClient.CompanyName,
                                UpdateDate = prop.UpdateDate,
                                Status = prop.Status
                            }).ToList();
            PropositionsList = myQuerry;
         }

        private bool CanSavePropositionExecute(object arg)
        {
            return true;
        }

        private void SavePropositionExecute(object obj)
        {

            if (_addNewProposition.IsCreated)
            {
                Proposition = new Proposition
                {
                    Id_user = _userId,
                    UpdateDate = _addNewProposition.UpdateDate,
                    Status = "New"
                };
                _ctx.Proposition.Add(Proposition);
                _addNewProposition.IsCreated = false;
                _ctx.SaveChanges();

                /*zapamietanie id propozycji*/
                var lastPropId = (from q in _ctx.Proposition
                    where q.Id_user == _userId
                    select q).ToList().Last();
                int  _currentPropositionId = lastPropId.Id;
                SelectAllPropositions();
            }
            else
            {
//                var prop = from p in _ctx.Proposition
//                           where
                MessageBox.Show("edytowano istniejaca propozycje");
            }
        }

        private bool CanShowPropositionExecute(object arg)
        {
            return true;
        }

        private void ShowPropositionExecute(object obj)
        {
            SelectAllPropositions();
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
                              UserEmail = user.Email,
                              IsCreated = true
                          }).SingleOrDefault();
            AddNewProposition = querry;
        }
        private bool CanCreateNewPropositionExecute(object arg)
        {
            return true;
        }

        private void PropDefaultSeller()
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
    }
}
