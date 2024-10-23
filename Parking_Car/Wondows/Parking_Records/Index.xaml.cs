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


namespace Parking_Car.Wondows.Parking_Records
{
    /// <summary>
    /// Interaction logic for Index.xaml
    /// </summary>
    public partial class Index : Window
    {
        private readonly DataBaseContext _context;
        private ParkingRecord _parkingRecord;

        public Index()
        {
            InitializeComponent();
            _context = new DataBaseContext();
            LoadData();
        }

        private void LoadData()
        {
            // Use LINQ to fetch parking records with navigation properties
            var records = _context.ParkingRecords
                .Include(p => p.Vehicle)
                .Include(p => p.ParkingSlot)
                .ToList();

            List<ParkingRecordData> parkingRecords = new List<ParkingRecordData>();

            foreach (var record in records)
            {
                parkingRecords.Add(new ParkingRecordData
                {
                    VehicleId = record.Vehicle.PlateNumber,
                    SlotId = record.ParkingSlot.Number.ToString(),
                    TimeIn = record.TimeIn.ToString(),
                    TimeOut = record.TimeOut.ToString(),
                    Parcode = record.Parcode
                });
            }

            driversGrid.ItemsSource = parkingRecords;
        }
        private void DeleteRecord(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is ParkingRecordData parkingRecordData)
            {
                var parkingRecord = _context.ParkingRecords.FirstOrDefault(p => p.Parcode == parkingRecordData.Parcode);

                _context.ParkingRecords.Remove(parkingRecord);
                _context.SaveChanges();
                LoadData(); // Refresh the DataGrid
            }
        }
        private void EditRecord(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is ParkingRecordData parkingRecordData)
            {
                var parkingRecord = _context.ParkingRecords.FirstOrDefault(p => p.Parcode == parkingRecordData.Parcode);


                Wondows.Parking_Records.Edit editWindow2 = new Wondows.Parking_Records.Edit(parkingRecord);
                if (editWindow2.ShowDialog() == true)
                {
                    _context.SaveChanges();
                   
                    LoadData(); // Refresh the DataGrid
                }
                
            }
        }
        private void AddDriver(object sender, RoutedEventArgs e)
        {
            Wondows.Parking_Records.Edit editWindow2 = new Wondows.Parking_Records.Edit(null);
            if (editWindow2.ShowDialog() == true)
            {
                _context.SaveChanges();

                LoadData(); // Refresh the DataGrid
            }
        }
    }
}
