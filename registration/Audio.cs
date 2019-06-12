using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace registration
{
    class Audio
    {
        public static void PlaySound(string name)
        {
            var sound = new SoundPlayer(GetSoundPath(name));
            sound.Play();
        }

        public static string GetSoundPath(string name)
        {
            var path = new StringBuilder();
            path.Append("music/")
                .Append(name)
                .Append(".wav");
            return path.ToString();
        }
    }
}
