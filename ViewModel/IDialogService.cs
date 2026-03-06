
using System.Collections.ObjectModel;

namespace ASI_GuessTheNumber.ViewModel
{
    public interface IDialogService
    {
        void ShowStartGamePopup(ObservableCollection<int> rangeOptions, Action<int> callback);
    }
}
