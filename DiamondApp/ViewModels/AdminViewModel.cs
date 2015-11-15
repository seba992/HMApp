using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using DiamondApp.DataGridObjectClasses;
using DiamondApp.EntityModel;
using DiamondApp.Tools;
using DiamondApp.Views;

namespace DiamondApp.ViewModels
{
    public class AdminViewModel : ObservableObject
    {
        private DiamondDBEntities _ctx;
        
        // do edycji propozycji
        private bool _saveFlag = false;  // czy edycja czy dodanie nowej jeśli nie zostały żadna wybrana
        private AdminProposition _selectedProposition; // wybrana poropozycja
        private int _idProposition;  //id propozycji

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
        private List<decimal> _secondTabNettoPrice = new List<decimal>(6);  // lista cen netto (tab2)
        private List<decimal> _secondTabNettoValue = new List<decimal>(6);   // list zsumowanych cen netto (tab2)
        private List<decimal> _secondTabBruttoValue = new List<decimal>(6);   // list zsumowanych cen netto (tab2)
        private decimal _computePriceAfterDiscount;
        private List<string> _propHallEqDict2; // lista zawierajaca wyposazenie dodatkowe sali (combobox tab 2)
        private List<float?> _vatList; // lista zawierajaca wartosci VAT


        private decimal _secondTabSumNettoValue;    // suma wartosci netto (tab2)
        private decimal _secondTabSumBruttoValue;   // suma wartosci brutto (tab2)
        //3tab
        private List<string> _propMenuGastThingDict;
        private List<PropMenuPosition> _propMenuPositions = new List<PropMenuPosition>(7);  // obiekt przechowujacy elementy uslug gastronomicznych
        private List<decimal?> _thirdTabNettoPrice = new List<decimal?>(7);  // lista cen netto (tab3)
        private List<PropMenuMerge> _propMenuMerges = new List<PropMenuMerge>(5);
        private List<string> _defaultMerges = new List<string>(5);  // lista domyslnych marzy
        private List<decimal> _thirdTabNettoValue = new List<decimal>(7);   // list zsumowanych cen netto (tab2)
        private List<decimal> _thirdTabBruttoValue = new List<decimal>(7);   // list zsumowanych cen netto (tab2)

        private decimal _thirdTabSumNettoValue;    // suma wartosci netto (tab3)
        private decimal _thirdTabSumBruttoValue;   // suma wartosci brutto (tab3)

        public AdminViewModel(int userId)
        {
            _ctx = new DiamondDBEntities();
            _userId = userId;
            SelectAllPropositions();
            SelectAllUsers();
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

        // wybrana propozycja do edycji
        public AdminProposition SelectedProposition
        {
            get { return _selectedProposition; }
            set
            {
                _selectedProposition = value;
                RaisePropertyChanged("SelectedProposition");
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

                // jeśli wybrana jest juz nazwa sali to ustaw 
                if (PropositionReservDetailsHall!=null)
                    PropHallEqThing0 = "Sala " + PropositionReservDetailsHall;
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
                    PropHallEqThing0 = "Sala "+value;
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

        public string PropHallEqThing0
        {
            get { return _propHallEquipment[0].Things; }
            set
            {
                _propHallEquipment[0].Things = value;
                RaisePropertyChanged("PropHallEqThing0");
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

        public decimal SecondTabNettoPrice0
        {
            get { return _secondTabNettoPrice[0]; }
            set
            {
                _secondTabNettoPrice[0] = value;
                RaisePropertyChanged("SecondTabNettoPrice0");
                // w chwili aktualizacji ceny brutto aktualizuj wartosci brutto/netto
                if (PropHallEqDays0 != null && PropHallEqAmount0 != null)
                {
                    SecondTabBruttoValue0 = ComputeBruttoValue(PropHallEqBrutto0, PropHallEqAmount0, PropHallEqDays0);
                    SecondTabNettoValue0 = ComputeNettoValue(SecondTabNettoPrice0, PropHallEqAmount0, PropHallEqDays0);
                }
            }
        }
        public decimal SecondTabNettoPrice1
        {
            get { return _secondTabNettoPrice[1]; }
            set
            {
                _secondTabNettoPrice[1] = value;
                RaisePropertyChanged("SecondTabNettoPrice1");
                // w chwili aktualizacji ceny brutto aktualizuj wartosci brutto/netto
                if (PropHallEqDays1 != null && PropHallEqAmount1 != null)
                {
                    SecondTabBruttoValue1 = ComputeBruttoValue(PropHallEqBrutto1, PropHallEqAmount1, PropHallEqDays1);
                    SecondTabNettoValue1 = ComputeNettoValue(SecondTabNettoPrice1, PropHallEqAmount1, PropHallEqDays1);
                }
            }
        }
        public decimal SecondTabNettoPrice2
        {
            get { return _secondTabNettoPrice[2]; }
            set
            {
                _secondTabNettoPrice[2] = value;
                RaisePropertyChanged("SecondTabNettoPrice2");
                // w chwili aktualizacji ceny brutto aktualizuj wartosci brutto/netto
                if (PropHallEqDays2 != null && PropHallEqAmount2 != null)
                {
                    SecondTabBruttoValue2 = ComputeBruttoValue(PropHallEqBrutto2, PropHallEqAmount2, PropHallEqDays2);
                    SecondTabNettoValue2 = ComputeNettoValue(SecondTabNettoPrice2, PropHallEqAmount2, PropHallEqDays2);
                }
            }
        }
        public decimal SecondTabNettoPrice3
        {
            get { return _secondTabNettoPrice[3]; }
            set
            {
                _secondTabNettoPrice[3] = value;
                RaisePropertyChanged("SecondTabNettoPrice3");
                // w chwili aktualizacji ceny brutto aktualizuj wartosci brutto/netto
                if (PropHallEqDays3 != null && PropHallEqAmount3 != null)
                {
                    SecondTabBruttoValue3 = ComputeBruttoValue(PropHallEqBrutto3, PropHallEqAmount3, PropHallEqDays3);
                    SecondTabNettoValue3 = ComputeNettoValue(SecondTabNettoPrice3, PropHallEqAmount3, PropHallEqDays3);
                }
            }
        }
        public decimal SecondTabNettoPrice4
        {
            get { return _secondTabNettoPrice[4]; }
            set
            {
                _secondTabNettoPrice[4] = value;
                RaisePropertyChanged("SecondTabNettoPrice4");
                // w chwili aktualizacji ceny brutto aktualizuj wartosci brutto/netto
                if (PropHallEqDays4 != null && PropHallEqAmount4 != null)
                {
                    SecondTabBruttoValue4 = ComputeBruttoValue(PropHallEqBrutto4, PropHallEqAmount4, PropHallEqDays4);
                    SecondTabNettoValue4 = ComputeNettoValue(SecondTabNettoPrice4, PropHallEqAmount4, PropHallEqDays4);
                }
            }
        }
        public decimal SecondTabNettoPrice5
        {
            get { return _secondTabNettoPrice[5]; }
            set
            {
                _secondTabNettoPrice[5] = value;
                RaisePropertyChanged("SecondTabNettoPrice5");
                // w chwili aktualizacji ceny brutto aktualizuj wartosci brutto/netto
                if (PropHallEqDays5 != null && PropHallEqAmount5 != null)
                {
                    SecondTabBruttoValue5 = ComputeBruttoValue(PropHallEqBrutto5, PropHallEqAmount5, PropHallEqDays5);
                    SecondTabNettoValue5 = ComputeNettoValue(SecondTabNettoPrice5, PropHallEqAmount5, PropHallEqDays5);
                }
            }
        }

        public float? PropHallEqBrutto0
        {
            get { return _propHallEquipment[0].BruttoPrice; }
            set
            {
                _propHallEquipment[0].BruttoPrice = value;
                RaisePropertyChanged("PropHallEqBrutto0");

                // liczenie i wyswietlanie w oknie (tab2) ceny netto w sytuacji domyslnego vat
                SecondTabNettoPrice0 = ComputeNettoPrice(value, PropHallEqVat0);

                // w chwili aktualizacji ceny brutto aktualizuj wartosci brutto/netto
                if (PropHallEqDays0 != null && PropHallEqAmount0 != null)
                {
                    SecondTabBruttoValue0 = ComputeBruttoValue(PropHallEqBrutto0, PropHallEqAmount0, PropHallEqDays0);
                    SecondTabNettoValue0 = ComputeNettoValue(SecondTabNettoPrice0, PropHallEqAmount0, PropHallEqDays0);
                }
                    
            }
        }
        public float? PropHallEqBrutto1
        {
            get { return _propHallEquipment[1].BruttoPrice; }
            set
            {
                _propHallEquipment[1].BruttoPrice = value;
                RaisePropertyChanged("PropHallEqBrutto1");
                SecondTabNettoPrice1 = ComputeNettoPrice(value, PropHallEqVat1);
                // w chwili aktualizacji ceny brutto aktualizuj wartosci brutto/netto
                if (PropHallEqDays1 != null && PropHallEqAmount1 != null)
                {
                    SecondTabBruttoValue1 = ComputeBruttoValue(PropHallEqBrutto1, PropHallEqAmount1, PropHallEqDays1);
                    SecondTabNettoValue1 = ComputeNettoValue(SecondTabNettoPrice1, PropHallEqAmount1, PropHallEqDays1);
                }
            }
        }
        public float? PropHallEqBrutto2
        {
            get { return _propHallEquipment[2].BruttoPrice; }
            set
            {
                _propHallEquipment[2].BruttoPrice = value;
                RaisePropertyChanged("PropHallEqBrutto2");
                SecondTabNettoPrice2 = ComputeNettoPrice(value, PropHallEqVat2);
                // w chwili aktualizacji ceny brutto aktualizuj wartosci brutto/netto
                if (PropHallEqDays2 != null && PropHallEqAmount2 != null)
                {
                    SecondTabBruttoValue2 = ComputeBruttoValue(PropHallEqBrutto2, PropHallEqAmount2, PropHallEqDays2);
                    SecondTabNettoValue2 = ComputeNettoValue(SecondTabNettoPrice2, PropHallEqAmount2, PropHallEqDays2);
                }
            }
        }
        public float? PropHallEqBrutto3
        {
            get { return _propHallEquipment[3].BruttoPrice; }
            set
            {
                _propHallEquipment[3].BruttoPrice = value;
                RaisePropertyChanged("PropHallEqBrutto3");
                SecondTabNettoPrice3 = ComputeNettoPrice(value, PropHallEqVat3);
                // w chwili aktualizacji ceny brutto aktualizuj wartosci brutto/netto
                if (PropHallEqDays3 != null && PropHallEqAmount3 != null)
                {
                    SecondTabBruttoValue3 = ComputeBruttoValue(PropHallEqBrutto3, PropHallEqAmount3, PropHallEqDays3);
                    SecondTabNettoValue3 = ComputeNettoValue(SecondTabNettoPrice3, PropHallEqAmount3, PropHallEqDays3);
                }
            }
        }
        public float? PropHallEqBrutto4
        {
            get { return _propHallEquipment[4].BruttoPrice; }
            set
            {
                _propHallEquipment[4].BruttoPrice = value;
                RaisePropertyChanged("PropHallEqBrutto4");
                SecondTabNettoPrice4 = ComputeNettoPrice(value, PropHallEqVat4);
                // w chwili aktualizacji ceny brutto aktualizuj wartosci brutto/netto
                if (PropHallEqDays4 != null && PropHallEqAmount4 != null)
                {
                    SecondTabBruttoValue4 = ComputeBruttoValue(PropHallEqBrutto4, PropHallEqAmount4, PropHallEqDays4);
                    SecondTabNettoValue4 = ComputeNettoValue(SecondTabNettoPrice4, PropHallEqAmount4, PropHallEqDays4);
                }
            }
        }
        public float? PropHallEqBrutto5
        {
            get { return _propHallEquipment[5].BruttoPrice; }
            set
            {
                _propHallEquipment[5].BruttoPrice = value;
                RaisePropertyChanged("PropHallEqBrutto5");
                SecondTabNettoPrice5 = ComputeNettoPrice(value, PropHallEqVat5);
                // w chwili aktualizacji ceny brutto aktualizuj wartosci brutto/netto
                if (PropHallEqDays5 != null && PropHallEqAmount5 != null)
                {
                    SecondTabBruttoValue5 = ComputeBruttoValue(PropHallEqBrutto5, PropHallEqAmount5, PropHallEqDays5);
                    SecondTabNettoValue5 = ComputeNettoValue(SecondTabNettoPrice5, PropHallEqAmount5, PropHallEqDays5);
                }
            }
        }

        public float? PropHallEqVat0
        {
            get { return _propHallEquipment[0].Vat; }
            set
            {
                _propHallEquipment[0].Vat = value;
                RaisePropertyChanged("PropHallEqVat0");
                // liczenie i wyswietlanie w oknie (tab2) ceny netto w sytuacji zmiany vat gdy jest cena brutto
                if(PropHallEqBrutto0!=null)
                    SecondTabNettoPrice0 = ComputeNettoPrice(PropHallEqBrutto0, value);
            }
        }
        public float? PropHallEqVat1
        {
            get { return _propHallEquipment[1].Vat; }
            set
            {
                _propHallEquipment[1].Vat = value;
                RaisePropertyChanged("PropHallEqVat1");
                // liczenie i wyswietlanie w oknie (tab2) ceny netto w sytuacji zmiany vat gdy jest cena brutto
                if(PropHallEqBrutto1!=null)
                    SecondTabNettoPrice1 = ComputeNettoPrice(PropHallEqBrutto1, value);
            }
        }
        public float? PropHallEqVat2
        {
            get { return _propHallEquipment[2].Vat; }
            set
            {
                _propHallEquipment[2].Vat = value;
                RaisePropertyChanged("PropHallEqVat2");
                // liczenie i wyswietlanie w oknie (tab2) ceny netto w sytuacji zmiany vat gdy jest cena brutto
                if (PropHallEqBrutto2 != null)
                    SecondTabNettoPrice2 = ComputeNettoPrice(PropHallEqBrutto2, value);
            }
        }
        public float? PropHallEqVat3
        {
            get { return _propHallEquipment[3].Vat; }
            set
            {
                _propHallEquipment[3].Vat = value;
                RaisePropertyChanged("PropHallEqVat3");
                // liczenie i wyswietlanie w oknie (tab2) ceny netto w sytuacji zmiany vat gdy jest cena brutto
                if (PropHallEqBrutto3 != null)
                    SecondTabNettoPrice3 = ComputeNettoPrice(PropHallEqBrutto3, value);
            }
        }
        public float? PropHallEqVat4
        {
            get { return _propHallEquipment[4].Vat; }
            set
            {
                _propHallEquipment[4].Vat = value;
                RaisePropertyChanged("PropHallEqVat4");
                // liczenie i wyswietlanie w oknie (tab2) ceny netto w sytuacji zmiany vat gdy jest cena brutto
                if (PropHallEqBrutto4 != null)
                    SecondTabNettoPrice4 = ComputeNettoPrice(PropHallEqBrutto4, value);
            }
        }
        public float? PropHallEqVat5
        {
            get { return _propHallEquipment[5].Vat; }
            set
            {
                _propHallEquipment[5].Vat = value;
                RaisePropertyChanged("PropHallEqVat5");
                // liczenie i wyswietlanie w oknie (tab2) ceny netto w sytuacji zmiany vat gdy jest cena brutto
                if (PropHallEqBrutto5 != null)
                    SecondTabNettoPrice5 = ComputeNettoPrice(PropHallEqBrutto5, value);
            }
        }

        public int? PropHallEqAmount0
        {
            get { return _propHallEquipment[0].Amount; }
            set
            {
                _propHallEquipment[0].Amount = value;
                RaisePropertyChanged("PropHallEqAmount0");

                //jeśli jest cena netto i ilosc dni to oblicz sume
                if (SecondTabNettoPrice0 != null && PropHallEqDays0 != null)
                {
                    SecondTabNettoValue0 = ComputeNettoValue(SecondTabNettoPrice0, value, PropHallEqDays0);
                    SecondTabBruttoValue0 = ComputeBruttoValue(PropHallEqBrutto0, value, PropHallEqDays0);
                }
            }
        }
        public int? PropHallEqAmount1
        {
            get { return _propHallEquipment[1].Amount; }
            set
            {
                _propHallEquipment[1].Amount = value;
                RaisePropertyChanged("PropHallEqAmount1");
                //jeśli jest cena netto i ilosc dni to oblicz sume
                if (SecondTabNettoPrice1 != null && PropHallEqDays1 != null)
                {
                    SecondTabNettoValue1 = ComputeNettoValue(SecondTabNettoPrice1, value, PropHallEqDays1);
                    SecondTabBruttoValue1 = ComputeBruttoValue(PropHallEqBrutto1, value, PropHallEqDays1);
                }
            }
        }
        public int? PropHallEqAmount2
        {
            get { return _propHallEquipment[2].Amount; }
            set
            {
                _propHallEquipment[2].Amount = value;
                RaisePropertyChanged("PropHallEqAmount2");
                //jeśli jest cena netto i ilosc dni to oblicz sume
                if (SecondTabNettoPrice2 != null && PropHallEqDays2 != null)
                {
                    SecondTabNettoValue2 = ComputeNettoValue(SecondTabNettoPrice2, value, PropHallEqDays2);
                    SecondTabBruttoValue2 = ComputeBruttoValue(PropHallEqBrutto2, value, PropHallEqDays2);
                }
            }
        }
        public int? PropHallEqAmount3
        {
            get { return _propHallEquipment[3].Amount; }
            set
            {
                _propHallEquipment[3].Amount = value;
                RaisePropertyChanged("PropHallEqAmount3");
                //jeśli jest cena netto i ilosc dni to oblicz sume
                if (SecondTabNettoPrice3 != null && PropHallEqDays3 != null)
                {
                    SecondTabNettoValue3 = ComputeNettoValue(SecondTabNettoPrice3, value, PropHallEqDays3);
                    SecondTabBruttoValue3 = ComputeBruttoValue(PropHallEqBrutto3, value, PropHallEqDays3);
                }
            }
        }
        public int? PropHallEqAmount4
        {
            get { return _propHallEquipment[4].Amount; }
            set
            {
                _propHallEquipment[4].Amount = value;
                RaisePropertyChanged("PropHallEqAmount4");
                //jeśli jest cena netto i ilosc dni to oblicz sume
                if (SecondTabNettoPrice4 != null && PropHallEqDays4 != null)
                {
                    SecondTabNettoValue4 = ComputeNettoValue(SecondTabNettoPrice4, value, PropHallEqDays4);
                    SecondTabBruttoValue4 = ComputeBruttoValue(PropHallEqBrutto4, value, PropHallEqDays4);
                }
            }
        }
        public int? PropHallEqAmount5
        {
            get { return _propHallEquipment[5].Amount; }
            set
            {
                _propHallEquipment[5].Amount = value;
                RaisePropertyChanged("PropHallEqAmount5");
                //jeśli jest cena netto i ilosc dni to oblicz sume
                if (SecondTabNettoPrice5 != null && PropHallEqDays5 != null)
                {
                    SecondTabNettoValue5 = ComputeNettoValue(SecondTabNettoPrice5, value, PropHallEqDays5);
                    SecondTabBruttoValue5 = ComputeBruttoValue(PropHallEqBrutto5, value, PropHallEqDays5);
                }
            }
        }

        public int? PropHallEqDays0
        {
            get { return _propHallEquipment[0].Days; }
            set
            {
                _propHallEquipment[0].Days = value;
                RaisePropertyChanged("PropHallEqDays0");

                //jeśli jest cena netto i ilosc dni to oblicz sume
                if (SecondTabNettoPrice0 != null && PropHallEqDays0 != null)
                {
                    SecondTabNettoValue0 = ComputeNettoValue(SecondTabNettoPrice0, PropHallEqAmount0, value);
                    SecondTabBruttoValue0 = ComputeBruttoValue(PropHallEqBrutto0, PropHallEqAmount0, value);
                }
            }
        }
        public int? PropHallEqDays1
        {
            get { return _propHallEquipment[1].Days; }
            set
            {
                _propHallEquipment[1].Days = value;
                RaisePropertyChanged("PropHallEqDays1");

                //jeśli jest cena netto i ilosc dni to oblicz sume
                if (SecondTabNettoPrice1 != null && PropHallEqDays1 != null)
                {
                    SecondTabNettoValue1 = ComputeNettoValue(SecondTabNettoPrice1, PropHallEqAmount1, value);
                    SecondTabBruttoValue1 = ComputeBruttoValue(PropHallEqBrutto1, PropHallEqAmount1, value);
                }
            }
        }
        public int? PropHallEqDays2
        {
            get { return _propHallEquipment[2].Days; }
            set
            {
                _propHallEquipment[2].Days = value;
                RaisePropertyChanged("PropHallEqDays2");

                //jeśli jest cena netto i ilosc dni to oblicz sume
                if (SecondTabNettoPrice2 != null && PropHallEqDays2 != null)
                {
                    SecondTabNettoValue2 = ComputeNettoValue(SecondTabNettoPrice2, PropHallEqAmount2, value);
                    SecondTabBruttoValue2 = ComputeBruttoValue(PropHallEqBrutto2, PropHallEqAmount2, value);
                }
            }
        }
        public int? PropHallEqDays3
        {
            get { return _propHallEquipment[3].Days; }
            set
            {
                _propHallEquipment[3].Days = value;
                RaisePropertyChanged("PropHallEqDays3");

                //jeśli jest cena netto i ilosc dni to oblicz sume
                if (SecondTabNettoPrice3 != null && PropHallEqDays3 != null)
                {
                    SecondTabNettoValue3 = ComputeNettoValue(SecondTabNettoPrice3, PropHallEqAmount3, value);
                    SecondTabBruttoValue3 = ComputeBruttoValue(PropHallEqBrutto3, PropHallEqAmount3, value);
                }
            }
        }
        public int? PropHallEqDays4
        {
            get { return _propHallEquipment[4].Days; }
            set
            {
                _propHallEquipment[4].Days = value;
                RaisePropertyChanged("PropHallEqDays4");

                //jeśli jest cena netto i ilosc dni to oblicz sume
                if (SecondTabNettoPrice4 != null && PropHallEqDays4 != null)
                {
                    SecondTabNettoValue4 = ComputeNettoValue(SecondTabNettoPrice4, PropHallEqAmount4, value);
                    SecondTabBruttoValue4 = ComputeBruttoValue(PropHallEqBrutto4, PropHallEqAmount4, value);
                }
            }
        }
        public int? PropHallEqDays5
        {
            get { return _propHallEquipment[5].Days; }
            set
            {
                _propHallEquipment[5].Days = value;
                RaisePropertyChanged("PropHallEqDays5");

                //jeśli jest cena netto i ilosc dni to oblicz sume
                if (SecondTabNettoPrice5 != null && PropHallEqDays5 != null)
                {
                    SecondTabNettoValue5 = ComputeNettoValue(SecondTabNettoPrice5, PropHallEqAmount5, value);
                    SecondTabBruttoValue5 = ComputeBruttoValue(PropHallEqBrutto5, PropHallEqAmount5, value);
                }
            }
        }

        public List<string> PropHallEqDict2
        {
            get { return _propHallEqDict2; }
            set
            {
                _propHallEqDict2 = value;
                RaisePropertyChanged("PropHallEqDict2");    
            }
        }

        public List<decimal> SecondTabNettoValue
        {
            get { return _secondTabNettoValue; }
            set
            {
                _secondTabNettoValue = value;
                RaisePropertyChanged("SecondTabNettoValue");    
            }
        }
        public List<decimal> SecondTabBruttoValue
        {
            get { return _secondTabBruttoValue; }
            set
            {
                _secondTabBruttoValue = value;
                RaisePropertyChanged("SecondTabBruttoValue");    
            }
        }

        public decimal SecondTabNettoValue0
        {
            get { return _secondTabNettoValue[0]; }
            set
            {
                _secondTabNettoValue[0] = value;
                RaisePropertyChanged("SecondTabNettoValue0");
            }
        }
        public decimal SecondTabNettoValue1
        {
            get { return _secondTabNettoValue[1]; }
            set
            {
                _secondTabNettoValue[1] = value;
                RaisePropertyChanged("SecondTabNettoValue1");
            }
        }
        public decimal SecondTabNettoValue2
        {
            get { return _secondTabNettoValue[2]; }
            set
            {
                _secondTabNettoValue[2] = value;
                RaisePropertyChanged("SecondTabNettoValue2");
            }
        }
        public decimal SecondTabNettoValue3
        {
            get { return _secondTabNettoValue[3]; }
            set
            {
                _secondTabNettoValue[3] = value;
                RaisePropertyChanged("SecondTabNettoValue3");
            }
        }
        public decimal SecondTabNettoValue4
        {
            get { return _secondTabNettoValue[4]; }
            set
            {
                _secondTabNettoValue[4] = value;
                RaisePropertyChanged("SecondTabNettoValue4");
            }
        }
        public decimal SecondTabNettoValue5
        {
            get { return _secondTabNettoValue[5]; }
            set
            {
                _secondTabNettoValue[5] = value;
                RaisePropertyChanged("SecondTabNettoValue5");
            }
        }

        public decimal SecondTabBruttoValue0
        {
            get { return _secondTabBruttoValue[0]; }
            set
            {
                _secondTabBruttoValue[0] = value;
                RaisePropertyChanged("SecondTabBruttoValue0");
                ComputeSecondTabSumBruttoValue();
                ComputeSecondTabSumNettoValue();
            }
        }
        public decimal SecondTabBruttoValue1
        {
            get { return _secondTabBruttoValue[1]; }
            set
            {
                _secondTabBruttoValue[1] = value;
                RaisePropertyChanged("SecondTabBruttoValue1");
                ComputeSecondTabSumBruttoValue();
                ComputeSecondTabSumNettoValue();
            }
        }
        public decimal SecondTabBruttoValue2
        {
            get { return _secondTabBruttoValue[2]; }
            set
            {
                _secondTabBruttoValue[2] = value;
                RaisePropertyChanged("SecondTabBruttoValue2");
                ComputeSecondTabSumBruttoValue();
                ComputeSecondTabSumNettoValue();
            }
        }
        public decimal SecondTabBruttoValue3
        {
            get { return _secondTabBruttoValue[3]; }
            set
            {
                _secondTabBruttoValue[3] = value;
                RaisePropertyChanged("SecondTabBruttoValue3");
                ComputeSecondTabSumBruttoValue();
                ComputeSecondTabSumNettoValue();
            }
        }
        public decimal SecondTabBruttoValue4
        {
            get { return _secondTabBruttoValue[4]; }
            set
            {
                _secondTabBruttoValue[4] = value;
                RaisePropertyChanged("SecondTabBruttoValue4");
                ComputeSecondTabSumBruttoValue();
                ComputeSecondTabSumNettoValue();
            }
        }
        public decimal SecondTabBruttoValue5
        {
            get { return _secondTabBruttoValue[5]; }
            set
            {
                _secondTabBruttoValue[5] = value;
                RaisePropertyChanged("SecondTabBruttoValue5");
                ComputeSecondTabSumBruttoValue();
                ComputeSecondTabSumNettoValue();
            }
        }

        public decimal SecondTabSumNettoValue
        {
            get { return _secondTabSumNettoValue; }
            set
            {
                _secondTabSumNettoValue = value;
                RaisePropertyChanged("SecondTabSumNettoValue");
            }
        }

        public decimal SecondTabSumBruttoValue
        {
            get { return _secondTabSumBruttoValue; }
            set
            {
                _secondTabSumBruttoValue = value;
                RaisePropertyChanged("SecondTabSumBruttoValue");
            }
        }

        //tab 3

        public List<float?> VatList
        {
            get { return _vatList; }
            set
            {
                _vatList = value;
                RaisePropertyChanged("VatList");
            }
        }

        public List<string> PropMenuGastThingDict
        {
            get { return _propMenuGastThingDict; }
            set
            {
                _propMenuGastThingDict = value;
                RaisePropertyChanged("PropMenuGastThingDict");               
            }
        }

        public List<PropMenuPosition> PropMenuPositions
        {
            get { return _propMenuPositions; }
            set
            {
                _propMenuPositions = value; 
                RaisePropertyChanged("PropMenuPositions");      
            }

        }

        public string PropMenuTypeOfServ0
        {
            get { return _propMenuPositions[0].TypeOfService; }
            set
            {
                   

                _propMenuPositions[0].TypeOfService = value;
                RaisePropertyChanged("PropMenuTypeOfServ0");

                // ustawienie ceny netto dla danego produktu
                ThirdTabNettoPrice0 = SetThirdDefaultNettoPrice(value);
                // ustawienie ceny brutto dla danego produktu
                PropMenuPosBrutto0 = SetMenuPosDefaultBrutto(value);
                
                // ustawienie vat
                PropMenuPosVat0 = SetMenuPosDefaultVat(value);

                // ustawienie typu marzy
                PropMenuPosMergeType0 = SetMenuPosDefaultMergeType(value);

                // doliczenie marzy do konkretnych cen
                ThirdTabNettoPrice0 = AddMergeToPrice(ThirdTabNettoPrice0, PropMenuPosMergeType0);
                PropMenuPosBrutto0 = AddMergeToPrice(PropMenuPosBrutto0, PropMenuPosMergeType0);

                // jesli jest wybrana ilosc i liczba dni to aktualizuj sume elementu
                if (PropMenuPosAmount0 != null && PropMenuPosDays0 != null)
                {
                    ThirdTabNettoValue0 = ComputeNettoValue((decimal)ThirdTabNettoPrice0, PropMenuPosAmount0,
                        PropMenuPosDays0);
                    ThirdTabBruttoValue0 = ComputeBruttoValue(PropMenuPosBrutto0, PropMenuPosAmount0,
                        PropMenuPosDays0);
                }
            }
        }

        public string PropMenuTypeOfServ1
        {
            get { return _propMenuPositions[1].TypeOfService; }
            set
            {

                _propMenuPositions[1].TypeOfService = value;
                RaisePropertyChanged("PropMenuTypeOfServ1");

                // ustawienie ceny netto dla danego produktu
                ThirdTabNettoPrice1 = SetThirdDefaultNettoPrice(value);
                // ustawienie ceny brutto dla danego produktu
                PropMenuPosBrutto1 = SetMenuPosDefaultBrutto(value);

                // ustawienie vat
                PropMenuPosVat1 = SetMenuPosDefaultVat(value);

                // ustawienie typu marzy
                PropMenuPosMergeType1 = SetMenuPosDefaultMergeType(value);

                // doliczenie marzy do konkretnych cen
                ThirdTabNettoPrice1 = AddMergeToPrice(ThirdTabNettoPrice1, PropMenuPosMergeType1);
                PropMenuPosBrutto1 = AddMergeToPrice(PropMenuPosBrutto1, PropMenuPosMergeType1);

                // jesli jest wybrana ilosc i liczba dni to aktualizuj sume elementu
                if (PropMenuPosAmount1 != null && PropMenuPosDays1 != null)
                {
                    ThirdTabNettoValue1 = ComputeNettoValue((decimal)ThirdTabNettoPrice1, PropMenuPosAmount1,
                        PropMenuPosDays1);
                    ThirdTabBruttoValue1 = ComputeBruttoValue(PropMenuPosBrutto1, PropMenuPosAmount1,
                        PropMenuPosDays1);
                }
            }
        }
        public string PropMenuTypeOfServ2
        {
            get { return _propMenuPositions[2].TypeOfService; }
            set
            {

                _propMenuPositions[2].TypeOfService = value;
                RaisePropertyChanged("PropMenuTypeOfServ2");

                // ustawienie ceny netto dla danego produktu
                ThirdTabNettoPrice2 = SetThirdDefaultNettoPrice(value);
                // ustawienie ceny brutto dla danego produktu
                PropMenuPosBrutto2 = SetMenuPosDefaultBrutto(value);

                // ustawienie vat
                PropMenuPosVat2 = SetMenuPosDefaultVat(value);

                // ustawienie typu marzy
                PropMenuPosMergeType2 = SetMenuPosDefaultMergeType(value);

                // doliczenie marzy do konkretnych cen
                ThirdTabNettoPrice2 = AddMergeToPrice(ThirdTabNettoPrice2, PropMenuPosMergeType2);
                PropMenuPosBrutto2 = AddMergeToPrice(PropMenuPosBrutto2, PropMenuPosMergeType2);

                // jesli jest wybrana ilosc i liczba dni to aktualizuj sume elementu
                if (PropMenuPosAmount2 != null && PropMenuPosDays2 != null)
                {
                    ThirdTabNettoValue2 = ComputeNettoValue((decimal)ThirdTabNettoPrice2, PropMenuPosAmount2,
                        PropMenuPosDays2);
                    ThirdTabBruttoValue2 = ComputeBruttoValue(PropMenuPosBrutto2, PropMenuPosAmount2,
                        PropMenuPosDays2);
                }
            }
        }
        public string PropMenuTypeOfServ3
        {
            get { return _propMenuPositions[3].TypeOfService; }
            set
            {

                _propMenuPositions[3].TypeOfService = value;
                RaisePropertyChanged("PropMenuTypeOfServ3");

                // ustawienie ceny netto dla danego produktu
                ThirdTabNettoPrice3 = SetThirdDefaultNettoPrice(value);
                // ustawienie ceny brutto dla danego produktu
                PropMenuPosBrutto3 = SetMenuPosDefaultBrutto(value);

                // ustawienie vat
                PropMenuPosVat3 = SetMenuPosDefaultVat(value);

                // ustawienie typu marzy
                PropMenuPosMergeType3 = SetMenuPosDefaultMergeType(value);

                // doliczenie marzy do konkretnych cen
                ThirdTabNettoPrice3 = AddMergeToPrice(ThirdTabNettoPrice3, PropMenuPosMergeType3);
                PropMenuPosBrutto3 = AddMergeToPrice(PropMenuPosBrutto3, PropMenuPosMergeType3);

                // jesli jest wybrana ilosc i liczba dni to aktualizuj sume elementu
                if (PropMenuPosAmount3 != null && PropMenuPosDays3 != null)
                {
                    ThirdTabNettoValue3 = ComputeNettoValue((decimal)ThirdTabNettoPrice3, PropMenuPosAmount3,
                        PropMenuPosDays3);
                    ThirdTabBruttoValue3 = ComputeBruttoValue(PropMenuPosBrutto3, PropMenuPosAmount3,
                        PropMenuPosDays3);
                }
            }
        }
        public string PropMenuTypeOfServ4
        {
            get { return _propMenuPositions[4].TypeOfService; }
            set
            {

                _propMenuPositions[4].TypeOfService = value;
                RaisePropertyChanged("PropMenuTypeOfServ4");

                // ustawienie ceny netto dla danego produktu
                ThirdTabNettoPrice4 = SetThirdDefaultNettoPrice(value);
                // ustawienie ceny brutto dla danego produktu
                PropMenuPosBrutto4 = SetMenuPosDefaultBrutto(value);

                // ustawienie vat
                PropMenuPosVat4 = SetMenuPosDefaultVat(value);

                // ustawienie typu marzy
                PropMenuPosMergeType4 = SetMenuPosDefaultMergeType(value);

                // doliczenie marzy do konkretnych cen
                ThirdTabNettoPrice4 = AddMergeToPrice(ThirdTabNettoPrice4, PropMenuPosMergeType4);
                PropMenuPosBrutto4 = AddMergeToPrice(PropMenuPosBrutto4, PropMenuPosMergeType4);

                // jesli jest wybrana ilosc i liczba dni to aktualizuj sume elementu
                if (PropMenuPosAmount4 != null && PropMenuPosDays4 != null)
                {
                    ThirdTabNettoValue4 = ComputeNettoValue((decimal)ThirdTabNettoPrice4, PropMenuPosAmount4,
                        PropMenuPosDays4);
                    ThirdTabBruttoValue4 = ComputeBruttoValue(PropMenuPosBrutto4, PropMenuPosAmount4,
                        PropMenuPosDays4);
                }
            }
        }
        public string PropMenuTypeOfServ5
        {
            get { return _propMenuPositions[5].TypeOfService; }
            set
            {

                _propMenuPositions[5].TypeOfService = value;
                RaisePropertyChanged("PropMenuTypeOfServ5");

                // ustawienie ceny netto dla danego produktu
                ThirdTabNettoPrice5 = SetThirdDefaultNettoPrice(value);
                // ustawienie ceny brutto dla danego produktu
                PropMenuPosBrutto5 = SetMenuPosDefaultBrutto(value);

                // ustawienie vat
                PropMenuPosVat5 = SetMenuPosDefaultVat(value);

                // ustawienie typu marzy
                PropMenuPosMergeType5 = SetMenuPosDefaultMergeType(value);

                // doliczenie marzy do konkretnych cen
                ThirdTabNettoPrice5 = AddMergeToPrice(ThirdTabNettoPrice5, PropMenuPosMergeType5);
                PropMenuPosBrutto5 = AddMergeToPrice(PropMenuPosBrutto5, PropMenuPosMergeType5);

                // jesli jest wybrana ilosc i liczba dni to aktualizuj sume elementu
                if (PropMenuPosAmount5 != null && PropMenuPosDays5 != null)
                {
                    ThirdTabNettoValue5 = ComputeNettoValue((decimal)ThirdTabNettoPrice5, PropMenuPosAmount5,
                        PropMenuPosDays5);
                    ThirdTabBruttoValue5 = ComputeBruttoValue(PropMenuPosBrutto5, PropMenuPosAmount5,
                        PropMenuPosDays5);
                }
            }
        }
        public string PropMenuTypeOfServ6
        {
            get { return _propMenuPositions[6].TypeOfService; }
            set
            {

                _propMenuPositions[6].TypeOfService = value;
                RaisePropertyChanged("PropMenuTypeOfServ6");

                // ustawienie ceny netto dla danego produktu
                ThirdTabNettoPrice6 = SetThirdDefaultNettoPrice(value);
                // ustawienie ceny brutto dla danego produktu
                PropMenuPosBrutto6 = SetMenuPosDefaultBrutto(value);

                // ustawienie vat
                PropMenuPosVat6 = SetMenuPosDefaultVat(value);

                // ustawienie typu marzy
                PropMenuPosMergeType6 = SetMenuPosDefaultMergeType(value);

                // doliczenie marzy do konkretnych cen
                ThirdTabNettoPrice6 = AddMergeToPrice(ThirdTabNettoPrice6, PropMenuPosMergeType6);
                PropMenuPosBrutto6 = AddMergeToPrice(PropMenuPosBrutto6, PropMenuPosMergeType6);

                // jesli jest wybrana ilosc i liczba dni to aktualizuj sume elementu
                if (PropMenuPosAmount6 != null && PropMenuPosDays6 != null)
                {
                    ThirdTabNettoValue6 = ComputeNettoValue((decimal)ThirdTabNettoPrice6, PropMenuPosAmount6,
                        PropMenuPosDays6);
                    ThirdTabBruttoValue6 = ComputeBruttoValue(PropMenuPosBrutto6, PropMenuPosAmount6,
                        PropMenuPosDays6);
                }
            }
        }

        public float? PropMenuPosBrutto0
        {
            get { return _propMenuPositions[0].BruttoPrice; }
            set
            {
                _propMenuPositions[0].BruttoPrice = value;
                RaisePropertyChanged("PropMenuPosBrutto0");
            }
        }
        public float? PropMenuPosBrutto1
        {
            get { return _propMenuPositions[1].BruttoPrice; }
            set
            {
                _propMenuPositions[1].BruttoPrice = value;
                RaisePropertyChanged("PropMenuPosBrutto1");
            }
        }
        public float? PropMenuPosBrutto2
        {
            get { return _propMenuPositions[2].BruttoPrice; }
            set
            {
                _propMenuPositions[2].BruttoPrice = value;
                RaisePropertyChanged("PropMenuPosBrutto2");
            }
        }
        public float? PropMenuPosBrutto3
        {
            get { return _propMenuPositions[3].BruttoPrice; }
            set
            {
                _propMenuPositions[3].BruttoPrice = value;
                RaisePropertyChanged("PropMenuPosBrutto3");
            }
        }
        public float? PropMenuPosBrutto4
        {
            get { return _propMenuPositions[4].BruttoPrice; }
            set
            {
                _propMenuPositions[4].BruttoPrice = value;
                RaisePropertyChanged("PropMenuPosBrutto4");
            }
        }
        public float? PropMenuPosBrutto5
        {
            get { return _propMenuPositions[5].BruttoPrice; }
            set
            {
                _propMenuPositions[5].BruttoPrice = value;
                RaisePropertyChanged("PropMenuPosBrutto5");
            }
        }
        public float? PropMenuPosBrutto6
        {
            get { return _propMenuPositions[6].BruttoPrice; }
            set
            {
                _propMenuPositions[6].BruttoPrice = value;
                RaisePropertyChanged("PropMenuPosBrutto6");
            }
        }

        public decimal? ThirdTabNettoPrice0
        {
            get { return _thirdTabNettoPrice[0]; }
            set
            {
                _thirdTabNettoPrice[0] = value;
                RaisePropertyChanged("ThirdTabNettoPrice0");
            }
        }
        public decimal? ThirdTabNettoPrice1
        {
            get { return _thirdTabNettoPrice[1]; }
            set
            {
                _thirdTabNettoPrice[1] = value;
                RaisePropertyChanged("ThirdTabNettoPrice1");
            }
        }
        public decimal? ThirdTabNettoPrice2
        {
            get { return _thirdTabNettoPrice[2]; }
            set
            {
                _thirdTabNettoPrice[2] = value;
                RaisePropertyChanged("ThirdTabNettoPrice2");
            }
        }
        public decimal? ThirdTabNettoPrice3
        {
            get { return _thirdTabNettoPrice[3]; }
            set
            {
                _thirdTabNettoPrice[3] = value;
                RaisePropertyChanged("ThirdTabNettoPrice3");
            }
        }
        public decimal? ThirdTabNettoPrice4
        {
            get { return _thirdTabNettoPrice[4]; }
            set
            {
                _thirdTabNettoPrice[4] = value;
                RaisePropertyChanged("ThirdTabNettoPrice4");
            }
        }
        public decimal? ThirdTabNettoPrice5
        {
            get { return _thirdTabNettoPrice[5]; }
            set
            {
                _thirdTabNettoPrice[5] = value;
                RaisePropertyChanged("ThirdTabNettoPrice5");
            }
        }
        public decimal? ThirdTabNettoPrice6
        {
            get { return _thirdTabNettoPrice[6]; }
            set
            {
                _thirdTabNettoPrice[6] = value;
                RaisePropertyChanged("ThirdTabNettoPrice6");
            }
        }

        public float? PropMenuPosVat0
        {
            get
            {
                if (_propMenuPositions[0].Vat == 8)
                {
                    return VatList[0];
                }
                return VatList[1];
            }
            set
            {
                _propMenuPositions[0].Vat = value;
                RaisePropertyChanged("PropMenuPosVat0");
                if (PropMenuTypeOfServ0 != null)
                    ThirdTabNettoPrice0 = ComputeNettoPrice(PropMenuPosBrutto0, _propMenuPositions[0].Vat);
            }
        }
        public float? PropMenuPosVat1
        {
            get
            {
                if (_propMenuPositions[1].Vat == 8)
                {
                    return VatList[0];
                }
                return VatList[1];
            }
            set
            {
                _propMenuPositions[1].Vat = value;
                RaisePropertyChanged("PropMenuPosVat1");
                if (PropMenuTypeOfServ1 != null)
                    ThirdTabNettoPrice1 = ComputeNettoPrice(PropMenuPosBrutto1, _propMenuPositions[1].Vat);
            }
        }
        public float? PropMenuPosVat2
        {
            get
            {
                if (_propMenuPositions[2].Vat == 8)
                {
                    return VatList[0];
                }
                return VatList[1];
            }
            set
            {
                _propMenuPositions[2].Vat = value;
                RaisePropertyChanged("PropMenuPosVat2");
                if (PropMenuTypeOfServ2 != null)
                    ThirdTabNettoPrice2 = ComputeNettoPrice(PropMenuPosBrutto2, _propMenuPositions[2].Vat);
            }
        }
        public float? PropMenuPosVat3
        {
            get
            {
                if (_propMenuPositions[3].Vat == 8)
                {
                    return VatList[0];
                }
                return VatList[1];
            }
            set
            {
                _propMenuPositions[3].Vat = value;
                RaisePropertyChanged("PropMenuPosVat3");
                if (PropMenuTypeOfServ3 != null)
                    ThirdTabNettoPrice3 = ComputeNettoPrice(PropMenuPosBrutto3, _propMenuPositions[3].Vat);
            }
        }
        public float? PropMenuPosVat4
        {
            get
            {
                if (_propMenuPositions[4].Vat == 8)
                {
                    return VatList[0];
                }
                return VatList[1];
            }
            set
            {
                _propMenuPositions[4].Vat = value;
                RaisePropertyChanged("PropMenuPosVat4");
                if (PropMenuTypeOfServ4 != null)
                    ThirdTabNettoPrice4 = ComputeNettoPrice(PropMenuPosBrutto4, _propMenuPositions[4].Vat);
            }
        }
        public float? PropMenuPosVat5
        {
            get
            {
                if (_propMenuPositions[5].Vat == 8)
                {
                    return VatList[0];
                }
                return VatList[1];
            }
            set
            {
                _propMenuPositions[5].Vat = value;
                RaisePropertyChanged("PropMenuPosVat5");
                if (PropMenuTypeOfServ5 != null)
                    ThirdTabNettoPrice5 = ComputeNettoPrice(PropMenuPosBrutto5, _propMenuPositions[5].Vat);
            }
        }
        public float? PropMenuPosVat6
        {
            get
            {
                if (_propMenuPositions[6].Vat == 8)
                {
                    return VatList[0];
                }
                return VatList[1];
            }
            set
            {
                _propMenuPositions[6].Vat = value;
                RaisePropertyChanged("PropMenuPosVat6");
                if (PropMenuTypeOfServ6 != null)
                    ThirdTabNettoPrice6 = ComputeNettoPrice(PropMenuPosBrutto6, _propMenuPositions[6].Vat);
            }
        }

        public string PropMenuPosMergeType0
        {
            get { return _propMenuPositions[0].MergeType; }
            set
            {
                _propMenuPositions[0].MergeType = value;
                RaisePropertyChanged("PropMenuPosMergeType0");
            }
        }
        public string PropMenuPosMergeType1
        {
            get { return _propMenuPositions[1].MergeType; }
            set
            {
                _propMenuPositions[1].MergeType = value;
                RaisePropertyChanged("PropMenuPosMergeType1");
            }
        }
        public string PropMenuPosMergeType2
        {
            get { return _propMenuPositions[2].MergeType; }
            set
            {
                _propMenuPositions[2].MergeType = value;
                RaisePropertyChanged("PropMenuPosMergeType2");
            }
        }
        public string PropMenuPosMergeType3
        {
            get { return _propMenuPositions[3].MergeType; }
            set
            {
                _propMenuPositions[3].MergeType = value;
                RaisePropertyChanged("PropMenuPosMergeType3");
            }
        }
        public string PropMenuPosMergeType4
        {
            get { return _propMenuPositions[4].MergeType; }
            set
            {
                _propMenuPositions[4].MergeType = value;
                RaisePropertyChanged("PropMenuPosMergeType4");
            }
        }
        public string PropMenuPosMergeType5
        {
            get { return _propMenuPositions[5].MergeType; }
            set
            {
                _propMenuPositions[5].MergeType = value;
                RaisePropertyChanged("PropMenuPosMergeType5");
            }
        }
        public string PropMenuPosMergeType6
        {
            get { return _propMenuPositions[6].MergeType; }
            set
            {
                _propMenuPositions[6].MergeType = value;
                RaisePropertyChanged("PropMenuPosMergeType6");
            }
        }

        public int? PropMenuPosAmount0
        {
            get { return _propMenuPositions[0].Amount; }
            set
            {
                _propMenuPositions[0].Amount = value;
                RaisePropertyChanged("PropMenuPosAmount0");

                // jesli jest wybrany przedmiot i wypelniona jest liczba dni to oblicz sume
                if (PropMenuTypeOfServ0 != null && PropMenuPosDays0 != null)
                {
                    ThirdTabNettoValue0 = ComputeNettoValue((decimal) ThirdTabNettoPrice0, PropMenuPosAmount0,
                        PropMenuPosDays0);
                    ThirdTabBruttoValue0 = ComputeBruttoValue(PropMenuPosBrutto0, PropMenuPosAmount0,
                        PropMenuPosDays0);
                }

            }
        }
        public int? PropMenuPosAmount1
        {
            get { return _propMenuPositions[1].Amount; }
            set
            {
                _propMenuPositions[1].Amount = value;
                RaisePropertyChanged("PropMenuPosAmount1");

                // jesli jest wybrany przedmiot i wypelniona jest liczba dni to oblicz sume
                if (PropMenuTypeOfServ1 != null && PropMenuPosDays1 != null)
                {
                    ThirdTabNettoValue1 = ComputeNettoValue((decimal)ThirdTabNettoPrice1, PropMenuPosAmount1,
                        PropMenuPosDays1);
                    ThirdTabBruttoValue1 = ComputeBruttoValue(PropMenuPosBrutto1, PropMenuPosAmount1,
                        PropMenuPosDays1);
                }
            }
        }
        public int? PropMenuPosAmount2
        {
            get { return _propMenuPositions[2].Amount; }
            set
            {
                _propMenuPositions[2].Amount = value;
                RaisePropertyChanged("PropMenuPosAmount2");

                // jesli jest wybrany przedmiot i wypelniona jest liczba dni to oblicz sume
                if (PropMenuTypeOfServ2 != null && PropMenuPosDays2 != null)
                {
                    ThirdTabNettoValue2 = ComputeNettoValue((decimal)ThirdTabNettoPrice2, PropMenuPosAmount2,
                        PropMenuPosDays2);
                    ThirdTabBruttoValue2 = ComputeBruttoValue(PropMenuPosBrutto2, PropMenuPosAmount2,
                        PropMenuPosDays2);
                }
            }
        }
        public int? PropMenuPosAmount3
        {
            get { return _propMenuPositions[3].Amount; }
            set
            {
                _propMenuPositions[3].Amount = value;
                RaisePropertyChanged("PropMenuPosAmount3");

                // jesli jest wybrany przedmiot i wypelniona jest liczba dni to oblicz sume
                if (PropMenuTypeOfServ3 != null && PropMenuPosDays3 != null)
                {
                    ThirdTabNettoValue3 = ComputeNettoValue((decimal)ThirdTabNettoPrice3, PropMenuPosAmount3,
                        PropMenuPosDays3);
                    ThirdTabBruttoValue3 = ComputeBruttoValue(PropMenuPosBrutto3, PropMenuPosAmount3,
                        PropMenuPosDays3);
                }
            }
        }
        public int? PropMenuPosAmount4
        {
            get { return _propMenuPositions[4].Amount; }
            set
            {
                _propMenuPositions[4].Amount = value;
                RaisePropertyChanged("PropMenuPosAmount4");

                // jesli jest wybrany przedmiot i wypelniona jest liczba dni to oblicz sume
                if (PropMenuTypeOfServ4 != null && PropMenuPosDays4 != null)
                {
                    ThirdTabNettoValue4 = ComputeNettoValue((decimal)ThirdTabNettoPrice4, PropMenuPosAmount4,
                        PropMenuPosDays4);
                    ThirdTabBruttoValue4 = ComputeBruttoValue(PropMenuPosBrutto4, PropMenuPosAmount4,
                        PropMenuPosDays4);
                }
            }
        }
        public int? PropMenuPosAmount5
        {
            get { return _propMenuPositions[5].Amount; }
            set
            {
                _propMenuPositions[5].Amount = value;
                RaisePropertyChanged("PropMenuPosAmount5");

                // jesli jest wybrany przedmiot i wypelniona jest liczba dni to oblicz sume
                if (PropMenuTypeOfServ5 != null && PropMenuPosDays5 != null)
                {
                    ThirdTabNettoValue5 = ComputeNettoValue((decimal)ThirdTabNettoPrice5, PropMenuPosAmount5,
                        PropMenuPosDays5);
                    ThirdTabBruttoValue5 = ComputeBruttoValue(PropMenuPosBrutto5, PropMenuPosAmount5,
                        PropMenuPosDays5);
                }
            }
        }
        public int? PropMenuPosAmount6
        {
            get { return _propMenuPositions[6].Amount; }
            set
            {
                _propMenuPositions[6].Amount = value;
                RaisePropertyChanged("PropMenuPosAmount6");

                // jesli jest wybrany przedmiot i wypelniona jest liczba dni to oblicz sume
                if (PropMenuTypeOfServ6 != null && PropMenuPosDays6 != null)
                {
                    ThirdTabNettoValue6 = ComputeNettoValue((decimal)ThirdTabNettoPrice6, PropMenuPosAmount6,
                        PropMenuPosDays6);
                    ThirdTabBruttoValue6 = ComputeBruttoValue(PropMenuPosBrutto6, PropMenuPosAmount6,
                        PropMenuPosDays6);
                }
            }
        }

        public int? PropMenuPosDays0
        {
            get { return _propMenuPositions[0].Days; }
            set
            {
                _propMenuPositions[0].Days = value;
                RaisePropertyChanged("PropMenuPosDays0");

                // jesli jest wybrany element i ilosc to aktualizuj sume netto i brutto elementu
                if (PropMenuPosAmount0 != null && PropMenuPosDays0 != null)
                {
                    ThirdTabNettoValue0 = ComputeNettoValue((decimal)ThirdTabNettoPrice0, PropMenuPosAmount0,
                        PropMenuPosDays0);
                    ThirdTabBruttoValue0 = ComputeBruttoValue(PropMenuPosBrutto0, PropMenuPosAmount0,
                        PropMenuPosDays0);
                }
            }
        }
        public int? PropMenuPosDays1
        {
            get { return _propMenuPositions[1].Days; }
            set
            {
                _propMenuPositions[1].Days = value;
                RaisePropertyChanged("PropMenuPosDays1");

                // jesli jest wybrany element i ilosc to aktualizuj sume netto i brutto elementu
                if (PropMenuPosAmount1 != null && PropMenuPosDays1 != null)
                {
                    ThirdTabNettoValue1 = ComputeNettoValue((decimal)ThirdTabNettoPrice1, PropMenuPosAmount1,
                        PropMenuPosDays1);
                    ThirdTabBruttoValue1 = ComputeBruttoValue(PropMenuPosBrutto1, PropMenuPosAmount1,
                        PropMenuPosDays1);
                }
            }
        }
        public int? PropMenuPosDays2
        {
            get { return _propMenuPositions[2].Days; }
            set
            {
                _propMenuPositions[2].Days = value;
                RaisePropertyChanged("PropMenuPosDays2");

                // jesli jest wybrany element i ilosc to aktualizuj sume netto i brutto elementu
                if (PropMenuPosAmount2 != null && PropMenuPosDays2 != null)
                {
                    ThirdTabNettoValue2 = ComputeNettoValue((decimal)ThirdTabNettoPrice2, PropMenuPosAmount2,
                        PropMenuPosDays2);
                    ThirdTabBruttoValue2 = ComputeBruttoValue(PropMenuPosBrutto2, PropMenuPosAmount2,
                        PropMenuPosDays2);
                }
            }
        }
        public int? PropMenuPosDays3
        {
            get { return _propMenuPositions[3].Days; }
            set
            {
                _propMenuPositions[3].Days = value;
                RaisePropertyChanged("PropMenuPosDays3");

                // jesli jest wybrany element i ilosc to aktualizuj sume netto i brutto elementu
                if (PropMenuPosAmount3 != null && PropMenuPosDays3 != null)
                {
                    ThirdTabNettoValue3 = ComputeNettoValue((decimal)ThirdTabNettoPrice3, PropMenuPosAmount3,
                        PropMenuPosDays3);
                    ThirdTabBruttoValue3 = ComputeBruttoValue(PropMenuPosBrutto3, PropMenuPosAmount3,
                        PropMenuPosDays3);
                }
            }
        }
        public int? PropMenuPosDays4
        {
            get { return _propMenuPositions[4].Days; }
            set
            {
                _propMenuPositions[4].Days = value;
                RaisePropertyChanged("PropMenuPosDays4");

                // jesli jest wybrany element i ilosc to aktualizuj sume netto i brutto elementu
                if (PropMenuPosAmount4 != null && PropMenuPosDays4 != null)
                {
                    ThirdTabNettoValue4 = ComputeNettoValue((decimal)ThirdTabNettoPrice4, PropMenuPosAmount4,
                        PropMenuPosDays4);
                    ThirdTabBruttoValue4 = ComputeBruttoValue(PropMenuPosBrutto4, PropMenuPosAmount4,
                        PropMenuPosDays4);
                }
            }
        }
        public int? PropMenuPosDays5
        {
            get { return _propMenuPositions[5].Days; }
            set
            {
                _propMenuPositions[5].Days = value;
                RaisePropertyChanged("PropMenuPosDays5");

                // jesli jest wybrany element i ilosc to aktualizuj sume netto i brutto elementu
                if (PropMenuPosAmount5 != null && PropMenuPosDays5 != null)
                {
                    ThirdTabNettoValue5 = ComputeNettoValue((decimal)ThirdTabNettoPrice5, PropMenuPosAmount5,
                        PropMenuPosDays5);
                    ThirdTabBruttoValue5 = ComputeBruttoValue(PropMenuPosBrutto5, PropMenuPosAmount5,
                        PropMenuPosDays5);
                }
            }
        }
        public int? PropMenuPosDays6
        {
            get { return _propMenuPositions[5].Days; }
            set
            {
                _propMenuPositions[5].Days = value;
                RaisePropertyChanged("PropMenuPosDays6");

                // jesli jest wybrany element i ilosc to aktualizuj sume netto i brutto elementu
                if (PropMenuPosAmount6 != null && PropMenuPosDays6 != null)
                {
                    ThirdTabNettoValue6 = ComputeNettoValue((decimal)ThirdTabNettoPrice6, PropMenuPosAmount6,
                        PropMenuPosDays6);
                    ThirdTabBruttoValue6 = ComputeBruttoValue(PropMenuPosBrutto6, PropMenuPosAmount6,
                        PropMenuPosDays6);
                }
            }
        }

        public float? PropMenuMerge0
        {
            get { return _propMenuMerges[0].DefaultValue; }
            set
            {

                // uaktualnienie ceny przedmiotu:
                // obecna wyswitlana cena brutto i netto jest cena wraz z marza
                // w celu zmiany marzy (integerUpDown) nalezy odjac obecna marze
                // a nastepnie dodac obecna (value)
                _propMenuMerges[0].DefaultValue = value;
                RaisePropertyChanged("PropMenuMerge0");

                if (PropMenuTypeOfServ0 != null && _propMenuMerges[0].MergeType == PropMenuPosMergeType0)
                    PropMenuTypeOfServ0 = _propMenuPositions[0].TypeOfService;
                if (PropMenuTypeOfServ1 != null && _propMenuMerges[0].MergeType == PropMenuPosMergeType1)
                    PropMenuTypeOfServ1 = _propMenuPositions[1].TypeOfService;
                if (PropMenuTypeOfServ2 != null && _propMenuMerges[0].MergeType == PropMenuPosMergeType2)
                    PropMenuTypeOfServ2 = _propMenuPositions[2].TypeOfService;
                if (PropMenuTypeOfServ3 != null && _propMenuMerges[0].MergeType == PropMenuPosMergeType3)
                    PropMenuTypeOfServ3 = _propMenuPositions[3].TypeOfService;
                if (PropMenuTypeOfServ4 != null && _propMenuMerges[0].MergeType == PropMenuPosMergeType4)
                    PropMenuTypeOfServ4 = _propMenuPositions[4].TypeOfService;
                if (PropMenuTypeOfServ5 != null && _propMenuMerges[0].MergeType == PropMenuPosMergeType5)
                    PropMenuTypeOfServ5 = _propMenuPositions[5].TypeOfService;
                if (PropMenuTypeOfServ6 != null && _propMenuMerges[0].MergeType == PropMenuPosMergeType6)
                    PropMenuTypeOfServ6 = _propMenuPositions[6].TypeOfService;
            }
        }


        public float? PropMenuMerge1
        {
            get { return _propMenuMerges[1].DefaultValue; }
            set
            {
                // uaktualnienie ceny przedmiotu:
                // obecna wyswitlana cena brutto i netto jest cena wraz z marza
                // w celu zmiany marzy (integerUpDown) nalezy odjac obecna marze
                // a nastepnie dodac obecna (value)

                _propMenuMerges[1].DefaultValue = value;
                RaisePropertyChanged("PropMenuMerge1");
           
                if (PropMenuTypeOfServ0 != null && _propMenuMerges[1].MergeType == PropMenuPosMergeType0)
                    PropMenuTypeOfServ0 = _propMenuPositions[0].TypeOfService;
                if (PropMenuTypeOfServ1 != null && _propMenuMerges[1].MergeType == PropMenuPosMergeType1)
                    PropMenuTypeOfServ1 = _propMenuPositions[1].TypeOfService;
                if (PropMenuTypeOfServ2 != null && _propMenuMerges[1].MergeType == PropMenuPosMergeType2)
                    PropMenuTypeOfServ2 = _propMenuPositions[2].TypeOfService;
                if (PropMenuTypeOfServ3 != null && _propMenuMerges[1].MergeType == PropMenuPosMergeType3)
                    PropMenuTypeOfServ3 = _propMenuPositions[3].TypeOfService;
                if (PropMenuTypeOfServ4 != null && _propMenuMerges[1].MergeType == PropMenuPosMergeType4)
                    PropMenuTypeOfServ4 = _propMenuPositions[4].TypeOfService;
                if (PropMenuTypeOfServ5 != null && _propMenuMerges[1].MergeType == PropMenuPosMergeType5)
                    PropMenuTypeOfServ5 = _propMenuPositions[5].TypeOfService;
                if (PropMenuTypeOfServ6 != null && _propMenuMerges[1].MergeType == PropMenuPosMergeType6)
                    PropMenuTypeOfServ6 = _propMenuPositions[6].TypeOfService;
            }
        }
        public float? PropMenuMerge2
        {
            get { return _propMenuMerges[2].DefaultValue; }
            set
            {
                // uaktualnienie ceny przedmiotu:
                // obecna wyswitlana cena brutto i netto jest cena wraz z marza
                // w celu zmiany marzy (integerUpDown) nalezy odjac obecna marze
                // a nastepnie dodac obecna (value)

                _propMenuMerges[2].DefaultValue = value;
                RaisePropertyChanged("PropMenuMerge2");

                if (PropMenuTypeOfServ0 != null && _propMenuMerges[2].MergeType == PropMenuPosMergeType0)
                    PropMenuTypeOfServ0 = _propMenuPositions[0].TypeOfService;
                if (PropMenuTypeOfServ1 != null && _propMenuMerges[2].MergeType == PropMenuPosMergeType1)
                    PropMenuTypeOfServ1 = _propMenuPositions[1].TypeOfService;
                if (PropMenuTypeOfServ2 != null && _propMenuMerges[2].MergeType == PropMenuPosMergeType2)
                    PropMenuTypeOfServ2 = _propMenuPositions[2].TypeOfService;
                if (PropMenuTypeOfServ3 != null && _propMenuMerges[2].MergeType == PropMenuPosMergeType3)
                    PropMenuTypeOfServ3 = _propMenuPositions[3].TypeOfService;
                if (PropMenuTypeOfServ4 != null && _propMenuMerges[2].MergeType == PropMenuPosMergeType4)
                    PropMenuTypeOfServ4 = _propMenuPositions[4].TypeOfService;
                if (PropMenuTypeOfServ5 != null && _propMenuMerges[2].MergeType == PropMenuPosMergeType5)
                    PropMenuTypeOfServ5 = _propMenuPositions[5].TypeOfService;
                if (PropMenuTypeOfServ6 != null && _propMenuMerges[2].MergeType == PropMenuPosMergeType6)
                    PropMenuTypeOfServ6 = _propMenuPositions[6].TypeOfService;
            }
        }
        public float? PropMenuMerge3
        {
            get { return _propMenuMerges[3].DefaultValue; }
            set
            {
                // uaktualnienie ceny przedmiotu:
                // obecna wyswitlana cena brutto i netto jest cena wraz z marza
                // w celu zmiany marzy (integerUpDown) nalezy odjac obecna marze
                // a nastepnie dodac obecna (value)

                _propMenuMerges[3].DefaultValue = value;
                RaisePropertyChanged("PropMenuMerge3");

                if (PropMenuTypeOfServ0 != null && _propMenuMerges[3].MergeType == PropMenuPosMergeType0)
                    PropMenuTypeOfServ0 = _propMenuPositions[0].TypeOfService;
                if (PropMenuTypeOfServ1 != null && _propMenuMerges[3].MergeType == PropMenuPosMergeType1)
                    PropMenuTypeOfServ1 = _propMenuPositions[1].TypeOfService;
                if (PropMenuTypeOfServ2 != null && _propMenuMerges[3].MergeType == PropMenuPosMergeType2)
                    PropMenuTypeOfServ2 = _propMenuPositions[2].TypeOfService;
                if (PropMenuTypeOfServ3 != null && _propMenuMerges[3].MergeType == PropMenuPosMergeType3)
                    PropMenuTypeOfServ3 = _propMenuPositions[3].TypeOfService;
                if (PropMenuTypeOfServ4 != null && _propMenuMerges[3].MergeType == PropMenuPosMergeType4)
                    PropMenuTypeOfServ4 = _propMenuPositions[4].TypeOfService;
                if (PropMenuTypeOfServ5 != null && _propMenuMerges[3].MergeType == PropMenuPosMergeType5)
                    PropMenuTypeOfServ5 = _propMenuPositions[5].TypeOfService;
                if (PropMenuTypeOfServ6 != null && _propMenuMerges[3].MergeType == PropMenuPosMergeType6)
                    PropMenuTypeOfServ6 = _propMenuPositions[6].TypeOfService;
            }
        }
        public float? PropMenuMerge4
        {
            get { return _propMenuMerges[4].DefaultValue; }
            set
            {
                // uaktualnienie ceny przedmiotu:
                // obecna wyswitlana cena brutto i netto jest cena wraz z marza
                // w celu zmiany marzy (integerUpDown) nalezy odjac obecna marze
                // a nastepnie dodac obecna (value)

                _propMenuMerges[4].DefaultValue = value;
                RaisePropertyChanged("PropMenuMerge4");

                if (PropMenuTypeOfServ0 != null && _propMenuMerges[4].MergeType == PropMenuPosMergeType0)
                    PropMenuTypeOfServ0 = _propMenuPositions[0].TypeOfService;
                if (PropMenuTypeOfServ1 != null && _propMenuMerges[4].MergeType == PropMenuPosMergeType1)
                    PropMenuTypeOfServ1 = _propMenuPositions[1].TypeOfService;
                if (PropMenuTypeOfServ2 != null && _propMenuMerges[4].MergeType == PropMenuPosMergeType2)
                    PropMenuTypeOfServ2 = _propMenuPositions[2].TypeOfService;
                if (PropMenuTypeOfServ3 != null && _propMenuMerges[4].MergeType == PropMenuPosMergeType3)
                    PropMenuTypeOfServ3 = _propMenuPositions[3].TypeOfService;
                if (PropMenuTypeOfServ4 != null && _propMenuMerges[4].MergeType == PropMenuPosMergeType4)
                    PropMenuTypeOfServ4 = _propMenuPositions[4].TypeOfService;
                if (PropMenuTypeOfServ5 != null && _propMenuMerges[4].MergeType == PropMenuPosMergeType5)
                    PropMenuTypeOfServ5 = _propMenuPositions[5].TypeOfService;
                if (PropMenuTypeOfServ6 != null && _propMenuMerges[4].MergeType == PropMenuPosMergeType6)
                    PropMenuTypeOfServ6 = _propMenuPositions[6].TypeOfService;
            }
        }

        public List<PropMenuMerge> PropMenuMerges
        {
            get { return _propMenuMerges; }
            set
            {
                _propMenuMerges = value;
                RaisePropertyChanged("PropMenuMerges");
            }
        }

        public List<decimal> ThirdTabNettoValue
        {
            get { return _thirdTabNettoValue; }
            set
            {
                _thirdTabNettoValue = value;
                RaisePropertyChanged("ThirdTabNettoValue");              
            }
        }

        public List<decimal> ThirdTabBruttoValue
        {
            get { return _thirdTabBruttoValue; }
            set
            {
                _thirdTabBruttoValue = value;
                RaisePropertyChanged("ThirdTabBruttoValue");          
            }
        }

                public decimal ThirdTabNettoValue0
        {
            get { return _thirdTabNettoValue[0]; }
            set
            {
                _thirdTabNettoValue[0] = value;
                RaisePropertyChanged("ThirdTabNettoValue0");
            }
        }
        public decimal ThirdTabNettoValue1
        {
            get { return _thirdTabNettoValue[1]; }
            set
            {
                _thirdTabNettoValue[1] = value;
                RaisePropertyChanged("ThirdTabNettoValue1");
            }
        }
        public decimal ThirdTabNettoValue2
        {
            get { return _thirdTabNettoValue[2]; }
            set
            {
                _thirdTabNettoValue[2] = value;
                RaisePropertyChanged("ThirdTabNettoValue2");
            }
        }
        public decimal ThirdTabNettoValue3
        {
            get { return _thirdTabNettoValue[3]; }
            set
            {
                _thirdTabNettoValue[3] = value;
                RaisePropertyChanged("ThirdTabNettoValue3");
            }
        }
        public decimal ThirdTabNettoValue4
        {
            get { return _thirdTabNettoValue[4]; }
            set
            {
                _thirdTabNettoValue[4] = value;
                RaisePropertyChanged("ThirdTabNettoValue4");
            }
        }
        public decimal ThirdTabNettoValue5
        {
            get { return _thirdTabNettoValue[5]; }
            set
            {
                _thirdTabNettoValue[5] = value;
                RaisePropertyChanged("ThirdTabNettoValue5");
            }
        }
        public decimal ThirdTabNettoValue6
        {
            get { return _thirdTabNettoValue[6]; }
            set
            {
                _thirdTabNettoValue[6] = value;
                RaisePropertyChanged("ThirdTabNettoValue6");
            }
        }

        public decimal ThirdTabBruttoValue0
        {
            get { return _thirdTabBruttoValue[0]; }
            set
            {
                _thirdTabBruttoValue[0] = value;
                RaisePropertyChanged("ThirdTabBruttoValue0");
                ComputeThirdTabSumBruttoValue();
                ComputeThirdTabSumNettoValue();
            }
        }
        public decimal ThirdTabBruttoValue1
        {
            get { return _thirdTabBruttoValue[1]; }
            set
            {
                _thirdTabBruttoValue[1] = value;
                RaisePropertyChanged("ThirdTabBruttoValue1");
                ComputeThirdTabSumBruttoValue();
                ComputeThirdTabSumNettoValue();
            }
        }
        public decimal ThirdTabBruttoValue2
        {
            get { return _thirdTabBruttoValue[2]; }
            set
            {
                _thirdTabBruttoValue[2] = value;
                RaisePropertyChanged("ThirdTabBruttoValue2");
                ComputeThirdTabSumBruttoValue();
                ComputeThirdTabSumNettoValue();
            }
        }
        public decimal ThirdTabBruttoValue3
        {
            get { return _thirdTabBruttoValue[3]; }
            set
            {
                _thirdTabBruttoValue[3] = value;
                RaisePropertyChanged("ThirdTabBruttoValue3");
                ComputeThirdTabSumBruttoValue();
                ComputeThirdTabSumNettoValue();
            }
        }
        public decimal ThirdTabBruttoValue4
        {
            get { return _thirdTabBruttoValue[4]; }
            set
            {
                _thirdTabBruttoValue[4] = value;
                RaisePropertyChanged("ThirdTabBruttoValue4");
                ComputeThirdTabSumBruttoValue();
                ComputeThirdTabSumNettoValue();
            }
        }
        public decimal ThirdTabBruttoValue5
        {
            get { return _thirdTabBruttoValue[5]; }
            set
            {
                _thirdTabBruttoValue[5] = value;
                RaisePropertyChanged("ThirdTabBruttoValue5");
                ComputeThirdTabSumBruttoValue();
                ComputeThirdTabSumNettoValue();
            }
        }
        public decimal ThirdTabBruttoValue6
        {
            get { return _thirdTabBruttoValue[6]; }
            set
            {
                _thirdTabBruttoValue[6] = value;
                RaisePropertyChanged("ThirdTabBruttoValue6");
                ComputeThirdTabSumBruttoValue();
                ComputeThirdTabSumNettoValue();
            }
        }

        public decimal ThirdTabSumNettoValue
        {
            get { return _thirdTabSumNettoValue; }
            set
            {
                _thirdTabSumNettoValue = value;
                RaisePropertyChanged("ThirdTabSumNettoValue");
                
            }
        }

        public decimal ThirdTabSumBruttoValue
        {
            get { return _thirdTabSumBruttoValue; }
            set
            {
                _thirdTabSumBruttoValue = value;
                RaisePropertyChanged("ThirdTabSumBruttoValue");
            }
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

            // wypelnianie listy dodatkowego wyposazenia sali 2tab
            var propHallEqList = (from he in _ctx.PropHallEquipmnet_Dictionary_Second
                select he.Things).ToList();
            PropHallEqDict2 = propHallEqList;

            var vat = (from he in _ctx.VatList
             select he.Vat).ToList();
            VatList = vat;

            //tab3
            // wypelnianie listy rzeczy gastro.
            var gastThingDict = (from gt in _ctx.PropMenuGastronomicThings_Dictionary_First
                select gt.ThingName).ToList();
            PropMenuGastThingDict = gastThingDict;

            // wypelnienie domyslnych marzy
            var merges = (from m in _ctx.PropMenuMerge_Dictionary_First
                select m).ToList();
            PropMenuMerge0 = merges[0].Value;
            PropMenuMerge1 = merges[1].Value;
            PropMenuMerge2 = merges[2].Value;
            PropMenuMerge3 = merges[3].Value;
            PropMenuMerge4 = merges[4].Value;

            _propMenuMerges[0].MergeName = merges[0].MergeName;
            _propMenuMerges[1].MergeName = merges[1].MergeName;
            _propMenuMerges[2].MergeName = merges[2].MergeName;
            _propMenuMerges[3].MergeName = merges[3].MergeName;
            _propMenuMerges[4].MergeName = merges[4].MergeName;

            var mergetype = (from m in _ctx.PropMergeTypes_Dictionary
                select m).ToList();
            _propMenuMerges[0].MergeType = mergetype[0].MergeType;
            _propMenuMerges[1].MergeType = mergetype[1].MergeType;
            _propMenuMerges[2].MergeType = mergetype[2].MergeType;
            _propMenuMerges[3].MergeType = mergetype[3].MergeType;
            _propMenuMerges[4].MergeType = mergetype[4].MergeType;

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
                PropositionClient.Id_proposition = currentPropositionId;
                _ctx.PropClient.Add(PropositionClient);

                //------------------------------
                // !! PROPRESERVATIONDETAILS !!
                PropositionReservDetails.Id_proposition = currentPropositionId;
                _ctx.PropReservationDetails.Add(PropositionReservDetails);

                //------------------------------
                // !! PROPHALLEQUPMENT !!

                // dodaje do bazy tylko te elementy listy, które posiadaja nazwe i cene brutto TODO: review solution
                for (int i = 0; i < _propHallEquipment.Count; i++)
                {
                    if (_propHallEquipment[i].Things != null && _propHallEquipment[i].BruttoPrice != null)
                    {
                        _propHallEquipment[i].Id_proposition = currentPropositionId;
                        _ctx.PropHallEquipment.Add(_propHallEquipment[i]);
                    }
                }
                //------------------------------
                // !! PROPHALLEQUPMENTDISCOUNT !!
                HallEquipmentDiscount.Id_proposition = currentPropositionId;
                _ctx.PropHallEquipmentDiscount.Add(HallEquipmentDiscount);

                //------------------------------
                // !! PROPMENUMERGES !!
                for (int i = 0; i < PropMenuMerges.Count; i++)
                {
                    PropMenuMerges[i].Id_proposition = currentPropositionId;
                    _ctx.PropMenuMerge.Add(PropMenuMerges[i]);
                }
                // !! PROPMENUPOSITIONS !!
                for (int i = 0; i < _propMenuPositions.Count; i++)
                {
                    if (_propMenuPositions[i].TypeOfService != null && _propMenuPositions[i].Amount != null &&
                        _propMenuPositions[i].Days != null)
                    {
                        _propMenuPositions[i].Id_proposition = currentPropositionId;
                        _ctx.PropMenuPosition.Add(_propMenuPositions[i]);
                    }
                }


                _ctx.SaveChanges();
                MessageBox.Show("dodano nowa propozycje");

                // po dodaniu propozycji odśwież listę propozycji
                SelectAllPropositions();
            }
            else
            {
                //               // Wybrany Id Propozycji
                // int currentPropositionId = SelectedProposition.PropositionId;

                //Edycja Propozycji
                // !! PROPCLIENT !!

                int idProposition = _idProposition;
                var editClient = (from q in _ctx.PropClient
                                  where q.Id_proposition == idProposition
                                  select q).SingleOrDefault();
                if (editClient != null)
                {
                    //editClient.Id_proposition = idProposition;
                    editClient.CompanyName = PropositionClient.CompanyName;
                    editClient.CompanyAdress = PropositionClient.CompanyAdress;
                    editClient.NIP = PropositionClient.NIP;
                    editClient.CustomerFullName = PropositionClient.CustomerFullName;
                    editClient.PhoneNum = PropositionClient.PhoneNum;

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


                }
                //-Edycja----------------------
                // !! PROPRESERVATIONDETAILS !!

                var propReservation = (from q in _ctx.PropReservationDetails
                                       where q.Id_proposition == idProposition
                                       select q).SingleOrDefault();
                if (propReservation == null)
                {
                    PropReservationDetails addPropReservationDetails = new PropReservationDetails();
                    addPropReservationDetails.Id_proposition = idProposition;
                    addPropReservationDetails.StartData = PropositionReservDetails.StartData;
                    addPropReservationDetails.EndData = PropositionReservDetails.EndData;
                    addPropReservationDetails.Hall = PropositionReservDetails.Hall;
                    addPropReservationDetails.HallSetting = PropositionReservDetails.HallSetting;
                    addPropReservationDetails.PeopleNumber = PropositionReservDetails.PeopleNumber;
                    addPropReservationDetails.EndTime = PropositionReservDetails.EndTime;
                    addPropReservationDetails.StartTime = PropositionReservDetails.StartTime;
                    addPropReservationDetails.Proposition = PropositionReservDetails.Proposition;
                    _ctx.PropReservationDetails.Add(addPropReservationDetails);

                }
                else
                {
                    // propReservation.Id_proposition = idProposition;
                    propReservation.StartData = PropositionReservDetails.StartData;
                    propReservation.EndData = PropositionReservDetails.EndData;
                    propReservation.Hall = PropositionReservDetails.Hall;
                    propReservation.HallSetting = PropositionReservDetails.HallSetting;
                    propReservation.PeopleNumber = PropositionReservDetails.PeopleNumber;
                    propReservation.EndTime = PropositionReservDetails.EndTime;
                    propReservation.StartTime = PropositionReservDetails.StartTime;
                    propReservation.Proposition = PropositionReservDetails.Proposition;

                }
                _ctx.SaveChanges();
                SelectedProposition = null;
                SelectAllPropositions();
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
        private bool CanShowUsersExecute(object arg)
        {
            return true;
        }
        private void ShowUsersExecute(object obj)
        {
            SelectAllUsers();
        }

        //Edycja porpozycji
        private ICommand _changePropositionCommand;
       

        private bool CanChangePropositionExecute(object arg)
        {
            return true;
        }
        private void ChangePropositionExecute(object obj)
        {
            _saveFlag = true;
            var hallDict1 = (from hd in _ctx.PropReservationDetails_Dictionary_HallCapacity
                             select hd.Hall).ToList();
            HallList = hallDict1;
            try
            {
                _idProposition = SelectedProposition.PropositionId;
                // wyszukanie dla podanego id propozycji klienta
                SelectedProposition = null;
                var editClient = (from q in _ctx.PropClient
                                  where q.Id_proposition == _idProposition
                                  select q).SingleOrDefault();

                //Uzupełnieni właściwości klienta
                PropositionClientCompanyName = editClient.CompanyName;
                PropositionClientCompanyAdress = editClient.CompanyAdress;
                PropositionClientCustromerFullName = editClient.CustomerFullName;
                PropositionClientPhoneNum = editClient.PhoneNum;
                PropositionClientNip = editClient.NIP;
                PropositionClientDecisingPerFullName = editClient.DecisingPersonFullName;
                PropositionClientCustomerEmail = editClient.CustomerEmail;
                PropositionClient = editClient;

                //detale rezerwacji
                var editDetalis = (from q in _ctx.PropReservationDetails
                                   where q.Id_proposition == _idProposition
                                   select q).SingleOrDefault();

                PropositionReservDetailsStartData = editDetalis.StartData;
                PropositionReservDetailsEndData = editDetalis.EndData;
                PropositionReservDetailsStartTime = editDetalis.StartTime;
                PropositionReservDetailsEndTime = editDetalis.EndTime;
                PropositionReservDetailsHall = editDetalis.Hall;
                PropositionReservDetailsHallSetting = editDetalis.HallSetting;
                PropositionReservDetailsPeopleNumber = editDetalis.PeopleNumber;
                PropositionReservDetails = editDetalis;
            }
            catch (Exception ex)
            {
                MessageBox.Show("no i poszło na grzybki");
                _saveFlag = false;
            }
        }
        private ICommand _editDictionaryCommand;

        public ICommand EditDictionaryCommand
        {
            get
            {
                if (_editDictionaryCommand == null)
                {
                    _editDictionaryCommand = new RelayCommand(EditDictionaryExecute, CanEditDictionarExecute);
                }
                return _editDictionaryCommand;
            }

        }

        private bool CanEditDictionarExecute(object art)
        {
            return true;
        }
        private void EditDictionaryExecute(object obj)
        {
            DictionaryView edit = new DictionaryView();
            edit.Show();
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

        /* Wyszukiwanie wszystkich danych potrzebnych do wypełnienia grida admina
         * - wszystkie propozycje wszystkich użytkowników posortowane według daty
         * (imie+nazwisko użytkownika , imie+nazwisko klienta, nazwa firmy klienta, data aktualizacji*/
        private void SelectAllPropositions()
        {
            var myQuerry = (from prop in _ctx.Proposition
                from user in _ctx.Users
                where prop.Id_user == user.Id
                orderby prop.UpdateDate descending, user.Name descending
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

        // Metoda ustawiajace cene sali po znizce + ustawiajaca cene brutto wybranej sali
        private void SetDiscountPrice()
        {
            if (PropHallEquipmentDiscountValue.HasValue && PropHallEquipmentDiscountStandPrice.HasValue)
            {
                ComputePriceAfterDiscount = Math.Ceiling((decimal) PropHallEquipmentDiscountStandPrice -
                                                         ((decimal) PropHallEquipmentDiscountStandPrice*
                                                          (decimal) PropHallEquipmentDiscountValue/100));
                PropHallEqBrutto0 = (float?)ComputePriceAfterDiscount;
            }
            else if (!PropHallEquipmentDiscountValue.HasValue)
            {
                PropHallEquipmentDiscountValue = 0;
            }
        }

        // wypelnianie zadeklarowanych pustych list 
        private void FillNeededList()
        {
            //PropHallEquipmentList
            for (int i = 0; i < _propHallEquipment.Capacity; i++)
                _propHallEquipment.Add(new PropHallEquipment());

            //SecondTabNettoPriceList
            for (int i = 0; i < _secondTabNettoPrice.Capacity; i++)
                _secondTabNettoPrice.Add(new decimal());

            //SecondTabNettoValueList
            for (int i = 0; i < _secondTabNettoValue.Capacity; i++)
                _secondTabNettoValue.Add(new decimal());

            //SecondTabBruttoValueList
            for (int i = 0; i < _secondTabNettoPrice.Capacity; i++)
                _secondTabBruttoValue.Add(new decimal());

            //PropMenuPositions
            for (int i = 0; i < _propMenuPositions.Capacity; i++)
                _propMenuPositions.Add(new PropMenuPosition());

            //ThirdTabNettoPriceList
            for (int i = 0; i < _thirdTabNettoPrice.Capacity; i++)
                _thirdTabNettoPrice.Add(new decimal?());

            //PropMenuMerge
            for (int i = 0; i < _propMenuMerges.Capacity; i++)
                _propMenuMerges.Add(new PropMenuMerge());
       //     FillPropMenuMergeList(_propMenuMerges);

            //Default merges dictionary
            for (int i = 0; i < _defaultMerges.Capacity; i++)
                _defaultMerges.Add("");

            //ThirdTabNettoValueList
            for (int i = 0; i < _thirdTabNettoValue.Capacity; i++)
                _thirdTabNettoValue.Add(new decimal());

            //ThirdTabBruttoValueList
            for (int i = 0; i < _thirdTabBruttoValue.Capacity; i++)
                _thirdTabBruttoValue.Add(new decimal());
        }

//        private void FillPropMenuMergeList(List<PropMenuMerge> propMenuMerges)
//        {
//            throw new NotImplementedException();
//        }

        // obliczanie ceny netto na podstawie ceny brutto i vatu (tab2)PropMenuMerge0
        private decimal ComputeNettoPrice(float? value, float? vat)
        {
            return Math.Round(((decimal) value * 100 /(100 + (decimal) vat)), 2);
        }

        // obliczanie zsumowanej wartosci brutto na podstawie ceny brutto, ilosci i liczby dni
        // nully sprawdzane musza byc przed wywolaniem metody (tab2)
        private decimal ComputeBruttoValue(float? bruttoPrice, int? amount, int? days)
        {
            return (decimal)bruttoPrice * (decimal)amount * (decimal)days;
        }

        // obliczanie zsumowanej wartosci netto na podstawie ceny netto, ilosci i liczby dni
        // nully sprawdzane musza byc przed wywolaniem metody (tab2)
        private decimal ComputeNettoValue(decimal nettoPrice, int? amount, int? days)
        {
            return (decimal)nettoPrice * (decimal)amount * (decimal)days;
        }

        // obliczanie sumy netto (tab2)
        private void ComputeSecondTabSumNettoValue()
        {
            decimal sum = 0;
            sum += SecondTabNettoValue0;
            sum += SecondTabNettoValue1;
            sum += SecondTabNettoValue2;
            sum += SecondTabNettoValue3;
            sum += SecondTabNettoValue4;
            sum += SecondTabNettoValue5; 

            SecondTabSumNettoValue = sum;
        }

        // obliczanie sumy brutto (tab2)
        private void ComputeSecondTabSumBruttoValue()
        {
            decimal sum = 0;
            sum += SecondTabBruttoValue0;
            sum += SecondTabBruttoValue1;
            sum += SecondTabBruttoValue2;
            sum += SecondTabBruttoValue3;
            sum += SecondTabBruttoValue4;
            sum += SecondTabBruttoValue5;

            SecondTabSumBruttoValue = sum;
        }


        //tab3

        // na podstawie nazwy produktu ustaw podstawowa stawke vat
        private float? SetMenuPosDefaultVat(string typeofservice)
        {
            var mvat = (from s in _ctx.PropMenuGastronomicThings_Dictionary_First
                where typeofservice == s.ThingName
                select s.Vat);
            return mvat.Single();
        }

        // na podstawie nazwy produktu ustaw cene netto
        private decimal? SetThirdDefaultNettoPrice(string typeofservice)
        {
            var thirdnetto = (from s in _ctx.PropMenuGastronomicThings_Dictionary_First
                        where typeofservice == s.ThingName
                        select s.NettoMini);
            return (decimal?)thirdnetto.Single();
        }

        // na podstawie nazwy produktu ustaw cene brutto
        private float? SetMenuPosDefaultBrutto(string typeofservice)
        {
            float? vat = SetMenuPosDefaultVat(typeofservice);
            var netto = (from s in _ctx.PropMenuGastronomicThings_Dictionary_First
                where typeofservice == s.ThingName
                select s.NettoMini).Single();
            float? toret = (float?) netto + (float?)netto*vat.Value/100;
            return toret;
        }

        // na podstawie nazwy produktu ustaw do domyslny typ marzy
        private string SetMenuPosDefaultMergeType(string typeofservice)
        {
            var mt = (from s in _ctx.PropMenuGastronomicThings_Dictionary_First
                where typeofservice == s.ThingName
                select s.MergeType).Single();
            return mt;
        }

        // na podstawie domyslnej ceny (netto lub brutto) dodaj marze na podstawie odpowiedniego typu
        private decimal? AddMergeToPrice(decimal? price, string mergeType)
        {
            var mergeValue =
                _propMenuMerges.Where(x => x.MergeType == mergeType)
                    .Select((x) => new { x = x.DefaultValue })
                    .Single();
            var toret = price + ((decimal)price * (decimal)mergeValue.x / 100);
            return toret;
        }

        // przeciazona powyzsza metoda
        private float? AddMergeToPrice(float? price, string mergeType)
        {
            var mergeValue =
                _propMenuMerges.Where(x => x.MergeType == mergeType)
                    .Select((x) => new { x = x.DefaultValue })
                    .Single();
            var toret = price + (price * mergeValue.x / 100);
            return toret;
        }

        // na podstawie domyslnej ceny (netto lub brutto) dodaj marze na podstawie odpowiedniego typu
        private decimal? SubMergeToPrice(decimal? price, string mergeType)
        {
            var mergeValue =
                _propMenuMerges.Where(x => x.MergeType == mergeType)
                    .Select((x) => new { x = x.DefaultValue })
                    .Single();
            var toret = price/(1 + (decimal?)mergeValue.x /100);
            return toret;
        }

        // przeciazona powyzsza metoda
        private float? SubMergeToPrice(float? price, string mergeType)
        {
            var mergeValue =
                _propMenuMerges.Where(x => x.MergeType == mergeType)
                    .Select((x) => new { x = x.DefaultValue })
                    .Single();
            var toret = price / (1 + mergeValue.x / 100);
            return toret;
        }

        // obliczanie sumy netto (tab3)
        private void ComputeThirdTabSumNettoValue()
        {
            decimal sum = 0;
            sum += ThirdTabNettoValue0;
            sum += ThirdTabNettoValue1;
            sum += ThirdTabNettoValue2;
            sum += ThirdTabNettoValue3;
            sum += ThirdTabNettoValue4;
            sum += ThirdTabNettoValue5;
            sum += ThirdTabNettoValue6;

            ThirdTabSumNettoValue = sum;
        }

        // obliczanie sumy brutto (tab3)
        private void ComputeThirdTabSumBruttoValue()
        {
            decimal sum = 0;
            sum += ThirdTabBruttoValue0;
            sum += ThirdTabBruttoValue1;
            sum += ThirdTabBruttoValue2;
            sum += ThirdTabBruttoValue3;
            sum += ThirdTabBruttoValue4;
            sum += ThirdTabBruttoValue5;
            sum += ThirdTabBruttoValue6;

            ThirdTabSumBruttoValue = sum;
        }
#endregion
    }
}
