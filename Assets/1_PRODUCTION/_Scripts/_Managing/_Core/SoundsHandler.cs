using System.Collections.Generic;
using akb.Core.Managing;
using akb.Core.Managing.LevelLoading;
using UnityEngine;
using UnityEngine.UI;

namespace akb.Core.Sounds
{
    public enum GameAudioClip
    {
        //Populate with sfx names
    }

    public class SoundsHandler : MonoBehaviour
    {
        [Header("Set in inspector")]
        [SerializeField] List<AudioClip> levelWideClips;
        [SerializeField] List<AudioClip> sfxClips;

        public AudioSource mainAudioSource;
        public AudioSource sfxSource;

        Slider musicSlider;

        private void Start()
        {
            ManagerHUB.GetManager.GameEventsHandler.onSceneChanged += ChangeSoundtrack;
        }

        void ChangeSoundtrack(GameScenes scenes)
        {
            switch (scenes)
            {
                case GameScenes.PlayerScene: //What plays in the main menu
                    //mainAudioSource.clip = levelWideClips[0];
                    //mainAudioSource.Play();
                    break;

                case GameScenes.PlayerHUB: //What plays in the hub
                    //mainAudioSource.clip = levelWideClips[1];
                    //mainAudioSource.Play();
                    break;

                case GameScenes.World1Scene: //What plays in world 1
                    //mainAudioSource.clip = levelWideClips[2];
                    //mainAudioSource.Play();
                    break;

                case GameScenes.World2Scene: //What plays in world 2
                    //mainAudioSource.clip = levelWideClips[3];
                    //mainAudioSource.Play();
                    break;
            }
        }

        public void SetSoundReferences(Slider musicSlider)
        {
            this.musicSlider = musicSlider;
        }

        public void SetVolume(float value)
        {
            mainAudioSource.volume = value;
            sfxSource.volume = value;

            musicSlider.value = value * 10f;
        }

        ///<summary>Plays the passed clip one time</summary>
        public void PlayOneShot(GameAudioClip clip)
        {
            sfxSource.PlayOneShot(sfxClips[(int)clip]);
        }

        ///<summary>Mutes/unmutes the sound</summary>
        public void SetGameWideSoundtrackState(bool pause)
        {
            if (pause)
            {
                mainAudioSource.Pause();
            }
            else
            {
                mainAudioSource.UnPause();
            }
        }

        private void OnDestroy()
        {
            ManagerHUB.GetManager.GameEventsHandler.onSceneChanged -= ChangeSoundtrack;
        }
    }
}