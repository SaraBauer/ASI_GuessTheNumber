using ASI_GuessTheNumber.ViewModel;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace ASI_GuessTheNumber
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            HttpClient HttpClient  = new HttpClient();
            DataContext = new MainViewModel(new DialogService(), new GuessApiService(HttpClient));

        }
        private void NumberOnly(object sender, TextCompositionEventArgs e) { e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$"); }
    }
}