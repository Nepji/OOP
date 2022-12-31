namespace Лаб;

public class RatingGame : PublicGame
{
    public RatingGame(GameAccount _playerOneAccount, GameAccount _playerTwoAccount) : base(_playerOneAccount,_playerTwoAccount)
    {
        _gameteg = Rating;
    }
}