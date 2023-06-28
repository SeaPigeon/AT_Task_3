using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Cinemachine;

public enum GameState
{
    Debug = 0,
    InMenu = 1,
    InGame = 2,
    InEditor = 3
}
public class GameManagerScript : MonoBehaviour
{
    [Header("Game Variables")]
    [SerializeField] PlayerScript _player;
    private Scene _activeScene;
    [SerializeField] string _activeSceneName;
    [SerializeField] int _sceneLoadedIndex;
    [SerializeField] GameObject _activeCanvas;
    [SerializeField] GameState _activeGameState;
    [SerializeField] Camera _activeCamera;
    [SerializeField] AudioClip _currentAudioClipLoaded;
    [SerializeField] bool _audioClipPlaying;
    
    [SerializeField] int _score;

    private static GameManagerScript _gameManagerInstance = null;

    void Awake()
    {
        GameManagerSingleton();
        
    }
    void Start()
    {
        SetUpReferences();
        SetUpGameManager();
        ResetScore();
    }

    // Getters && Setters
    public static GameManagerScript GMInstance { get { return _gameManagerInstance; } }

    // G&S States
    public Scene ActiveScene { get { return _activeScene; } set { _activeScene = value; } }
    public string ActiveSceneName { get { return _activeSceneName; } set { _activeSceneName = value; } }
    public int SceneLoadedIndex { get { return _sceneLoadedIndex; } set { _sceneLoadedIndex = value; } }
    public GameObject ActiveCanvas { get { return _activeCanvas; } set { _activeCanvas = value; } }
    public GameState ActiveGameState { get { return _activeGameState; } set { _activeGameState = value; } }
    public Camera ActiveCamera {get { return _activeCamera; } set { _activeCamera = value; } }
    public AudioClip CurrentAudioClipLoaded { get { return _currentAudioClipLoaded; } set { _currentAudioClipLoaded = value; } }
    public bool AudioClipPlaying { get { return _audioClipPlaying; } set { _audioClipPlaying = value; } }
    public int Score { get { return _score; } set { _score = value; } }

    // Methods
    private void GameManagerSingleton()
    {
        if (_gameManagerInstance == null)
        {
            _gameManagerInstance = this;
        }
        else if (_gameManagerInstance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    private void SetUpReferences()
    {
        _player = FindObjectOfType<PlayerScript>();
    }
    void SetUpGameManager()
    {
        ActiveScene = SceneManager.GetActiveScene();
        ActiveSceneName = ActiveScene.name;
        SceneLoadedIndex = ActiveScene.buildIndex;
        SetGameState();
    }
    public void SetGameState()
    {
        if (ActiveScene.name == "DebugScene")
        {
            ActiveGameState = GameState.Debug;
        }
        else if (ActiveScene.name == "MainMenu")
        {
            ActiveGameState = GameState.InMenu;
        }
        else if (ActiveScene.name == "ControlsMenu")
        {
            ActiveGameState = GameState.InMenu;
        }
        else if (ActiveScene.name == "LevelEditor")
        {
            ActiveGameState = GameState.InEditor;
        }
        else if (ActiveScene.name == "Level_1" || 
                 ActiveScene.name == "Level_2" ||
                 ActiveScene.name == "Level_3")
        {
            ActiveGameState = GameState.InGame;
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void GameManagerDebugLog()
    {
        Debug.Log("SceneManager_ActiveSceneName: " + SceneManager.GetActiveScene().name);
        Debug.Log("GM_ActiveSceneName: " + ActiveSceneName);
        Debug.Log("GM_ActiveCanvas: " + ActiveCanvas);
        Debug.Log("GM_GameState: " + ActiveGameState);
        Debug.Log("GM_ClipLoaded: " + CurrentAudioClipLoaded);
        Debug.Log("GM_AudioclipPlaying: " + AudioClipPlaying);
    }
    public void ChangeScore(int modifier)
    {
        Score += modifier;
    }
    public void ResetScore()
    {
        Score = 0;
    }
}

