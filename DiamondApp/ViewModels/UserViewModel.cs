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
        private List<string> _hallList;
        private ICommand _changePropositionCommand;
        private bool _saveFlag = false;
        private int _idProposition;
        private int? _hallPrice;
        private string _eventMonth;
        private PropReservationDetails_Dictionary_HallCapacity _hallCapacity = new PropReservationDetails_Dictionary_HallCapacity();


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

        public int? HallPrice
        {
            get { return _hallPrice; }
            set
            {
                _hallPrice = value;
                RaisePropertyChanged("HallPrice");
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

        public TimeSpan? PropositionReservDetailsEndTime
        {
            get { return _propositionReservDetails.EndTime; }
            set
            {
                _propositionReservDetails.EndTime = value;
                RaisePropertyChanged("PropositionReservDetailsEndTime");
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

                var querry = (from s in _ctx.PropReservationDetails_Dictionary_HallCapacity
                              where s.Hall == value
                              select s).SingleOrDefault();
                HallCapacity = querry;

                RaisePropertyChanged("PropositionReservDetailsHall");

                // wyciagnij z bazy i ustaw cene wybranej cali w danym miesiacu
                SetHallPrice();
            }
        }

        public string PropositionReservDetailsHallSetting
        {
            get { return _propositionReservDetails.HallSetting; }
            set
            {
                _propositionReservDetails.HallSetting = value;
                RaisePropertyChanged("PropositionReservDetailsHallSetting");

                SetHallPrice();
            }
        }


        public List<string> HallList
        {
            get { return _hallList; }
            set
            {
                _hallList = value;
                RaisePropertyChanged("HallList");
            }
        }

        public PropReservationDetails_Dictionary_HallCapacity HallCapacity
        {
            get { return _hallCapacity; }
            set
            {
                _hallCapacity = value;
                RaisePropertyChanged("HallCapacity");
            }
        }

        public string EventMonth
        {
            get { return _eventMonth; }
            set
            {
                _eventMonth = value;
                RaisePropertyChanged("EventMonth");
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
                _addNewProposition.IsCreated = false;
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
                // int currentPropositionId = SelectedProposition.PropositionId;

                //Edycja Propozycji
                // !! PROPCLIENT !!

                int idProposition = _idProposition;
                var editClient = (from q in _ctx.PropClient
                        where q.Id_proposition == idProposition
                        select q).SingleOrDefault();
                if (editClient != null)
                {
                    editClient.Id_proposition = idProposition;
                    editClient.CompanyName = PropositionClient.CompanyName;
                    editClient.CompanyAdress = PropositionClient.CompanyAdress;
                    editClient.NIP = PropositionClient.NIP;
                    editClient.CustomerFullName = PropositionClient.CustomerFullName;
                    editClient.PhoneNum = PropositionClient.PhoneNum;
                    _ctx.SaveChanges();
                }
                else
                {
                    PropClient addNewClient = new PropClient();

                    addNewClient.Id_proposition = idProposition;
                    addNewClient.CompanyName = PropositionClient.CompanyName;
                    addNewClient.CompanyAdress = PropositionClient.CompanyAdress;
                    addNewClient.NIP = PropositionClient.NIP;
                    addNewClient.CustomerFullName = PropositionClient.CustomerFullName;
                    addNewClient.PhoneNum = PropositionClient.PhoneNum;
                    _ctx.PropClient.Add(addNewClient);
                    _ctx.SaveChanges();

                }
                SelectAllPropositions();
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
            _saveFlag = false;
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
          
            try
            {
                _idProposition = SelectedProposition.PropositionId;
            
                var test = (from q in _ctx.PropClient
                    where q.Id_proposition == _idProposition
                    select q).SingleOrDefault();

              //  if (test != null || idProposition != 0)
                {
                    PropositionClientCompanyName = test.CompanyName;
                    PropositionClientCompanyAdress = test.CompanyAdress;
                    PropositionClientCustromerFullName = test.CustomerFullName;
                    PropositionClientPhoneNum = test.PhoneNum;
                    PropositionClientNip = test.NIP;
                    PropositionClientDecisingPerFullName = test.DecisingPersonFullName;
                    PropositionClientCustomerEmail = test.CustomerEmail;
                    PropositionClient = test;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("no i poszło na grzybki" );
                _saveFlag = false;
            }
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
                              UserEmail = user.Email,
                              IsCreated = true
                          }).SingleOrDefault();
           
           _addNewProposition = querry;
        }

        private void SetHallPrice()
        {
            int? price = null;
            if (!string.IsNullOrEmpty(PropositionReservDetailsHall) && PropositionReservDetailsStartData != null)
            {
                // zapisanie angielskiej nazwy miesiaca (jest ona rowna kolumnie w tabeli PropReservationDetails_Dictionary_HallPrices
                string columnName = DateTimeConverter.ToMonthEnglishName(PropositionReservDetailsStartData);

                // zapytanie zwracajace wiersz zawierajacy wybrana przez uzytkownika sale
                var q = (from s in _ctx.PropReservationDetails_Dictionary_HallPrices
                         where s.Hall == PropositionReservDetailsHall
                         select s).ToList();

                // za pomoca refleksji wybieramy interesujaca nas kolumne
                // domyslnie w entity nie mozna wybierac dynamicznej nazwy kolumny 
                // (w naszym przypadku zależna jest ona od wybranego miesiąca)

                var names = q.Select(x => x.GetType().GetProperty(columnName).GetValue(x).ToString());

                // przypisanie odpowiadającej ceny w danym miesiącu danej sali
                price = Convert.ToInt32(names.First());
            }
            HallPrice = price;
        }

        #endregion
    }
}
