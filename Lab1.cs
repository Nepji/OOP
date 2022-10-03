// ReSharper disable FieldCanBeMadeReadOnly.Local

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace OOP
{
public class Lab1
{
    private class GameAccount
    {
        private string _userName;
        private int _curentRating;
        private int _gamesCount;
        private List<HistoryNote> history = new();

        public void SetCurentRating(int curentRating) { this._curentRating = curentRating; }
        public void SetGamesCount(int gamesCount) { this._gamesCount = gamesCount; }

        public int GetCurentRating() { return _curentRating; }
        public int GetGamesCount() { return _gamesCount; }
        public string GetUserName() { return _userName; }
        
        public void WinGame()
        {
            _gamesCount++;
            _curentRating += 10;
        }

        public void LoseGame()
        {
            _gamesCount++;
            if (_curentRating > 6)
                _curentRating -= 5;
        }

        public void Stats()
        {
            Console.WriteLine("Game Stats of Player: " + _userName + "\n\tPlayer Rating: "+_curentRating+"\n\tGames Count: "+_gamesCount+"\n");
        }

        public void ShowHistory()
        {
            Console.WriteLine($"Personally Game History of {_userName}:");
            foreach (var note in history)
                note.ShowNote();
        }
        public GameAccount(string userName)
        {
                _userName = userName;
                _curentRating = 1000;
                _gamesCount = 0;
        }

        public void NewHistoryNote(HistoryNote note)
        {
            history.Add(new HistoryNote(ref note));
        }
    }
    
    private class HistoryNote
    {
        private static int _currnetGameId = 0;
        private int _gameId;
        private GameAccount _playerOneName;
        private string? _playerOneChoose;
        private GameAccount _playerTwoName;
        private string? _playerTwoChoose;
        private int _gameResult; //1r2 win 0-draw

        public string getPlayerOneName() { return _playerOneName.GetUserName(); }
        public string GetPlayerTwoName() { return _playerTwoName.GetUserName(); }
        public int GetGameResult() { return _gameResult; }
        public int GetGameId() { return _gameId; }

        public HistoryNote(GameAccount playerOneName, GameAccount playerTwoName, int gameResult, string playerOneChoose, string playerTwoChoose)
        {
            _playerOneName = playerOneName;
            _playerTwoName = playerTwoName;
            _gameResult = gameResult;
            _gameId = _currnetGameId++;
            _playerOneChoose = playerOneChoose;
            _playerTwoChoose = playerTwoChoose;
        }

        public HistoryNote(ref HistoryNote CopyNote)
        {
            this._playerOneName = CopyNote._playerOneName;
            this._playerTwoName = CopyNote._playerTwoName;
            this._gameResult = CopyNote._gameResult;
            this._gameId = CopyNote._gameId;
            this._playerOneChoose = CopyNote._playerOneChoose;
            this._playerTwoChoose = CopyNote._playerTwoChoose;
        }

            public void ShowNote()
        { 
            Console.WriteLine("\tGame ID: "+_gameId+"\n\tPlayer One: "+_playerOneName.GetUserName()+ "\t\tChoose: "+ _playerOneChoose +"\n\tPlayer Two: "+_playerTwoName.GetUserName() + "\t\tChoose: "+ _playerTwoChoose);
            Console.WriteLine( _gameResult == 0? "\tResult of game: DRAW" :("\tWinner of the game is " +( _gameResult == 1 ? _playerOneName.GetUserName() : _playerTwoName.GetUserName())));
            Console.WriteLine("--------------------------------------"); 
        }
    }

    public class Game
    {
        private List<GameAccount> _accounts = new();
        private List<HistoryNote> _global_history = new();
        private enum GameAtributes
        {
            Rock = 1,
            Paper = 2,
            Scissors = 3
        }
        
        public void GameSimulation(int playerOneId, int playerTwoId)
        {
            Random rand = new Random();
            string? playerOneChoose = Enum.GetName(typeof(GameAtributes), rand.Next(1,3));
            string? playerTwoChoose = Enum.GetName(typeof(GameAtributes), rand.Next(1,3));

            int gameResult = -1; //1 - player 1 win, 2 - player 2 win, 0 - drow

            ///////////////////////////////////////////////////
            switch (playerOneChoose)
            {
                case "Rock":
                    switch (playerTwoChoose)
                    {
                        case "Rock":
                            gameResult = 0;
                            break;
                        case "Paper":
                            gameResult = 2;
                            break;
                        case "Scissors":
                            gameResult = 1;
                            break;
                    }
                    break;
                case "Paper":
                    switch (playerTwoChoose)
                    {
                        case "Rock":
                            gameResult = 1;
                            break;
                        case "Paper":
                            gameResult = 0;
                            break;
                        case "Scissors":
                            gameResult = 2;
                            break;
                    }
                    break;
                case "Scissors":
                    switch (playerTwoChoose)
                    {
                        case "Rock":
                            gameResult = 2;
                            break;
                        case "Paper":
                            gameResult = 1;
                            break;
                        case "Scissors":
                            gameResult = 0;
                            break;
                    }
                    break;
            }
            
            switch (gameResult)
            {
                case 1:
                    _accounts[playerOneId].WinGame();
                    _accounts[playerTwoId].LoseGame(); 
                break;
                
                case 2:
                    _accounts[playerTwoId].WinGame();
                    _accounts[playerOneId].LoseGame(); 
                break;
                
                default:
                    _accounts[playerOneId].LoseGame();
                    _accounts[playerTwoId].LoseGame();
                break;
            }
            ///////////////////////////////////////////////////
            HistoryNote(playerOneId, playerTwoId, playerOneChoose, playerTwoChoose, gameResult);
        }

        private void HistoryNote(int playerOneId, int playerTwoId,  string? playerOneChoose,  string? playerTwoChoose, int winner)
        {
            _global_history.Add(new HistoryNote(_accounts[playerOneId], _accounts[playerTwoId], winner, playerOneChoose, playerTwoChoose));
            _accounts[playerOneId].NewHistoryNote(_global_history[_global_history.Count-1]);
            _accounts[playerTwoId].NewHistoryNote(_global_history[_global_history.Count-1]);
        }

        public void NewPlayer()
        {
            Console.Write("Enter Name for new Player:\n::");
            string? nick = Console.ReadLine();
            try
            {
                if (nick.Equals(null))
                    throw new Exception("Eroor...Trying create new account without Name");
                _accounts.Add(new GameAccount(nick));
                Console.WriteLine($"New Player created successfully. Player`s {nick} ID: " + (_accounts.Count() - 1));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }

        public int NumberOfPlayers(){return _accounts.Count();}

        public int CheackAvaliablePlayer(int PlayerOneID, int PlayerTwoID)
        {
            try
            {
                if (PlayerOneID > _accounts.Count || PlayerOneID < 0)
                    throw new Exception("Error...Player One ID is not detected");

                if (PlayerTwoID > _accounts.Count || PlayerTwoID < 0)
                    throw new Exception("Error...Player Two ID is not detected");

                if (PlayerTwoID == PlayerOneID)
                    throw new Exception("Error...Can not be the same players is on game");
                return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }
        
        public void ShowHistory()
        {
            Console.WriteLine("Global Game History:");
            foreach (var note in _global_history)
                note.ShowNote();
        }

        public void ShowStatsOfPlayer(int PlayerID) { _accounts[PlayerID].Stats(); }
        
        public void ShowPersonallyHistoryOfPlayer(int PlayerID) {_accounts[PlayerID].ShowHistory();}
    }
    
    public static void Main(String[] args)
    {
        Game currentGame = new Game();
        int playerOneId, playerTwoId;
        Console.WriteLine("Game Created...Avalible accounts: 0. Creating new player...");
        while (true)
        {
            currentGame.NewPlayer();
            if (currentGame.NumberOfPlayers() < 2) continue;
            Console.Write($"\nNumber of Avaliable players is {currentGame.NumberOfPlayers()}.Do you wanna create one more?(y/n)\n::");
            if(Console.ReadLine() != "y")
                break;
        }
        Console.WriteLine("--------------------------------------");
        
        Console.Write("Choose the number of simulations of the game \"rock paper scissors\"\n::");
        int countOfGames = Convert.ToInt32(Console.ReadLine());
        for (int i = 0; i < countOfGames; i++)
        {
            do
            {
                Console.Write($"Choose first players for game #{i} (input ID) \n::");
                playerOneId = Convert.ToInt32(Console.ReadLine());
                Console.Write($"Choose second players for game #{i} (input ID) \n::");
                playerTwoId = Convert.ToInt32(Console.ReadLine());
            } while (currentGame.CheackAvaliablePlayer(playerOneId,playerTwoId) != 1);
            currentGame.GameSimulation(playerOneId, playerTwoId);
        }
        
        Console.Write("Games already ended. Choose player to display Stats (player ID)\n::");
        currentGame.ShowStatsOfPlayer(Convert.ToInt32(Console.ReadLine()));
        currentGame.ShowHistory();
        Console.Write("Choose player to display personally history\n::");
        currentGame.ShowPersonallyHistoryOfPlayer(Convert.ToInt32(Console.ReadLine()));
    }
}
}