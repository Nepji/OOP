namespace Лаб;
public class Simulation
        {
            private List<Game> _gameNHistory = new();
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
                            _gameNHistory.Add(new PublicGame(_accounts[_playerOneID], _accounts[_playerTwoID]));
                        break;
                    
                    case 2:
                        for (int i = 0; i < GameLoops; i++)
                            _gameNHistory.Add(new RatingGame(_accounts[_playerOneID], _accounts[_playerTwoID]));
                        break;
                    
                    default:
                        for(int i=0;i<GameLoops;i++)
                            _gameNHistory.Add(new TrainingGame(_accounts[_playerOneID], _accounts[_playerTwoID]));
                        break;
                }
                _gameNHistory[_gameNHistory.Count-1].StartGame();
                
                CheckAccountUpdate();
                _gameNHistory[_gameNHistory.Count - 1] = new Game(_gameNHistory[_gameNHistory.Count - 1]);
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
                    _accounts[_playerOneID] = new BasicVipAccount(_accounts[_playerOneID]);

                if (_accounts[_playerTwoID].getRating() > 1300 && _accounts[_playerOneID].getVipUpdate() == false)
                    _accounts[_playerTwoID] = new BasicVipAccount(_accounts[_playerTwoID]);
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
                foreach (var note in _gameNHistory)
                    note.GameStats();
            }
        }