namespace Лаб;

public class Game
{
    private static int GameID = 0;
    private int CurrentGameID;
            
    protected GameAccount _playerOneAccount;
    protected GameAccount _playerTwoAccount;
    public String? _gameteg;
    protected int _gameWinner;
            

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
        Console.WriteLine(_gameWinner != 0
            ? $"\tWinner of the game : {(_gameWinner == 1 ? _playerOneAccount.getName() : _playerTwoAccount.getName())}"
            : "\tResult : Draw");
        Console.WriteLine("------------------------------");
    }
}