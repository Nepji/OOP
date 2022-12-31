namespace Лаб;

public class PublicGame : GameModification
{
    public PublicGame(GameAccount _playerOneAccount, GameAccount _playerTwoAccount) : base(_playerOneAccount,_playerTwoAccount)
    {
        _gameteg = Public;
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