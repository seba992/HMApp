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
using DiamondApp.EntityModel;
using DiamondApp.ViewModels;

namespace DiamondApp.Views
{
    /// <summary>
    /// Interaction logic for DictionaryView.xaml
    /// </summary>
    public partial class DictionaryView : Window
    {
        public DictionaryView()
        {
            InitializeComponent();
          
        }

        public void Changeview(object sender, EventArgs e)
        {

            string test = DictionaryList.SelectionBoxItem.ToString();
            switch (test)
            {
                case "Gastronomia": LTyp.Visibility = Visibility.Hidden;
                                    Ctyp.Visibility = Visibility.Hidden;
                                    Dictionary.ItemsSource = "{Binding Gastronomic}";
                               
                    //MessageBox.Show(Dictionary.ItemsSource.ToString());
                                    break;
                case "Pokoje": LTyp.Visibility = Visibility.Visible;
                               Ctyp.Visibility = Visibility.Visible;
                               Dictionary.ItemsSource = "{Binding HallPrices}";
                               Dictionary.Items.Refresh();
                               break;
            }

        }

    }
  
}
