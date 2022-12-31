namespace Лаб;

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
}