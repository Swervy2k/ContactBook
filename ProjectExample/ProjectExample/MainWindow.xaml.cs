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

namespace ProjectExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateContact_button_Click(object sender, RoutedEventArgs e)
        {
            Create_Contact createContact = new Create_Contact();
            createContact.Show();
        }

        private void ViewContact_button_Click(object sender, RoutedEventArgs e)
        {
            View_Contacts viewContacts = new View_Contacts();
            viewContacts.Show();
        }

        private void Exit_button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
