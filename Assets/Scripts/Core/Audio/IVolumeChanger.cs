using System.Collections.Generic;

namespace Game.Core
{
    public interface IVolumeChanger
    {
        public void ChangeVolume(List<string> groups, float volume);
    }
}
