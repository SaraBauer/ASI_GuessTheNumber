using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace ASI_GuessTheNumber.ViewModel
{
    public class StartGamePopupViewModel
    {
        public ObservableCollection<int> RangeOptions { get; }
        public int SelectedRange { get; set; }

        public ICommand StartCommand { get; }
        public ICommand CancelCommand { get; }

        private readonly Window _window;
        private readonly Action<int> _callback;

        public StartGamePopupViewModel(ObservableCollection<int> rangeOptions,  Window window,
            Action<int> callback)
        {
            RangeOptions = rangeOptions;
            SelectedRange = rangeOptions[0]; // default selection

            _window = window;
            _callback = callback;

            StartCommand = new RelayCommand(_ => Start());
            CancelCommand = new RelayCommand(_ => Cancel());
        }

        private void Start()
        {
            _callback(SelectedRange);
            _window.Close();
        }

        private void Cancel()
        {
            _callback(-1); // -1 means "cancel"
            _window.Close();
        }
    }

}
