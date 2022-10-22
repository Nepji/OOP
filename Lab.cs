// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Лаб
{
    public class Lab
    {
        public class GameAccount
        {
            protected List<Game> _history = new();

            protected string _userName;
            protected int _curentRating;
            protected int _gamesCount;
            protected int _userID;
            protected bool _basicVip_update = false;
            
            public bool getVipUpdate() { return _basicVip_update;}

            private static int userIDcounter=0;
            public string? getName() { return _userName;}

            public int getRating() { return _curentRating;}
            
            public GameAccount(string _userName)
            {
                this._userName = _userName;
                _curentRating = 1000;
                _gamesCount = 0;
                _userID=userIDcounter++;
            }

            public GameAccount(GameAccount NewAccount)
            {
                this._userName = NewAccount._userName;
                this._curentRating = NewAccount._curentRating;
                this._gamesCount = NewAccount._gamesCount;
                this._userID = NewAccount._userID;
                this._history = NewAccount._history;
            }
            
            public virtual void WinGame(Game Mod)
            {}

            public virtual void LoseGame(Game Mod)
            {}

            public virtual void Stats()
            {
                Console.WriteLine($"Account ID : {_userID}\n\tPlayer Name: {_userName}\n\tGame Count : {_gamesCount}\n\tCurrent Rating : {_curentRating}");
            }

            public void History()
            {
                foreach (var note in _history)
                    note.GameStats();
            }

            public void NewHistoryNote(Game Note)
            {
                _history.Add(new Game(Note));
            }

            public void NewCounter()
            {_gamesCount++;}
        }

        public class GameAccountModifications : GameAccount
        {
            protected const int losestore = 5;
            protected const int winstore = 10;
            
            protected int _loseGameModificator;
            protected int _winGameModificator;
            
            protected int _winStreakCount = 0;
            protected int _winStreakBonus;

            public GameAccountModifications(string _userName) : base(_userName)
            {}
            public GameAccountModifications(GameAccount NewAccount) : base(NewAccount)
            {}
        }

        public class BasicGameAccount : GameAccountModifications
        {

            public bool GetVipStatus()
            {
                return _basicVip_update;}

            public BasicGameAccount(string _userName) : base(_userName)
            {
                _loseGameModificator = 2;
                _winGameModificator = 1;

                _basicVip_update = false;
            }

            public BasicGameAccount(GameAccount NewAccount) : base(NewAccount)
            {
                _basicVip_update = true;
            }
            public override void WinGame(Game Mod)
            {
                if (Mod._gameteg == "Rating game")
                {
                    _winStreakBonus = (_winStreakCount++) * _winGameModificator;

                    if (_winStreakCount > 2) _curentRating += winstore + _winStreakBonus;
                    else _curentRating += winstore;
                }
            }

            public override void LoseGame(Game Mod)
            {
                if (Mod._gameteg == "Rating game")
                {
                    _winStreakCount = 0;
                    if (_curentRating > 5 / _loseGameModificator)
                        _curentRating -= losestore / _loseGameModificator;
                }
            }
        }

        public class BasicVipAccount : BasicGameAccount
        {
            public BasicVipAccount(GameAccount newAccount) : base(newAccount)
            {
                _loseGameModificator = 5;
                _winGameModificator = 2;
            }

            public override void WinGame(Game Mod)
            {
                _winStreakBonus = (_winStreakCount++)*_winGameModificator;
                _curentRating += winstore + _winStreakBonus;
            }

            public override void Stats()
            {
                base.Stats();
                Console.WriteLine($"\tVIP Status : ON");
            }
        }

        public class VipAccount : GameAccountModifications
        {
            protected int _loseStreakCount;
            protected int _loseStreakBonus;
            
            public VipAccount(string _userName) : base(_userName)
            {
                _loseGameModificator = 5;
                _winGameModificator = 3;
                _loseStreakCount = 0;
                _loseStreakBonus = 0;
                _basicVip_update = true;
            }

            public override void WinGame(Game Mod)
            {
                if (Mod._gameteg == "Rating game")
                {
                    _curentRating += winstore * _winGameModificator + _loseStreakBonus;
                
                    _loseStreakBonus = 0;
                    _loseStreakCount = 0; 
                }
            }

            public override void LoseGame(Game Mod)
            {
                if (Mod._gameteg == "Rating game")
                {
                    _loseStreakBonus++;
                    if (_curentRating > (5 / _loseGameModificator) - _loseStreakBonus)
                        _curentRating -= losestore / _loseGameModificator - _loseStreakBonus;
                }
            }

            public override void Stats()
            {
                base.Stats();
                Console.WriteLine($"\tVIP Status : ON");
            }
        }
        
        
        ////////////////////////////////

        public class Simulation
        {
            private List<Game> _game_n_history = new();
            private List<GameAccount> _accounts = new();
            
            private bool _VIP_acoount;

            private int _playerOneID=-1, _playerTwoID=-1;

            public Simulation()
            {
                Console.WriteLine("Game Simulation`s start...\n\n");
                Start();
            }

            private void Start()
            {
                NewPlayer(_VIP_acoount);
                do
                {
                    NewPlayer(_VIP_acoount);
                    Console.Write($"Current numbers of Basic Account`s is {_accounts.Count}. Do you wont to create more? (y/n)\n::");
                } while (Console.ReadLine() == "y");
                Console.Write("Accounts created. Do you wont to create VIP account?(y/n)\n::");
                if(Console.ReadLine() == "y")
                    NewPlayer(_VIP_acoount=true);
                Console.WriteLine("Done...\n\tNext step...");
                GameChooseNStart();
                EndGame();
            }

            private void GameChooseNStart()
            {
                Console.WriteLine("Choose game mode:\n\t<0>Training Game\n\t<1> Public game\n\t<2> Rating Game");
                int GameModeChoosed = Convert.ToInt32(Console.ReadLine());
                Console.Write("Choose Number of games with Two RANDOM players\n::");
                int GameLoops = Convert.ToInt32(Console.ReadLine());

                Random rand = new Random();
                _playerOneID = rand.Next(0, _accounts.Count);
                do
                {
                    _playerTwoID = rand.Next(0, _accounts.Count);
                } while(_playerTwoID == _playerOneID);
                
                
                
                switch (GameModeChoosed)
                {
                    case 1:
                        for (int i = 0; i < GameLoops; i++)
                        {
                            _game_n_history.Add(new PublicGame(_accounts[_playerOneID], _accounts[_playerTwoID]));
                            _game_n_history[_game_n_history.Count-1].StartGame();
                        }
                        break;
                    
                    case 2:
                        for (int i = 0; i < GameLoops; i++)
                        {
                            _game_n_history.Add(new RatingGame(_accounts[_playerOneID], _accounts[_playerTwoID]));
                            _game_n_history[_game_n_history.Count-1].StartGame();
                        }
                        break;
                    
                    default:
                        for(int i=0;i<GameLoops;i++)
                        {
                            _game_n_history.Add(new TrainingGame(_accounts[_playerOneID], _accounts[_playerTwoID]));
                            _game_n_history[_game_n_history.Count-1].StartGame();
                        }
                        break;
                }
                
                CheckAccountUpdate();
                Console.Write("Current Simulations End. Do you wont to start one more?(y/n)");
                if(Console.ReadLine() == "y") GameChooseNStart();
            }

            private void EndGame()
            {
                Console.Write("All games ended... Do you wont to see global history?(y/n)\n::");
                if (Console.ReadLine() == "y") ShowGlobalHistory();
                Console.WriteLine($"\nChoose Player to display personal stats & history (0,{_accounts.Count-1})\n ::");
                int Choose = Convert.ToInt32(Console.ReadLine());
                if (Choose>0 && Choose < _accounts.Count)
                {
                    _accounts[Choose].Stats();
                    Console.WriteLine("");
                    _accounts[Choose].History();
                }
            }

            private void CheckAccountUpdate()
            {
                if (_accounts[_playerOneID].getRating() > 1300 && _accounts[_playerOneID].getVipUpdate() == false)
                {
                    _accounts[_playerOneID] = new BasicVipAccount(_accounts[_playerOneID]);
                }

                if (_accounts[_playerTwoID].getRating() > 1300 && _accounts[_playerOneID].getVipUpdate() == false)
                {
                    _accounts[_playerTwoID] = new BasicVipAccount(_accounts[_playerTwoID]);
                }
            }
            private void NewPlayer(bool VIP)
            {
                Console.Write("Enter Name for new Player:\n::");
                string? nick = Console.ReadLine();
                try
                {
                    if (nick.Equals(null))
                        throw new Exception("Eroor...Trying create new account without Name");
                    if(VIP == false) _accounts.Add(new BasicGameAccount(nick));
                    else _accounts.Add(new VipAccount(nick));
                    Console.WriteLine(
                        $"New Player created successfully. Player`s {nick} ID: " + (_accounts.Count() - 1));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
            }

            private void ShowGlobalHistory()
            {
                foreach (var note in _game_n_history)
                    note.GameStats();
            }
        }
        
        ////////////////////////////////
     

        public class Game
        {
            private static int GameID = 0;
            private int CurrentGameID;
            
            protected GameAccount _playerOneAccount;
            protected GameAccount _playerTwoAccount;
            public String? _gameteg;
            protected int _gameWinner;
            protected enum GameAtributes
            {
                Rock = 1,
                Paper = 2,
                Scissors = 3
            }

            public Game(GameAccount _playerOneAccount, GameAccount _playerTwoAccount)
            {
                this._playerOneAccount = _playerOneAccount;
                this._playerTwoAccount = _playerTwoAccount;
                CurrentGameID = GameID++;
            }

            public Game(Game Note)
            {
                this._playerOneAccount = Note._playerOneAccount;
                this._playerTwoAccount = Note._playerTwoAccount;
                this._gameteg = Note._gameteg;
                this._gameWinner = Note._gameWinner;
                this.CurrentGameID = Note.CurrentGameID;
            }

            public virtual void StartGame(){}
            protected virtual void GameResult() {}

            public virtual void GameStats()
            {
                Console.Write($"Game ID#{CurrentGameID}\n\tPlayer One : {_playerOneAccount.getName()}\n\tPlayer Two : {_playerTwoAccount.getName()}\n\tGame mode : {_gameteg}\n");
                if(_gameWinner != 0) Console.WriteLine($"\tWinner of the game : {(_gameWinner == 1 ? _playerOneAccount.getName() : _playerTwoAccount.getName())}");
                    else Console.WriteLine("\tResult : Draw");
                Console.WriteLine("n------------------------------");
            }
        }

        public class GameModification : Game
        {
            public GameModification(GameAccount _playerOneAccount, GameAccount _playerTwoAccount) : base(_playerOneAccount,_playerTwoAccount){}
            
            public void GameSolution(string? playerOneChoose, string? playerTwoChoose)
            {
                switch (playerOneChoose)
                {
                    case "Rock":
                        switch (playerTwoChoose)
                        {
                            case "Rock":
                                _gameWinner = 0;
                                break;
                            case "Paper":
                                _gameWinner = 2;
                                break;
                            case "Scissors":
                                _gameWinner = 1;
                                break;
                        }

                        break;
                    case "Paper":
                        switch (playerTwoChoose)
                        {
                            case "Rock":
                                _gameWinner = 1;
                                break;
                            case "Paper":
                                _gameWinner = 0;
                                break;
                            case "Scissors":
                                _gameWinner = 2;
                                break;
                        }

                        break;
                    case "Scissors":
                        switch (playerTwoChoose)
                        {
                            case "Rock":
                                _gameWinner = 2;
                                break;
                            case "Paper":
                                _gameWinner = 1;
                                break;
                            case "Scissors":
                                _gameWinner = 0;
                                break;
                        }

                        break;
                }
                _playerOneAccount.NewHistoryNote(this);
                _playerTwoAccount.NewHistoryNote(this);
                GameResult();
            }

            protected override void GameResult()
            {
                _playerOneAccount.NewHistoryNote(this);
                _playerTwoAccount.NewHistoryNote(this);
            }

            public override void StartGame()
            {
                Random rand = new Random();
                string? playerOneChoose = Enum.GetName(typeof(GameAtributes), rand.Next(1, 3));
                string? playerTwoChoose = Enum.GetName(typeof(GameAtributes), rand.Next(1, 3));
                GameSolution(playerOneChoose,playerTwoChoose);
            }
            
        }

        public class TrainingGame : GameModification
        {
            public TrainingGame(GameAccount _playerOneAccount, GameAccount _playerTwoAccount) : base(_playerOneAccount,_playerTwoAccount)
            {
                _gameteg = "Training";
            }
        }

        public class PublicGame : GameModification
        {
            public PublicGame(GameAccount _playerOneAccount, GameAccount _playerTwoAccount) : base(_playerOneAccount,_playerTwoAccount)
            {
                _gameteg = "Public game";
            }

            protected override void GameResult()
            {
                _playerOneAccount.NewCounter();
                _playerTwoAccount.NewCounter();
                
                if (_gameWinner == 1)
                {
                    _playerOneAccount.WinGame(this);
                    _playerTwoAccount.LoseGame(this);
                }
                else if (_gameWinner == 2)
                {
                    _playerTwoAccount.WinGame(this);
                    _playerOneAccount.LoseGame(this);
                }
                else
                {
                    _playerOneAccount.LoseGame(this);
                    _playerTwoAccount.LoseGame(this);
                }

            }
        }

        public class RatingGame : PublicGame
        {
            public RatingGame(GameAccount _playerOneAccount, GameAccount _playerTwoAccount) : base(_playerOneAccount,_playerTwoAccount)
            {
                _gameteg = "Rating game";
            }
        }

        
        ////////////////////////////////
        
        public static void Main(String[] args)
        {
            Simulation game = new Simulation();
        }
    }
}