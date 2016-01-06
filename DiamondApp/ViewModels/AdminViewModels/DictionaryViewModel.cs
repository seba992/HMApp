using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using DiamondApp.Model;
using DiamondApp.Tools.MvvmClasses;

namespace DiamondApp.ViewModels.AdminViewModels
{
    public class DictionaryViewModel : ObservableObject
    {
        public DictionaryViewModel()
        {
            _ctx = new DiamondDBEntities();
            var s = (from q in _ctx.PropMenuGastronomicThings_Dictionary_First
                select q).ToList();
         //MessageBox.Show(_ctx.GetType().GetProperties().ToList()[0].ToString());
         //PropertyInfop = _ctx.GetType().GetProperties().ToList();
            //Gastronamia
            DictionaryUpdate();

        }

        private DiamondDBEntities _ctx;

        private ObservableCollection<PropMenuGastronomicThings_Dictionary_First> _gastronomic; 
        private List<PropReservationDetails_Dictionary_HallPrices> _hallPrices;
        private List<string> _filter;
        private List<string> _listTable = new List<string> { "Gastronomia", "Pokoje", "Sale" };
        private string _selectTable;
        private string _selectFilter;
        private List<PropAccomodation_Dictionary> _listAccomaDictionaries;
        private PropMenuGastronomicThings_Dictionary_First _seletedDeleteElement;
        private ICommand _deleteCommand;


        private void DictionaryUpdate()
        {
            ObservableCollection<PropMenuGastronomicThings_Dictionary_First> test1 = new ObservableCollection<PropMenuGastronomicThings_Dictionary_First>((from x in _ctx.PropMenuGastronomicThings_Dictionary_First
                select x).ToList());
            Gastronomic = test1;
            // Sale
            var test2 = (from x in _ctx.PropReservationDetails_Dictionary_HallPrices
                         select x).ToList();
            HallPrices = test2;
            //Filtrowanie gatronomia
            _filter=new List<string>();
            _filter.Add(" ");
            _filter.AddRange((from x in _ctx.PropMenuGastronomicThings_Dictionary_First
                       group x by x.SpecificType into g
                       select g.Key).ToList());
            SelectTable = "Pokoje";
            var room = (from x in _ctx.PropAccomodation_Dictionary
               select x).ToList();
            ListAccomaDictionaries = room;
           
        }

        public ObservableCollection<PropMenuGastronomicThings_Dictionary_First> Gastronomic
        {
            get { return _gastronomic;}
            set
            {
                _gastronomic = value;
                RaisePropertyChanged("Gastronomic");
            }
        }

        public string SelectTable
        {
            get { return _selectTable; }
            set
            {
                _selectTable = value;
                HallPrices = (from x in _ctx.PropReservationDetails_Dictionary_HallPrices
                              select x).ToList();
                Gastronomic=new ObservableCollection<PropMenuGastronomicThings_Dictionary_First>((from x in _ctx.PropMenuGastronomicThings_Dictionary_First
                    select x).ToList());
                ListAccomaDictionaries=(from x in _ctx.PropAccomodation_Dictionary
                                       select x).ToList();
                RaisePropertyChanged("SelectPropertyInfo");
                
            }
        }

        public List<PropReservationDetails_Dictionary_HallPrices> HallPrices
        {
            get { return _hallPrices; }
            set
            {
                _hallPrices = value;
                RaisePropertyChanged("HallPrices");
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

        public List<string> ListTable {
            get { return _listTable; }
            set
            {
                _listTable = value;
                RaisePropertyChanged("ListTable");
            }
        }

        public string SelectedFilter {
            get { return _selectFilter; }
            set
            {
                _selectFilter = value;
                RaisePropertyChanged("SelectedFilter");
                if (value != " ")
                {
                    Gastronomic = new ObservableCollection<PropMenuGastronomicThings_Dictionary_First>((from x in _ctx.PropMenuGastronomicThings_Dictionary_First
                        where x.SpecificType == value
                        select x).ToList());
                }
                else
                {
                    Gastronomic = new ObservableCollection<PropMenuGastronomicThings_Dictionary_First>((from x in _ctx.PropMenuGastronomicThings_Dictionary_First
                        select x).ToList());
                }
            }
        }

        public List<PropAccomodation_Dictionary> ListAccomaDictionaries {
            get { return _listAccomaDictionaries; }
            set
            {
                _listAccomaDictionaries = value;
                RaisePropertyChanged("ListAccomaDictionaries");
            }

        }

        public PropMenuGastronomicThings_Dictionary_First SelectedDeleteElement
        {
            get { return _seletedDeleteElement;}
            set
            {
                _seletedDeleteElement = value;
                RaisePropertyChanged("SelectedDeleteElement");
            }
        }


        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = new RelayCommand(DeleteCommandExecucte, CanDeleteCommandExecute);
                }
                return _deleteCommand;
            }
        }

        private bool CanDeleteCommandExecute(object arg)
        {
            return true;
        }

        private void DeleteCommandExecucte(object obj)
        {
            if (SelectedDeleteElement != null && SelectedDeleteElement.Id!=0)
            {
                var test = (from x in _ctx.PropMenuGastronomicThings_Dictionary_First
                    where x.Id == SelectedDeleteElement.Id
                    select x).SingleOrDefault();
                _ctx.PropMenuGastronomicThings_Dictionary_First.Remove(test);
                _ctx.SaveChanges();
                Gastronomic =
                    new ObservableCollection<PropMenuGastronomicThings_Dictionary_First>(
                        (from q in _ctx.PropMenuGastronomicThings_Dictionary_First
                            select q).ToList());
                Xceed.Wpf.Toolkit.MessageBox.Show("Usunieto rekord", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }


    }
}
