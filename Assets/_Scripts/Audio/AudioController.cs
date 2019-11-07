using UnityEngine;
using System.Collections.Generic;

using Splitten.Extensions;

namespace Splitten.Audio
{
    public class AudioController : Singleton<AudioController>
    {
        //audio source for background music
        //audio source for ui sounds
        //audio source for soundfx

        [SerializeField]private Dictionary<AudioTag, AudioManager> audioManagers;
        private void Awake()
        {
            this.Setup();
        }
        private void Setup()
        {
            //Checking if dictionary is created or not. Will be created if not.
            if(this.audioManagers == null)
                this.audioManagers = new Dictionary<AudioTag, AudioManager>();
            
            if(this.audioManagers.Count <= 0 )
            {
                string[] audioTags = System.Enum.GetNames(typeof(AudioTag));

                for(int i =0; i < (int)audioTags.Length; i++)
                {
                    string currentTag = audioTags[i];
                    
                    this.audioManagers.Add((AudioTag)System.Enum.Parse(typeof(AudioTag), currentTag),
                                            this.CreateAudioManager((AudioTag)System.Enum.Parse(typeof(AudioTag), currentTag))
                    );

                }
            }
        }

        private AudioManager CreateAudioManager(AudioTag audioTag)
        {
            //Creating new game object.
            GameObject newAudioManager = new GameObject();

            //Setting the AudioController gameobject the parent of the AudioManager gameobject.
            newAudioManager.transform.parent = this.transform;

            //Setting the name of the gameobject.
            newAudioManager.name = $"[AudioManager] {audioTag} ";

            //Adding  Audiomanager component.
            AudioManager audioManager =  newAudioManager.AddComponent<AudioManager>();

            //Adding AudioSource component.
            newAudioManager.AddComponent<AudioSource>();

            //Setting the audio tag.
            audioManager.AudioTag = audioTag;

            audioManager.Setup();

            return audioManager;
        }
        
        public void PlayAudio(AudioTag audioTag, AudioClip clip)
        {
            AudioManager audioManager = this.FetchAudioManager(audioTag);

            audioManager.PlayAudio(clip);
            
        }

        public void SetVolume(AudioTag audioTag, float value = 0)
        {
            Debug.Log(this.FetchAudioManager(audioTag));
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.R)){
                this.SetVolume(AudioTag.BACKGROUND);
            }
            if(Input.GetKeyDown(KeyCode.T)){
                this.SetVolume(AudioTag.SFX);
            }
            if(Input.GetKeyDown(KeyCode.Q)){
                this.SetVolume(AudioTag.UI);
            }
        }

        private AudioManager FetchAudioManager(AudioTag audioTag)
        {
            AudioManager audioManager = null;

            if(this.audioManagers.ContainsKey(audioTag) == false)
                audioManager = this.CreateAudioManager(audioTag);
            else
                audioManager = this.audioManagers[audioTag];
            
            return audioManager;
        }
    }

}