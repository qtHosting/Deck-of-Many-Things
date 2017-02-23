using System.Windows;

namespace DeckOfManyThings
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
        }

        private void OpenGameLicense(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(@".\ogl.html");
        }

        private void QiwiTrailsWebsite(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.qiwitrails.com");
        }
    }
}
