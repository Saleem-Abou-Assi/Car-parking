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

namespace Parking_Car.Wondows.Parking_Records
{
    /// <summary>
    /// Interaction logic for Edit.xaml
    /// </summary>
    public partial class Edit : Window
    {
        private ParkingRecord _parkingRecord;
        private readonly DataBaseContext _context;

        public Edit(ParkingRecord parkingRecord)
        {
            InitializeComponent();
            parkingRecord = _parkingRecord;
            _context = new DataBaseContext();
            DataContext = _context;

            if (_parkingRecord != null)
            {
                VehicleComboBox.ItemsSource = _context.Vehicles.Local.ToObservableCollection();
                SlotComboBox.ItemsSource = _context.ParkingSlots.Local.ToObservableCollection();

                 bool ff = TimeSpan.TryParse(TimeINTextBox.Text, out TimeSpan parsedTime);
                TimeSpan time = parsedTime;
              

                DataContext = _parkingRecord;
                VehicleComboBox.Text = _parkingRecord.Vehicle.PlateNumber.ToString();
                SlotComboBox.Text = _parkingRecord.ParkingSlot.Number.ToString();
                TimeINTextBox.Text = time.ToString();
                ParcodeTextBox.Text = _parkingRecord.Parcode;
                
                SaveButton.Visibility = Visibility.Visible;
                AddButton.Visibility = Visibility.Hidden;
            }
            else
            {
                VehicleComboBox.Text = string.Empty;
                SlotComboBox.Text = string.Empty;       
                TimeINTextBox.Text = string.Empty;       
                ParcodeTextBox.Text = string.Empty;     
                  
                                                        
                SaveButton.Visibility = Visibility.Hidden;
                AddButton.Visibility = Visibility.Visible;

            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //get time
            bool ff = TimeSpan.TryParse(TimeINTextBox.Text, out TimeSpan parsedTime);
            TimeSpan time = parsedTime;

            //get parcode
            var lastAddedItem = _context.ParkingRecords
        .OrderByDescending(x => x.Id) // Replace "Id" with your primary key column
        .FirstOrDefault();

           

            _parkingRecord.VehicleId = int.Parse(VehicleComboBox.Text);
            _parkingRecord.SlotId = int.Parse(SlotComboBox.Text);
            _parkingRecord.TimeIn = DateTime.Today.Add(parsedTime);

            if (lastAddedItem != null)
            {
                // Do something with the last added item
                _parkingRecord.Parcode = (int.Parse(lastAddedItem.Parcode) + 1).ToString();
            }
            else
            {
                _parkingRecord.Parcode = 1.ToString();
            }


            DialogResult = true;
            Close();
   
        }

        private void AddButton_Click(Object sender, RoutedEventArgs e)
        {
             _context.ParkingRecords.Load();
            string vehicle = VehicleComboBox.Text;
            

            var existingDriver = _context.ParkingRecords
    .FirstOrDefault(p => (p.Parcode == ParcodeTextBox.Text ));
            if (existingDriver != null)
            {
                MessageBox.Show("This slot is already in use");
            }
            else
            {
                var findVehicle = _context.Vehicles.FirstOrDefault(v => (v.PlateNumber == VehicleComboBox.Text));
                var findSlot = _context.ParkingSlots.FirstOrDefault(s => (s.Number == int.Parse(SlotComboBox.Text)));

                bool ff = TimeSpan.TryParse(TimeINTextBox.Text, out TimeSpan parsedTime);
                TimeSpan time = parsedTime;

                var lastAddedItem = _context.ParkingRecords
               .OrderByDescending(x => x.Id) // Replace "Id" with your primary key column
               .FirstOrDefault();

                int parcode1 = 1;
                if (lastAddedItem != null)
                {
                    // Do something with the last added item
                    parcode1 = int.Parse(lastAddedItem.Parcode)+1;
                }

                _context.ParkingRecords.Add(new ParkingRecord
                {
                    VehicleId = findVehicle.Id,
                    SlotId = findSlot.Id,
                    TimeIn = DateTime.Today.Add(parsedTime),
                    Parcode = parcode1.ToString()
                });

                _context.SaveChanges();

                MessageBox.Show("Record saved correctly!");

                DialogResult = true;
                Close();
            }
            

            

            
        }
    }
}

