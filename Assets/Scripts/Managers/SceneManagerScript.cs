using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SceneManagerScript : MonoBehaviour
{
    Scene _activeScene;

    public event Action<string, int> OnSceneTransitionEvent;
    
    private static SceneManagerScript _SceneManagerInstance;
    [SerializeField] private GameManagerScript _gameManager;
    [SerializeField] private UIManagerScript _uiManager;

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
    private void Update() {
        //Debug.Log(ActiveScene.name);
        
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
        _uiManager = FindObjectOfType<UIManagerScript>();
    }
    private void SubscribeToEvents()
    {
        OnSceneTransitionEvent += LoadScene;
        SceneManager.sceneLoaded += SetActiveScene;
    }
    
    public void LoadScene(string sceneName, int canvasIndex)
    {
        SceneManager.LoadScene(sceneName);
        /*ActiveScene = SceneManager.GetActiveScene();
        _gameManager.ActiveScene = ActiveScene;
        _gameManager.ActiveSceneName = ActiveScene.name;
        _gameManager.SceneLoadedIndex = ActiveScene.buildIndex;
        _gameManager.SetGameState();*/
        //Debug.Log(_gameManager.ActiveGameState);
        _uiManager.LoadCanvas(canvasIndex);
    }

    void SetActiveScene(Scene scene, LoadSceneMode mode)
    {
        ActiveScene = SceneManager.GetActiveScene();
        _gameManager.ActiveScene = ActiveScene;
        _gameManager.ActiveSceneName = ActiveScene.name;
        _gameManager.SceneLoadedIndex = ActiveScene.buildIndex;
        _gameManager.SetGameState();
    }
    
    // Events
    public void OnLoadSceneEvent(string sceneName, int canvasIndex)
    {
        OnSceneTransitionEvent?.Invoke(sceneName, canvasIndex);
    }
}
