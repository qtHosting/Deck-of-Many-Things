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

namespace DeckOfManyThings
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        private string strDatabaseName = Properties.Settings.Default.DatabaseName.Replace(@"\", string.Empty);
        private string strDeckName = Properties.Settings.Default.DeckToUse.Replace(@"\", string.Empty);
        public Settings()
        {
            InitializeComponent();
            databaseTextBox.Text = strDatabaseName;
            deckNameTextBox.Text = strDeckName;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if(!databaseTextBox.Text.Equals(strDatabaseName))
            {
                Properties.Settings.Default.DatabaseName = databaseTextBox.Text;
            }
            
            if(!deckNameTextBox.Text.Equals(strDeckName))
            {
                Properties.Settings.Default.DeckToUse = @"\" + deckNameTextBox.Text + @"\";
            }

            this.Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
