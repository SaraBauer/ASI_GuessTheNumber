using ASI_GuessTheNumber.View;
using System.Collections.ObjectModel;
using System.Windows;
namespace ASI_GuessTheNumber.ViewModel
{
    public class DialogService : IDialogService
    {
        public bool ShowNewGamePrompt(ObservableCollection<int> rangeOptions, Action<int> callback)
        {
            var popup = new StartGamePopup(rangeOptions, callback);
            popup.Owner = Application.Current.MainWindow;
            popup.ShowDialog();
            return true;
        }
        public void ShowStartGamePopup(ObservableCollection<int> rangeOptions, Action<int> callback)
        {
            var popup = new StartGamePopup(rangeOptions, callback);
            popup.Owner = Application.Current.MainWindow;
            popup.ShowDialog();
        }
    }


}
