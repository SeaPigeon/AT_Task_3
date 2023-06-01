using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SceneManagerScript : MonoBehaviour
{
    [SerializeField] string[] MenuScenes;
    [SerializeField] string[] InGameScenes;
    [SerializeField] string[] InEditorScenes;
    [SerializeField] string[] InDebugScenes;

    Scene _activeScene;
    
    private static SceneManagerScript _instance;
    private GameManagerScript _gameManager;

    private void Awake()
    {
        SceneManagerSingleton();
    }
    private void Start()
    {
        ActiveScene = SceneManager.GetActiveScene();
        SetUpGameManager();
    }

    public Scene ActiveScene { get { return _activeScene; } set { _activeScene = value; } }

    private void SceneManagerSingleton()
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
        _gameManager = FindObjectOfType<GameManagerScript>();
        _gameManager.ActiveScene = ActiveScene;
        _gameManager.ActiveSceneName = ActiveScene.name;
        SetUpGameState();
        
    }
    private void SetUpGameState()
    {
        if (ActiveScene.name == "MainMenu")
        {
            _gameManager.ActiveGameState = GameState.InMenu;
        }
        else if (ActiveScene.name == "ControlsMenu")
        {
            _gameManager.ActiveGameState = GameState.InMenu;
        }
        else if (ActiveScene.name == "Level_1")
        {
            _gameManager.ActiveGameState = GameState.InGame;
        }
        else if (ActiveScene.name == "LevelEditor")
        {
            _gameManager.ActiveGameState = GameState.InEditor;
        }
        else if (ActiveScene.name == "DebugScene")
        {
            _gameManager.ActiveGameState = GameState.Debug;
        }
        /*foreach (var item in MenuScenes)
        {
            if (item == ActiveScene.name)
            {
                _gameManager.ActiveGameState = GameState.InMenu;
                return;
            }
        }
        foreach (var item in InGameScenes)
        {
            if (item == ActiveScene.name)
            {
                _gameManager.ActiveGameState = GameState.InGame;
                
                return;
            }
        }
        
        foreach (var item in InEditorScenes)
        {
            if (item == ActiveScene.name)
            {
                _gameManager.ActiveGameState = GameState.InEditor;
                return;
                Debug.Log("test");
            }
        }*/

    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        ActiveScene = SceneManager.GetActiveScene();
        _gameManager.ActiveScene = ActiveScene;
        _gameManager.ActiveSceneName = ActiveScene.name;
        SetUpGameState();
    }
}
