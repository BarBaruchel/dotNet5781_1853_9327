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
using BLAPI;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for BusDetails.xaml
    /// </summary>
    public partial class BusDetails : Window
    {
       IBl bl;
        private Bus bOBus;

        public BusDetails(Bus bOBus)   //the window get the Bus that the user press on to update the details
        {
            InitializeComponent();
            this.bOBus = bOBus;
           
            this.bl =BLAPI.BLFactory.getBL();  // מפנה את המשתנה לפונקציות של המחלקה שלו 
            mainGrid.DataContext = this.bOBus;   // מקור הנתונים של הגריד זה האוטובוס הנבחר
        }

        private void RefreshBus()  // בגלל שעשינו שינויים בכפתורי התידלוק והטיפול נקרא לריפרש שיחפש שוב את האוטובוס ע"י מספר רישוי ויעדכן את הנתונים לתןך 
        {  // המיין גריד נקודה דאטה קונטקס ויציג את הנתונים בחלון
            mainGrid.DataContext = bl.getBusByLicense(bOBus.LicenseNum);
        }

        private void TidlukBtn_Click(object sender, RoutedEventArgs e)  // the funcation send the bus to the BL layer to update the fuel
        {
            bl.fuelBus(bOBus);
            RefreshBus();
        }

        private void TreatmentBtn_Click(object sender, RoutedEventArgs e) // the funcation send the bus to the BL layer to update the bus`s treatment
        {
            bl.treatBus(bOBus);
            RefreshBus();
        }
    }
}
