
using System.Collections.ObjectModel;

namespace ASI_GuessTheNumber.ViewModel
{
    public interface IDialogService
    {
        void ShowNewGamePopup(ObservableCollection<int> rangeOptions, Action<int> callback);
        void ShowStartGamePopup(ObservableCollection<int> rangeOptions, Action<int> callback);

        void ShowCancelAndStartGamePopup(ObservableCollection<int> rangeOptions, Action<int> callback);
    }

}
