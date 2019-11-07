
using UnityEngine;

namespace Splitten.Audio
{
    [System.Serializable]
    public class AudioManager : MonoBehaviour
    {
        public AudioTag AudioTag;
        public AudioSource Source;

        public AudioClip Clip;

        public float Volume = 1;

        public float Pitch = 1;

        public float Panning = 0;

        public bool Loop = false;

        public float Length;

        public bool IsPlaying 
        {
            get => this.Source.isPlaying; 
        }

        public bool PlayOnAwake;

        public void Setup()
        {
            this.Source = this.GetComponent<AudioSource>();

            if(this.Source != null)
            {
                this.Source.loop = this.Loop;
                this.Source.volume = this.Volume;
                this.Source.pitch = this.Pitch;
                this.Source.playOnAwake = this.PlayOnAwake;
            }
        }

        public void PlayAudio(AudioClip clip)
        {
            if(this.IsPlaying == false)
            {
                this.Source.clip = clip;
                this.Clip = clip;
                this.Source.Play(); 
            }
        }
        
    }

}