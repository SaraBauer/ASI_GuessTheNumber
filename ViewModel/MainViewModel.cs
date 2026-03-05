using ASI_GuessTheNumber.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Threading;

namespace ASI_GuessTheNumber.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly GameRepository _repository;
        private readonly DispatcherTimer _timer;
        private readonly IDialogService _dialogService;

        private int _targetNumber;
        private int _guessCount;
        private string _inputText = "";
        private string _errorMessage = "";
        private string _result = "";
        private int _selectedRange;
        private TimeSpan _time;
        private int _currentGameId;

        private readonly IGuessApiService _guessApi;
    
        public MainViewModel(IDialogService dialogService, IGuessApiService guessApi)
        {
            _repository = new();   // in-memory DB

            RangeOptions = new ObservableCollection<int> { 10, 100 };
            SelectedRange = 10;
            _dialogService = dialogService;
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += TimerTick;

            _guessApi = guessApi; 
        
            ProcessNumberCommand = new RelayCommand(async _ => await CheckGuess(), _ => CanProcess());
            StartGameCommand = new RelayCommand(_ => StartGame());
            NewGameCommand = new RelayCommand(_ => NewGame());
            ShowStartPopupCommand = new RelayCommand(_ => ShowStartPopup());

        }
        public ICommand StartGameCommand { get; }
        public GameResult CurrentGame { get; private set; }

        public ObservableCollection<int> RangeOptions { get; }

        private bool _isGameFinished;
        public bool IsGameFinished
        {
            get => _isGameFinished;
            set
            {
                _isGameFinished = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested(); 
            }
        }
 
        private bool _isGameStarted;
        public bool IsGameStarted
        {
            get => _isGameStarted;
            set
            {
                _isGameStarted = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public ICommand ShowStartPopupCommand { get; }

        private bool _isStartEnabled = true; 
        public bool IsStartEnabled { get => _isStartEnabled; set { _isStartEnabled = value; OnPropertyChanged(); } }


        public int SelectedRange
        {
            get => _selectedRange;
            set
            {
                _selectedRange = value;
                OnPropertyChanged();
            }
        }

        public string InputText
        {
            get => _inputText;
            set
            {
                _inputText = value;
                Validate();
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public string Result
        {
            get => _result;
            set
            {
                _result = value;
                OnPropertyChanged();
            }
        }

        public int GuessCount
        {
            get => _guessCount;
            set
            {
                _guessCount = value;
                OnPropertyChanged();
            }
        }

        public string TimeElapsed => _time.ToString(@"mm\:ss");

        public ICommand ProcessNumberCommand { get; }
        public ICommand NewGameCommand { get; }

        private void TimerTick(object? sender, EventArgs e)
        {
            _time = _time.Add(TimeSpan.FromSeconds(1));
            OnPropertyChanged(nameof(TimeElapsed));
        }

        private async Task StartNewGameAsync()
        {
            _currentGameId = await _guessApi.CreateGameAsync(SelectedRange);
        }

        private async Task ProcessGuessAsync(int guess)
        {
            await _guessApi.SendGuessAsync(_currentGameId, guess);

            if (guess == _targetNumber)
            {
                await FinalizeGameAsync();
            }
        }

        private async Task FinalizeGameAsync()
        {
            await _guessApi.FinalizeGameAsync(
                _currentGameId,
                GuessCount,
                _time
            );
        }

        private async Task NewGame()
        {
            CurrentGame = new GameResult
            {
                Range = SelectedRange,
                PlayedAt = DateTime.Now
            };
            await StartNewGameAsync();

            _targetNumber = new Random().Next(1, SelectedRange + 1);
            GuessCount = 0;
            InputText = "";
            ErrorMessage = "";
            Result = $"New game started. Range: 1–{SelectedRange}.";

            _time = TimeSpan.Zero;
            OnPropertyChanged(nameof(TimeElapsed));
            IsGameFinished = false;
            _timer.Start();
        }


        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(InputText))
            {
                ErrorMessage = "Input cannot be empty.";
            }
            else if (!int.TryParse(InputText, out _))
            {
                ErrorMessage = "Please enter a valid number.";
            }
            else
            {
                ErrorMessage = "";
            }
        }

        private bool CanProcess()
        {
            return IsGameStarted && !IsGameFinished && int.TryParse(InputText, out _);
        }

        private async Task StartGame()
        {
            IsStartEnabled = false;


            // Start a new game with that range
            await NewGame();

            IsGameStarted = true;
        }

        private async Task ShowStartPopup()
        {
            _dialogService.ShowStartGamePopup(RangeOptions, async selectedRange =>
            {
                if (selectedRange == -1)
                    return; // user cancelled

                SelectedRange = selectedRange;
                IsStartEnabled = false;
                IsGameStarted = true;

                await NewGame();
            });
        }


        private async Task CheckGuess()
        {
            GuessCount++;

            int guess = int.Parse(InputText);

            //CurrentGame.Guesses.Add(new GuessEntry
            //{
            //    Guess = guess,
            //    Time = DateTime.Now
            //});

            await ProcessGuessAsync(guess);
            if (guess == _targetNumber)
            {
                _timer.Stop();
                IsGameFinished = true;

                CurrentGame.Attempts = GuessCount;
                CurrentGame.TimeTaken = _time;
                CurrentGame.PlayedAt = DateTime.Now;

               // _repository.AddGame(CurrentGame);   // <-- saved in EF Core InMemory DB

                Result = $"Correct! Number: {_targetNumber}. Attempts: {GuessCount}. Time: {TimeElapsed}.";


                string msg = $"You solved it in {GuessCount} attempts.\nTime: {TimeElapsed}";

                _dialogService.ShowStartGamePopup(RangeOptions, async selectedRange =>
                {
                    if (selectedRange == -1)
                        return; // user cancelled

                    SelectedRange = selectedRange;
                    IsStartEnabled = false;
                    IsGameStarted = true;

                    await NewGame();
                });
            }
            else if (guess < _targetNumber)
            {
                Result = "Too low — try again!";
            }
            else
            {
                Result = "Too high — try again!";
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

}