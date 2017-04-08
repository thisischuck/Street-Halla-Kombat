using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace SHK
{
    public class AudioManager
    {
        public Song song;
        protected Random rnd = new Random();
        public float songVolume = 0.2f;

        public Song PlayRandomSong(List<Song> lista)
        {
            int randomSong = rnd.Next(1, 3); //min <= rnd < max
            MediaPlayer.Volume = songVolume;
            song = lista[randomSong];
            return song;
        }
    }
}
