using System.Collections.Generic;
using System.Linq;
using DiamondApp.EntityModel;
using DiamondApp.Tools;

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
