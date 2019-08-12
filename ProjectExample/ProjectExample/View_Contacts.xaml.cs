using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace ProjectExample
{
    /// <summary>
    /// Interaction logic for View_Contacts.xaml
    /// </summary>
    public partial class View_Contacts : Window
    {
        private SimpleDataSource SDS = new SimpleDataSource("localhost", "database", 3306, "root", "root");

        public View_Contacts()
        {
            InitializeComponent();
        }

        private void ViewAllContact_button_Click(object sender, RoutedEventArgs e)
        {
            DataTable Table = SDS.QueryDataTable("SELECT id AS `Contact ID`, firstName `First Name`, lastName `Last Name`, preferredName `Preferred Name`, emailAddress `eMail Address`, homePhone `Home Phoneline`, mobileNumber `Mobile Number`, homeAddress `Home Address` FROM Contacts", new Dictionary<string, string>());
            dataGrid.ItemsSource = Table.DefaultView;
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Edit_button_Click(object sender, RoutedEventArgs e)
        {
            Create_Contact createContact = new Create_Contact();
            createContact.Show();
        }

        private void Delete_button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Enter Contact ID.");
            Delete_Contact deleteContact = new Delete_Contact();
            deleteContact.Show();
        }
    }
}
