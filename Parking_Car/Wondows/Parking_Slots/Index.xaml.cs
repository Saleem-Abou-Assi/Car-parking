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
using Microsoft.EntityFrameworkCore;
using Parking_Car.Models;


namespace Parking_Car.Wondows.Parking_Slots
{
    /// <summary>
    /// Interaction logic for Index.xaml
    /// </summary>
    public partial class Index : Window
    {
        private readonly DataBaseContext _context;

        public Index()
        {
            InitializeComponent();
            _context = new DataBaseContext();
            LoadData();
        }

        private void LoadData()
        {
            _context.ParkingSlots.Load();
            slotsGrid.ItemsSource = null;
            slotsGrid.ItemsSource = _context.ParkingSlots.Local.ToObservableCollection();

        }
        private void DeleteSlot(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is ParkingSlot parkingSlot)
            {
                _context.ParkingSlots.Remove(parkingSlot);
                _context.SaveChanges();
                LoadData(); // Refresh the DataGrid
            }
        }
        private void EditSlot(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is ParkingSlot parkingSlot)
            {
                Wondows.Parking_Slots.Edit editWindow = new Wondows.Parking_Slots.Edit(parkingSlot);
                if (editWindow.ShowDialog() == true)
                {
                    _context.SaveChanges();
                   
                    LoadData(); // Refresh the DataGrid
                }
                
            }
        }
        private void AddSlot(object sender, RoutedEventArgs e)
        {
            Wondows.Parking_Slots.Edit editWindow = new Wondows.Parking_Slots.Edit(null);
            if (editWindow.ShowDialog() == true)
            {
                _context.SaveChanges();

                LoadData(); // Refresh the DataGrid
            }
        }
    }
}
