namespace Лаб;

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
        if (Mod._gameteg != Rating) return;
                
        _winStreakBonus = (_winStreakCount++) * _winGameModificator;

        if (_winStreakCount > 2) _curentRating += winstore + _winStreakBonus;
        else _curentRating += winstore;
                
    }

    public override void LoseGame(Game Mod)
    {
        if (Mod._gameteg != Rating) return;
                
        _winStreakCount = RESET;
        if (_curentRating > 5 / _loseGameModificator)
            _curentRating -= losestore / _loseGameModificator;
            
    }
}