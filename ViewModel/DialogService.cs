using ASI_GuessTheNumber.View;
using System.Collections.ObjectModel;
using System.Windows;
namespace ASI_GuessTheNumber.ViewModel
{
    public class DialogService : IDialogService
    {
        public void ShowNewGamePopup(ObservableCollection<int> rangeOptions, Action<int> callback)
        {
            var popup = new NewGamePopup(rangeOptions, callback);
            popup.Owner = Application.Current.MainWindow;
            popup.ShowDialog();
        }
        public void ShowStartGamePopup(ObservableCollection<int> rangeOptions, Action<int> callback)
        {
            var popup = new StartGamePopup(rangeOptions, callback);
            popup.Owner = Application.Current.MainWindow;
            popup.ShowDialog();
        }

        public void ShowCancelAndStartGamePopup(ObservableCollection<int> rangeOptions, Action<int> callback)
        {
            var popup = new CancelAndStartNewGamePopup(rangeOptions, callback);
            popup.Owner = Application.Current.MainWindow;
            popup.ShowDialog();
        }
    }


}
