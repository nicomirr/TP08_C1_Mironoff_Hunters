using System;

namespace Game.Core
{
    public interface IHealthChangedNotifier
    {
        public event Action<float, float> OnHealthChanged;
    }

}

