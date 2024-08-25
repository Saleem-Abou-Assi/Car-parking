using Parking_Car.Models;
using System;
using System.Data.Entity;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Parking_Car
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DataBaseContext _context;
        public MainWindow()
        {
            InitializeComponent();
            _context = new DataBaseContext();
            DataContext = _context;
            

        }
        private void Vdriver(object sender, RoutedEventArgs e)
        {
            _context.Drivers.Load();
            _context.Vehicles.Load();

           string driver_name = txtname.Text;

            var allDrivers = _context.Drivers.ToList();
            var driver = allDrivers.FirstOrDefault(d => d.FirstName
                                    .Equals(driver_name, StringComparison.CurrentCultureIgnoreCase));

            if(driver != null)
            {
                var V_driver = _context.Vehicles
                .Where(v => v.Driver.Id == driver.Id)
                .Select(v => new { v.PlateNumber, v.Type, v.Description }).ToList();

                dataGrid.ItemsSource = V_driver;
                
            }
            else
            {
                MessageBox.Show("Driver is not exist!");
                dataGrid.ItemsSource = null;

            }

            

        }
        //private void LoadData()
        //{
        //    // Add sample data (you'd query from your SQLite database)
        //    //_context.Drivers.Add(new Driver { FirstName = "John", LastName = "Doe", Phone = "0959877217" });
        //    //_context.Vehicles.Add(new Vehicle { PlateNumber = "ABC123", DriverId = 1, Type = VehicleType.سيارة, Description = "asdasd" });

        //    //_context.SaveChanges();
        //    _context.Drivers.Load();
        //    _context.Vehicles.Load();
        //    //DataGrid1.ItemsSource = _context.Drivers.Local.ToObservableCollection();
        //    DataGrid2.ItemsSource = _context.Vehicles.Local.ToObservableCollection();

        //    var Drivers = _context.Drivers.ToList();
        //    var selectedCol = Drivers.Select(d => new
        //    {
        //        firstName = d.FirstName,
        //        lastName = d.LastName,
        //        phone = d.Phone,
        //        totalParking = d.TotalParking,
        //    }).ToList();

        //    DataGrid1.ItemsSource = selectedCol;
        //}


        //private void SearchDriverByName(object sender, RoutedEventArgs e)
        //{
        //    _context.Drivers.Load();

        //    var driverName = txtSearchName.Text.Trim(); // Get the entered name
        //    MessageBox.Show(driverName.ToString());
        //    var filteredDrivers = _context.Drivers
        //        //.Where(d => d.FirstName.Contains(driverName.ToString()))
        //        .ToList();

        //    DataGrid3.ItemsSource = filteredDrivers;
        //}


    }
}
