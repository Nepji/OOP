namespace Лаб;

public class GameAccount
        {
            protected List<Game> _history = new();

            protected string _userName;
            protected int _curentRating;
            protected int _gamesCount;
            protected int _userID;
            protected bool _basicVip_update = false;
            protected bool _VIPStatus;
            
            public bool getVipUpdate() { return _basicVip_update;}

            private static int userIDcounter=0;
            public string? getName() { return _userName;}

            public int getRating() { return _curentRating;}
            
            public GameAccount(string _userName)
            {
                this._userName = _userName;
                _curentRating = 1000;
                _gamesCount = 0;
                _userID=userIDcounter++;
            }

            public GameAccount(GameAccount NewAccount)
            {
                this._userName = NewAccount._userName;
                this._curentRating = NewAccount._curentRating;
                this._gamesCount = NewAccount._gamesCount;
                this._userID = NewAccount._userID;
                this._history = NewAccount._history;
                this._history = NewAccount._history;
                this._VIPStatus = NewAccount._VIPStatus;
            }
            
            public virtual void WinGame(Game Mod)
            {}

            public virtual void LoseGame(Game Mod)
            {}

            public void Stats()
            {
                Console.Write($"Account ID : {_userID}\n\tPlayer Name: {_userName}\n\tVip Status:");
                if(_VIPStatus == true)
                    Console.WriteLine("Vip Upgrade");
                else if(_basicVip_update == true)
                    Console.WriteLine("Basic Vip");
                else 
                    Console.WriteLine("None");
                Console.WriteLine($"\tGame Count : {_gamesCount}\n\tCurrent Rating : {_curentRating}");
            }

            public void History()
            {
                foreach (var note in _history)
                    note.GameStats();
            }

            public void NewHistoryNote(Game Note)
            {
                _history.Add(new Game(Note));
            }

            public void NewCounter()
            {_gamesCount++;}
        }