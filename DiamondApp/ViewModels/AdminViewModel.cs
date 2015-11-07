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
        private AddNewProposition _addNewProposition;
        private Proposition _proposition;

        private List<Users> _usersList;
        private int _userId;
        private int _selectedPropositionId;



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
            get { return _usersList; }
            set
            {
                SelectUsers();
                _usersList = value;
                RaisePropertyChanged("UsersList");
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
                Proposition = new Proposition
                {
                    Id_user = _userId,
                    UpdateDate = _addNewProposition.UpdateDate,
                    Status = "New"  //TODO uaktualnić ewentualnie z enuma lub obgadać jak rozwiązać
                };
                _ctx.Proposition.Add(Proposition);
                _addNewProposition.IsCreated = false;
                _ctx.SaveChanges();

                /* wyciągnięcie Id dodanej propozycji który potrzebny bedzie przy dodawaniu
                 * do bazy pozostałych tabel (klucz obcy) */

                var lastPropId = (from prop in _ctx.Proposition
                    select prop).ToList().Last();
                int currentPropositionId = lastPropId.Id;
                
                //------------------------------


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


#endregion

#region Methods

        private void SelectUsers()
        {
            var q = (from s in _ctx.Users
                     select s).ToList();
            _usersList = q;
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
#endregion
    }
}
