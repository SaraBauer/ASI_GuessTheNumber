# ASI_GuessTheNumber

ASI_GuessTheNumer is a WPF number guessing game where an user can guess a randomly generated number from a preselected range.
- If the correct number was guessed, a popup window enables the user to start
-  a new game or to cancel, the latter leaving the game in an idle mode awaiting further commands.
- A started guessing game can be aborted and a new game can be started thereafter (or resumed).

WPF application written in C# with .Net 9.


GameController
| Call |  Methods  | Result |
|:-----|:--------:|------:|

| api/game   | HttpPost | creates a new game |
| api/game/{id}   |  HttpPut |  updates an existing game with certain id |
| api/game  | HttpGet |   returns all games from the database|

GuessController
| Call |  Methods  | Result |
|:-----|:--------:|------:|
| api/guess   | HttpPost |  adds a guess to an existing gameresult as a foreign key relation |

#Entity Framework Core
Entity Framework Core was used together with InMemoryDatabase to save results. </br>
DTOs were used to mitigate the transfer between the API and the Database ensuring decoupling. At this stage, not auto mapping was implemented.

#Installing

1. Either run directly
```bash
dotnet run --launch-profile https
```
2. Or (recommended) run in VisualStudio togehter with [ASI_GuessTheNumber](https://github.com/SaraBauer/ASI_GuessTheNumber) in one solution with multiple startup projectcs:
<img width="793" height="280" alt="image" src="https://github.com/user-attachments/assets/26aed9fb-575d-4455-9e85-9f237258fec8" />
