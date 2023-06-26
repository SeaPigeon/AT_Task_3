using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SceneManagerScript : MonoBehaviour
{
    Scene _activeScene;

    public event Action<string> LoadSceneEvent;
    
    private static SceneManagerScript _SceneManagerInstance;
    [SerializeField] private GameManagerScript _gameManager;
    [SerializeField] private InputManagerScript _inputManager;
    [SerializeField] private PlayerScript _player;

    // Main
    private void Awake()
    {
        SceneManagerSingleton();
    }
    private void Start()
    {
        SetUpReferences();
        SubscribeToEvents();
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
    private void SetUpReferences()
    {
        _gameManager = FindObjectOfType<GameManagerScript>();
        _inputManager = FindObjectOfType<InputManagerScript>();
        _player = FindObjectOfType<PlayerScript>();
    }
    private void SubscribeToEvents()
    {
        LoadSceneEvent += LoadScene;
        SceneManager.sceneLoaded += SetActiveScene;
    }
    
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    void SetActiveScene(Scene scene, LoadSceneMode mode)
    {
        ActiveScene = SceneManager.GetActiveScene();
        _gameManager.ActiveScene = ActiveScene;
        _gameManager.ActiveSceneName = ActiveScene.name;
        _gameManager.SceneLoadedIndex = ActiveScene.buildIndex;
        _gameManager.SetGameState();
        if (_gameManager.ActiveGameState == GameState.InGame)
        {
            _inputManager.ActivateInputMap(_inputManager.InputMap.Game);
            _player.SpawnPlayer();
        }
        else
        {
            _inputManager.ActivateInputMap(_inputManager.InputMap.UI);
            _player.TogglePlayerMesh(false);
        }
        
        Debug.Log("SceneManager LoadScene/OnLoadScene: " + SceneManager.GetActiveScene().name);
    }
    
    // Events
    public void OnLoadScene(string sceneName)
    {
        LoadSceneEvent?.Invoke(sceneName);
    }
}
