using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using DiamondApp.EntityModel;
using DiamondApp.Tools;


namespace DiamondApp.ViewModels
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
            var test1 = (from x in _ctx.PropMenuGastronomicThings_Dictionary_First
                         select x).ToList();
            Gastronomic = test1;
            // Sale
            var test2 = (from x in _ctx.PropReservationDetails_Dictionary_HallPrices
                         select x).ToList();
            HallPrices = test2;
            //Filtrowanie gatronowmi
            _filter = (from x in _ctx.PropMenuGastronomicThings_Dictionary_First
                       group x by x.SpecificType into g
                       select g.Key).ToList();
            SelectTable = "Pokoje";

        }

        private DiamondDBEntities _ctx;
        
        private List<DiamondDBEntities> _items;
        private List<PropMenuGastronomicThings_Dictionary_First> _gastronomic;
        private List<PropReservationDetails_Dictionary_HallPrices> _hallPrices;
        private List<string> _filter;
        private List<string> _listTable = new List<string> { "Gastronomia", "Pokoje" };
        private string _selectTable;
        private string _selectFilter;

        public List<PropMenuGastronomicThings_Dictionary_First> Gastronomic
        {
            get { return _gastronomic;}
            set
            {
                _gastronomic = value;
                RaisePropertyChanged("Gastronomic");
            }
        }

       /* public List<string> ListTable 
        {
            get { return _listTable; }
            set
            {
                _listTable = value;
                RaisePropertyChanged("ListTable");
            }
        }*/

        public string SelectTable
        {
            get { return _selectTable; }
            set
            {
                _selectTable = value;
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
                Gastronomic = (from x in _ctx.PropMenuGastronomicThings_Dictionary_First
                    where x.SpecificType == value
                    select x).ToList();
            }
        }



    }
}
