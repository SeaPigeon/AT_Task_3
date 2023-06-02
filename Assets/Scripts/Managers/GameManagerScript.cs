using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

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
    [SerializeField] string _activeSceneName;
    private Scene _activeScene;
    [SerializeField] GameObject _activeCanvas;
    [SerializeField] GameState _activeGameState;
    [SerializeField] AudioClip _currentAudioClipLoaded;
    [SerializeField] bool _audioClipPlaying;
    [SerializeField] int _sceneLoadedIndex;

    private static GameManagerScript _instance = null;

    void Awake()
    {
        GameManagerSingleton();
        SetUpGameManager();
    }

    // Getters && Setters
    public GameManagerScript GameManagerInstance { get { return _instance; } }

    // G&S States
    public GameState ActiveGameState { get { return _activeGameState; } set { _activeGameState = value; } }
    public Scene ActiveScene { get { return _activeScene; } set { _activeScene = value; } }
    public string ActiveSceneName { get { return _activeSceneName; } set { _activeSceneName = value; } }
    public int SceneLoadedIndex { get { return _sceneLoadedIndex; } set { _sceneLoadedIndex = value; } }
    public GameObject ActiveCanvas { get { return _activeCanvas; } set { _activeCanvas = value; } }
    public AudioClip CurrentAudioClipLoaded { get { return _currentAudioClipLoaded; } set { _currentAudioClipLoaded = value; } }
    public bool AudioClipPlaying { get { return _audioClipPlaying; } set { _audioClipPlaying = value; } }
    
    // Methods
    private void GameManagerSingleton()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    private void SetUpGameManager()
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
        else if (ActiveScene.name == "Level_1")
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
}

