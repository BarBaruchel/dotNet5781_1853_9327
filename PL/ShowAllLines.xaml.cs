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

namespace PL
{
    /// <summary>
    /// Interaction logic for ShowAllLines.xaml
    /// </summary>
    public partial class ShowAllLines : Window
    {
        IBl bl;
        public ShowAllLines(IBl bl)
        {
            this.bl = bl;
            InitializeComponent();
            LinesDataGrid.ItemsSource = bl.getAllLines();// עידכון הדאטה גריד
        }
        private void RefreshLines()
        {
            LinesDataGrid.ItemsSource = bl.getAllLines();
            LinesDataGrid.Items.Refresh();
        }
        private void LinesDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.Line row = (BO.Line)LinesDataGrid.SelectedItems[0];
            int id = row.Id;
            BO.Line BOLine = bl.getLineById(id);
            LineDetails lineDetails = new LineDetails(bl, BOLine);
            lineDetails.Show();
        }
        private void AddLineButton_Click(object sender, RoutedEventArgs e)
        {
            AddLineWindow Addline = new AddLineWindow(bl);
            Addline.ShowDialog();
            RefreshLines();
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e) // press on the one of the update button  in one of the bus in the table start processor
        {
            BO.Line row = (BO.Line)LinesDataGrid.SelectedItems[0];
            int id = row.Id;
            BO.Line BOLine = bl.getLineById(id);
            UpdateLineWindow updateLine = new UpdateLineWindow(bl, BOLine);
            updateLine.ShowDialog();
            RefreshLines();
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            BO.Line row = (BO.Line)LinesDataGrid.SelectedItems[0];
            int id = row.Id;
            BO.Line BOLine = bl.getLineById(id);
            try
            {
                bl.deleteLine(BOLine);
            }
            catch (BO.NotExistException ex)
            {
                MessageBox.Show(ex.Message);
            }
            RefreshLines();
        }
    }
}
