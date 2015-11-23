using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using DiamondApp.EntityModel;
using DiamondApp.Tools;
using DiamondApp.DataGridObjectClasses;

namespace DiamondApp.ViewModels
{
    public class UserViewModel : ObservableObject
    {
        private DiamondDBEntities _ctx;

        private int _idProposition; //id propozycji
        private AdminProposition _selectedProposition;  // wyciagnieta pojedyńcza propozycja
        private bool _saveFlag = false; // czy edycja czy dodanie nowej jeśli nie zostały żadna wybrana


        private int _userId;
        // public List<Proposition> propositionList;
        private List<AdminProposition> _propositionList; // lista propozycji do wyświetlenia
        private ICommand _createNewPropositionCommand;  // dodanie nowej porpozycji  i uzupełnienie domyślnych danych
        private AddNewProposition _addNewProposition;   // domyślne dane o użytkowniku w propozycji
        private ICommand _savePropositionCommand; // zapis propozycji

        private Proposition _proposition;
        //private int _currentPropositionId;
        private ICommand _showPropositionsCommand;
        private PropClient _propositionClient = new PropClient(); // klient propozycji
        private PropReservationDetails _propositionReservDetails = new PropReservationDetails(); // detale
        private List<string> _hallList; // lista sal (controlbox 1tab)
        private ICommand _changePropositionCommand; // zmiana porpozycji 

        private int? _hallPrice;
        private string _eventMonth;
        private PropReservationDetails_Dictionary_HallCapacity _hallCapacity = new PropReservationDetails_Dictionary_HallCapacity();// obiekt zawierajacy dane wybranej sali (1 tab dol)
        private List<string> _hallSettingList; // lista sal (controlbox 1tab)
        //Wyposazenie dodatkowe
        private PropHallEquipmentDiscount _propHallEquipmentDiscount = new PropHallEquipmentDiscount();
        private decimal _propHallPriceAfterDiscount;
        private List<PropHallEquipment> _propHallEquipment = new List<PropHallEquipment>(6);
        private List<decimal> _secondTabNettoPrice = new List<decimal>(6);
        private List<decimal> _secondTabNettoValue = new List<decimal>(6);   // list zsumowanych cen netto (tab2)
        private List<decimal> _secondTabBruttoValue = new List<decimal>(6);   // list zsumowanych cen netto (tab2)
        private List<string> _propHallEqDict2; // lista zawierajaca wyposazenie dodatkowe sali (combobox tab 2)
        private List<float?> _vatList = new List<float?>(2); // lista zawierajaca wartosci VAT


        private decimal _secondTabSumNettoValue;    // suma wartosci netto (tab2)
        private decimal _secondTabSumBruttoValue;   // suma wartosci brutto (tab2)
        //3tab
        private List<string> _propMenuGastThingDict0;
        private List<string> _propMenuGastThingDict1;
        private List<string> _propMenuGastThingDict2;
        private List<string> _propMenuGastThingDict3;
        private List<string> _propMenuGastThingDict4;
        private List<string> _propMenuGastThingDict5;
        private List<string> _propMenuGastThingDict6;

        private List<PropMenuPosition> _propMenuPositions = new List<PropMenuPosition>(7);  // obiekt przechowujacy elementy uslug gastronomicznych
        private List<decimal?> _thirdTabNettoPrice = new List<decimal?>(7);  // lista cen netto (tab3)
        private List<PropMenuMerge> _propMenuMerges = new List<PropMenuMerge>(5);
        private List<string> _defaultMerges = new List<string>(5);  // lista domyslnych marzy
        private List<decimal> _thirdTabNettoValue = new List<decimal>(7);   // list zsumowanych cen netto (tab2)
        private List<decimal> _thirdTabBruttoValue = new List<decimal>(7);   // list zsumowanych cen netto (tab2)

        private List<string> _selectedType = new List<string>(7);
        private List<string> _filter = new List<string>(7);

        private decimal _thirdTabSumNettoValue;    // suma wartosci netto (tab3)
        private decimal _thirdTabSumBruttoValue;   // suma wartosci brutto (tab3)
        private List<string> roomExistList;

        //4 tab
        private List<PropAccomodation> _propAccomodations = new List<PropAccomodation>(6);
        private List<PropAccomodation_Dictionary> _propAccomDictionary = new List<PropAccomodation_Dictionary>();
        private PropAccomodationDiscount _propAccomDiscount = new PropAccomodationDiscount();
        private List<float?> _vatList4 = new List<float?>(2);
        private List<decimal?> _fourthTabNettoPrice = new List<decimal?>(6);  // lista cen netto (tab4)
        private List<decimal> _fourthTabNettoValue = new List<decimal>(6);   // list zsumowanych cen netto (tab4)
        private List<decimal> _fourthTabBruttoValue = new List<decimal>(6);   // list zsumowanych cen netto (tab4)

        private decimal _fourthTabSumNettoValue;    // suma wartosci netto (tab3)
        private decimal _fourthTabSumBruttoValue;   // suma wartosci brutto (tab3)

        //5 tab
        private List<string> _propPaySuggDictFirst; // zaplata combo 1
        private List<string> _propPaySuggDictSecond; // zaplata combo 2
        private List<string> _propPaySuggDictThird; // zaplata combo 3
        private List<string> _propPaySuggDictFourth; // zaplata combo 4
        private List<PropExtraServices> _propExtraServ = new List<PropExtraServices>(4);
        private PropPaymentSuggestions _propPaymentSugg = new PropPaymentSuggestions();
        private List<string> _propExtraServTypeDict;
        private List<decimal?> _fifthTabNettoPrice = new List<decimal?>(4);  // lista cen netto (tab5)

        private List<decimal> _fifthTabNettoValue = new List<decimal>(4);   // list zsumowanych cen netto (tab5)
        private List<decimal> _fifthTabBruttoValue = new List<decimal>(4);   // list zsumowanych cen netto (tab5)

        private decimal _fifthTabSumNettoValue;    // suma wartosci netto (tab5)
        private decimal _fifthTabSumBruttoValue;   // suma wartosci brutto (tab5)

        private decimal _fullSumNetto;    // suma wartosci netto (tab5)
        private decimal _fullSumBrutto;   // suma wartosci brutto (tab5)
        

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
            FillNeededList();
            SetDefaultValues();
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

                // zaktualizuj miesiąc wyswietlany w oknie tworzenia propozycji
                EventMonth = DateTimeConverter.ToMonthPolishName(value);
                RaisePropertyChanged("PropositionReservDetailsStartData");

                // jeśli wybrana jest juz nazwa sali to ustaw 
                if (PropositionReservDetailsHall != null)
                    PropHallEqThing0 = "Sala " + PropositionReservDetailsHall;

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

                // jeśli wybrana jest juz data poczatkowa wyswietl nazwe sali w tab2 poz1
                if (PropositionReservDetailsStartData.HasValue)
                    PropHallEqThing0 = "Sala " + value;

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
                if (value == null)
                    value = 0;
                _propHallEquipmentDiscount.Discount = value;
                RaisePropertyChanged("PropHallEquipmentDiscountValue");

                SetEquipmentDiscountPrice();

            }
        }
        public float? PropHallEquipmentDiscountStandPrice
        {
            get { return _propHallEquipmentDiscount.StandardPrice; }
            set
            {
                _propHallEquipmentDiscount.StandardPrice = value;
                RaisePropertyChanged("PropHallEquipmentDiscountStandPrice");

                SetEquipmentDiscountPrice();
            }
        }
        public decimal PropHallPriceAfterDiscount
        {
            get { return _propHallPriceAfterDiscount; }
            set
            {
                _propHallPriceAfterDiscount = value;
                RaisePropertyChanged("PropHallPriceAfterDiscount");
            }
        }

        #region tab2
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
                RaisePropertyChanged("PropHallEqThing1");
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
                if (PropHallEqBrutto0 != null)
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
                if (PropHallEqBrutto1 != null)
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
        #endregion tab2
        //tab 3
        #region tab3
        public List<float?> VatList
        {
            get { return _vatList; }
            set
            {
                _vatList = value;
                RaisePropertyChanged("VatList");
            }
        }

        public List<string> PropMenuGastThingDict0
        {
            get { return _propMenuGastThingDict0; }
            set
            {
                _propMenuGastThingDict0 = value;
                RaisePropertyChanged("PropMenuGastThingDict0");
            }
        }
        public List<string> PropMenuGastThingDict1
        {
            get { return _propMenuGastThingDict1; }
            set
            {
                _propMenuGastThingDict1 = value;
                RaisePropertyChanged("PropMenuGastThingDict1");
            }
        }
    
        public List<string> PropMenuGastThingDict2
        {
            get { return _propMenuGastThingDict2; }
            set
            {
                _propMenuGastThingDict2 = value;
                RaisePropertyChanged("PropMenuGastThingDict2");
            }
        }
        public List<string> PropMenuGastThingDict3
        {
            get { return _propMenuGastThingDict3; }
            set
            {
                _propMenuGastThingDict3 = value;
                RaisePropertyChanged("PropMenuGastThingDict3");
            }
        }
        public List<string> PropMenuGastThingDict4
        {
            get { return _propMenuGastThingDict4; }
            set
            {
                _propMenuGastThingDict4 = value;
                RaisePropertyChanged("PropMenuGastThingDict4");
            }
        }
        public List<string> PropMenuGastThingDict5
        {
            get { return _propMenuGastThingDict5; }
            set
            {
                _propMenuGastThingDict5 = value;
                RaisePropertyChanged("PropMenuGastThingDict5");
            }
        }
        public List<string> PropMenuGastThingDict6
        {
            get { return _propMenuGastThingDict6; }
            set
            {
                _propMenuGastThingDict6 = value;
                RaisePropertyChanged("PropMenuGastThingDict6");
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
            get { return _propMenuPositions[0].Vat; }
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
            get { return _propMenuPositions[1].Vat; }
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
            get { return _propMenuPositions[2].Vat; }
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
            get { return _propMenuPositions[3].Vat; }
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
            get { return _propMenuPositions[4].Vat; }
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
            get { return _propMenuPositions[5].Vat; }
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
            get { return _propMenuPositions[6].Vat; }
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
                    ThirdTabNettoValue0 = ComputeNettoValue((decimal)ThirdTabNettoPrice0, PropMenuPosAmount0,
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
                if (value == null)
                    value = 0;
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
                if (value == null)
                    value = 0;
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
                if (value == null)
                    value = 0;
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
                if (value == null)
                    value = 0;
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
                if (value == null)
                    value = 0;
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

        public List<string> Filter
        {
            get
            {
                return _filter;
            }
            set
            {
                _filter = value;
                RaisePropertyChanged("Filter");
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

        public string SelectedType0 {
            get { return _selectedType[0]; }
            set
            {
                
                _selectedType[0] = value;
                RaisePropertyChanged("SelectedType0");
                PropMenuGastThingDict0 = (from x in _ctx.PropMenuGastronomicThings_Dictionary_First
                               where x.SpecificType == value
                               select x.ThingName).ToList();
            }
        }

        public string SelectedType1
        {
            get { return _selectedType[1]; }
            set
            {
                _selectedType[1] = value;
                RaisePropertyChanged("SelectedType1");
                PropMenuGastThingDict1 = (from x in _ctx.PropMenuGastronomicThings_Dictionary_First
                                         where x.SpecificType == value
                                         select x.ThingName).ToList();
            }
        }
        public string SelectedType2
        {
            get { return _selectedType[2]; }
            set
            {
                _selectedType[2] = value;
                RaisePropertyChanged("SelectedType2");
                PropMenuGastThingDict2 = (from x in _ctx.PropMenuGastronomicThings_Dictionary_First
                                         where x.SpecificType == value
                                         select x.ThingName).ToList();
            }
        }
        public string SelectedType3
        {
            get { return _selectedType[3]; }
            set
            {
                _selectedType[3] = value;
                RaisePropertyChanged("SelectedType3");
                PropMenuGastThingDict3 = (from x in _ctx.PropMenuGastronomicThings_Dictionary_First
                                         where x.SpecificType == value
                                         select x.ThingName).ToList();
            }
        }
        public string SelectedType4
        {
            get { return _selectedType[4]; }
            set
            {
                _selectedType[4] = value;
                RaisePropertyChanged("SelectedType4");
                PropMenuGastThingDict4 = (from x in _ctx.PropMenuGastronomicThings_Dictionary_First
                                         where x.SpecificType == value
                                         select x.ThingName).ToList();
            }
        }
        public string SelectedType5
        {
            get { return _selectedType[5]; }
            set
            {
                _selectedType[5] = value;
                RaisePropertyChanged("SelectedType5");
                PropMenuGastThingDict5 = (from x in _ctx.PropMenuGastronomicThings_Dictionary_First
                                         where x.SpecificType == value
                                         select x.ThingName).ToList();
            }
        }
        public string SelectedType6
        {
            get { return _selectedType[6]; }
            set
            {
                _selectedType[6] = value;
                RaisePropertyChanged("SelectedType6");
                PropMenuGastThingDict6 = (from x in _ctx.PropMenuGastronomicThings_Dictionary_First
                                         where x.SpecificType == value
                                         select x.ThingName).ToList();
            }
        }

        #endregion tab3
        //tab4
        #region tab4

        public List<PropAccomodation> PropAccomodations
        {
            get { return _propAccomodations; }
            set
            {
                _propAccomodations = value;
                RaisePropertyChanged("PropAccomodations");
            }
        }

        public List<PropAccomodation_Dictionary> PropAccomDictionary
        {
            get { return _propAccomDictionary; }
            set
            {
                _propAccomDictionary = value;
                RaisePropertyChanged("PropAccomDictionary");
            }
        }

        public string PropAccomTypeOfRoom0
        {
            get { return _propAccomodations[0].TypeOfRoom; }
            set
            {
                _propAccomodations[0].TypeOfRoom = value;
                RaisePropertyChanged("PropAccomTypeOfRoom0");

            }
        }
        public string PropAccomTypeOfRoom1
        {
            get { return _propAccomodations[1].TypeOfRoom; }
            set
            {
                _propAccomodations[1].TypeOfRoom = value;
                RaisePropertyChanged("PropAccomTypeOfRoom1");
            }
        }
        public string PropAccomTypeOfRoom2
        {
            get { return _propAccomodations[2].TypeOfRoom; }
            set
            {
                _propAccomodations[2].TypeOfRoom = value;
                RaisePropertyChanged("PropAccomTypeOfRoom2");
            }
        }
        public string PropAccomTypeOfRoom3
        {
            get { return _propAccomodations[3].TypeOfRoom; }
            set
            {
                _propAccomodations[3].TypeOfRoom = value;
                RaisePropertyChanged("PropAccomTypeOfRoom3");
            }
        }
        public string PropAccomTypeOfRoom4
        {
            get { return _propAccomodations[4].TypeOfRoom; }
            set
            {
                _propAccomodations[4].TypeOfRoom = value;
                RaisePropertyChanged("PropAccomTypeOfRoom4");
            }
        }
        public string PropAccomTypeOfRoom5
        {
            get { return _propAccomodations[5].TypeOfRoom; }
            set
            {
                _propAccomodations[5].TypeOfRoom = value;
                RaisePropertyChanged("PropAccomTypeOfRoom5");
            }
        }

        public PropAccomodationDiscount PropAccomDiscount
        {
            get { return _propAccomDiscount; }
            set
            {
                _propAccomDiscount = value;
                RaisePropertyChanged("PropAccomDiscount");
            }
        }

        public float? PropAccomDiscountValue
        {
            get { return _propAccomDiscount.Discount; }
            set
            {
                if (value == null)
                    value = 0;

                _propAccomDiscount.Discount = value;
                RaisePropertyChanged("PropAccomDiscountValue");

                var price = (from s in _ctx.PropAccomodation_Dictionary
                             select s.Price).ToList();
                var f = price[0];
                if (f != null)
                {
                    PropAccomBrutto0 = SetDiscount(price[0].Value, _propAccomDiscount.Discount);
                    PropAccomBrutto1 = SetDiscount(price[1].Value, _propAccomDiscount.Discount);
                    PropAccomBrutto2 = SetDiscount(price[2].Value, _propAccomDiscount.Discount);
                    PropAccomBrutto3 = SetDiscount(price[3].Value, _propAccomDiscount.Discount);
                    PropAccomBrutto4 = SetDiscount(price[4].Value, _propAccomDiscount.Discount);
                    PropAccomBrutto5 = SetDiscount(price[5].Value, _propAccomDiscount.Discount);
                }
            }
        }

        public float? SetDiscount(float? itemPrice, float? intDiscountValue)
        {
            if (itemPrice == null)
                itemPrice = 0;
            if (intDiscountValue == null)
                intDiscountValue = 0;
            return itemPrice - itemPrice * intDiscountValue / 100;
        }
        public List<float?> VatList4
        {
            get { return _vatList4; }
            set
            {
                _vatList4 = value;
                RaisePropertyChanged("VatList4");
            }
        }

        public float? PropAccomBrutto0
        {
            get { return _propAccomodations[0].BruttoPrice; }
            set
            {
                _propAccomodations[0].BruttoPrice = value;
                RaisePropertyChanged("PropAccomBrutto0");

                // w sytuacji ustawienia ceny brutto uaktualnij cenę netto uwzgledniajac stawke vat
                if (PropAccomVat0 != null)
                    FourthTabNettoPrice0 = ComputeNettoPrice(value, PropAccomVat0);
            }
        }
        public float? PropAccomBrutto1
        {
            get { return _propAccomodations[1].BruttoPrice; }
            set
            {
                _propAccomodations[1].BruttoPrice = value;
                RaisePropertyChanged("PropAccomBrutto1");

                if (PropAccomVat1 != null)
                    FourthTabNettoPrice1 = ComputeNettoPrice(value, PropAccomVat1);
            }
        }
        public float? PropAccomBrutto2
        {
            get { return _propAccomodations[2].BruttoPrice; }
            set
            {

                _propAccomodations[2].BruttoPrice = value;
                RaisePropertyChanged("PropAccomBrutto2");

                if (PropAccomVat2 != null)
                    FourthTabNettoPrice2 = ComputeNettoPrice(value, PropAccomVat2);
            }
        }
        public float? PropAccomBrutto3
        {
            get { return _propAccomodations[3].BruttoPrice; }
            set
            {
                _propAccomodations[3].BruttoPrice = value;
                RaisePropertyChanged("PropAccomBrutto3");

                if (PropAccomVat3 != null)
                    FourthTabNettoPrice3 = ComputeNettoPrice(value, PropAccomVat3);
            }
        }
        public float? PropAccomBrutto4
        {
            get { return _propAccomodations[4].BruttoPrice; }
            set
            {
                _propAccomodations[4].BruttoPrice = value;
                RaisePropertyChanged("PropAccomBrutto4");

                if (PropAccomVat4 != null)
                    FourthTabNettoPrice4 = ComputeNettoPrice(value, PropAccomVat4);
            }
        }
        public float? PropAccomBrutto5
        {
            get { return _propAccomodations[5].BruttoPrice; }
            set
            {
                _propAccomodations[5].BruttoPrice = value;
                RaisePropertyChanged("PropAccomBrutto5");

                if (PropAccomVat5 != null)
                    FourthTabNettoPrice5 = ComputeNettoPrice(value, PropAccomVat5);
            }
        }

        public decimal? FourthTabNettoPrice0
        {
            get { return _fourthTabNettoPrice[0]; }
            set
            {
                _fourthTabNettoPrice[0] = value;
                RaisePropertyChanged("FourthTabNettoPrice0");

                if (PropAccomAmount0 != null && PropAccomDays0 != null)
                    FourthTabNettoValue0 = ComputeNettoValue((decimal)FourthTabNettoPrice0, PropAccomAmount0, PropAccomDays0);
            }
        }
        public decimal? FourthTabNettoPrice1
        {
            get { return _fourthTabNettoPrice[1]; }
            set
            {
                _fourthTabNettoPrice[1] = value;
                RaisePropertyChanged("FourthTabNettoPrice1");

                if (PropAccomAmount1 != null && PropAccomDays1 != null)
                    FourthTabNettoValue1 = ComputeNettoValue((decimal)FourthTabNettoPrice1, PropAccomAmount1, PropAccomDays1);
            }
        }
        public decimal? FourthTabNettoPrice2
        {
            get { return _fourthTabNettoPrice[2]; }
            set
            {
                _fourthTabNettoPrice[2] = value;
                RaisePropertyChanged("FourthTabNettoPrice2");

                if (PropAccomAmount2 != null && PropAccomDays2 != null)
                    FourthTabNettoValue2 = ComputeNettoValue((decimal)FourthTabNettoPrice2, PropAccomAmount2, PropAccomDays2);
            }
        }
        public decimal? FourthTabNettoPrice3
        {
            get { return _fourthTabNettoPrice[3]; }
            set
            {
                _fourthTabNettoPrice[3] = value;
                RaisePropertyChanged("FourthTabNettoPrice3");

                if (PropAccomAmount3 != null && PropAccomDays3 != null)
                    FourthTabNettoValue3 = ComputeNettoValue((decimal)FourthTabNettoPrice3, PropAccomAmount3, PropAccomDays3);
            }
        }
        public decimal? FourthTabNettoPrice4
        {
            get { return _fourthTabNettoPrice[4]; }
            set
            {
                _fourthTabNettoPrice[4] = value;
                RaisePropertyChanged("FourthTabNettoPrice4");

                if (PropAccomAmount4 != null && PropAccomDays4 != null)
                    FourthTabNettoValue4 = ComputeNettoValue((decimal)FourthTabNettoPrice4, PropAccomAmount4, PropAccomDays4);
            }
        }
        public decimal? FourthTabNettoPrice5
        {
            get { return _fourthTabNettoPrice[5]; }
            set
            {
                _fourthTabNettoPrice[5] = value;
                RaisePropertyChanged("FourthTabNettoPrice5");

                if (PropAccomAmount5 != null && PropAccomDays5 != null)
                    FourthTabNettoValue5 = ComputeNettoValue((decimal)FourthTabNettoPrice5, PropAccomAmount5, PropAccomDays5);
            }
        }

        public float? PropAccomVat0
        {
            get { return _propAccomodations[0].Vat; }
            set
            {
                _propAccomodations[0].Vat = value;
                RaisePropertyChanged("PropAccomVat0");

                if (PropAccomTypeOfRoom0 != null)
                    FourthTabNettoPrice0 = ComputeNettoPrice(PropAccomBrutto0, _propAccomodations[0].Vat);
            }
        }
        public float? PropAccomVat1
        {
            get { return _propAccomodations[1].Vat; }
            set
            {
                _propAccomodations[1].Vat = value;
                RaisePropertyChanged("PropAccomVat1");
                if (PropAccomTypeOfRoom0 != null)
                    FourthTabNettoPrice1 = ComputeNettoPrice(PropAccomBrutto1, _propAccomodations[1].Vat);
            }
        }
        public float? PropAccomVat2
        {
            get { return _propAccomodations[2].Vat; }
            set
            {
                _propAccomodations[2].Vat = value;
                RaisePropertyChanged("PropAccomVat2");
                if (PropAccomTypeOfRoom0 != null)
                    FourthTabNettoPrice2 = ComputeNettoPrice(PropAccomBrutto2, _propAccomodations[2].Vat);
            }
        }
        public float? PropAccomVat3
        {
            get { return _propAccomodations[3].Vat; }
            set
            {
                _propAccomodations[3].Vat = value;
                RaisePropertyChanged("PropAccomVat3");
                if (PropAccomTypeOfRoom0 != null)
                    FourthTabNettoPrice3 = ComputeNettoPrice(PropAccomBrutto3, _propAccomodations[3].Vat);
            }
        }
        public float? PropAccomVat4
        {
            get { return _propAccomodations[4].Vat; }
            set
            {
                _propAccomodations[4].Vat = value;
                RaisePropertyChanged("PropAccomVat4");
                if (PropAccomTypeOfRoom0 != null)
                    FourthTabNettoPrice4 = ComputeNettoPrice(PropAccomBrutto4, _propAccomodations[4].Vat);
            }
        }
        public float? PropAccomVat5
        {
            get { return _propAccomodations[5].Vat; }
            set
            {
                _propAccomodations[5].Vat = value;
                RaisePropertyChanged("PropAccomVat5");
                if (PropAccomTypeOfRoom0 != null)
                    FourthTabNettoPrice5 = ComputeNettoPrice(PropAccomBrutto5, _propAccomodations[5].Vat);
            }
        }
        public int? PropAccomDays0
        {
            get { return _propAccomodations[0].Days; }
            set
            {
                _propAccomodations[0].Days = value;
                RaisePropertyChanged("PropAccomDays0");

                // jesli jest wybrany element i ilosc to aktualizuj sume netto i brutto elementu
                if (PropAccomTypeOfRoom0 != null && PropAccomAmount0 != null)
                {
                    FourthTabNettoValue0 = ComputeNettoValue((decimal)FourthTabNettoPrice0, PropAccomAmount0,
                        PropAccomDays0);
                    FourthTabBruttoValue0 = ComputeBruttoValue(PropAccomBrutto0, PropAccomAmount0,
                        PropAccomDays0);
                }
            }
        }
        public int? PropAccomDays1
        {
            get { return _propAccomodations[1].Days; }
            set
            {
                _propAccomodations[1].Days = value;
                RaisePropertyChanged("PropAccomDays1");

                // jesli jest wybrany element i ilosc to aktualizuj sume netto i brutto elementu
                if (PropAccomTypeOfRoom1 != null && PropAccomAmount1 != null)
                {
                    FourthTabNettoValue1 = ComputeNettoValue((decimal)FourthTabNettoPrice1, PropAccomAmount1,
                        PropAccomDays1);
                    FourthTabBruttoValue1 = ComputeBruttoValue(PropAccomBrutto1, PropAccomAmount1,
                        PropAccomDays1);
                }
            }
        }
        public int? PropAccomDays2
        {
            get { return _propAccomodations[2].Days; }
            set
            {
                _propAccomodations[2].Days = value;
                RaisePropertyChanged("PropAccomDays2");

                // jesli jest wybrany element i ilosc to aktualizuj sume netto i brutto elementu
                if (PropAccomTypeOfRoom2 != null && PropAccomAmount2 != null)
                {
                    FourthTabNettoValue2 = ComputeNettoValue((decimal)FourthTabNettoPrice2, PropAccomAmount2,
                        PropAccomDays2);
                    FourthTabBruttoValue2 = ComputeBruttoValue(PropAccomBrutto2, PropAccomAmount2,
                        PropAccomDays2);
                }
            }
        }
        public int? PropAccomDays3
        {
            get { return _propAccomodations[3].Days; }
            set
            {
                _propAccomodations[3].Days = value;
                RaisePropertyChanged("PropAccomDays3");

                // jesli jest wybrany element i ilosc to aktualizuj sume netto i brutto elementu
                if (PropAccomTypeOfRoom3 != null && PropAccomAmount3 != null)
                {
                    FourthTabNettoValue3 = ComputeNettoValue((decimal)FourthTabNettoPrice3, PropAccomAmount3,
                        PropAccomDays3);
                    FourthTabBruttoValue3 = ComputeBruttoValue(PropAccomBrutto3, PropAccomAmount3,
                        PropAccomDays3);
                }
            }
        }
        public int? PropAccomDays4
        {
            get { return _propAccomodations[4].Days; }
            set
            {
                _propAccomodations[4].Days = value;
                RaisePropertyChanged("PropAccomDays4");

                // jesli jest wybrany element i ilosc to aktualizuj sume netto i brutto elementu
                if (PropAccomTypeOfRoom4 != null && PropAccomAmount4 != null)
                {
                    FourthTabNettoValue4 = ComputeNettoValue((decimal)FourthTabNettoPrice4, PropAccomAmount4,
                        PropAccomDays4);
                    FourthTabBruttoValue4 = ComputeBruttoValue(PropAccomBrutto4, PropAccomAmount4,
                        PropAccomDays4);
                }
            }
        }
        public int? PropAccomDays5
        {
            get { return _propAccomodations[5].Days; }
            set
            {
                _propAccomodations[5].Days = value;
                RaisePropertyChanged("PropAccomDays5");

                // jesli jest wybrany element i ilosc to aktualizuj sume netto i brutto elementu
                if (PropAccomTypeOfRoom5 != null && PropAccomAmount5 != null)
                {
                    FourthTabNettoValue5 = ComputeNettoValue((decimal)FourthTabNettoPrice5, PropAccomAmount5,
                        PropAccomDays5);
                    FourthTabBruttoValue5 = ComputeBruttoValue(PropAccomBrutto5, PropAccomAmount5,
                        PropAccomDays5);
                }
            }
        }

        public int? PropAccomAmount0
        {
            get { return _propAccomodations[0].Amount; }
            set
            {
                _propAccomodations[0].Amount = value;
                RaisePropertyChanged("PropAccomAmount0");

                // jesli jest wybrany przedmiot i wypelniona jest liczba dni to oblicz sume

                if (PropAccomTypeOfRoom0 != null && PropAccomDays0 != null)
                {
                    FourthTabNettoValue0 = ComputeNettoValue((decimal)FourthTabNettoPrice0, PropAccomAmount0,
                        PropAccomDays0);
                    FourthTabBruttoValue0 = ComputeBruttoValue(PropAccomBrutto0, PropAccomAmount0,
                        PropAccomDays0);
                }

            }
        }
        public int? PropAccomAmount1
        {
            get { return _propAccomodations[1].Amount; }
            set
            {
                _propAccomodations[1].Amount = value;
                RaisePropertyChanged("PropAccomAmount1");

                // jesli jest wybrany przedmiot i wypelniona jest liczba dni to oblicz sume

                if (PropAccomTypeOfRoom1 != null && PropAccomDays1 != null)
                {
                    FourthTabNettoValue1 = ComputeNettoValue((decimal)FourthTabNettoPrice1, PropAccomAmount1,
                        PropAccomDays1);
                    FourthTabBruttoValue1 = ComputeBruttoValue(PropAccomBrutto1, PropAccomAmount1,
                        PropAccomDays1);
                }

            }
        }
        public int? PropAccomAmount2
        {
            get { return _propAccomodations[2].Amount; }
            set
            {
                _propAccomodations[2].Amount = value;
                RaisePropertyChanged("PropAccomAmount2");

                // jesli jest wybrany przedmiot i wypelniona jest liczba dni to oblicz sume

                if (PropAccomTypeOfRoom2 != null && PropAccomDays2 != null)
                {
                    FourthTabNettoValue2 = ComputeNettoValue((decimal)FourthTabNettoPrice2, PropAccomAmount2,
                        PropAccomDays2);
                    FourthTabBruttoValue2 = ComputeBruttoValue(PropAccomBrutto2, PropAccomAmount2,
                        PropAccomDays2);
                }

            }
        }
        public int? PropAccomAmount3
        {
            get { return _propAccomodations[3].Amount; }
            set
            {
                _propAccomodations[3].Amount = value;
                RaisePropertyChanged("PropAccomAmount3");

                // jesli jest wybrany przedmiot i wypelniona jest liczba dni to oblicz sume

                if (PropAccomTypeOfRoom3 != null && PropAccomDays3 != null)
                {
                    FourthTabNettoValue3 = ComputeNettoValue((decimal)FourthTabNettoPrice3, PropAccomAmount3,
                        PropAccomDays3);
                    FourthTabBruttoValue3 = ComputeBruttoValue(PropAccomBrutto3, PropAccomAmount3,
                        PropAccomDays3);
                }

            }
        }
        public int? PropAccomAmount4
        {
            get { return _propAccomodations[4].Amount; }
            set
            {
                _propAccomodations[4].Amount = value;
                RaisePropertyChanged("PropAccomAmount4");

                // jesli jest wybrany przedmiot i wypelniona jest liczba dni to oblicz sume

                if (PropAccomTypeOfRoom4 != null && PropAccomDays4 != null)
                {
                    FourthTabNettoValue4 = ComputeNettoValue((decimal)FourthTabNettoPrice4, PropAccomAmount4,
                        PropAccomDays4);
                    FourthTabBruttoValue4 = ComputeBruttoValue(PropAccomBrutto4, PropAccomAmount4,
                        PropAccomDays4);
                }

            }
        }
        public int? PropAccomAmount5
        {
            get { return _propAccomodations[5].Amount; }
            set
            {
                _propAccomodations[5].Amount = value;
                RaisePropertyChanged("PropAccomAmount5");

                // jesli jest wybrany przedmiot i wypelniona jest liczba dni to oblicz sume

                if (PropAccomTypeOfRoom5 != null && PropAccomDays5 != null)
                {
                    FourthTabNettoValue5 = ComputeNettoValue((decimal)FourthTabNettoPrice5, PropAccomAmount5,
                        PropAccomDays5);
                    FourthTabBruttoValue5 = ComputeBruttoValue(PropAccomBrutto5, PropAccomAmount5,
                        PropAccomDays5);
                }

            }
        }

        public decimal FourthTabNettoValue0
        {
            get { return _fourthTabNettoValue[0]; }
            set
            {
                _fourthTabNettoValue[0] = value;
                RaisePropertyChanged("FourthTabNettoValue0");
            }
        }
        public decimal FourthTabNettoValue1
        {
            get { return _fourthTabNettoValue[1]; }
            set
            {
                _fourthTabNettoValue[1] = value;
                RaisePropertyChanged("FourthTabNettoValue1");
            }
        }
        public decimal FourthTabNettoValue2
        {
            get { return _fourthTabNettoValue[2]; }
            set
            {
                _fourthTabNettoValue[2] = value;
                RaisePropertyChanged("FourthTabNettoValue2");
            }
        }
        public decimal FourthTabNettoValue3
        {
            get { return _fourthTabNettoValue[3]; }
            set
            {
                _fourthTabNettoValue[3] = value;
                RaisePropertyChanged("FourthTabNettoValue3");
            }
        }
        public decimal FourthTabNettoValue4
        {
            get { return _fourthTabNettoValue[4]; }
            set
            {
                _fourthTabNettoValue[4] = value;
                RaisePropertyChanged("FourthTabNettoValue4");
            }
        }
        public decimal FourthTabNettoValue5
        {
            get { return _fourthTabNettoValue[5]; }
            set
            {
                _fourthTabNettoValue[5] = value;
                RaisePropertyChanged("FourthTabNettoValue5");
            }
        }

        public decimal FourthTabBruttoValue0
        {
            get { return _fourthTabBruttoValue[0]; }
            set
            {
                _fourthTabBruttoValue[0] = value;
                RaisePropertyChanged("FourthTabBruttoValue0");
                ComputeFourthTabSumBruttoValue();
                ComputeFourthTabSumNettoValue();
            }
        }
        public decimal FourthTabBruttoValue1
        {
            get { return _fourthTabBruttoValue[1]; }
            set
            {
                _fourthTabBruttoValue[1] = value;
                RaisePropertyChanged("FourthTabBruttoValue1");
                ComputeFourthTabSumBruttoValue();
                ComputeFourthTabSumNettoValue();
            }
        }
        public decimal FourthTabBruttoValue2
        {
            get { return _fourthTabBruttoValue[2]; }
            set
            {
                _fourthTabBruttoValue[2] = value;
                RaisePropertyChanged("FourthTabBruttoValue2");
                ComputeFourthTabSumBruttoValue();
                ComputeFourthTabSumNettoValue();
            }
        }
        public decimal FourthTabBruttoValue3
        {
            get { return _fourthTabBruttoValue[3]; }
            set
            {
                _fourthTabBruttoValue[3] = value;
                RaisePropertyChanged("FourthTabBruttoValue3");
                ComputeFourthTabSumBruttoValue();
                ComputeFourthTabSumNettoValue();
            }
        }
        public decimal FourthTabBruttoValue4
        {
            get { return _fourthTabBruttoValue[4]; }
            set
            {
                _fourthTabBruttoValue[4] = value;
                RaisePropertyChanged("FourthTabBruttoValue4");
                ComputeFourthTabSumBruttoValue();
                ComputeFourthTabSumNettoValue();
            }
        }
        public decimal FourthTabBruttoValue5
        {
            get { return _fourthTabBruttoValue[5]; }
            set
            {
                _fourthTabBruttoValue[5] = value;
                RaisePropertyChanged("FourthTabBruttoValue5");
                ComputeFourthTabSumBruttoValue();
                ComputeFourthTabSumNettoValue();
            }
        }

        public decimal FourthTabSumNettoValue
        {
            get { return _fourthTabSumNettoValue; }
            set
            {
                _fourthTabSumNettoValue = value;
                RaisePropertyChanged("FourthTabSumNettoValue");

            }
        }

        public decimal FourthTabSumBruttoValue
        {
            get { return _fourthTabSumBruttoValue; }
            set
            {
                _fourthTabSumBruttoValue = value;
                RaisePropertyChanged("FourthTabSumBruttoValue");
            }
        }
        #endregion tab4
#region tab5
        public List<string> PropPaySuggDictFirst
        {
            get { return _propPaySuggDictFirst; }
            set
            {
                _propPaySuggDictFirst = value;
                RaisePropertyChanged("PropPaySuggDictFirst");
            }
        }
        public List<string> PropPaySuggDictSecond
        {
            get { return _propPaySuggDictSecond; }
            set
            {
                _propPaySuggDictSecond = value;
                RaisePropertyChanged("PropPaySuggDictSecond");
            }
        }
        public List<string> PropPaySuggDictThird
        {
            get { return _propPaySuggDictThird; }
            set
            {
                _propPaySuggDictThird = value;
                RaisePropertyChanged("PropPaySuggDictThird");
            }
        }
        public List<string> PropPaySuggDictFourth
        {
            get { return _propPaySuggDictFourth; }
            set
            {
                _propPaySuggDictFourth = value;
                RaisePropertyChanged("PropPaySuggDictFourth");
            }
        }


        public decimal FifthTabSumNettoValue
        {
            get { return _fifthTabSumNettoValue; }
            set
            {
                _fifthTabSumNettoValue = value;
                RaisePropertyChanged("FifthTabSumNettoValue");
                ComputeFullSumNetto();
            }
        }

        public decimal FifthTabSumBruttoValue
        {
            get { return _fifthTabSumBruttoValue; }
            set
            {
                _fifthTabSumBruttoValue = value;
                RaisePropertyChanged("FifthTabSumBruttoValue");
                ComputeFullSumBrutto();
            }
        }

        public List<string> PropExtraServTypeDict
        {
            get { return _propExtraServTypeDict; }
            set
            {
                _propExtraServTypeDict = value;
                RaisePropertyChanged("PropExtraServTypeDict");
            }
        }

        public string PropExtraServType0
        {
            get { return _propExtraServ[0].ServiceType; }
            set
            {
                _propExtraServ[0].ServiceType = value;
                RaisePropertyChanged("PropExtraServType0");

                PropExtraServBrutto0 = SetExtraServBruttoPrice(_propExtraServ[0].ServiceType);

                if (PropExtraServVat0 != null)
                    FifthTabNettoPrice0 = ComputeNettoPrice(PropExtraServBrutto0, PropExtraServVat0);
            }
        }
        public string PropExtraServType1
        {
            get { return _propExtraServ[1].ServiceType; }
            set
            {
                _propExtraServ[1].ServiceType = value;
                RaisePropertyChanged("PropExtraServType1");

            }
        }
        public string PropExtraServType2
        {
            get { return _propExtraServ[2].ServiceType; }
            set
            {
                _propExtraServ[2].ServiceType = value;
                RaisePropertyChanged("PropExtraServType2");

            }
        }
        public string PropExtraServType3
        {
            get { return _propExtraServ[3].ServiceType; }
            set
            {
                _propExtraServ[3].ServiceType = value;
                RaisePropertyChanged("PropExtraServType3");

            }
        }

        private float? SetExtraServBruttoPrice(string serviceType)
        {
            var r = (from s in _ctx.PropExtraServices_Dictionary
                     where s.ServiceType == serviceType
                     select s.Price).Single();
            return r;
        }

        public float? PropExtraServBrutto0
        {
            get { return _propExtraServ[0].BruttoPrice; }
            set
            {
                _propExtraServ[0].BruttoPrice = value;
                RaisePropertyChanged("PropExtraServBrutto0");
            }
        }
        public float? PropExtraServBrutto1
        {
            get { return _propExtraServ[1].BruttoPrice; }
            set
            {
                _propExtraServ[1].BruttoPrice = value;
                RaisePropertyChanged("PropExtraServBrutto1");

                if (PropExtraServVat1 != null)
                    FifthTabNettoPrice1 = ComputeNettoPrice(PropExtraServBrutto1, PropExtraServVat1);
            }
        }
        public float? PropExtraServBrutto2
        {
            get { return _propExtraServ[2].BruttoPrice; }
            set
            {
                _propExtraServ[2].BruttoPrice = value;
                RaisePropertyChanged("PropExtraServBrutto2");

                if (PropExtraServVat2 != null)
                    FifthTabNettoPrice2 = ComputeNettoPrice(PropExtraServBrutto2, PropExtraServVat2);
            }
        }
        public float? PropExtraServBrutto3
        {
            get { return _propExtraServ[3].BruttoPrice; }
            set
            {
                _propExtraServ[3].BruttoPrice = value;
                RaisePropertyChanged("PropExtraServBrutto3");

                if (PropExtraServVat3 != null)
                    FifthTabNettoPrice3 = ComputeNettoPrice(PropExtraServBrutto3, PropExtraServVat3);
            }
        }

        public float? PropExtraServVat0
        {
            get { return _propExtraServ[0].Vat; }
            set
            {
                _propExtraServ[0].Vat = value;
                RaisePropertyChanged("PropExtraServVat0");

                FifthTabNettoPrice0 = ComputeNettoPrice(PropExtraServBrutto0, PropExtraServVat0);
            }
        }
        public float? PropExtraServVat1
        {
            get { return _propExtraServ[1].Vat; }
            set
            {
                _propExtraServ[1].Vat = value;
                RaisePropertyChanged("PropExtraServVat1");

                FifthTabNettoPrice1 = ComputeNettoPrice(PropExtraServBrutto1, PropExtraServVat1);
            }
        }
        public float? PropExtraServVat2
        {
            get { return _propExtraServ[2].Vat; }
            set
            {
                _propExtraServ[2].Vat = value;
                RaisePropertyChanged("PropExtraServVat2");

                FifthTabNettoPrice2 = ComputeNettoPrice(PropExtraServBrutto2, PropExtraServVat2);
            }
        }
        public float? PropExtraServVat3
        {
            get { return _propExtraServ[3].Vat; }
            set
            {
                _propExtraServ[3].Vat = value;
                RaisePropertyChanged("PropExtraServVat3");

                FifthTabNettoPrice3 = ComputeNettoPrice(PropExtraServBrutto3, PropExtraServVat3);
            }
        }

        public decimal? FifthTabNettoPrice0
        {
            get { return _fifthTabNettoPrice[0]; }
            set
            {
                _fifthTabNettoPrice[0] = value;
                RaisePropertyChanged("FifthTabNettoPrice0");

                if (PropExtraServAmount0 != null && PropExtraServDays0 != null)
                    FifthTabNettoValue0 = ComputeNettoValue((decimal)FifthTabNettoPrice0, PropExtraServAmount0, PropExtraServDays0);
            }
        }
        public decimal? FifthTabNettoPrice1
        {
            get { return _fifthTabNettoPrice[1]; }
            set
            {
                _fifthTabNettoPrice[1] = value;
                RaisePropertyChanged("FifthTabNettoPrice1");
            }
        }
        public decimal? FifthTabNettoPrice2
        {
            get { return _fifthTabNettoPrice[2]; }
            set
            {
                _fifthTabNettoPrice[2] = value;
                RaisePropertyChanged("FifthTabNettoPrice2");
            }
        }
        public decimal? FifthTabNettoPrice3
        {
            get { return _fifthTabNettoPrice[3]; }
            set
            {
                _fifthTabNettoPrice[3] = value;
                RaisePropertyChanged("FifthTabNettoPrice3");
            }
        }

        public int? PropExtraServAmount0
        {
            get { return _propExtraServ[0].Amount; }
            set
            {
                _propExtraServ[0].Amount = value;
                RaisePropertyChanged("PropExtraServAmount0");

                // jesli jest wybrany przedmiot i wypelniona jest liczba dni to oblicz sume

                if (PropExtraServBrutto0 != null && PropExtraServDays0 != null)
                {
                    FifthTabNettoValue0 = ComputeNettoValue((decimal)FifthTabNettoPrice0, PropExtraServAmount0,
                        PropExtraServDays0);
                    FifthTabBruttoValue0 = ComputeBruttoValue(PropExtraServBrutto0, PropExtraServAmount0,
                        PropExtraServDays0);
                }
            }
        }
        public int? PropExtraServAmount1
        {
            get { return _propExtraServ[1].Amount; }
            set
            {
                _propExtraServ[1].Amount = value;
                RaisePropertyChanged("PropExtraServAmount1");

                // jesli jest wybrany przedmiot i wypelniona jest liczba dni to oblicz sume

                if (PropExtraServBrutto1 != null && PropExtraServDays1 != null)
                {
                    FifthTabNettoValue1 = ComputeNettoValue((decimal)FifthTabNettoPrice1, PropExtraServAmount1,
                        PropExtraServDays1);
                    FifthTabBruttoValue1 = ComputeBruttoValue(PropExtraServBrutto1, PropExtraServAmount1,
                        PropExtraServDays1);
                }
            }
        }
        public int? PropExtraServAmount2
        {
            get { return _propExtraServ[2].Amount; }
            set
            {
                _propExtraServ[2].Amount = value;
                RaisePropertyChanged("PropExtraServAmount2");

                // jesli jest wybrany przedmiot i wypelniona jest liczba dni to oblicz sume

                if (PropExtraServBrutto2 != null && PropExtraServDays2 != null)
                {
                    FifthTabNettoValue2 = ComputeNettoValue((decimal)FifthTabNettoPrice2, PropExtraServAmount2,
                        PropExtraServDays3);
                    FifthTabBruttoValue2 = ComputeBruttoValue(PropExtraServBrutto2, PropExtraServAmount2,
                        PropExtraServDays2);
                }
            }
        }
        public int? PropExtraServAmount3
        {
            get { return _propExtraServ[3].Amount; }
            set
            {
                _propExtraServ[3].Amount = value;
                RaisePropertyChanged("PropExtraServAmount3");

                // jesli jest wybrany przedmiot i wypelniona jest liczba dni to oblicz sume

                if (PropExtraServBrutto3 != null && PropExtraServDays3 != null)
                {
                    FifthTabNettoValue3 = ComputeNettoValue((decimal)FifthTabNettoPrice3, PropExtraServAmount3,
                        PropExtraServDays3);
                    FifthTabBruttoValue3 = ComputeBruttoValue(PropExtraServBrutto3, PropExtraServAmount3,
                        PropExtraServDays3);
                }
            }
        }

        public int? PropExtraServDays0
        {
            get { return _propExtraServ[0].Days; }
            set
            {
                _propExtraServ[0].Days = value;
                RaisePropertyChanged("PropExtraServDays0");

                // jesli jest wybrany element i ilosc to aktualizuj sume netto i brutto elementu
                if (PropExtraServBrutto0 != null && PropExtraServAmount0 != null)
                {
                    FifthTabNettoValue0 = ComputeNettoValue((decimal)FifthTabNettoPrice0, PropExtraServAmount0,
                        PropExtraServDays0);
                    FifthTabBruttoValue0 = ComputeBruttoValue(PropExtraServBrutto0, PropExtraServAmount0,
                        PropExtraServDays0);
                }
            }
        }
        public int? PropExtraServDays1
        {
            get { return _propExtraServ[1].Days; }
            set
            {
                _propExtraServ[1].Days = value;
                RaisePropertyChanged("PropExtraServDays1");

                // jesli jest wybrany element i ilosc to aktualizuj sume netto i brutto elementu
                if (PropExtraServBrutto1 != null && PropExtraServAmount1 != null)
                {
                    FifthTabNettoValue1 = ComputeNettoValue((decimal)FifthTabNettoPrice1, PropExtraServAmount1,
                        PropExtraServDays1);
                    FifthTabBruttoValue1 = ComputeBruttoValue(PropExtraServBrutto1, PropExtraServAmount1,
                        PropExtraServDays1);
                }
            }
        }
        public int? PropExtraServDays2
        {
            get { return _propExtraServ[2].Days; }
            set
            {
                _propExtraServ[2].Days = value;
                RaisePropertyChanged("PropExtraServDays2");

                // jesli jest wybrany element i ilosc to aktualizuj sume netto i brutto elementu
                if (PropExtraServBrutto2 != null && PropExtraServAmount2 != null)
                {
                    FifthTabNettoValue2 = ComputeNettoValue((decimal)FifthTabNettoPrice2, PropExtraServAmount2,
                        PropExtraServDays2);
                    FifthTabBruttoValue2 = ComputeBruttoValue(PropExtraServBrutto2, PropExtraServAmount2,
                        PropExtraServDays2);
                }
            }
        }
        public int? PropExtraServDays3
        {
            get { return _propExtraServ[3].Days; }
            set
            {
                _propExtraServ[3].Days = value;
                RaisePropertyChanged("PropExtraServDays3");

                // jesli jest wybrany element i ilosc to aktualizuj sume netto i brutto elementu
                if (PropExtraServBrutto3 != null && PropExtraServAmount3 != null)
                {
                    FifthTabNettoValue3 = ComputeNettoValue((decimal)FifthTabNettoPrice3, PropExtraServAmount3,
                        PropExtraServDays3);
                    FifthTabBruttoValue3 = ComputeBruttoValue(PropExtraServBrutto3, PropExtraServAmount3,
                        PropExtraServDays3);
                }
            }
        }

        public decimal FifthTabNettoValue0
        {
            get { return _fifthTabNettoValue[0]; }
            set
            {
                _fifthTabNettoValue[0] = value;
                RaisePropertyChanged("FifthTabNettoValue0");
                ComputeFifthTabSumNettoValue();
            }
        }
        public decimal FifthTabNettoValue1
        {
            get { return _fifthTabNettoValue[1]; }
            set
            {
                _fifthTabNettoValue[1] = value;
                RaisePropertyChanged("FifthTabNettoValue1");
                ComputeFifthTabSumNettoValue();
            }
        }
        public decimal FifthTabNettoValue2
        {
            get { return _fifthTabNettoValue[2]; }
            set
            {
                _fifthTabNettoValue[2] = value;
                RaisePropertyChanged("FifthTabNettoValue2");
                ComputeFifthTabSumNettoValue();
            }
        }
        public decimal FifthTabNettoValue3
        {
            get { return _fifthTabNettoValue[3]; }
            set
            {
                _fifthTabNettoValue[3] = value;
                RaisePropertyChanged("FifthTabNettoValue3");
                ComputeFifthTabSumNettoValue();
            }
        }

        public List<decimal> FifthTabNettoValue
        {
            get { return _fifthTabNettoValue; }
            set
            {
                _fifthTabNettoValue = value;
                RaisePropertyChanged("FifthTabNettoValue");
            }
        }

        public List<decimal> FifthTabBruttoValue
        {
            get { return _fifthTabBruttoValue; }
            set
            {
                _fifthTabBruttoValue = value;
                RaisePropertyChanged("FifthTabBruttoValue");
            }
        }

        public decimal FifthTabBruttoValue0
        {
            get { return _fifthTabBruttoValue[0]; }
            set
            {
                _fifthTabBruttoValue[0] = value;
                RaisePropertyChanged("FifthTabBruttoValue0");
                ComputeFifthTabSumBruttoValue();
            }
        }
        public decimal FifthTabBruttoValue1
        {
            get { return _fifthTabBruttoValue[1]; }
            set
            {
                _fifthTabBruttoValue[1] = value;
                RaisePropertyChanged("FifthTabBruttoValue1");
                ComputeFifthTabSumBruttoValue();
            }
        }
        public decimal FifthTabBruttoValue2
        {
            get { return _fifthTabBruttoValue[2]; }
            set
            {
                _fifthTabBruttoValue[2] = value;
                RaisePropertyChanged("FifthTabBruttoValue2");
                ComputeFifthTabSumBruttoValue();
            }
        }
        public decimal FifthTabBruttoValue3
        {
            get { return _fifthTabBruttoValue[3]; }
            set
            {
                _fifthTabBruttoValue[3] = value;
                RaisePropertyChanged("FifthTabBruttoValue3");
                ComputeFifthTabSumBruttoValue();
            }
        }

        public decimal FullSumNetto
        {
            get { return _fullSumNetto; }
            set
            {
                _fullSumNetto = value;
                RaisePropertyChanged("FullSumNetto");
            }
        }

        public decimal FullSumBrutto
        {
            get { return _fullSumBrutto; }
            set
            {
                _fullSumBrutto = value;
                RaisePropertyChanged("FullSumBrutto");
            }
        }

        public string PaymentSuggestPaymentForm
        {
            get { return _propPaymentSugg.PaymentForm; }
            set
            {
                _propPaymentSugg.PaymentForm = value;
                RaisePropertyChanged("PaymentSuggestPaymentForm");
            }
        }
        public string PaymentSuggestInvServName
        {
            get { return _propPaymentSugg.InvoiceServiceName; }
            set
            {
                _propPaymentSugg.InvoiceServiceName = value;
                RaisePropertyChanged("PaymentSuggestInvServName");
            }
        }
        public string PaymentSuggestIndividOrder
        {
            get { return _propPaymentSugg.IndividualOrders; }
            set
            {
                _propPaymentSugg.IndividualOrders = value;
                RaisePropertyChanged("PaymentSuggestIndividOrder");
            }
        }
        public string PaymentSuggestCarPark
        {
            get { return _propPaymentSugg.CarPark; }
            set
            {
                _propPaymentSugg.CarPark = value;
                RaisePropertyChanged("PaymentSuggestCarPark");
            }
        }
#endregion tab5
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
                try
                {
                    var propositionToBase = new Proposition
                    {
                        Id_user = _userId,
                        UpdateDate = _addNewProposition.UpdateDate,
                        Status = "New" //TODO uaktualnić ewentualnie z enuma lub obgadać jak rozwiązać
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

                    //------------------------------
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

                    //------------------------------
                    // !! PROPACCOMODATIONS !!
                    for (int i = 0; i < _propAccomodations.Count; i++)
                    {
                        if (_propAccomodations[i].Amount != null && _propAccomodations[i].Days != null)
                        {
                            _propAccomodations[i].Id_proposition = currentPropositionId;
                            _ctx.PropAccomodation.Add(_propAccomodations[i]);
                        }
                    }

                    //------------------------------
                    // !! PROPACCOMODATIONSDISCOUNT !!

                    PropAccomDiscount.Id_proposition = currentPropositionId;
                    _ctx.PropAccomodationDiscount.Add(PropAccomDiscount);

                    //------------------------------
                    // !! PROPEXTRASERVICES !!
                    for (int i = 0; i < _propExtraServ.Count; i++)
                    {
                        if (_propExtraServ[i].ServiceType != null && _propExtraServ[i].BruttoPrice != null &&
                            _propExtraServ[i].Days != null && _propExtraServ[i].Amount != null)
                        {
                            _propExtraServ[i].Id_proposition = currentPropositionId;
                            _ctx.PropExtraServices.Add(_propExtraServ[i]);
                        }
                    }

                    //------------------------------
                    // !! PROPPAYMENTSUGGESTIONS !!

                    _propPaymentSugg.Id_proposition = currentPropositionId;
                    _ctx.PropPaymentSuggestions.Add(_propPaymentSugg);

                    _ctx.SaveChanges();
                    MessageBox.Show("dodano nowa propozycje");

                    // po dodaniu propozycji odśwież listę propozycji
                    SelectAllPropositions();
                }
                catch (Exception w)
                {
                    MessageBox.Show(w.ToString());
                }

            }
            else
            {
                //fox
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
                    addPropReservationDetails.HallSetting = PropositionReservDetailsHallSetting;
                    addPropReservationDetails.PeopleNumber = PropositionReservDetailsPeopleNumber;
                    addPropReservationDetails.EndTime = PropositionReservDetails.EndTime;
                    addPropReservationDetails.StartTime = PropositionReservDetails.StartTime;
                    addPropReservationDetails.Proposition = PropositionReservDetails.Proposition;
                    _ctx.PropReservationDetails.Add(addPropReservationDetails);

                }
                else
                {
                    //propReservation.Id_proposition = idProposition;
                    propReservation.StartData = PropositionReservDetails.StartData;
                    propReservation.EndData = PropositionReservDetails.EndData;
                    propReservation.Hall = PropositionReservDetails.Hall;
                    propReservation.HallSetting = PropositionReservDetailsHallSetting;
                    propReservation.PeopleNumber = PropositionReservDetailsPeopleNumber;
                    propReservation.EndTime = PropositionReservDetails.EndTime;
                    propReservation.StartTime = PropositionReservDetails.StartTime;
                    propReservation.Proposition = PropositionReservDetails.Proposition;
                    

                }

                 _ctx.SaveChanges();
                var propEquipment = (from q in _ctx.PropHallEquipment
                      where q.Id_proposition == idProposition
                      select q).ToList();
                var hall = propEquipment.Find(item => item.Things == PropHallEqThing0);
                if (PropHallEqAmount0 != null && PropHallEqDays0 !=null)
                {
                    if (hall.Days != PropHallEqDays0)
                        hall.Days = PropHallEqDays0;
                    if (hall.Amount != PropHallEqAmount0)
                        hall.Amount = PropHallEqAmount0;
                    _ctx.SaveChanges();
                }
                //wposażenie


                if (PropHallEqThing1 != null)
                {
                    var thing1 = propEquipment.Find(item => item.Things == PropHallEqThing1);
                    if (thing1 != null)
                    {
                        thing1.Id_proposition = _idProposition;                     
                        thing1.Things = PropHallEqThing1;
                        thing1.Amount = PropHallEqAmount1;
                        thing1.Days = PropHallEqDays1;
                        thing1.BruttoPrice = PropHallEqBrutto1;
                    }
                    else
                    {
                        PropHallEquipment newqEquipment = new PropHallEquipment();
                        newqEquipment.Things = PropHallEqThing1;
                        newqEquipment.Amount = PropHallEqAmount1;
                        newqEquipment.Days = PropHallEqDays1;
                        newqEquipment.BruttoPrice = PropHallEqBrutto1;
                        newqEquipment.Id_proposition = _idProposition;
                        _ctx.PropHallEquipment.Add(newqEquipment);


                    }
                }
                _ctx.SaveChanges();
                if (PropHallEqThing2 != null)
                {
                    var thing1 = propEquipment.Find(item => item.Things == PropHallEqThing2);
                    if (thing1 != null)
                    {
                        thing1.Id_proposition = _idProposition;
                        thing1.Things = PropHallEqThing2;
                        thing1.Amount = PropHallEqAmount2;
                        thing1.Days = PropHallEqDays2;
                        thing1.BruttoPrice = PropHallEqBrutto2;
                    }
                    else
                    {
                        PropHallEquipment newqEquipment = new PropHallEquipment();
                        newqEquipment.Things = PropHallEqThing2;
                        newqEquipment.Amount = PropHallEqAmount2;
                        newqEquipment.Days = PropHallEqDays2;
                        newqEquipment.BruttoPrice = PropHallEqBrutto2;
                        newqEquipment.Id_proposition = _idProposition;
                        _ctx.PropHallEquipment.Add(newqEquipment);


                    }
                }
                _ctx.SaveChanges();
                if (PropHallEqThing3 != null)
                {
                    var thing1 = propEquipment.Find(item => item.Things == PropHallEqThing3);
                    if (thing1 != null)
                    {
                        thing1.Id_proposition = _idProposition;
                        thing1.Things = PropHallEqThing3;
                        thing1.Amount = PropHallEqAmount3;
                        thing1.Days = PropHallEqDays3;
                        thing1.BruttoPrice = PropHallEqBrutto3;
                    }
                    else
                    {
                        PropHallEquipment newqEquipment = new PropHallEquipment();
                        newqEquipment.Things = PropHallEqThing3;
                        newqEquipment.Amount = PropHallEqAmount3;
                        newqEquipment.Days = PropHallEqDays3;
                        newqEquipment.BruttoPrice = PropHallEqBrutto3;
                        newqEquipment.Id_proposition = _idProposition;
                        _ctx.PropHallEquipment.Add(newqEquipment);


                    }
                }
                _ctx.SaveChanges();
                if (PropHallEqThing4 != null)
                {
                    var thing1 = propEquipment.Find(item => item.Things == PropHallEqThing4);
                    if (thing1 != null)
                    {
                        thing1.Id_proposition = _idProposition;
                        thing1.Things = PropHallEqThing4;
                        thing1.Amount = PropHallEqAmount4;
                        thing1.Days = PropHallEqDays4;
                        thing1.BruttoPrice = PropHallEqBrutto4;
                    }
                    else
                    {
                        PropHallEquipment newqEquipment = new PropHallEquipment();
                        newqEquipment.Things = PropHallEqThing4;
                        newqEquipment.Amount = PropHallEqAmount4;
                        newqEquipment.Days = PropHallEqDays4;
                        newqEquipment.BruttoPrice = PropHallEqBrutto4;
                        newqEquipment.Id_proposition = _idProposition;
                        _ctx.PropHallEquipment.Add(newqEquipment);


                    }
                }

                if (PropHallEqThing5 != null)
                {
                    var thing1 = propEquipment.Find(item => item.Things == PropHallEqThing5);
                    if (thing1 != null)
                    {
                        thing1.Id_proposition = _idProposition;
                        thing1.Things = PropHallEqThing5;
                        thing1.Amount = PropHallEqAmount5;
                        thing1.Days = PropHallEqDays5;
                        thing1.BruttoPrice = PropHallEqBrutto5;
                    }
                    else
                    {
                        PropHallEquipment newqEquipment = new PropHallEquipment();
                        newqEquipment.Things = PropHallEqThing5;
                        newqEquipment.Amount = PropHallEqAmount5;
                        newqEquipment.Days = PropHallEqDays5;
                        newqEquipment.BruttoPrice = PropHallEqBrutto5;
                        newqEquipment.Id_proposition = _idProposition;
                        _ctx.PropHallEquipment.Add(newqEquipment);
                        _ctx.SaveChanges();

                    }
                }
//Gastronomia
                var editGastronomic = (from q in _ctx.PropMenuPosition
                                       where q.Id_proposition == _idProposition
                                       select q).ToList();
                if (PropMenuTypeOfServ0 != null)
                {
                    var service = editGastronomic.Find(item => item.TypeOfService == PropMenuTypeOfServ0);
                    if (service != null)
                    {
                        service.Id_proposition = idProposition;
                        service.TypeOfService = PropMenuTypeOfServ0;
                        service.Amount = PropMenuPosAmount0;
                        service.Days = PropMenuPosDays0;
                        service.MergeType = SetMenuPosDefaultMergeType(PropMenuTypeOfServ0);
                        service.BruttoPrice = SetMenuPosDefaultBrutto(PropMenuTypeOfServ0);
                        service.Vat = SetMenuPosDefaultVat(PropMenuTypeOfServ0);
                    }
                    else
                    {
                        PropMenuPosition newPosition =new PropMenuPosition();
                        newPosition.Id_proposition = idProposition;
                        service.TypeOfService = PropMenuTypeOfServ0;
                        newPosition.Amount = PropMenuPosAmount0;
                        newPosition.Days = PropMenuPosDays0;
                        newPosition.MergeType = SetMenuPosDefaultMergeType(PropMenuTypeOfServ0);
                        newPosition.BruttoPrice = SetMenuPosDefaultBrutto(PropMenuTypeOfServ0);
                        newPosition.Vat = SetMenuPosDefaultVat(PropMenuTypeOfServ0);
                        _ctx.PropMenuPosition.Add(newPosition);
                    }
                }
                _ctx.SaveChanges();

                if (PropMenuTypeOfServ1 != null)
                {
                    var service = editGastronomic.Find(item => item.TypeOfService == PropMenuTypeOfServ1);
                    if (service != null)
                    {
                        service.Id_proposition = idProposition;
                        service.TypeOfService = PropMenuTypeOfServ1;
                        service.Amount = PropMenuPosAmount1;
                        service.Days = PropMenuPosDays1;
                        service.MergeType = SetMenuPosDefaultMergeType(PropMenuTypeOfServ1);
                        service.BruttoPrice = SetMenuPosDefaultBrutto(PropMenuTypeOfServ1);
                        service.Vat = SetMenuPosDefaultVat(PropMenuTypeOfServ1);
                    }
                    else
                    {
                        PropMenuPosition newPosition = new PropMenuPosition();
                        newPosition.Id_proposition = idProposition;
                        service.TypeOfService = PropMenuTypeOfServ1;
                        newPosition.Amount = PropMenuPosAmount1;
                        newPosition.Days = PropMenuPosDays1;
                        newPosition.MergeType = SetMenuPosDefaultMergeType(PropMenuTypeOfServ1);
                        newPosition.BruttoPrice = SetMenuPosDefaultBrutto(PropMenuTypeOfServ1);
                        newPosition.Vat = SetMenuPosDefaultVat(PropMenuTypeOfServ1);
                        _ctx.PropMenuPosition.Add(newPosition);
                    }
                }
                _ctx.SaveChanges();

                if (PropMenuTypeOfServ2 != null)
                {
                    var service = editGastronomic.Find(item => item.TypeOfService == PropMenuTypeOfServ2);
                    if (service != null)
                    {
                        service.Id_proposition = idProposition;
                        service.TypeOfService = PropMenuTypeOfServ2;
                        service.Amount = PropMenuPosAmount2;
                        service.Days = PropMenuPosDays2;
                        service.MergeType = SetMenuPosDefaultMergeType(PropMenuTypeOfServ2);
                        service.BruttoPrice = SetMenuPosDefaultBrutto(PropMenuTypeOfServ2);
                        service.Vat = SetMenuPosDefaultVat(PropMenuTypeOfServ2);
                    }
                    else
                    {
                        PropMenuPosition newPosition = new PropMenuPosition();
                        newPosition.Id_proposition = idProposition;
                        service.TypeOfService = PropMenuTypeOfServ2;
                        newPosition.Amount = PropMenuPosAmount2;
                        newPosition.Days = PropMenuPosDays2;
                        newPosition.MergeType = SetMenuPosDefaultMergeType(PropMenuTypeOfServ2);
                        newPosition.BruttoPrice = SetMenuPosDefaultBrutto(PropMenuTypeOfServ2);
                        newPosition.Vat = SetMenuPosDefaultVat(PropMenuTypeOfServ2);
                        _ctx.PropMenuPosition.Add(newPosition);
                    }
                }
                _ctx.SaveChanges();
                if (PropMenuTypeOfServ3 != null)
                {
                    var service = editGastronomic.Find(item => item.TypeOfService == PropMenuTypeOfServ3);
                    if (service != null)
                    {
                        service.Id_proposition = idProposition;
                        service.TypeOfService = PropMenuTypeOfServ3;
                        service.Amount = PropMenuPosAmount3;
                        service.Days = PropMenuPosDays3;
                        service.MergeType = SetMenuPosDefaultMergeType(PropMenuTypeOfServ3);
                        service.BruttoPrice = SetMenuPosDefaultBrutto(PropMenuTypeOfServ3);
                        service.Vat = SetMenuPosDefaultVat(PropMenuTypeOfServ3);
                    }
                    else
                    {
                        PropMenuPosition newPosition = new PropMenuPosition();
                        newPosition.Id_proposition = idProposition;
                        service.TypeOfService = PropMenuTypeOfServ3;
                        newPosition.Amount = PropMenuPosAmount3;
                        newPosition.Days = PropMenuPosDays3;
                        newPosition.MergeType = SetMenuPosDefaultMergeType(PropMenuTypeOfServ3);
                        newPosition.BruttoPrice = SetMenuPosDefaultBrutto(PropMenuTypeOfServ3);
                        newPosition.Vat = SetMenuPosDefaultVat(PropMenuTypeOfServ3);
                        _ctx.PropMenuPosition.Add(newPosition);
                    }
                }
                _ctx.SaveChanges();
                if (PropMenuTypeOfServ4 != null)
                {
                    var service = editGastronomic.Find(item => item.TypeOfService == PropMenuTypeOfServ4);
                    if (service != null)
                    {
                        service.Id_proposition = idProposition;
                        service.TypeOfService = PropMenuTypeOfServ4;
                        service.Amount = PropMenuPosAmount4;
                        service.Days = PropMenuPosDays4;
                        service.MergeType = SetMenuPosDefaultMergeType(PropMenuTypeOfServ4);
                        service.BruttoPrice = SetMenuPosDefaultBrutto(PropMenuTypeOfServ4);
                        service.Vat = SetMenuPosDefaultVat(PropMenuTypeOfServ4);
                    }
                    else
                    {
                        PropMenuPosition newPosition = new PropMenuPosition();
                        newPosition.Id_proposition = idProposition;
                        service.TypeOfService = PropMenuTypeOfServ4;
                        newPosition.Amount = PropMenuPosAmount4;
                        newPosition.Days = PropMenuPosDays4;
                        newPosition.MergeType = SetMenuPosDefaultMergeType(PropMenuTypeOfServ4);
                        newPosition.BruttoPrice = SetMenuPosDefaultBrutto(PropMenuTypeOfServ4);
                        newPosition.Vat = SetMenuPosDefaultVat(PropMenuTypeOfServ4);
                        _ctx.PropMenuPosition.Add(newPosition);
                    }
                }
                _ctx.SaveChanges();
                if (PropMenuTypeOfServ5 != null)
                {
                    var service = editGastronomic.Find(item => item.TypeOfService == PropMenuTypeOfServ5);
                    if (service != null)
                    {
                        service.Id_proposition = idProposition;
                        service.TypeOfService = PropMenuTypeOfServ5;
                        service.Amount = PropMenuPosAmount5;
                        service.Days = PropMenuPosDays5;
                        service.MergeType = SetMenuPosDefaultMergeType(PropMenuTypeOfServ5);
                        service.BruttoPrice = SetMenuPosDefaultBrutto(PropMenuTypeOfServ5);
                        service.Vat = SetMenuPosDefaultVat(PropMenuTypeOfServ5);
                    }
                    else
                    {
                        PropMenuPosition newPosition = new PropMenuPosition();
                        newPosition.Id_proposition = idProposition;
                        newPosition.Amount = PropMenuPosAmount5;
                        newPosition.Days = PropMenuPosDays5;
                        newPosition.MergeType = SetMenuPosDefaultMergeType(PropMenuTypeOfServ5);
                        newPosition.BruttoPrice = SetMenuPosDefaultBrutto(PropMenuTypeOfServ5);
                        newPosition.Vat = SetMenuPosDefaultVat(PropMenuTypeOfServ5);
                        _ctx.PropMenuPosition.Add(newPosition);
                    }
                }
                _ctx.SaveChanges();
                if (PropMenuTypeOfServ6 != null)
                {
                    var service = editGastronomic.Find(item => item.TypeOfService == PropMenuTypeOfServ6);
                    if (service != null)
                    {
                        service.Id_proposition = idProposition;
                        service.TypeOfService = PropMenuTypeOfServ6;
                        service.Amount = PropMenuPosAmount6;
                        service.Days = PropMenuPosDays6;
                        service.MergeType = SetMenuPosDefaultMergeType(PropMenuTypeOfServ6);
                        service.BruttoPrice = SetMenuPosDefaultBrutto(PropMenuTypeOfServ6);
                        service.Vat = SetMenuPosDefaultVat(PropMenuTypeOfServ6);
                    }
                    else
                    {
                        PropMenuPosition newPosition = new PropMenuPosition();
                        newPosition.Id_proposition = idProposition;
                        service.TypeOfService = PropMenuTypeOfServ6;
                        newPosition.Amount = PropMenuPosAmount6;
                        newPosition.Days = PropMenuPosDays6;
                        newPosition.MergeType = SetMenuPosDefaultMergeType(PropMenuTypeOfServ6);
                        newPosition.BruttoPrice = SetMenuPosDefaultBrutto(PropMenuTypeOfServ6);
                        newPosition.Vat = SetMenuPosDefaultVat(PropMenuTypeOfServ6);
                        _ctx.PropMenuPosition.Add(newPosition);
                    }
                }
                _ctx.SaveChanges();

                var merge = (from q in _ctx.PropMenuMerge
                    where q.Id_proposition == idProposition
                    select q).ToList();
                merge[0].DefaultValue = PropMenuMerge0;
                merge[1].DefaultValue = PropMenuMerge1;
                merge[2].DefaultValue = PropMenuMerge2;
                merge[3].DefaultValue = PropMenuMerge3;
                merge[4].DefaultValue = PropMenuMerge4;
                _ctx.SaveChanges();

                var room = (from q in _ctx.PropAccomodation
                    where q.Id_proposition == idProposition
                    select q).ToList();
                for (int i = 0; i < room.Count; i++)
                {
                    switch (room[i].TypeOfRoom)
                    {
                        case "POKÓJ 1-OSOBOWY":
                           room[i].Amount = PropAccomAmount0;
                           room[i].Days = PropAccomDays0;
                            break;
                        case "POKÓJ 2-OSOBOWY":
                            room[i].Amount=PropAccomAmount1 ;
                            room[i].Days=PropAccomDays1;
                            break;
                        case "POKÓJ BUSSINES 1-OSOBOWY":
                            room[i].Amount =  PropAccomAmount2 ;
                            room[i].Days=PropAccomDays2;
                            break;
                        case "POKÓJ BUSSINES 2-OSOBOWY":
                           room[i].Amount = PropAccomAmount3;
                           room[i].Days= PropAccomDays3 ;
                            break;
                        case "APARTAMENT":
                            room[i].Amount=PropAccomAmount4;
                            room[i].Days= PropAccomDays4;
                            break;
                        case "POKOJ DLA NIEPEŁNOSPRAWNYCH":
                            room[i].Amount=PropAccomAmount5 ;
                            room[i].Days=PropAccomDays5;
                            break;
                    }    
                }
                for (int i = 0; i < roomExistList.Count; i++)
                {
                    switch (roomExistList[i])
                    {
                        case "POKÓJ 1-OSOBOWY":
                            if (PropAccomAmount0 != null || PropAccomDays0 != null)
                            {
                                PropAccomodation newroom= new PropAccomodation();
                                newroom.Id_proposition = _idProposition;
                                newroom.Amount = PropAccomAmount0;
                                newroom.BruttoPrice = PropAccomBrutto0;
                                newroom.Days = PropAccomDays0;
                                newroom.TypeOfRoom = PropAccomTypeOfRoom0;
                                newroom.Vat = PropAccomVat0;
                                _ctx.PropAccomodation.Add(newroom);
                            }
                            break;
                        case "POKÓJ 2-OSOBOWY":
                            if (PropAccomAmount1 != null || PropAccomDays1 != null)
                            {
                                PropAccomodation newroom = new PropAccomodation();
                                newroom.Id_proposition = _idProposition;
                                newroom.Amount = PropAccomAmount1;
                                newroom.BruttoPrice = PropAccomBrutto1;
                                newroom.Days = PropAccomDays1;
                                newroom.TypeOfRoom = PropAccomTypeOfRoom1;
                                newroom.Vat = PropAccomVat1;
                                _ctx.PropAccomodation.Add(newroom);
                            }
                            break;
                        case "POKÓJ BUSSINES 1-OSOBOWY":
                            if (PropAccomAmount2 != null || PropAccomDays2 != null)
                            {
                                PropAccomodation newroom = new PropAccomodation();
                                newroom.Id_proposition = _idProposition;
                                newroom.Amount = PropAccomAmount2;
                                newroom.BruttoPrice = PropAccomBrutto2;
                                newroom.Days = PropAccomDays2;
                                newroom.TypeOfRoom = PropAccomTypeOfRoom2;
                                newroom.Vat = PropAccomVat2;
                                _ctx.PropAccomodation.Add(newroom);
                            }
                            break;
                        case "POKÓJ BUSSINES 2-OSOBOWY":
                            if (PropAccomAmount3 != null || PropAccomDays3 != null)
                            {
                                PropAccomodation newroom = new PropAccomodation();
                                newroom.Id_proposition = _idProposition;
                                newroom.Amount = PropAccomAmount3;
                                newroom.BruttoPrice = PropAccomBrutto3;
                                newroom.Days = PropAccomDays3;
                                newroom.TypeOfRoom = PropAccomTypeOfRoom3;
                                newroom.Vat = PropAccomVat3;
                                _ctx.PropAccomodation.Add(newroom);
                            }
                            break;
                        case "APARTAMENT":
                            if (PropAccomAmount4 != null || PropAccomDays4 != null)
                            {
                                PropAccomodation newroom = new PropAccomodation();
                                newroom.Id_proposition = _idProposition;
                                newroom.Amount = PropAccomAmount4;
                                newroom.BruttoPrice = PropAccomBrutto4;
                                newroom.Days = PropAccomDays4;
                                newroom.TypeOfRoom = PropAccomTypeOfRoom4;
                                newroom.Vat = PropAccomVat4;
                                _ctx.PropAccomodation.Add(newroom);
                            }
                            break;
                        case "POKOJ DLA NIEPEŁNOSPRAWNYCH":
                            if (PropAccomAmount5 != null || PropAccomDays5 != null)
                            {
                                PropAccomodation newroom = new PropAccomodation();
                                newroom.Id_proposition = _idProposition;
                                newroom.Amount = PropAccomAmount5;
                                newroom.BruttoPrice = PropAccomBrutto5;
                                newroom.Days = PropAccomDays5;
                                newroom.TypeOfRoom = PropAccomTypeOfRoom5;
                                newroom.Vat = PropAccomVat5;
                                _ctx.PropAccomodation.Add(newroom);
                            }
                            break;
                    }    
                }
                var mergeaccom = (from q in _ctx.PropAccomodationDiscount
                                  where q.Id_proposition==idProposition
                    select q).SingleOrDefault();
                mergeaccom.Discount = PropAccomDiscountValue;
                _ctx.SaveChanges();

                SelectedProposition = null;
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
            InitializeObjects();
            FillNeededList();
            SetDefaultValues();

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
            PropMenuGastThingDict0 = gastThingDict;
            PropMenuGastThingDict1 = gastThingDict;
            PropMenuGastThingDict2 = gastThingDict;
            PropMenuGastThingDict3 = gastThingDict;
            PropMenuGastThingDict4 = gastThingDict;
            PropMenuGastThingDict5 = gastThingDict;
            PropMenuGastThingDict6 = gastThingDict;

            _filter = (from x in _ctx.PropMenuGastronomicThings_Dictionary_First
                       group x by x.SpecificType into g
                       select g.Key).ToList();
            Filter = _filter;


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

            //tab4
            // wypełnienie stawkami VAT
            var vat4 = (from he in _ctx.VatList
                        select he.Vat).ToList();
            VatList4 = vat4;

            PropAccomVat0 = vat4[0];
            PropAccomVat1 = vat4[0];
            PropAccomVat2 = vat4[0];
            PropAccomVat3 = vat4[0];
            PropAccomVat4 = vat4[0];
            PropAccomVat5 = vat4[0];


            //wypelnienie nazw pokoi
            var rooms = (from r in _ctx.PropAccomodation_Dictionary
                         select r).ToList();
            PropAccomTypeOfRoom0 = rooms[0].TypeOfRoom;
            PropAccomTypeOfRoom1 = rooms[1].TypeOfRoom;
            PropAccomTypeOfRoom2 = rooms[2].TypeOfRoom;
            PropAccomTypeOfRoom3 = rooms[3].TypeOfRoom;
            PropAccomTypeOfRoom4 = rooms[4].TypeOfRoom;
            PropAccomTypeOfRoom5 = rooms[5].TypeOfRoom;

            // wypełnienie domyslnymi cenami brutto
            PropAccomBrutto0 = rooms[0].Price;
            PropAccomBrutto1 = rooms[1].Price;
            PropAccomBrutto2 = rooms[2].Price;
            PropAccomBrutto3 = rooms[3].Price;
            PropAccomBrutto4 = rooms[4].Price;
            PropAccomBrutto5 = rooms[5].Price;
            // uzupelnianie slownikami form platnosci
            var fi = (from r in _ctx.PropPaymentSuggestions_Dictionary_First
                      select r.PaymentForm).ToList();
            PropPaySuggDictFirst = fi;

            var fii = (from r in _ctx.PropPaymentSuggestions_Dictionary_Second
                       select r.InvoiceServiceName).ToList();
            PropPaySuggDictSecond = fii;

            var fiii = (from r in _ctx.PropPaymentSuggestions_Dictionary_Third
                        select r.IndividualOrders).ToList();
            PropPaySuggDictThird = fiii;

            var fiv = (from r in _ctx.PropPaymentSuggestions_Dictionary_Fourth
                       select r.CarPark).ToList();
            PropPaySuggDictFourth = fiv;

            // uzupelnienie slownikiem z parkingami (dodatkowe wypos.)
            var ext = (from r in _ctx.PropExtraServices_Dictionary
                       select r.ServiceType).ToList();
            PropExtraServTypeDict = ext;

            PropExtraServVat0 = vat4[0];
            PropExtraServVat1 = vat4[0];
            PropExtraServVat2 = vat4[0];
            PropExtraServVat3 = vat4[0];
        }

        private bool CanChangePropositionExecute(object arg)
        {
            return true;
        }
        private void ChangePropositionExecute(object obj)
        {
            _saveFlag = true;
            HallListFunction();
            //InitializeObjects();
            //FillNeededList();
            //SetDefaultValues();
            roomExistList = (from q in _ctx.PropAccomodation_Dictionary
                select q.TypeOfRoom).ToList();
            _idProposition = SelectedProposition.PropositionId;
            SelectedProposition = null;
            try
            {
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
            }
            catch (Exception e)
            {
                MessageBox.Show("Klient sie zjebał"+e.ToString());
            }
            var editDetalis = (from q in _ctx.PropReservationDetails
                               where q.Id_proposition == _idProposition
                               select q).SingleOrDefault();
            try
            {
                

                PropositionReservDetailsStartData = editDetalis.StartData;
                PropositionReservDetailsEndData = editDetalis.EndData;
                PropositionReservDetailsStartTime = editDetalis.StartTime;
                PropositionReservDetailsEndTime = editDetalis.EndTime;
                if (editDetalis.Hall != null)
                {
                    PropositionReservDetailsHall = editDetalis.Hall;
                }
                if (editDetalis.PeopleNumber != null)
                {
                    PropositionReservDetailsPeopleNumber = editDetalis.PeopleNumber;
                }
                PropositionReservDetailsHallSetting = editDetalis.HallSetting;

            }
            catch (Exception e)
            {

                MessageBox.Show("Detale nie dziłaja" + e.ToString());
            }
            
            List<PropHallEquipment> editEquipment = (from q in _ctx.PropHallEquipment
                                                     where q.Id_proposition == _idProposition
                                                     select q).ToList();
            try
            {
               
                var halla = editEquipment.Find(item => item.Things == "Sala " + editDetalis.Hall);
                if (halla != null)
                {
                    PropHallEqThing0 = halla.Things;
                    PropHallEqAmount0 = halla.Amount;
                    PropHallEqDays0 = halla.Days;
                    PropHallEqVat0 = halla.Vat;
                    editEquipment.Remove(halla);
                }
            }
            catch (Exception e)
            {
              
              MessageBox.Show("Pierwsza linia szczegłów" + e.ToString());
                
            }
 
            try
            {
                for (int i = 0; i < editEquipment.Count; i++)
                {

                    switch (i)
                    {
                        case 0:

                            PropHallEqThing1 = editEquipment[0].Things;
                            PropHallEqBrutto1 = editEquipment[0].BruttoPrice;
                            PropHallEqAmount1 = editEquipment[0].Amount;
                            PropHallEqDays1 = editEquipment[0].Days;
                            PropHallEqVat1 = editEquipment[0].Vat;
                            break;
                        case 1:
                            PropHallEqThing2 = editEquipment[1].Things;
                            PropHallEqBrutto2 = editEquipment[1].BruttoPrice;
                            PropHallEqAmount2 = editEquipment[1].Amount;
                            PropHallEqDays2 = editEquipment[1].Days;
                            break;
                        case 2:
                            PropHallEqThing3 = editEquipment[2].Things;
                            PropHallEqBrutto3 = editEquipment[2].BruttoPrice;
                            PropHallEqAmount3 = editEquipment[2].Amount;
                            PropHallEqDays3 = editEquipment[2].Days;
                            PropHallEqVat3 = editEquipment[2].Vat;
                            break;
                        case 3:
                            PropHallEqThing4 = editEquipment[3].Things;
                            PropHallEqBrutto4 = editEquipment[3].BruttoPrice;
                            PropHallEqAmount4 = editEquipment[3].Amount;
                            PropHallEqDays4 = editEquipment[3].Days;
                            PropHallEqVat4 = editEquipment[3].Vat;
                            break;
                        case 4:
                            PropHallEqThing5 = editEquipment[4].Things;
                            PropHallEqBrutto5 = editEquipment[4].BruttoPrice;
                            PropHallEqAmount5 = editEquipment[4].Amount;
                            PropHallEqDays5 = editEquipment[4].Days;
                            PropHallEqVat5 = editEquipment[4].Vat;
                            break;
                    }
                }
                var editEquipmentDiscount = (from propHallEquipment in _ctx.PropHallEquipmentDiscount
                    where propHallEquipment.Id_proposition == _idProposition
                    select propHallEquipment).SingleOrDefault();
                if (editEquipmentDiscount.Discount != null) 
                PropHallEquipmentDiscountValue = editEquipmentDiscount.Discount;
                if (editEquipmentDiscount.StandardPrice != null) 
                PropHallEquipmentDiscountStandPrice = editEquipmentDiscount.StandardPrice;
            }
            catch (Exception e)
            {
                MessageBox.Show("Reszta szczegłów" + e.ToString());
            }

            var editGastronomicMerge = (from q in _ctx.PropMenuMerge
                where q.Id_proposition == _idProposition
                select q).ToList();
            if (editGastronomicMerge != null)
            {
                _propMenuMerges = editGastronomicMerge;
                PropMenuMerge0 = editGastronomicMerge[0].DefaultValue;
                PropMenuMerge1 = editGastronomicMerge[1].DefaultValue;
                PropMenuMerge2 = editGastronomicMerge[2].DefaultValue;
                PropMenuMerge3 = editGastronomicMerge[3].DefaultValue;
                PropMenuMerge4 = editGastronomicMerge[4].DefaultValue;
            }
            var editGastronomic = (from q in _ctx.PropMenuPosition
                                   where q.Id_proposition == _idProposition
                                   select q).ToList();

            //PropMenuPositions = editGastronomic;
            //_propMenuPositions = editGastronomic;
            
            try
            {
                
               // MessageBox.Show(editGastronomic[0].TypeOfService.ToString());

                    for (int i = 0; i < editGastronomic.Count; i++)
                 {
                     switch (i)
                     {
                         case 0:

                             var typ = (from q in _ctx.PropMenuGastronomicThings_Dictionary_First
                                        select q).ToList().Where(x => x.ThingName == editGastronomic[0].TypeOfService).SingleOrDefault();
                             
                             SelectedType0 = typ.SpecificType;
                          PropMenuPosMergeType0 = editGastronomic[0].MergeType;
                          PropMenuPosVat0 = editGastronomic[0].Vat;
                          PropMenuTypeOfServ0 = editGastronomic[0].TypeOfService;
                           if(editGastronomic[0].Amount!=null)
                          PropMenuPosAmount0 = editGastronomic[0].Amount;
                          if(editGastronomic[0].Days !=null)
                          PropMenuPosDays0 = editGastronomic[0].Days;
                          break;
                      case 1:
                             
                              var typ1 = (from q in _ctx.PropMenuGastronomicThings_Dictionary_First
                                        select q).ToList().Where(x => x.ThingName == editGastronomic[1].TypeOfService).SingleOrDefault();
                             SelectedType1 = typ1.SpecificType;
                          PropMenuPosMergeType1 = editGastronomic[1].MergeType;
                          PropMenuPosVat1 = editGastronomic[1].Vat;
                          PropMenuTypeOfServ1 = editGastronomic[1].TypeOfService;
                          PropMenuPosAmount1 = editGastronomic[1].Amount;
                          PropMenuPosDays1 = editGastronomic[1].Days;
                          //SelectedType1 = editGastronomic[0].;
                           
                          break;
                      case 2:
                                var typ2 = (from q in _ctx.PropMenuGastronomicThings_Dictionary_First
                                 select q).ToList().Where(x => x.ThingName == editGastronomic[2].TypeOfService).SingleOrDefault();;
                             SelectedType2 = typ2.SpecificType;
                          PropMenuPosMergeType2 = editGastronomic[2].MergeType;
                          PropMenuPosVat2 = editGastronomic[2].Vat;
                          PropMenuTypeOfServ2 = editGastronomic[2].TypeOfService;
                          PropMenuPosAmount2 = editGastronomic[2].Amount;
                          PropMenuPosDays2 = editGastronomic[2].Days;
                            
                          break;
                      case 3:
                                 var typ3 = (from q in _ctx.PropMenuGastronomicThings_Dictionary_First
                               select q).ToList().Where(x => x.ThingName == editGastronomic[3].TypeOfService).SingleOrDefault();;
                            SelectedType3 = typ3.SpecificType;
                          PropMenuPosMergeType3 = editGastronomic[3].MergeType;
                          PropMenuPosVat3 = editGastronomic[3].Vat;
                          PropMenuTypeOfServ3 = editGastronomic[3].TypeOfService;
                          PropMenuPosAmount3 = editGastronomic[3].Amount;
                          PropMenuPosDays3 = editGastronomic[3].Days;
                            
                          break;
                      case 4:
                              var typ4 = (from q in _ctx.PropMenuGastronomicThings_Dictionary_First
                                    select q).ToList().Where(x => x.ThingName == editGastronomic[4].TypeOfService).SingleOrDefault();;
                            SelectedType3 = typ4.SpecificType;
                          PropMenuPosMergeType4 = editGastronomic[4].MergeType;
                          PropMenuPosVat4 = editGastronomic[4].Vat;
                          PropMenuTypeOfServ4 = editGastronomic[4].TypeOfService;
                          PropMenuPosAmount4 = editGastronomic[4].Amount;
                          PropMenuPosDays4 = editGastronomic[4].Days;
                          PropMenuPosMergeType4 = editGastronomic[4].MergeType;
                          break;
                      case 5:
                                var typ5 = (from q in _ctx.PropMenuGastronomicThings_Dictionary_First
                             
                              select q).ToList().Where(x => x.ThingName == editGastronomic[0].TypeOfService).SingleOrDefault();;
                         SelectedType5 = typ5.SpecificType;
                          PropMenuPosMergeType5 = editGastronomic[5].MergeType;
                          PropMenuPosVat5 = editGastronomic[5].Vat;
                          PropMenuTypeOfServ5 = editGastronomic[5].TypeOfService;
                          PropMenuPosAmount5 = editGastronomic[5].Amount;
                          PropMenuPosDays5 = editGastronomic[5].Days;
                            
                          break;
                      case 6:
                                    var typ6 = (from q in _ctx.PropMenuGastronomicThings_Dictionary_First
                                 
                                 select q).ToList().Where(x => x.ThingName == editGastronomic[0].TypeOfService).SingleOrDefault();
                            SelectedType6 = typ6.SpecificType;
                          PropMenuPosMergeType6 = editGastronomic[6].MergeType;
                          PropMenuPosVat6 = editGastronomic[6].Vat;
                          PropMenuTypeOfServ6 = editGastronomic[6].TypeOfService;
                          PropMenuPosAmount6 = editGastronomic[6].Amount;
                          PropMenuPosDays6 = editGastronomic[6].Days;
                          PropMenuPosMergeType6 = editGastronomic[6].MergeType;
                          break;
                  }

              }
            }
            catch(Exception e)
            {
                 MessageBox.Show("Gastronomia " + e.ToString());
            }

            try
            {
                var propAccomodation = (from q in _ctx.PropAccomodation
                    where q.Id_proposition == _idProposition
                    select q).ToList();
               for(int i = 0 ; i < propAccomodation.Count;i++)
                {
                    switch (propAccomodation[i].TypeOfRoom)
                    {
                        case "POKÓJ 1-OSOBOWY":
                            if (propAccomodation[i].Amount!=null)
                            PropAccomAmount0 = propAccomodation[i].Amount;
                            if (propAccomodation[i].Days != null)
                            PropAccomDays0 = propAccomodation[i].Days;
                           // MessageBox.Show(roomExistList.Count.ToString());
                            roomExistList.Remove("POKÓJ 1-OSOBOWY");
                            //MessageBox.Show(roomExistList.Count.ToString());
                            break;
                        case "POKÓJ 2-OSOBOWY":
                            if (propAccomodation[i].Amount!=null)
                            PropAccomAmount1 = propAccomodation[i].Amount;
                            if (propAccomodation[i].Days != null)
                            PropAccomDays1 = propAccomodation[i].Days;
                            roomExistList.Remove("POKÓJ 2-OSOBOWY");
                            break;
                        case "POKÓJ BUSSINES 1-OSOBOWY":
                            if (propAccomodation[i].Amount!=null)
                            PropAccomAmount2 = propAccomodation[i].Amount;
                            if (propAccomodation[i].Days != null)
                            PropAccomDays2 = propAccomodation[i].Days;
                            roomExistList.Remove("POKÓJ BUSSINES 1-OSOBOWY");
                            break;
                        case "POKÓJ BUSSINES 2-OSOBOWY":
                            if (propAccomodation[i].Amount!=null)
                            PropAccomAmount3 = propAccomodation[i].Amount;
                            if (propAccomodation[i].Days != null)
                            PropAccomDays3 = propAccomodation[i].Days;
                            roomExistList.Remove("POKÓJ BUSSINES 2-OSOBOWY");
                            break;
                        case "APARTAMENT":
                            if (propAccomodation[i].Amount!=null)
                            PropAccomAmount4 = propAccomodation[i].Amount;
                            if (propAccomodation[i].Days != null)
                            PropAccomDays4 = propAccomodation[i].Days;
                            roomExistList.Remove("APARTAMENT");
                            break;
                        case "POKOJ DLA NIEPEŁNOSPRAWNYCH":
                            if (propAccomodation[i].Amount!=null)
                            PropAccomAmount5 = propAccomodation[i].Amount;
                            if (propAccomodation[i].Days != null)
                            PropAccomDays5 = propAccomodation[i].Days;
                            roomExistList.Remove("POKOJ DLA NIEPEŁNOSPRAWNYCH");
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                   MessageBox.Show("nocleg " + e.ToString());
            }
            var propAccomDiscountValue = (from q in _ctx.PropAccomodationDiscount
                where q.Id_proposition == _idProposition
                select q).SingleOrDefault();
            if (propAccomDiscountValue !=null)
                PropAccomDiscountValue = propAccomDiscountValue.Discount;
            //MessageBox.Show(roomExistList.Count.ToString());
//Dodatkowe
            var extra = (from q in _ctx.PropExtraServices
                where q.Id_proposition == _idProposition
                select q).ToList();
            for (int i = 0; i < extra.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        PropExtraServType0 = extra[0].ServiceType;
                        PropExtraServVat0 = extra[0].Vat;
                        PropExtraServAmount0 = extra[0].Amount;
                        PropExtraServDays0 = extra[0].Days;
                        PropExtraServBrutto0 = extra[0].BruttoPrice;

                        break;
                    case 1: 
                        PropExtraServType1 = extra[1].ServiceType;
                        PropExtraServVat1 = extra[1].Vat;
                        PropExtraServAmount1 = extra[1].Amount;
                        PropExtraServDays1 = extra[1].Days;
                        PropExtraServBrutto1 = extra[1].BruttoPrice;
                        break;
                    case 2: 
                        PropExtraServType2 = extra[2].ServiceType;
                        PropExtraServVat2 = extra[2].Vat;
                        PropExtraServAmount2 = extra[2].Amount;
                        PropExtraServDays2 = extra[2].Days;
                        PropExtraServBrutto2 = extra[2].BruttoPrice;
                        break;
                    case 3: 
                        PropExtraServType3 = extra[3].ServiceType;
                        PropExtraServVat3 = extra[3].Vat;
                        PropExtraServAmount3 = extra[3].Amount;
                        PropExtraServDays3 = extra[3].Days;
                        PropExtraServBrutto3 = extra[3].BruttoPrice;
                        break;
                        
                }
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
            PropHallEquipmentDiscountStandPrice = price;
        }

        private void SetEquipmentDiscountPrice()
        {
            if (PropHallEquipmentDiscountValue.HasValue && PropHallEquipmentDiscountStandPrice.HasValue)
            {
                PropHallPriceAfterDiscount = Math.Ceiling((decimal)PropHallEquipmentDiscountStandPrice -
                                                         ((decimal)PropHallEquipmentDiscountStandPrice *
                                                          (decimal)PropHallEquipmentDiscountValue / 100));
                PropHallEqBrutto0 = (float?)PropHallPriceAfterDiscount;
            }
            else if (!PropHallEquipmentDiscountValue.HasValue)
            {
                PropHallEquipmentDiscountValue = 0;
            }
        }

        private void HallListFunction()
        {

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
            var gastThingDict = (from gt in _ctx.PropMenuGastronomicThings_Dictionary_First
                                 select gt.ThingName).ToList();
            PropMenuGastThingDict0 = gastThingDict;
            PropMenuGastThingDict1 = gastThingDict;
            PropMenuGastThingDict2= gastThingDict;
            PropMenuGastThingDict3 = gastThingDict;
            PropMenuGastThingDict4 = gastThingDict;
            PropMenuGastThingDict5 = gastThingDict;
            PropMenuGastThingDict6 = gastThingDict;
            Filter = (from x in _ctx.PropMenuGastronomicThings_Dictionary_First
                       group x by x.SpecificType into g
                       select g.Key).ToList();

  

            //tab4
            // wypełnienie stawkami VAT
            var vat4 = (from he in _ctx.VatList
                        select he.Vat).ToList();
            VatList4 = vat4;

            PropAccomVat0 = vat4[0];
            PropAccomVat1 = vat4[0];
            PropAccomVat2 = vat4[0];
            PropAccomVat3 = vat4[0];
            PropAccomVat4 = vat4[0];
            PropAccomVat5 = vat4[0];


            //wypelnienie nazw pokoi
            var rooms = (from r in _ctx.PropAccomodation_Dictionary
                         select r).ToList();
            PropAccomTypeOfRoom0 = rooms[0].TypeOfRoom;
            PropAccomTypeOfRoom1 = rooms[1].TypeOfRoom;
            PropAccomTypeOfRoom2 = rooms[2].TypeOfRoom;
            PropAccomTypeOfRoom3 = rooms[3].TypeOfRoom;
            PropAccomTypeOfRoom4 = rooms[4].TypeOfRoom;
            PropAccomTypeOfRoom5 = rooms[5].TypeOfRoom;

            // wypełnienie domyslnymi cenami brutto
            PropAccomBrutto0 = rooms[0].Price;
            PropAccomBrutto1 = rooms[1].Price;
            PropAccomBrutto2 = rooms[2].Price;
            PropAccomBrutto3 = rooms[3].Price;
            PropAccomBrutto4 = rooms[4].Price;
            PropAccomBrutto5 = rooms[5].Price;

            // uzupelnianie slownikami form platnosci
            var fi = (from r in _ctx.PropPaymentSuggestions_Dictionary_First
                      select r.PaymentForm).ToList();
            PropPaySuggDictFirst = fi;

            var fii = (from r in _ctx.PropPaymentSuggestions_Dictionary_Second
                       select r.InvoiceServiceName).ToList();
            PropPaySuggDictSecond = fii;

            var fiii = (from r in _ctx.PropPaymentSuggestions_Dictionary_Third
                        select r.IndividualOrders).ToList();
            PropPaySuggDictThird = fiii;

            var fiv = (from r in _ctx.PropPaymentSuggestions_Dictionary_Fourth
                       select r.CarPark).ToList();
            PropPaySuggDictFourth = fiv;

            // uzupelnienie slownikiem z parkingami (dodatkowe wypos.)
            var ext = (from r in _ctx.PropExtraServices_Dictionary
                       select r.ServiceType).ToList();
            PropExtraServTypeDict = ext;

            PropExtraServVat0 = vat4[0];
            PropExtraServVat1 = vat4[0];
            PropExtraServVat2 = vat4[0];
            PropExtraServVat3 = vat4[0];

        }

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

            //FourthTabNettoValueList
            for (int i = 0; i < _fourthTabNettoValue.Capacity; i++)
                _fourthTabNettoValue.Add(new decimal());

            //ThirdTabBruttoValueList
            for (int i = 0; i < _fourthTabBruttoValue.Capacity; i++)
                _fourthTabBruttoValue.Add(new decimal());

            //FourthTabNettoPriceList
            for (int i = 0; i < _fourthTabNettoPrice.Capacity; i++)
                _fourthTabNettoPrice.Add(new decimal?());

            //PropositionAccomodation
            for (int i = 0; i < _propAccomodations.Capacity; i++)
                _propAccomodations.Add(new PropAccomodation());
            for (int i = 0; i < _propAccomodations.Capacity; i++)
                _propExtraServ.Add(new PropExtraServices());

            //FifthTabNettoPriceList
            for (int i = 0; i < _fifthTabNettoPrice.Capacity; i++)
                _fifthTabNettoPrice.Add(new decimal?());

            //FifthTabNettoValueList
            for (int i = 0; i < _fifthTabNettoValue.Capacity; i++)
                _fifthTabNettoValue.Add(new decimal());

            //FifthTabBruttoValueList
            for (int i = 0; i < _fifthTabBruttoValue.Capacity; i++)
                _fifthTabBruttoValue.Add(new decimal());
        }

        private void InitializeObjects()
        {
            _propositionClient = new PropClient();
            _propositionReservDetails = new PropReservationDetails();
            _hallCapacity = new PropReservationDetails_Dictionary_HallCapacity();
            _propHallEquipmentDiscount = new PropHallEquipmentDiscount();
            _propHallEquipment = new List<PropHallEquipment>(6);
            _secondTabNettoPrice = new List<decimal>(6);
            _secondTabNettoValue = new List<decimal>(6);
            _secondTabBruttoValue = new List<decimal>(6);
            _vatList = new List<float?>(2);
            _propMenuPositions = new List<PropMenuPosition>(7);
            _thirdTabNettoPrice = new List<decimal?>(7);
            _propMenuMerges = new List<PropMenuMerge>(5);
            _defaultMerges = new List<string>(5);
            _thirdTabNettoValue = new List<decimal>(7);
            _thirdTabBruttoValue = new List<decimal>(7);
            _propAccomodations = new List<PropAccomodation>(6);
            _propAccomDictionary = new List<PropAccomodation_Dictionary>();
            _propAccomDiscount = new PropAccomodationDiscount();
            _vatList4 = new List<float?>(2);
            _fourthTabNettoPrice = new List<decimal?>(6);
            _fourthTabNettoValue = new List<decimal>(6);
            _fourthTabBruttoValue = new List<decimal>(6);
            _propExtraServ = new List<PropExtraServices>(2);
            _fifthTabNettoPrice = new List<decimal?>(4);
            _fifthTabNettoValue = new List<decimal>(4);
            _fifthTabBruttoValue = new List<decimal>(4);
            _propPaymentSugg = new PropPaymentSuggestions();
            _selectedType = new List<string>(7);
        }

        private void SetDefaultValues()
        {
            PropHallEquipmentDiscountValue = 0;
            PropAccomDiscountValue = 0;
            PropHallEqVat0 = 23;
            PropHallEqVat1 = 23;
            PropHallEqVat2 = 23;
            PropHallEqVat3 = 23;
            PropHallEqVat4 = 23;
            PropHallEqVat5 = 23;

            PropAccomVat0 = 23;
            PropAccomVat1 = 23;
            PropAccomVat2 = 23;
            PropAccomVat3 = 23;
            PropAccomVat4 = 23;
            PropAccomVat5 = 23;

            PropExtraServVat0 = 23;
            PropExtraServVat1 = 23;
            PropExtraServVat2 = 23;
            PropExtraServVat3 = 23;
            for (int i = 0; i < _selectedType.Capacity; i++)
            {
                _selectedType.Add("");
            }
       
        }
        // obliczanie ceny netto na podstawie ceny brutto i vatu (tab2)PropMenuMerge0ComputeNettoPrice
        private decimal ComputeNettoPrice(float? value, float? vat)
        {
            if (value == null)
             value = 0;
   
            return Math.Round(((decimal)value * 100 / (100 + (decimal)vat)), 2);
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
            float? toret = (float?)netto + (float?)netto * vat.Value / 100;
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
            //            if (price == null)
            //                price = 0;
            var mergeValue =
                _propMenuMerges.Where(x => x.MergeType == mergeType)
                    .Select((x) => new { x = x.DefaultValue })
                    .Single();

            if (mergeValue.x != null)
            {
                var toret = price + ((decimal)price * (decimal)mergeValue.x / 100);
                return toret;
            }
            return price;
        }

        // przeciazona powyzsza metoda
        private float? AddMergeToPrice(float? price, string mergeType)
        {
            var mergeValue =
                _propMenuMerges.Where(x => x.MergeType == mergeType)
                    .Select((x) => new { x = x.DefaultValue })
                    .Single();

            if (mergeValue.x != null)
            {
                var toret = price + (price * mergeValue.x / 100);
                return toret;
            }
            return price;
        }

        // na podstawie domyslnej ceny (netto lub brutto) dodaj marze na podstawie odpowiedniego typu
        private decimal? SubMergeToPrice(decimal? price, string mergeType)
        {
            var mergeValue =
                _propMenuMerges.Where(x => x.MergeType == mergeType)
                    .Select((x) => new { x = x.DefaultValue })
                    .Single();
            var toret = price / (1 + (decimal?)mergeValue.x / 100);
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

        // obliczanie sumy netto (tab4)
        private void ComputeFourthTabSumNettoValue()
        {
            decimal sum = 0;
            sum += FourthTabNettoValue0;
            sum += FourthTabNettoValue1;
            sum += FourthTabNettoValue2;
            sum += FourthTabNettoValue3;
            sum += FourthTabNettoValue4;
            sum += FourthTabNettoValue5;

            FourthTabSumNettoValue = sum;
        }

        // obliczanie sumy brutto (tab4)
        private void ComputeFourthTabSumBruttoValue()
        {
            decimal sum = 0;
            sum += FourthTabBruttoValue0;
            sum += FourthTabBruttoValue1;
            sum += FourthTabBruttoValue2;
            sum += FourthTabBruttoValue3;
            sum += FourthTabBruttoValue4;
            sum += FourthTabBruttoValue5;

            FourthTabSumBruttoValue = sum;
        }

        // obliczanie sumy netto (tab5)
        private void ComputeFifthTabSumNettoValue()
        {
            decimal sum = 0;
            sum += FifthTabNettoValue0;
            sum += FifthTabNettoValue1;
            sum += FifthTabNettoValue2;
            sum += FifthTabNettoValue3;

            FifthTabSumNettoValue = sum;
        }

        // obliczanie sumy brutto (tab5)
        private void ComputeFifthTabSumBruttoValue()
        {
            decimal sum = 0;
            sum += FifthTabBruttoValue0;
            sum += FifthTabBruttoValue1;
            sum += FifthTabBruttoValue2;
            sum += FifthTabBruttoValue3;

            FifthTabSumBruttoValue = sum;
        }

        private void ComputeFullSumNetto()
        {
            decimal sum = 0;
            sum += SecondTabSumNettoValue;
            sum += ThirdTabSumNettoValue;
            sum += FourthTabSumNettoValue;
            sum += FifthTabSumNettoValue;

            FullSumNetto = sum;
        }

        private void ComputeFullSumBrutto()
        {
            decimal sum = 0;
            sum += SecondTabSumBruttoValue;
            sum += ThirdTabSumBruttoValue;
            sum += FourthTabSumBruttoValue;
            sum += FifthTabSumBruttoValue;

            FullSumBrutto = sum;
        }
        #endregion
    }
}
