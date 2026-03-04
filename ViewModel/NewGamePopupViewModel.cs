using System;
using System.Windows;
using System.Windows.Input;

namespace ASI_GuessTheNumber.ViewModel
{
    public class NewGamePopupViewModel
    {
        public string Message { get; }
        public ICommand NewGameCommand { get; }
        public ICommand CloseCommand { get; }

        private readonly Action _startNewGame;
        private readonly Window _window;

        public NewGamePopupViewModel(string message, Action startNewGame, Window window)
        {
            Message = message;
            _startNewGame = startNewGame;
            _window = window;

            NewGameCommand = new RelayCommand(_ => StartNewGame());
            CloseCommand = new RelayCommand(_ => Close());
        }

        private void StartNewGame()
        {
            _window.Close();
            _startNewGame();
        }

        private void Close()
        {
            _window.Close();
        }
    }

}
