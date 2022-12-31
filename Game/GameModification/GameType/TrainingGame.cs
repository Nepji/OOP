namespace Лаб;
public class TrainingGame : GameModification
{
    public TrainingGame(GameAccount _playerOneAccount, GameAccount _playerTwoAccount) : base(_playerOneAccount,_playerTwoAccount)
    {
        _gameteg = Training;
    }
}