namespace Лаб;

public class VipAccount : GameAccountModifications
{
    protected int _loseStreakCount;
    protected int _loseStreakBonus;
            
    public VipAccount(string _userName) : base(_userName)
    {
        _loseGameModificator = 5;
        _winGameModificator = 3;
        _loseStreakCount = RESET;
        _loseStreakBonus = RESET;
        _basicVip_update = true;
    }

    public override void WinGame(Game Mod)
    {
        if (Mod._gameteg != "Rating game") return;
                
        _curentRating += winstore * _winGameModificator + _loseStreakBonus;
            
        _loseStreakBonus = RESET;
        _loseStreakCount = RESET; 
                
    }

    public override void LoseGame(Game Mod)
    {
        if (Mod._gameteg == "Rating game")
        {
            _loseStreakBonus++;
            if (_curentRating > (5 / _loseGameModificator) - _loseStreakBonus - _loseStreakCount)
                _curentRating -= losestore / _loseGameModificator - _loseStreakBonus - _loseStreakCount;
        }
    }
}