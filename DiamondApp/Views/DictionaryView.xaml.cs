using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using DiamondApp.Model;
using DiamondApp.ViewModels;
using DiamondApp.ViewModels.AdminViewModels;

namespace DiamondApp.Views
{
    /// <summary>
    /// Interaction logic for DictionaryView.xaml
    /// </summary>
    public partial class DictionaryView : Window
    {
        private DiamondDBEntities _ctx;
        private string selectDictionry;
        private string _selectDictionry;
        private List<PropMenuGastronomicThings_Dictionary_First> _gastronomic;
        private List<PropReservationDetails_Dictionary_HallPrices> _hallPriceses;
        private DataRowView rowBeingEdited;
        private int id;
        private DictionaryViewModel tmp;
        private List<string> testing;
       
#region Construkt
        public DictionaryView( )
        {
           InitializeComponent();
           DictionaryViewModel dic = new DictionaryViewModel();
            DataContext = dic;
             _ctx = new DiamondDBEntities();
             this.Language = XmlLanguage.GetLanguage(Thread.CurrentThread.CurrentCulture.Name);  // potrzebne do zmiany wyswietlanej waluty $ -> zl
            
        }
#endregion
        public void Changeview(object sender, EventArgs e)
        {

            //selectDictionry = DictionaryList.SelectionBoxItem.ToString();
            selectDictionry = DictionaryList.SelectedItem.ToString();
           

            switch (selectDictionry)
            {
                case "Gastronomia":
                           
                               LTyp.Visibility = Visibility.Visible;
                               Ctyp.Visibility =Visibility.Visible;
                               GstronomicGrid.Visibility = Visibility.Visible;                    
                              //Dictionary.DataContext = _ctx.PropMenuGastronomicThings_Dictionary_First;
                               HallGrid.Visibility = Visibility.Hidden;
                               RoomGrid.Visibility = Visibility.Hidden;
                               DeleteButton.Visibility = Visibility.Visible;
                              

                                    break;
                case "Pokoje":
                                  
                               //Dictionary.DataContext = _ctx.PropReservationDetails_Dictionary_HallPrices;
                                GstronomicGrid.Visibility = Visibility.Hidden;
                                HallGrid.Visibility = Visibility.Hidden;
                                RoomGrid.Visibility = Visibility.Visible;
                                LTyp.Visibility = Visibility.Hidden;
                                Ctyp.Visibility = Visibility.Hidden;
                                DeleteButton.Visibility = Visibility.Hidden;
                               
                               break;
                case "Sale":
                               
                                RoomGrid.Visibility = Visibility.Hidden;
                                HallGrid.Visibility = Visibility.Visible;
                                GstronomicGrid.Visibility = Visibility.Hidden;
                                LTyp.Visibility = Visibility.Hidden;
                                Ctyp.Visibility = Visibility.Hidden;
                                DeleteButton.Visibility = Visibility.Hidden;
                                 break;
            }

        }

        public void RowEditGstronomic(object sender, DataGridRowEditEndingEventArgs e)
        {
           
            try
            {
                object item = GstronomicGrid.SelectedItem;
                string ID = (GstronomicGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                int selected = Int32.Parse(ID);
                // int selected = ((PropMenuGastronomicThings_Dictionary_First)e.Row.Item).Id;

                dynamic userRow = GstronomicGrid.SelectedItem;

                if (selected != 0 && userRow.ThingName != ""  && userRow.ThingName != null && userRow.NettoMini != null && userRow.NettoMini != 0 && userRow.MergeType != null && userRow.SpecificType != null && userRow.SpecificType != "")
                { 
                    PropMenuGastronomicThings_Dictionary_First userUpdate = 
                        (from q in _ctx.PropMenuGastronomicThings_Dictionary_First
                                        where q.Id == selected
                                        // int selected!!! you know what i want up to date
                                        select q).First();
                                userUpdate.ThingName = userRow.ThingName;
                                userUpdate.NettoMini = (float)userRow.NettoMini;
                                userUpdate.Vat = userRow.Vat;
                                userUpdate.MergeType = userRow.MergeType;
                                userUpdate.SpecificType = userRow.SpecificType;
                                
                }
                else if (userRow.ThingName != "" && userRow.ThingName != null && userRow.NettoMini != null && userRow.NettoMini != 0 && userRow.MergeType != null && userRow.SpecificType !=null && userRow.SpecificType != "")
                {
                    PropMenuGastronomicThings_Dictionary_First userUpdate = new PropMenuGastronomicThings_Dictionary_First();
                                    
                    userUpdate.ThingName = userRow.ThingName;
                    userUpdate.NettoMini = (float)userRow.NettoMini;
                    userUpdate.Vat = userRow.Vat;
                    userUpdate.MergeType = userRow.MergeType;
                    userUpdate.SpecificType = userRow.SpecificType;
                    _ctx.PropMenuGastronomicThings_Dictionary_First.Add(userUpdate);

                }
                else
                {
                    Xceed.Wpf.Toolkit.MessageBox.Show("Należy wypełnić wszystkie komórki!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                _ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Należy wypełnić wszystkie komórki!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void RowEditHall(object sender, DataGridRowEditEndingEventArgs e)
        {
            
           try
            {  
                object item = HallGrid.SelectedItem;
                string ID = (HallGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                int selected = Int32.Parse(ID);
                dynamic hallRow = GstronomicGrid.SelectedItem;
                if ( hallRow.April > 0 && hallRow.August > 0 && hallRow.December > 0
                    && hallRow.February > 0 && hallRow.January > 0 
                    && hallRow.July > 0 && hallRow.June > 0 
                    && hallRow.March > 0 && hallRow.May > 0 
                    && hallRow.November > 0 && hallRow.October > 0 
                    && hallRow.September > 0 && hallRow.Other > 0)
                {
                    PropReservationDetails_Dictionary_HallPrices hallUpdate =
                        (from q in _ctx.PropReservationDetails_Dictionary_HallPrices
                            where q.Id == selected
                            // int selected!!! you know what i want up to date
                            select q).First();



                    hallUpdate.February = hallRow.February;
                    hallUpdate.January = hallRow.January;
                    hallUpdate.March = hallRow.March;
                    hallUpdate.April = hallRow.April;
                    hallUpdate.May = hallRow.May;
                    hallUpdate.August = hallRow.August;
                    hallUpdate.July = hallRow.July;
                    hallUpdate.June = hallRow.June;
                    hallUpdate.November = hallRow.November;
                    hallUpdate.October = hallRow.October;
                    hallUpdate.December = hallRow.December;
                    hallUpdate.Hall = hallRow.Hall;
                    hallUpdate.September = hallRow.September;
                    hallUpdate.Other = hallRow.Other;
                }
                else 
                {
                    Xceed.Wpf.Toolkit.MessageBox.Show("Należy wypełnić wszystkie komórki wartościami wiekszymi niż zero!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                _ctx.SaveChanges();
            }
           catch (Exception ex)
           {
               MessageBox.Show(ex.ToString());
           }
                //
            
        }

        private void RowEditAccoma(object sender, DataGridRowEditEndingEventArgs e)
        {
            object item = RoomGrid.SelectedItem;
            string ID = (RoomGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            int selected = Int32.Parse(ID);
            dynamic roomRow = RoomGrid.SelectedItem;
            if (roomRow.Price > 0)
            {
                PropAccomodation_Dictionary roomUpdate = (from q in _ctx.PropAccomodation_Dictionary
                    where q.Id == selected
                    // int selected!!! you know what i want up to date
                    select q).First();
                roomUpdate.Price = roomRow.Price;
                roomUpdate.TypeOfRoom = roomRow.TypeOfRoom;
            }
            else
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Należy wypełnić wszystkie komórki wartościami wiekszymi niż zero!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
            _ctx.SaveChanges();
        }
    }
  
}
