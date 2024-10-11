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

namespace Parking_Car.Wondows.Drivers
{
    /// <summary>
    /// Interaction logic for Edit.xaml
    /// </summary>
    public partial class Edit : Window
    {
        private Driver _driver;
        private readonly DataBaseContext _context;

        public Edit(Driver driver)
        {
            InitializeComponent();
            _driver = driver;
            _context = new DataBaseContext();
            DataContext = _context;

            if (_driver != null)
            {
                DataContext = _driver;
                NameTextBox.Text = _driver.FirstName;
                LastNameTextBox.Text = _driver.LastName;
                PhoneNumberTextBox.Text = _driver.Phone;

                SaveButton.Visibility = Visibility.Visible;
                AddButton.Visibility = Visibility.Hidden;
            }
            else
            {
                NameTextBox.Text = string.Empty;
                LastNameTextBox.Text = string.Empty;
                PhoneNumberTextBox.Text = string.Empty;

                SaveButton.Visibility = Visibility.Hidden;
                AddButton.Visibility = Visibility.Visible;

            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _driver.FirstName = NameTextBox.Text.ToLower();
            _driver.LastName = LastNameTextBox.Text.ToLower();
            _driver.Phone = PhoneNumberTextBox.Text;
           

            DialogResult = true;
            Close();
   
        }

        private void AddButton_Click(Object sender, RoutedEventArgs e)
        {
             _context.Drivers.Load();


            var existingDriver = _context.Drivers
    .FirstOrDefault(d => (d.FirstName == NameTextBox.Text && d.LastName == LastNameTextBox.Text) );
            if (existingDriver != null)
            {
                MessageBox.Show("The driver name is already exist!");
            }
            else
            {
                _context.Drivers.Add(new Driver
                {
                    FirstName = NameTextBox.Text.ToLower(),
                    LastName = LastNameTextBox.Text.ToLower(),
                    Phone = PhoneNumberTextBox.Text
                });

                _context.SaveChanges();

                MessageBox.Show("Driver saved correctly!");

                DialogResult = true;
                Close();
            }
            

            

            
        }
    }
}

