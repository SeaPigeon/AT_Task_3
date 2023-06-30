using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    [Header("Playlists")]
    [SerializeField] private AudioClip[] _gameMusic;
    [SerializeField] private AudioClip[] _gameSFX;

    [Header("Debug")]
    [SerializeField] private GameManagerScript _gameManager;
    [SerializeField] private AudioClip _currentAudioClipLoaded;
    [SerializeField] private bool _audioClipPlaying;
    [SerializeField] private static AudioSource _audioSourceInstance;

    private static AudioManagerScript _audioManagerInstance = null;

    void Awake()
    {
        AudioManagerSingleton();
    }

    private void Start()
    {
        SetUpReferences();
        SetUpEvents();
    }

    // Getters & Setters
    public static AudioManagerScript AMInstance { get { return _audioManagerInstance; } } 
    public AudioSource AudioSourceInstance { get { return _audioSourceInstance; } set { _audioSourceInstance = value; } }
    public AudioClip[] GameMusicList {get { return _gameMusic; } }
    public AudioClip[] GameSFXList { get { return _gameSFX; } }
    public AudioClip CurrentAudioClipLoaded { get { return _currentAudioClipLoaded; } set { _currentAudioClipLoaded = value; } }
    public bool AudioClipPlaying { get { return _audioClipPlaying; } set { _audioClipPlaying = value; } }

    // Methods
    private void AudioManagerSingleton()
    {
        if (_audioManagerInstance == null)
        {
            _audioManagerInstance = this;
        }
        else if (_audioManagerInstance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    private void SetUpReferences()
    {
        _gameManager = GameManagerScript.GMInstance;
    }
    private void SetUpEvents()
    {
        _gameManager.OnGMSetUpComplete += SetUpAudioManager;
    }

    private void SetUpAudioManager()
    {
        //_gameManager.CurrentAudioClipLoaded = _audioSourceInstance.clip;
        //_gameManager.AudioClipPlaying = _audioSourceInstance.isPlaying;
        _audioSourceInstance = FindObjectOfType<AudioSource>();
        Debug.Log("SetUpAudioManagerOnGMEventCall");
    }
    public void PlayMusic(int clipIndex)
    {
        if ((!_audioSourceInstance.isPlaying) || 
             (_audioSourceInstance.isPlaying &&
              _audioSourceInstance.clip != _gameMusic[clipIndex]))
        {
            _audioSourceInstance.clip = _gameMusic[clipIndex];
            _currentAudioClipLoaded = _gameMusic[clipIndex];

            _audioSourceInstance.Play();
            _audioClipPlaying = _audioSourceInstance.isPlaying;

            SetUpAudioManager();
        }
    }
    public void PlaySFX(int clipIndex)
    {
        if ((!_audioSourceInstance.isPlaying) ||
             (_audioSourceInstance.isPlaying &&
              _audioSourceInstance.clip != _gameSFX[clipIndex]))
        {
            _audioSourceInstance.clip = _gameSFX[clipIndex];
            _currentAudioClipLoaded = _gameSFX[clipIndex];

            _audioSourceInstance.Play();
        }
    }
}

