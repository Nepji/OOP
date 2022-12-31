namespace Лаб;

public class GameModification : Game
        {
            protected string Training = "Training";
            protected string Public = "Public game";
            protected string Rating = "Rating game";
            
            protected enum GameAtributes
            {
                Rock = 1,
                Paper = 2,
                Scissors = 3
            }
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