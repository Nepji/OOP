namespace Лаб;

public class GameAccountModifications : GameAccount
{
    protected string Training = "Training";
    protected string Public = "Public game";
    protected string Rating = "Rating game";
    protected int RESET = 0;
            
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