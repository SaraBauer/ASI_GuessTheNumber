using ASI_GuessTheNumber.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ASI_GuessTheNumber.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class NewGamePopup : Window
    {
        public NewGamePopup(string message, Action startNewGame)
        {
            InitializeComponent();
            DataContext = new NewGamePopupViewModel(message, startNewGame, this);
        }
        public NewGamePopup(ObservableCollection<int> rangeOptions, Action<int> callback)
        {
            InitializeComponent();
            DataContext = new StartGamePopupViewModel(rangeOptions, this, callback);
        }

        
    }

}
