using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Parking_Car
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Initialize the database
            using (var context = new DataBaseContext())
            {
                context.Database.EnsureCreated(); // Or EnsureCreated()
            }

            // Other startup logic...
        }
    }


}
