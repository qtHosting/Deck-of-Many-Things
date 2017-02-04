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

namespace DeckOfManyThings
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string strLocationData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        private string strLocationName = Properties.Settings.Default.LocationName.ToString();
        private string strArtLocation = Properties.Settings.Default.Pictures.ToString();
        private string strDeckToUse = Properties.Settings.Default.DeckToUse.ToString();
        private string strDatabaseName = Properties.Settings.Default.DatabaseName.ToString();
        private string strBackgroundImage = "background.jpg";
        public MainWindow()
        {
            InitializeComponent();
            cardImage.Source = new BitmapImage(new Uri(strLocationData + strLocationName + strArtLocation + strDeckToUse +  strBackgroundImage));
        }

        private void SetSettings(object sender, RoutedEventArgs e)
        {
            Settings set = new Settings();
            set.Show();
        }
        
        private void DrawNewCard(object sender, RoutedEventArgs e)
        {
            BLL.DeckOfManyThings deck = new BLL.DeckOfManyThings();

            Card card = new Card();

            card = deck.DrawACard();

            DisplayCardInformation(card);
        }
        /// <summary>
        /// Displays all of the information you get when you pull a card from the Deck.
        /// </summary>
        /// <param name="card">This holds all the information we're going to display.</param>
        private void DisplayCardInformation(Card card)
        {
            Title = card.strCardName;
            cardDescriptionRichTextBox.Document.Blocks.Clear();
            cardImage.Source = new BitmapImage(new Uri(card.StrCardArtLocation));
            cardSummaryTextBox.Text = card.strCardSummary;
            cardDescriptionRichTextBox.Document.Blocks.Add(new Paragraph(new Run(card.strCardDescription)));
        }

    }
}
