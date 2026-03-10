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

namespace AppointmentPlanner.Presentation
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();

            
        }

        private void Verder(object sender, RoutedEventArgs e)
        {
            string login = "Tristan Pilat";
            string ww = "meow";


            if (login == Naam.Text && ww == Passwordbox.Password)
            {
                MainWindow mainWindow = new MainWindow(login);//data doorgeven naar andere window
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Probeer opniuew.");
            }

            
        }
    }
}
