using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Schema;
using DiamondApp.EntityModel;
using DiamondApp.ViewModels;

namespace DiamondApp.Views
{
    /// <summary>
    /// Interaction logic for DictionaryView.xaml
    /// </summary>
    public partial class DictionaryView : Window
    {
        private DiamondDBEntities _ctx;

        public DictionaryView()
        {
            InitializeComponent();
           _ctx = new DiamondDBEntities();
           var test1 = (from x in _ctx.PropMenuGastronomicThings_Dictionary_First
                        select x).ToList();
           Gastronomic = test1;
            var test2 = (from x in _ctx.PropReservationDetails_Dictionary_HallPrices
                select x).ToList();
            HallPriceses = test2;
        }

        private List<PropMenuGastronomicThings_Dictionary_First> _gastronomic;
        public List<PropMenuGastronomicThings_Dictionary_First> Gastronomic
        {
            get { return _gastronomic; }
            set { _gastronomic = value; }
        }

        private List<PropReservationDetails_Dictionary_HallPrices> _hallPriceses;

        public List<PropReservationDetails_Dictionary_HallPrices> HallPriceses
        {
            get {return _hallPriceses; }
            set { _hallPriceses = value; }
        }



        public void Changeview(object sender, EventArgs e)
        {

            string test = DictionaryList.SelectionBoxItem.ToString();
            switch (test)
            {
                case "Gastronomia":
                               LTyp.Visibility = Visibility.Hidden;
                               Ctyp.Visibility = Visibility.Hidden;
                    
                              //Dictionary.DataContext = _ctx.PropMenuGastronomicThings_Dictionary_First;
                              Dictionary.ItemsSource = Gastronomic;
                               
                    //MessageBox.Show(Dictionary.ItemsSource.ToString());
                                    break;
                case "Pokoje": LTyp.Visibility = Visibility.Visible;
                               Ctyp.Visibility = Visibility.Visible;
                               //Dictionary.DataContext = _ctx.PropReservationDetails_Dictionary_HallPrices;
                               Dictionary.ItemsSource = HallPriceses;
                               break;
            }

        }

    }
  
}
