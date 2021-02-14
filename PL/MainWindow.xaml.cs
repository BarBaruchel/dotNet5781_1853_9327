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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BL;
using BLAPI;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBl bl = BLFactory.getBL();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginManager_Click(object sender, RoutedEventArgs e)
        {
            bool isAdmin = bl.loginAdmin(username.Text, passwordPB.Password);
            if (!isAdmin)
                MessageBox.Show("Username or password incorrect");
            else
            {

                ManagerWindow newWindow = new ManagerWindow();
                newWindow.Show();
            }
        }
    }
}
