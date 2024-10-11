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

namespace Parking_Car.Wondows.Parking_Slots
{
    /// <summary>
    /// Interaction logic for Edit.xaml
    /// </summary>
    public partial class Edit : Window
    {
        private ParkingSlot _parkingSlot;
        private readonly DataBaseContext _context;

        public Edit(ParkingSlot parkingSlot)
        {
            InitializeComponent();
            _parkingSlot = parkingSlot;
            _context = new DataBaseContext();
            DataContext = _context;

            if (_parkingSlot != null)
            {
                DataContext = _parkingSlot;
                NumTextBox.Text = _parkingSlot.Number.ToString();
                StatusComboBox.Text = _parkingSlot.Status.ToString();

                SaveButton.Visibility = Visibility.Visible;
                AddButton.Visibility = Visibility.Hidden;
            }
            else
            {
                NumTextBox.Text = string.Empty;
                StatusComboBox.Text = string.Empty;
                

                SaveButton.Visibility = Visibility.Hidden;
                AddButton.Visibility = Visibility.Visible;

            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _parkingSlot.Number = int.Parse(NumTextBox.Text);
            _parkingSlot.Status = (SlotStatus)Enum.Parse(typeof(SlotStatus), StatusComboBox.Text);
            
            
            DialogResult = true;
            Close();
   
        }

        private void AddButton_Click(Object sender, RoutedEventArgs e)
        {
             _context.ParkingSlots.Load();


            var existingDriver = _context.ParkingSlots
    .FirstOrDefault(d => (d.Number == int.Parse(NumTextBox.Text )) );
            if (existingDriver != null)
            {
                MessageBox.Show("The slot number is already exist!");
            }
            else
            {
                _context.ParkingSlots.Add(new ParkingSlot
                {
                    Number = int.Parse(NumTextBox.Text),
                    Status = (SlotStatus)Enum.Parse(typeof(SlotStatus), StatusComboBox.Text)
              
                });

                _context.SaveChanges();

                MessageBox.Show("Slot saved correctly!");

                DialogResult = true;
                Close();
            }
            

            

            
        }
    }
}

