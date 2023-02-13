using System.Collections;
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

        [SerializeField] AudioSource mainAudioSource;
        [SerializeField] AudioSource sfxSource;
        [SerializeField] float fadeSpeed;

        IEnumerator activeBehaviour;
        float gamewideCache;
        float sfxCache;

        private void Start()
        {
            ManagerHUB.GetManager.SetSoundsHandlerReference(this);

            ManagerHUB.GetManager.GameEventsHandler.onSceneChanged += ChangeSoundtrack;
            ManagerHUB.GetManager.GameEventsHandler.onFadeOut += MusicFadeOut;
            ManagerHUB.GetManager.GameEventsHandler.onAstarothFirstPhase += PlayAstarothOST;

            mainAudioSource.loop = true;

            SetMasterVolume(0.25f);
        }

        void ChangeSoundtrack(GameScenes scenes)
        {
            switch (scenes)
            {
                case GameScenes.PlayerScene: //What plays in the main menu
                    mainAudioSource.clip = levelWideClips[0];
                    mainAudioSource.Play();
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

        public void SetMasterVolume(float value)
        {
            ControlMainAudioSource(value);
            ControlSFXAudioSource(value);

            AudioListener.volume = value;
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

        void MusicFadeOut()
        {
            activeBehaviour = ManageMusicFadeOut(fadeSpeed);
            StartCoroutine(activeBehaviour);
        }

        IEnumerator ManageMusicFadeOut(float speed)
        {
            gamewideCache = mainAudioSource.volume;
            sfxCache = sfxSource.volume;

            //Fade Out
            while(mainAudioSource.volume > 0.005f && sfxSource.volume > 0.005f)
            {
                mainAudioSource.volume -= Time.deltaTime * speed;
                sfxSource.volume -= Time.deltaTime * speed;
                Debug.Log("Fade out");
                yield return null;
            }

            Debug.Log("Faded out");
            MusicFadeIn();
        }

        void MusicFadeIn()
        {
            activeBehaviour = ManageMusicFadeIn(fadeSpeed);
            StartCoroutine(activeBehaviour);
        }

        IEnumerator ManageMusicFadeIn(float speed)
        {
            //Fade in
            while (mainAudioSource.volume < gamewideCache && sfxSource.volume < sfxCache)
            {
                mainAudioSource.volume += Time.deltaTime * speed;
                sfxSource.volume += Time.deltaTime * speed;
                Debug.Log("Fade in");
                yield return null;

            }

            Debug.Log("Faded in");
            yield return null;
        }

        private void OnDestroy()
        {
            ManagerHUB.GetManager.GameEventsHandler.onSceneChanged -= ChangeSoundtrack;
            ManagerHUB.GetManager.GameEventsHandler.onAstarothFirstPhase -= PlayAstarothOST;
            ManagerHUB.GetManager.GameEventsHandler.onFadeOut -= MusicFadeOut;
        }
    }
}