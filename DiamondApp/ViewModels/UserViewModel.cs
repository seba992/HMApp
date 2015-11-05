using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using DiamondApp.EntityModel;
using DiamondApp.Tools;
using Microsoft.Practices.ServiceLocation;

namespace DiamondApp.ViewModels
{
    public class UserViewModel : ObservableObject
    {
        private DiamondDBEntities _ctx;
        public int _userId;
        public List<Proposition> propositionList;


        public UserViewModel()
        {
            _ctx = new DiamondDBEntities();
            MyMethod();
        }

        public UserViewModel(int userId) :base()
        {
            _userId = userId;
            _ctx = new DiamondDBEntities();
            MyMethod();
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


        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        private void MyMethod()
        {
            var myQuerry = (from s in _ctx.Proposition
                select s).FirstOrDefault();
        }
    }
}
