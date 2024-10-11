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


namespace Parking_Car.Wondows.Drivers
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
            _context.Drivers.Load();
            driversGrid.ItemsSource = null;
            driversGrid.ItemsSource = _context.Drivers.Local.ToObservableCollection();

        }
        private void DeleteDriver(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Driver driver)
            {
                _context.Drivers.Remove(driver);
                _context.SaveChanges();
                LoadData(); // Refresh the DataGrid
            }
        }
        private void EditDriver(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Driver driver)
            {
                Wondows.Drivers.Edit editWindow = new Wondows.Drivers.Edit(driver);
                if (editWindow.ShowDialog() == true)
                {
                    _context.SaveChanges();
                   
                    LoadData(); // Refresh the DataGrid
                }
                
            }
        }
        private void AddDriver(object sender, RoutedEventArgs e)
        {
            Wondows.Drivers.Edit editWindow = new Wondows.Drivers.Edit(null);
            if (editWindow.ShowDialog() == true)
            {
                _context.SaveChanges();

                LoadData(); // Refresh the DataGrid
            }
        }
    }
}
