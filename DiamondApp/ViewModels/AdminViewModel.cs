using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using DiamondApp.Tools;
using System.Windows.Controls;
using DiamondApp.EntityModel;

namespace DiamondApp.ViewModels
{
    class AdminViewModel : ObservableObject
    {
        private DiamondDBEntities _ctx;
        private List<Proposition> _proposition;
        public AdminViewModel()
        {
            _ctx = new DiamondDBEntities();

            FillPropositions();
        }

        private void FillPropositions()
        {
            
            var q = (from a in _ctx.Proposition
                     select a).ToList();
            this._proposition = q;
          
        }

        public List<Proposition> Propositions
        {
            get
            {
                return _proposition;
            }
            set
            {
                _proposition = value;
                RaisePropertyChanged("Propositions");
            }
        }
    }
}
