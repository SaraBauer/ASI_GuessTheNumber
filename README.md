# ASI_GuessTheNumber

ASI_GuessTheNumer is a WPF number guessing game where an user can guess a randomly generated number from a preselected range.

# Principal Game Logic
- The first game only starts after pressing the **`Start`** button which shows a popup for the range selection
  - this ensures a save starting point for the player, and enables range selection on the first try
  - a timer starts up on every new game to record the time taken and is also displayed
- A guess is entered into the input field which only allows digits from 1-10; letters are prefiltered and not seen in the input
- Clicking **`Guess`** submits the guess and triggers the comparison logic
- a counter tracks the number of attemts and is displayed in the window
- if the wrong number was guessed, a message is printed to show if the number guessed was too high or too low
- If the correct number was guessed, the triumph is displayed ("Winner Winner chicken dinner") together with number of attemps needed and the elapsed time
- a popup window enables the user to start either:
  - a new game
  - cancel, which leaves the game in an idle mode awaiting further commands
- A running guessing game can be aborted by pressing and the **`New Game`** can be started thereafter (or resumed).

# Data Management
For every game, the data is sent over a REST API to be collected and saved. 
- On start of a new game
  - a new entry is created, containing the range and targed number (playedAt is computed automatically)
  - this information is sed with a HttpPost request and json content (returning the id for later)
  - every subsequent guess is also sent (including the gameId for the foreign key relation and the guess
  - if the correct number was guessed, the game will be finalized with a guess cound and the time taken to guess the correct number
  - 
A WebAPI called [ASI_APIService](https://github.com/SaraBauer/ASI_APIService) was implemented and MUST be started concurringly to ensure the listening and processing of the received data.

WPF application written in C# with .Net 9.
MVVC pattern

#Installing

1. Either run directly
```bash
dotnet run 
```
2. Or (recommended) run in VisualStudio togehter with [ASI_APIService](https://github.com/SaraBauer/ASI_APIService) in one solution with multiple startup projectcs:
<img width="793" height="280" alt="image" src="https://github.com/user-attachments/assets/26aed9fb-575d-4455-9e85-9f237258fec8" />
