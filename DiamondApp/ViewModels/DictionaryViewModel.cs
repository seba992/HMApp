using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using DiamondApp.EntityModel;
using DiamondApp.Tools;


namespace DiamondApp.ViewModels
{
    class DictionaryViewModel : ObservableObject
    {
        public DictionaryViewModel()
        {
            _ctx = new DiamondDBEntities();
            var s = (from q in _ctx.PropMenuGastronomicThings_Dictionary_First
                select q).ToList();
         //MessageBox.Show(_ctx.GetType().GetProperties().ToList()[0].ToString());
         //PropertyInfop = _ctx.GetType().GetProperties().ToList();
        }
        private DiamondDBEntities _ctx;
        
        private List<DiamondDBEntities> _items;
        private List<PropMenuGastronomicThings_Dictionary_First> _gastronomic;
        private List<PropReservationDetails_Dictionary_HallPrices> _hallPrices; 
        private List<string> _listTable = new List<string>()
        {
            "Gastronomia",
            "Pokoje"
        };

        private string _selectTable;
        public List<PropMenuGastronomicThings_Dictionary_First> Gastronomic
        {
            get { return _gastronomic;}
            set
            {
                _gastronomic = value;
                RaisePropertyChanged("Gastronomic");
            }
        }

        public List<string> ListTable 
        {
            get { return _listTable; }
            set
            {
                _listTable = value;
                RaisePropertyChanged("ListTable");
            }
        }

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
    }
}
