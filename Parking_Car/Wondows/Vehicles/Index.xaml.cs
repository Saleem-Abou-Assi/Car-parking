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


namespace Parking_Car.Wondows.Vehicles
{
  
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
            _context.Vehicles.Load();
            vehiclesGrid.ItemsSource = null;
            vehiclesGrid.ItemsSource = _context.Vehicles.Local.ToObservableCollection();
            var vehiclesCollection = _context.Vehicles.Local.ToObservableCollection();

            var driverIds = vehiclesCollection.Select(v => v.DriverId).ToList();

            var driverNames = _context.Drivers
                     .Where(d => driverIds.Contains(d.Id))
                     .Select(d => new DriverName { FullName = $"{d.FirstName} {d.LastName}" })
                     .ToList();

            DriverNameGrid.ItemsSource = driverNames;


        }
        private void DeleteVehicle(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Vehicle vehicle)
            {
                _context.Vehicles.Remove(vehicle);
                _context.SaveChanges();
                LoadData(); // Refresh the DataGrid
            }
        }
        private void EditVehicle(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Vehicle vehicle)
            {
                Wondows.Vehicles.Edit editWindow = new Wondows.Vehicles.Edit(vehicle);
                if (editWindow.ShowDialog() == true)
                {
                    _context.SaveChanges();
                   
                    LoadData(); // Refresh the DataGrid
                }
                
            }
        }
        private void AddVehicle(object sender, RoutedEventArgs e)
        {
            Wondows.Vehicles.Edit editWindow = new Wondows.Vehicles.Edit(null);
            if (editWindow.ShowDialog() == true)
            {
                _context.SaveChanges();

                LoadData(); // Refresh the DataGrid
            }
        }

        
    }
}
