using MySql.Data.MySqlClient;
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

namespace ProjectExample
{
    /// <summary>
    /// Interaction logic for Delete_Contact.xaml
    /// </summary>
    public partial class Delete_Contact : Window
    {
        private SimpleDataSource SDS = new SimpleDataSource("localhost", "database", 3306, "root", "root");

        public Delete_Contact()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Deletes an existing contact.
        /// </summary>
        private void Update_button_Click(object sender, RoutedEventArgs e)
        {
            MySqlDataReader Query = SDS.QueryPreparedStatement("DELETE FROM database.Contacts WHERE id = '"+ idtextBox.Text+"'", new Dictionary<string, string>());
            MessageBox.Show("Contact Deleted");
        }
    }
}
