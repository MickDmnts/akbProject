using System.Collections.Generic;
using akb.Core.Managing;
using akb.Core.Managing.LevelLoading;
using UnityEngine;
using UnityEngine.UI;

namespace akb.Core.Sounds
{
    public enum GameAudioClip
    {
        AdvancementLevelingUp,
        AdvancementLeveled,
        HealthFountainIDLE,
        HealthReceived,
        ChangePageGrimoire,
        CloseGrimoire,
        CloseMenu,
        OpenGrimoire,
        OpenMenu,
        UIClickSound,
        BossFireOrbs,
        BossStartFireFromBelow,
        BrainShaperBeforeTeleport,
        FireBloatAttack1,
        FireBloatAttack2,
        FireBloatAttack3,
        FireBloatAttack4,
        FireBloatAttack5,
        FireBloatOnDeath,
        FlamechargerFootsteps,
        LargeMonsterFootstep_4,
        GrimbatAttack1,
        GrimbatAttack2,
        HellCreeperAttack1,
        HellCreeperAttack2,
        HellCreeperAttack3,
        HellCreeperFootsteps,
        SuccubusBeforeTeleport,
        WraithCharge,
        SpearSwing1,
        SpearSwing2,
        SpearSwing3,
        SpearSwing4,
        SpearSwing5,
        SpearStabFlesh1,
        SpearStabFlesh2,
        SpearStabFlesh3,
        SpearStabFlesh4,
        TridentCharge,
        TridentHit,
        TridentThrow,
        DevilRageON,
        DevilRageReady,
        Dodgeroll,
        GetHit,
        YouLost,
        IntroTheme,
        GetEnflamed,
        GetCharmed,
        GetConfused,
        GetElectrified,
        ArcAttack,
        ArcDrawingSword,
        MineExplode,
        MineTrigger,
        ProjectileHit,
        ProjectileShoot,
        SpikeTrapOut,
        SpikeTrapTrigger,
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
            ManagerHUB.GetManager.SetSoundsHandlerReference(this);

            ManagerHUB.GetManager.GameEventsHandler.onSceneChanged += ChangeSoundtrack;
            ManagerHUB.GetManager.GameEventsHandler.onAstarothFirstPhase += PlayAstarothOST;

            mainAudioSource.loop = true;
        }

        void ChangeSoundtrack(GameScenes scenes)
        {
            switch (scenes)
            {
                case GameScenes.PlayerScene: //What plays in the main menu
                    mainAudioSource.clip = levelWideClips[0];
                    mainAudioSource.Play();

                    Debug.Log(scenes);
                    break;

                case GameScenes.PlayerHUB: //What plays in the hub
                    mainAudioSource.clip = levelWideClips[0];
                    mainAudioSource.Play();
                    break;

                case GameScenes.World1Scene: //What plays in world 1
                    mainAudioSource.clip = levelWideClips[1];
                    mainAudioSource.Play();
                    break;

                    /* case GameScenes.World2Scene: //What plays in world 2
                        mainAudioSource.clip = levelWideClips[3];
                        mainAudioSource.Play();
                        break; */
            }

        }

        void PlayAstarothOST()
        {
            mainAudioSource.clip = levelWideClips[2];
            mainAudioSource.Play();
        }

        public void SetSoundReferences(Slider musicSlider)
        {
            this.musicSlider = musicSlider;
        }

        public void SetMasterVolume(float value)
        {
            mainAudioSource.volume = value;
            sfxSource.volume = value;
        }

        public void ControlMainAudioSource(float value)
        {
            mainAudioSource.volume = value;
        }

        public void ControlSFXAudioSource(float value)
        {
            sfxSource.volume = value;
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
            ManagerHUB.GetManager.GameEventsHandler.onAstarothFirstPhase -= PlayAstarothOST;
        }
    }
}