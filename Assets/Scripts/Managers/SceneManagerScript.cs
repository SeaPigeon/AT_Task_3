using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SceneManagerScript : MonoBehaviour
{
    Scene _activeScene;
    
    private static SceneManagerScript _SceneManagerInstance;
    private GameManagerScript _gameManager;

    // Main
    private void Awake()
    {
        SceneManagerSingleton();
    }
    private void Start()
    {
        SetUpGameManager();
    }

    // Getters & Setters
    public Scene ActiveScene { get { return _activeScene; } set { _activeScene = value; } }

    // Methods
    private void SceneManagerSingleton()
    {
        if (_SceneManagerInstance == null)
        {
            _SceneManagerInstance = this;
        }
        else if (_SceneManagerInstance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    private void SetUpGameManager()
    {
        _gameManager = FindObjectOfType<GameManagerScript>();
    }
    
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        ActiveScene = SceneManager.GetActiveScene();
        _gameManager.ActiveScene = ActiveScene;
        _gameManager.ActiveSceneName = ActiveScene.name;
        _gameManager.SceneLoadedIndex = ActiveScene.buildIndex;
        _gameManager.SetGameState();
    }
}
