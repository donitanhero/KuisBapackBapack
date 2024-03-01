using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Private Field Region

        /// <summary>
        /// how many sounds or audio in game
        /// </summary>
        [SerializeField] private SoundData_SO _soundData;
        [SerializeField] private AudioSource _sfxSound;
        [SerializeField] private AudioSource _sfxButton;
        [SerializeField] private AudioSource _bgmSound;
        /*/// <summary>
        /// access to setting data scriptableObject
        /// </summary>
        [SerializeField] private SettingsData _settingsData;*/

        private bool _isEven;

        #endregion


        #region Private Method Region

        /// <summary>
        /// set up singleton and create audio Sources for each audio
        /// </summary>
        private void Awake()
        {
            StaticAction.OnMusicPlay = PlaySound;
            StaticAction.OnSFXSoundPlay = PlaySoundOnce;
        }
        

        private void OnDestroy()
        {
            StaticAction.OnMusicPlay = null;
            StaticAction.OnSFXSoundPlay = null;
        }

        #endregion

        #region Public Method Region

        /// <summary>
        /// method function used to play sound
        /// </summary>
        /// <param name="ID"></param>
        public void PlaySoundOnce(int ID)
        {

            if(ID == ConstVar.SOUND_BUTTON_CLICK_SFX){

                if(!_sfxButton.isPlaying){
                    _sfxButton.PlayOneShot(_soundData.SoundData[ID].clip);
                }
            }
            else{
                
            if (!_sfxSound.isPlaying)
            {
                _sfxSound.PlayOneShot(_soundData.SoundData[ID].clip);
            }
            }

            
        }
        
        /// <summary>
        /// used to play sound multiple time without caring the it already being play
        /// </summary>
        /// <param name="ID"></param>
        public void PlaySound(int ID)
        {
            
            _bgmSound.clip = _soundData.SoundData[ID].clip;
            _bgmSound.volume = _soundData.SoundData[ID].volume;
            _bgmSound.Play();
        }

        public void SetSoundVolume(int vol, SoundType type)
        {
            foreach (Sound s in _soundData.SoundData)
            {
                if (s._soundType == type)
                {
                    s.source.volume = vol;
                }
            }
        }

    

        #endregion
}

[Serializable]
public class Sound
{
    /// <summary>
    /// name of the audio
    /// </summary>
    public string audioName;
        
    /// <summary>
    /// audio ID clip
    /// </summary>
    public int audioID;

    /// <summary>
    /// the clip of the audio 
    /// </summary>
    public AudioClip clip;

    /// <summary>
    /// audio volume
    /// </summary>
    [Range(0f, 1f)] public float volume;
        
    /// <summary>
    /// audio pitch
    /// </summary>
    [Range(.1f, 3f)] public float pitch;

    /// <summary>
    /// should the audio clip looping 
    /// </summary>
    public bool loop;

    /// <summary>
    /// audio sources
    /// </summary>
    [HideInInspector] public AudioSource source;

    public SoundType _soundType;
}

public enum SoundType
{
    Music,
    SoundFX
}