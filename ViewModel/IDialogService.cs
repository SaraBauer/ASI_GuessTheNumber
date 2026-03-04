
using System.Collections.ObjectModel;

namespace ASI_GuessTheNumber.ViewModel
{
    public interface IDialogService
    {
        bool ShowNewGamePrompt(ObservableCollection<int> rangeOptions, Action<int> callback);
        //bool ShowNewGamePrompt(string message, Action startNewGame);
        void ShowStartGamePopup(ObservableCollection<int> rangeOptions, Action<int> callback);
    }

}
