using System.Collections.Generic;

namespace Game.Core
{
    public interface IVolumeProvider
    {
        public Dictionary<string, float> Volumes { get; }
    }

}
