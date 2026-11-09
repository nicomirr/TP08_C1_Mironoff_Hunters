using System;

namespace Game.Core
{
    public interface IPauseNotifier
    {
        public event Action OnGamePaused;
        public event Action OnGameUnpaused;
    }
}
