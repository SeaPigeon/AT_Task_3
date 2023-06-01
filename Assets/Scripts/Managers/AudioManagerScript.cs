using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    [Header("Playlists")]
    [SerializeField] private AudioClip[] _gameMusic;
    [SerializeField] private AudioClip[] _gameSFX;

    [Header("Debug")]
    [SerializeField] private AudioClip _currentAudioClipLoaded;
    [SerializeField] private bool _audioClipPlaying;
    [SerializeField] private AudioSource _audioSourceInstance;

    private static AudioManagerScript _audioManagerInstance = null;
    private GameManagerScript _gameManager;

    void Awake()
    {
        AudioManagerSingleton();
    }

    private void Start()
    {
        SetUpGameManager();
    }

    // Getters & Setters
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
    private void SetUpGameManager()
    {
        _gameManager = FindObjectOfType<GameManagerScript>();
        _gameManager.CurrentAudioClipLoaded = _audioSourceInstance.clip;
        _gameManager.AudioClipPlaying = _audioSourceInstance.isPlaying;
    }

    public void PlayMusic(AudioSource ASource, int clipIndex)
    {
        if ((!ASource.isPlaying) || 
             (ASource.isPlaying && 
              ASource.clip != _gameMusic[clipIndex]))
        {
            ASource.clip = _gameMusic[clipIndex];
            _currentAudioClipLoaded = _gameMusic[clipIndex];
            _gameManager.CurrentAudioClipLoaded = _currentAudioClipLoaded;

            ASource.Play();
            _audioClipPlaying = ASource.isPlaying;
            _gameManager.AudioClipPlaying = _audioClipPlaying;
        }
    }
}

