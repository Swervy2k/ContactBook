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
    /// Interaction logic for Create_Contact.xaml
    /// </summary>
    public partial class Create_Contact : Window
    {
        private SimpleDataSource SDS = new SimpleDataSource("localhost", "database", 3306, "root", "root");

        public Create_Contact()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Creates an new contact.
        /// </summary>
        private void Save_button_Click(object sender, RoutedEventArgs e)
        {
            //"INSERT INTO TABLENAME (FIELDS) VALUES (@VAR1, @VAR2...);";
            MySqlDataReader Query = SDS.QueryPreparedStatement("INSERT INTO database.Contacts (id, firstName, lastName, preferredName, emailAddress, homePhone, mobileNumber, homeAddress) VALUES ('" + titleTextbox.Text+"','"+firstNameTextbox.Text+"','"+lastNameTextbox.Text+ "','"+preferredNameTextbox.Text+ "','"+emailTextbox.Text+ "','"+homePhoneTextbox.Text+ "','"+mobilePhoneTextbox.Text+ "','"+homeAddressTextbox.Text+"')", new Dictionary<string, string>());
            MessageBox.Show("Saved");
        }

        /// <summary>
        /// Edits an existing contacts.
        /// Deletes an existing contact.
        /// </summary>
        private void Update_button_Click(object sender, RoutedEventArgs e)
        {
            SDS.Update("UPDATE database.Contacts SET firstName = '"+firstNameTextbox.Text+"', lastName = '"+lastNameTextbox.Text+ "', preferredName = '"+preferredNameTextbox.Text+ "', emailAddress = '"+emailTextbox.Text + "', homePhone = '"+homePhoneTextbox.Text+ "', mobileNumber = '"+mobilePhoneTextbox.Text+ "', homeAddress = '"+homeAddressTextbox.Text+ "' WHERE id = '"+titleTextbox.Text+ "'");
            MessageBox.Show("Updated");
        }
    }
}
