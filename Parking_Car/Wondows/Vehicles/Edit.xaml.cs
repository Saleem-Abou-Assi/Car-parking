using Parking_Car.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace Parking_Car.Wondows.Vehicles
{
    /// <summary>
    /// Interaction logic for Edit.xaml
    /// </summary>
    public partial class Edit : Window
    {
        private Vehicle _vehicle;
        private readonly DataBaseContext _context;

        public Edit(Vehicle vehicle)
        {
            InitializeComponent();
            _vehicle = vehicle;
            _context = new DataBaseContext();
            DataContext = _context;

            if (_vehicle != null)
            {
                DataContext = _vehicle;
                PlateTextBox.Text = _vehicle.PlateNumber;
                TypeComboBox.Text = _vehicle.Type.ToString();
                DescriptionTextBox.Text = _vehicle.Description;

                var driver = _context.Drivers.FirstOrDefault(d => d.Id == vehicle.DriverId);
                DriverNameTextBox.Text = driver?.FirstName + " " + driver?.LastName;


                SaveButton.Visibility = Visibility.Visible;
                AddButton.Visibility = Visibility.Hidden;
            }
            else
            {
                PlateTextBox.Text = string.Empty;
               TypeComboBox.Text = string.Empty;
               DriverNameTextBox.Text = string.Empty;
                DescriptionTextBox.Text = string.Empty;

                SaveButton.Visibility = Visibility.Hidden;
                AddButton.Visibility = Visibility.Visible;

            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _vehicle.PlateNumber = PlateTextBox.Text;
            _vehicle.Type = (VehicleType)Enum.Parse(typeof(VehicleType), TypeComboBox.Text);
            string[] name = DriverNameTextBox.Text.Split();

            var allDrivers = _context.Drivers.ToList();
            var driver = allDrivers.FirstOrDefault(d =>
                        d.FirstName.Equals(name[0], StringComparison.CurrentCultureIgnoreCase) &&
                        d.LastName.Equals(name[1], StringComparison.CurrentCultureIgnoreCase));

            _vehicle.DriverId = driver.Id;
           

            DialogResult = true;
            Close();
   
        }

        private void AddButton_Click(Object sender, RoutedEventArgs e)
        {
             _context.Vehicles.Load();


            var existingVehicle = _context.Vehicles
    .FirstOrDefault(v => v.PlateNumber == PlateTextBox.Text );
            if (existingVehicle != null)
            {
                MessageBox.Show("The vehicle plate number is already exist!");
            }
            else
            {
                string[] name = DriverNameTextBox.Text.Split();
                var allDrivers = _context.Drivers.ToList();
                var driver = allDrivers.FirstOrDefault(d =>
                            d.FirstName.Equals(name[0], StringComparison.CurrentCultureIgnoreCase) &&
                            d.LastName.Equals(name[1], StringComparison.CurrentCultureIgnoreCase));



                _context.Vehicles.Add(new Vehicle
                {
                    PlateNumber = PlateTextBox.Text,
                    Type = (VehicleType)Enum.Parse(typeof(VehicleType), TypeComboBox.Text),
                    DriverId = driver.Id,
                    Description = DescriptionTextBox.Text
                });

                _context.SaveChanges();

                MessageBox.Show("Vehicle saved correctly!");

                DialogResult = true;
                Close();
            }
            

            

            
        }
    }
}

