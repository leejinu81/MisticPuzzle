using Zenject;

namespace Lonely
{
    public static class GameCommands
    {
        public class EnemyTurn : Command { }

        public class Escape : Command { }

        public class Die : Command { }
    }
}