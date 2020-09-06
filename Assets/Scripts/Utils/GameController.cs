

namespace Utils
{
    public static class GameController
    {
        public static GameState Action = GameState.joining;
        public static int Turn;
        public static bool ShouldRoadClickable = false;
        public static bool ShouldSettlementClickable = false;
    }

    public enum GameState
    {
        joining,
        init1,
        init2,
        play_development_card,
        trade_buy_build,
        thief_tile,    
        dice,
        trade_question
    }
}