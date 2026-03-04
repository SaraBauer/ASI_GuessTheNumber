using System.Collections.ObjectModel;
using ASI_GuessTheNumber.ViewModel;
using System.Windows;


namespace ASI_GuessTheNumber.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class StartGamePopup : Window
    {
        public StartGamePopup(ObservableCollection<int> rangeOptions, Action<int> callback)
        {
            InitializeComponent();
            DataContext = new StartGamePopupViewModel(rangeOptions, this, callback);
        }
    }

}
