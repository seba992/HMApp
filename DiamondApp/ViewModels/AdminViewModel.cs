using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using DiamondApp.DataGridObjectClasses;
using DiamondApp.EntityModel;
using DiamondApp.Tools;

namespace DiamondApp.ViewModels
{
    public class AdminViewModel : ObservableObject
    {
        private DiamondDBEntities _ctx;

        private List<AdminProposition> _propositionList;
        private List<User> _userListGrid;
        private AddNewProposition _addNewProposition;

        private PropClient _propositionClient = new PropClient();   // obiekt zawierajacy dane propozycji klienta (1 tab gora)
        private PropReservationDetails _propositionReservDetails = new PropReservationDetails();     // obiekt zawierajacy dane szczegolow zamownienia (1 tab dol)
        private List<string> _hallList; // lista sal (controlbox 1tab)
        private List<string> _hallSettingList; // lista sal (controlbox 1tab)
        
        private PropReservationDetails_Dictionary_HallCapacity _hallCapacity = new PropReservationDetails_Dictionary_HallCapacity();    // obiekt zawierajacy dane wybranej sali (1 tab dol)
        private string _eventMonth; // miesiac wydarzenia potrzebny do ustalenia celi sali
        private List<Users> _usersList; // lista uzytkownikow TODO: FOR WHAT
        private int _userId;    // id zalogowanego uzytkownika
        private int _selectedPropositionId; // id wybranej propozycji (do edycji)
        private int? _hallPrice;
        private PropHallEquipmentDiscount _propHallEquipmentDiscount = new PropHallEquipmentDiscount(); // tabela rabat w szczegory rezerwacji
        private List<PropHallEquipment> _propHallEquipment = new List<PropHallEquipment>(6);
        private List<decimal> _secondTabNettoPrice = new List<decimal>(6);
 
        public AdminViewModel(int userId)
        {
            _ctx = new DiamondDBEntities();
            _userId = userId;
            SelectAllPropositions();
            SelectAllUsers();
            CacheMethodWhichAllowRunsAdminWindowOnCreateNewPropositionTabControl();         // CACHE
            FillNeededList();
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
            get { return _usersList; }
            set
            {
                _usersList = value;
                RaisePropertyChanged("UsersList");
            }
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

        // command ktory nastepuje po naciśnięciu Nowa propozycja z menu
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

        // command ktory nastepuje po naciśnięciu zapisz propozycję w oknie tworzenia propozycji
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

        public ICommand ShowUsersCommand
        {
            get
            {
                if (_showUsersCommand == null)
                {
                    _showUsersCommand = new RelayCommand(ShowUsersExecute, CanShowUsersExecute);
                }
                return _showUsersCommand;
            }

        }

        // objekt zawierajacy wszystkie elementy wpisane przez uzytkownika /dotyczy tabeli PropClient/
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

                // zaktualizuj miesiąc wyswietlany w oknie tworzenia propozycji
                EventMonth = DateTimeConverter.ToMonthPolishName(value);
                RaisePropertyChanged("PropositionReservDetailsStartData");

                // wyciagnij z bazy i ustaw cene wybranej cali w danym miesiacu
                SetHallPrice();
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

        public DateTime? PropositionReservDetailsEndData
        {
            get { return _propositionReservDetails.EndData; }
            set
            {
                _propositionReservDetails.EndData = value;
                RaisePropertyChanged("PropositionReservDetailsEndData");
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

                // jeśli wybrana jest juz data poczatkowa wyswietl nazwe sali w tab2 poz1
                if (PropositionReservDetailsStartData.HasValue)
                    HallFullName0 = "Sala "+value;
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

        public List<string> HallSettingList
        {
            get { return _hallSettingList; }
            set
            {
                _hallSettingList = value;
                RaisePropertyChanged("HallSettingList");
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

        public PropHallEquipmentDiscount HallEquipmentDiscount
        {
            get { return _propHallEquipmentDiscount; }
            set
            {
                _propHallEquipmentDiscount = value;
                RaisePropertyChanged("HallEquipmentDiscount");
            }
        }

        public float? PropHallEquipmentDiscountValue
        {
            get { return _propHallEquipmentDiscount.Discount; }
            set
            {
                _propHallEquipmentDiscount.Discount = value;
                RaisePropertyChanged("PropHallEquipmentDiscountValue");

                SetDiscountPrice();
            }
        }

        public float? PropHallEquipmentDiscountStandPrice
        {
            get { return _propHallEquipmentDiscount.StandardPrice; }
            set
            {
                _propHallEquipmentDiscount.StandardPrice = value;
                RaisePropertyChanged("PropHallEquipmentDiscountStandPrice");

                SetDiscountPrice();
            }
        }

        public decimal ComputePriceAfterDiscount
        {
            get { return _computePriceAfterDiscount; }
            set
            {
                _computePriceAfterDiscount = value;
                RaisePropertyChanged("ComputePriceAfterDiscount");
            }
        }


        public string HallFullName0
        {
            get { return _propHallEquipment[0].Things; }
            set
            {
                _propHallEquipment[0].Things = value;
                RaisePropertyChanged("HallFullName1");
            }
        }
        public string PropHallEqThing1
        {
            get { return _propHallEquipment[1].Things; }
            set
            {
                _propHallEquipment[1].Things = value;
                RaisePropertyChanged("HallFullName1");
            }
        }
        public string PropHallEqThing2
        {
            get { return _propHallEquipment[2].Things; }
            set
            {
                _propHallEquipment[2].Things = value;
                RaisePropertyChanged("PropHallEqThing2");
            }
        }
        public string PropHallEqThing3
        {
            get { return _propHallEquipment[3].Things; }
            set
            {
                _propHallEquipment[3].Things = value;
                RaisePropertyChanged("PropHallEqThing3");
            }
        }
        public string PropHallEqThing4
        {
            get { return _propHallEquipment[4].Things; }
            set
            {
                _propHallEquipment[4].Things = value;
                RaisePropertyChanged("PropHallEqThing4");
            }
        }
        public string PropHallEqThing5
        {
            get { return _propHallEquipment[5].Things; }
            set
            {
                _propHallEquipment[5].Things = value;
                RaisePropertyChanged("PropHallEqThing5");
            }
        }

        public List<decimal> SecondTabNettoPrice0
        {
            get { return _secondTabNettoPrice[0]. }
            set { _secondTabNettoPrice = value; }
        }

        #endregion

#region Commands

        /* przy tworzeniu nowej propozycji utórz obiekt klasy AddNewProposition
         * Dodaj wymagane dane, które mają być domyślnie wprowadzone do arkusza propozycji
         * i zapisz w zmiennej _addNewProposition*/

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
                    UserEmail = user.Email,
                    IsCreated = true
                }).SingleOrDefault();
            AddNewProposition = querry;

            // wypełnienie listy umieszczonej jako SALA 1tab
            var hallDict1 = (from hd in _ctx.PropReservationDetails_Dictionary_HallCapacity
                select hd.Hall).ToList();
            HallList = hallDict1;

            var hallDict2 = (from hd in _ctx.PropReservationDetails_Dictionary_HallSettings
                select hd.Setting).ToList();
            HallSettingList = hallDict2;
        }

        /*zapisz propozycję - należy brać pod wzgląd czy jest to pierwszy zapis propozycji
         * (utworzenie nowego rekordu w bazie) czy tylko aktualizacja
         * INPROGRESS*/
        private ICommand _savePropositionCommand;
        private bool CanSavePropositionExecute(object arg)
        {
            return true;
        }

        private void SavePropositionExecute(object obj)
        {
            if (_addNewProposition.IsCreated)
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
                PropositionClient.Id_proposition = currentPropositionId;
//                var propClientToBase = new PropClient
//                {
//                    Id_proposition = currentPropositionId,
//                    CompanyName = PropositionClient.CompanyName,
//                    CompanyAdress =  PropositionClient.CompanyAdress,
//                    NIP = PropositionClient.NIP,
//                    CustomerFullName = PropositionClient.CustomerFullName,
//                    PhoneNum = PropositionClient.PhoneNum
//                };
                _ctx.PropClient.Add(PropositionClient);


                //------------------------------
                // !! PROPRESERVATIONDETAILS !!
                PropositionReservDetails.Id_proposition = currentPropositionId;

                _ctx.PropReservationDetails.Add(PropositionReservDetails);
                _ctx.SaveChanges();
                MessageBox.Show("dodano nowa propozycje");

                // po dodaniu propozycji odśwież listę propozycji
                SelectAllPropositions();
            }
            else
            {
//                var prop = from p in _ctx.Proposition
//                           where
                MessageBox.Show("edytowano istniejaca propozycje");
            }
        }

        /* lista propozycji - command odświeżający listę propozycji w czasie klikniecia w pozycje menu /lista prop../ */
        private ICommand _showPropositionsCommand;
        private bool CanShowPropositionExecute(object arg)
        {
            return true;
        }

        //wywołuje metodę uzupełniającą odpowiednio elementy listy propozycji
        private void ShowPropositionExecute(object obj)
        {
            SelectAllPropositions();
        }

        //lista userow
        private ICommand _showUsersCommand;
        private decimal _computePriceAfterDiscount;

        private bool CanShowUsersExecute(object arg)
        {
            return true;
        }

        private void ShowUsersExecute(object obj)
        {
            SelectAllUsers();
        }



#endregion

#region Methods

        private void SelectUsers()
        {
            var q = (from s in _ctx.Users
                     where s.Proposition.Any()
                     select s).ToList();
            UsersList = q;
        }

        private void SelectAllUsers()
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
                                UserAccountType = user.AccountType,
                                UserLogin = user.Login

                            }).ToList();

            _userListGrid = myQuerry;
        }

        private void SelectUserById()
        {

        }


        private void CacheMethodWhichAllowRunsAdminWindowOnCreateNewPropositionTabControl()
        {
            DateTime today = DateTime.Today;
            var querry = (from user in _ctx.Users
                          where user.Id == _userId
                          select new AddNewProposition
                          {
                              UpdateDate = today,
                              UserFirstName = user.Name,
                              UserSurname = user.Surname,
                              UserPhoneNum = user.PhoneNum,
                              UserEmail = user.Email,
                              IsCreated = true
                          }).SingleOrDefault();
            AddNewProposition = querry;
        }

        /* Wyszukiwanie wszystkich danych potrzebnych do wypełnienia grida admina
         * - wszystkie propozycje wszystkich użytkowników posortowane według daty
         * (imie+nazwisko użytkownika , imie+nazwisko klienta, nazwa firmy klienta, data aktualizacji*/
        private void SelectAllPropositions()
        {
            var myQuerry = (from prop in _ctx.Proposition
                from user in _ctx.Users
                where prop.Id_user == user.Id
                orderby prop.UpdateDate, user.Name
                select new AdminProposition
                {
                    PropositionId = prop.Id,   // myślę że id propozycji się przyda CACHE
                    UserFirstName = user.Name,
                    UserSurname = user.Surname,
                    CustomerFullName = prop.PropClient.CustomerFullName,
                    CompanyName = prop.PropClient.CompanyName,
                    UpdateDate = prop.UpdateDate,
                    Status = prop.Status
                }).ToList();

            PropositionsList = myQuerry;
        }

        // Metoda ustawiająca cenę sali gdy jest wypełniona nazwa sali i miesiąc wydarzenia
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

                HallPrice = price;
                PropHallEquipmentDiscountStandPrice = price;
            }
        }

        private void SetDiscountPrice()
        {
            if (PropHallEquipmentDiscountValue.HasValue && PropHallEquipmentDiscountStandPrice.HasValue)
            {
                ComputePriceAfterDiscount = Math.Ceiling((decimal) PropHallEquipmentDiscountStandPrice -
                                                         ((decimal) PropHallEquipmentDiscountStandPrice*
                                                          (decimal) PropHallEquipmentDiscountValue/100));
            }
            else if (!PropHallEquipmentDiscountValue.HasValue)
            {
                PropHallEquipmentDiscountValue = 0;
            }
        }

        private void FillNeededList()
        {
            //PropHallEquipmentList
            for (int i = 0; i < _propHallEquipment.Capacity; i++)
                _propHallEquipment.Add(new PropHallEquipment());

            //SecondTabNettoPriceList
            for (int i = 0; i < _secondTabNettoPrice.Capacity; i++)
                _secondTabNettoPrice.Add(new decimal());
        }
#endregion
    }
}
