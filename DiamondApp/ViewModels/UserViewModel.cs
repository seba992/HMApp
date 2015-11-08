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
        private List<AdminProposition> _propositionList;
        private ICommand _createNewPropositionCommand;
        private AddNewProposition _addNewProposition;
        private ICommand _savePropositionCommand;
        private Proposition _proposition;
        //private int _currentPropositionId;
        private ICommand _showPropositionsCommand;
        private AdminProposition _selectedProposition;
        private PropClient _propositionClient = new PropClient();
        private PropReservationDetails _propositionReservDetails = new PropReservationDetails();
        private List<PropHallEquipmnet_Dictionary_First> _hallList;
        private ICommand _changePropositionCommand;
        private bool _saveFlag = false;


        public UserViewModel()
        {
            _ctx = new DiamondDBEntities();
            SelectAllPropositions();
        }

        public UserViewModel(int userId)
            : base()
        {
            _userId = userId;
            _ctx = new DiamondDBEntities();
            SelectAllPropositions();
            PropDefaultSeller();
            ListHall();
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

        public ICommand ChangePropositionCommand
        {
            get
            {
                if (_changePropositionCommand == null)
                {
                    _changePropositionCommand = new RelayCommand(ChangePropositionExecute, CanChangePropositionExecute);
                }
                return _changePropositionCommand;
            }
        }

        public AdminProposition SelectedProposition
        {
            get { return _selectedProposition; }
            set
            {
                _selectedProposition = value;
                RaisePropertyChanged("SelectedProposition");
            }
        }

        public PropClient PropositionClient
        {
            get { return _propositionClient; }
            set
            {
                _propositionClient = value;
                RaisePropertyChanged("PropositionClient");
            }
        }

        public string PropositionClientCompanyName
        {
            get { return _propositionClient.CompanyName; }
            set
            {
                _propositionClient.CompanyName = value;
                RaisePropertyChanged("PropositionClientCompanyName");
            }
        }

        public string PropositionClientCompanyAdress
        {
            get { return _propositionClient.CompanyAdress; }
            set
            {
                _propositionClient.CompanyAdress = value;
                RaisePropertyChanged("PropositionClientCompanyAdress");
            }
        }

        public string PropositionClientNip
        {
            get { return _propositionClient.NIP; }
            set
            {
                _propositionClient.NIP = value;
                RaisePropertyChanged("PropositionClientNip");
            }
        }

        public string PropositionClientCustromerFullName
        {
            get { return _propositionClient.CustomerFullName; }
            set
            {
                _propositionClient.CustomerFullName = value;
                PropositionClientDecisingPerFullName = value;
                RaisePropertyChanged("PropositionClientCustromerFullName");
            }
        }

        public string PropositionClientPhoneNum
        {
            get { return _propositionClient.PhoneNum; }
            set
            {
                _propositionClient.PhoneNum = value;
                RaisePropertyChanged("PropositionClientPhoneNum");
            }
        }

        public string PropositionClientDecisingPerFullName
        {
            get { return _propositionClient.DecisingPersonFullName; }
            set
            {
                _propositionClient.DecisingPersonFullName = value;
                RaisePropertyChanged("PropositionClientDecisingPerFullName");
            }
        }

        public string PropositionClientCustomerEmail
        {
            get { return _propositionClient.CustomerEmail; }
            set
            {
                _propositionClient.CustomerEmail = value;
                RaisePropertyChanged("PropositionClientCustomerEmail");
            }
        }

        public PropReservationDetails PropositionReservDetails
        {
            get { return _propositionReservDetails; }
            set
            {
                _propositionReservDetails = value;
                RaisePropertyChanged("PropositionReservDetails");
            }
        }

        public DateTime? PropositionReservDetailsStartData
        {
            get { return _propositionReservDetails.StartData; }
            set
            {
                _propositionReservDetails.StartData = value;
                RaisePropertyChanged("PropositionReservDetailsStartData");
            }
        }

        public TimeSpan? PropositionReservDetailsStartTime
        {
            get { return _propositionReservDetails.StartTime; }
            set
            {
                _propositionReservDetails.StartTime = value;
                RaisePropertyChanged("PropositionReservDetailsStartTime");
            }
        }

        public DateTime? PropositionReservDetailsEndData
        {
            get { return _propositionReservDetails.EndData; }
            set
            {
                _propositionReservDetails.EndData = value;
                RaisePropertyChanged("PropositionReservDetailsEndData");
            }
        }

        public int? PropositionReservDetailsPeopleNumber
        {
            get { return _propositionReservDetails.PeopleNumber; }
            set
            {
                _propositionReservDetails.PeopleNumber = value;
                RaisePropertyChanged("PropositionReservDetailsPeopleNumber");
            }
        }

        public string PropositionReservDetailsHall
        {
            get { return _propositionReservDetails.Hall; }
            set
            {
                _propositionReservDetails.Hall = value;
                RaisePropertyChanged("PropositionReservDetailsHall");
            }
        }

        public string PropositionReservDetailsHallSetting
        {
            get { return _propositionReservDetails.HallSetting; }
            set
            {
                _propositionReservDetails.HallSetting = value;
                RaisePropertyChanged("PropositionReservDetailsHallSetting");
            }
        }

        public List<PropHallEquipmnet_Dictionary_First> HallList
        {
            get { return _hallList; }
            set
            {
                _hallList = value;
                RaisePropertyChanged("HallList");
            }
        }

#endregion

#region Method
        private void SelectAllPropositions()
        {
            var myQuerry = (from prop in _ctx.Proposition
                            from user in _ctx.Users
                            where user.Id == _userId
                            where prop.Id_user == UserId
                            select new AdminProposition
                            {
                                PropositionId = prop.Id,
                                UserFirstName = user.Name,
                                UserSurname = user.Surname,
                                CustomerFullName = prop.PropClient.CustomerFullName,
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
            
            
            if (_addNewProposition.IsCreated && !_saveFlag)
            {
                // tworzy obiekt z aktualnymi danymi tabeli Proposition i dodaje go do bazy

                // !! PROPOSITION !! 
                var propositionToBase = new Proposition
                {
                    Id_user = _userId,
                    UpdateDate = _addNewProposition.UpdateDate,
                    Status = "New"  //TODO uaktualnić ewentualnie z enuma lub obgadać jak rozwiązać
                };
                _ctx.Proposition.Add(propositionToBase);
                _addNewProposition.IsCreated = true;
                _ctx.SaveChanges();

                /* wyciągnięcie Id dodanej propozycji który potrzebny bedzie przy dodawaniu
                 * do bazy pozostałych tabel (klucz obcy) */

                var lastPropId = (from prop in _ctx.Proposition
                                  select prop).ToList().Last();
                int currentPropositionId = lastPropId.Id;

                //------------------------------
                // !! PROPCLIENT !!
                var propClientToBase = new PropClient
                {
                    Id_proposition = currentPropositionId,
                    CompanyName = PropositionClient.CompanyName,
                    CompanyAdress = PropositionClient.CompanyAdress,
                    NIP = PropositionClient.NIP,
                    CustomerFullName = PropositionClient.CustomerFullName,
                    PhoneNum = PropositionClient.PhoneNum
                };
                _ctx.PropClient.Add(propClientToBase);
                _ctx.SaveChanges();

                //------------------------------
                // !! PROPCLIENT !!


                MessageBox.Show("dodano nowa propozycje");

                // po dodaniu propozycji odśwież listę propozycji
                SelectAllPropositions();
            }
            else
            {
                // Wybrany Id Propozycji
                int currentPropositionId = SelectedProposition.PropositionId;

                //Edycja Propozucji
                // !! PROPCLIENT !!

                int idProposition = SelectedProposition.PropositionId;
                var test = (from q in _ctx.PropClient
                where q.Id_proposition == idProposition
                select q).SingleOrDefault();

                test.Id_proposition = currentPropositionId;
                test.CompanyName = PropositionClient.CompanyName;
                test.CompanyAdress = PropositionClient.CompanyAdress;
                test.NIP = PropositionClient.NIP;
                test.CustomerFullName = PropositionClient.CustomerFullName;
                test.PhoneNum = PropositionClient.PhoneNum;
                
                _ctx.SaveChanges();
                
                //SelectAllPropositions();
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
       
        private bool CanCreateNewPropositionExecute(object arg)
        {
            return true;
        }
        private void CreateNewPropositionExecute(object obj)
        {
            _saveFlag = true;
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

        private bool CanChangePropositionExecute(object arg)
        {
            return true;
        }
        private void ChangePropositionExecute(object obj)
        {
            _saveFlag = true;
            int idProposition = SelectedProposition.PropositionId;
            var test = (from q in _ctx.PropClient
                where q.Id_proposition == idProposition
                select q).SingleOrDefault();
            _propositionClient = test;
            MessageBox.Show(test.Id_proposition.ToString());
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

        private void ListHall()
        {
            var q = (from x in _ctx.PropHallEquipmnet_Dictionary_First
                select x).ToList();
            HallList = q;

        }

        #endregion
    }
}
