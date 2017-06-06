using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace SHK
{
    public class AudioManager
    {
        public Song song;
        protected Random rnd = new Random();
        public float songVolume = 0.2f;

        public void PlayRandomSong(List<Song> lista)
        {
            int randomSong = rnd.Next(0, lista.Count - 1); //min <= rnd < max
            MediaPlayer.Volume = songVolume;
            song = lista[randomSong];
            MediaPlayer.Play(song);
        }

        private float GiveSoundEffectRandomPitch()
        {
            float randomPitch = (float)(rnd.NextDouble() * (0.5 - (-0.5)) + ( -0.5)); //random.NextDouble() * (maximum - minimum) + minimum;
            return randomPitch;
        }

        public void PlaySoundEffectRandomPitch(SoundEffect effect, float volume)
        {
            float pitch = GiveSoundEffectRandomPitch();
            effect.Play(volume, pitch, 0f);
        }

        public void PlaySoundEffect(SoundEffect effect, float volume)
        {
            float pitch = 0.0f;
            effect.Play(volume, pitch, 0f);
        }

        public AudioManager()
        {

        }
    }
}
