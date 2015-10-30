using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DiamondApp.EntityModel;
using DiamondApp.Tools;
using Microsoft.Practices.ServiceLocation;

namespace DiamondApp.ViewModels
{
    class AdminViewModel : ObservableObject
    {
        private DiamondDBEntities _ctx;
        public List<Proposition> propositionList;
        private int _userId;
        private string _tag;

        public AdminViewModel(int userId)
        {
            _ctx = new DiamondDBEntities();
            _userId = userId;

            FillPropositions();
        }

#region Properties

        public List<Proposition> PropositionsList
        {
            get
            {
                return propositionList;
            }
            set
            {
                propositionList = value;
                RaisePropertyChanged("PropositionsList");
            }
        }

#endregion

        private void FillPropositions()
        {
            // wybierz wszystkie propozycje obecnie zalogowanego uzytkownika

            //var q = (from a in _ctx.Proposition
            //         where a.Id_user == a.Users.Id
            //         where a.Id_user == _userId
            //         select a).ToList();


            // wybierz wszystkie propozycje userow

            var q = (from a in _ctx.Proposition
                     select a).ToList();

            propositionList = q;

            MessageBox.Show(_userId.ToString());

        }
    }
}
