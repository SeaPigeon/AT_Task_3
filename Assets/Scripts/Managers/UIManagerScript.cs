using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIManagerScript : MonoBehaviour
{
    [SerializeField] GameObject[] _canvasList;
    [SerializeField] GameObject _activeCanvas;

    private static UIManagerScript _UIManagerInstance = null;
    [SerializeField] private GameManagerScript _gameManager;
    //[SerializeField] private SceneManagerScript _sceneManager;
    [SerializeField] private EventSystem _eventSystem;

    void Awake()
    {
        UIManagerSingleton();    
    }
    private void Start()
    {
        SetUpReferences();
        //SubscribeToEvents();
        SetUpStartingCanvas();
        SetUpEventSystem();
    }

    // Getter & Setters
    public GameObject ActiveCanvas { get { return _activeCanvas; } set { _activeCanvas = value; } }
   
   // Methods
    private void UIManagerSingleton()
    {
        if (_UIManagerInstance == null)
        {
            _UIManagerInstance = this;
        }
        else if (_UIManagerInstance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }   
    private void SetUpReferences() 
    {
        _gameManager = FindObjectOfType<GameManagerScript>();
        //_sceneManager = FindObjectOfType<SceneManagerScript>();
        //_gameManager.GameManagerDebugLog();
    }
    
    
    
    /*
    private void SubscribeToEvents()
    {
        SceneManager.sceneLoaded += SetActiveCanvas;
    }*/


    private void SetUpStartingCanvas()
    {





        /*if (_sceneManager.ActiveScene.buildIndex == 0)
        {
            LoadCanvas(0);
        }
        else if (_sceneManager.ActiveScene.buildIndex == 1)
        {
            LoadCanvas(1);
        }
        else if (_sceneManager.ActiveScene.name == "ControlsMenu")
        {
            LoadCanvas(2);
        }
        else if (_sceneManager.ActiveScene.buildIndex == 3)
        {
            LoadCanvas(3);
        }
        else if (_sceneManager.ActiveScene.buildIndex == 4)
        {
            LoadCanvas(4);
        }*/



        foreach (GameObject item in _canvasList)
        {
            item.SetActive(false);
        }

        _canvasList[_gameManager.SceneLoadedIndex].SetActive(true);
        ActiveCanvas = _canvasList[_gameManager.SceneLoadedIndex];
        _gameManager.ActiveCanvas = ActiveCanvas;
        
    }
    private void SetUpEventSystem()
    {
        _eventSystem = gameObject.GetComponentInChildren<EventSystem>();
        if(_gameManager.ActiveGameState != GameState.InGame)
        {
            _eventSystem.SetSelectedGameObject(ActiveCanvas.GetComponent<RectTransform>().GetChild(0).gameObject);
        }
        
    }   
    public void LoadCanvas(int canvasIndex)
    {
        _activeCanvas.SetActive(false);
        _canvasList[canvasIndex].SetActive(true);
        _activeCanvas = _canvasList[canvasIndex];
        _gameManager.ActiveCanvas = ActiveCanvas;
        //var activeButton = _activeCanvas.GetComponent<RectTransform>().GetChild(0);
        //_eventSystem.SetSelectedGameObject(activeButton.gameObject);
        if (_gameManager.ActiveGameState != GameState.InGame)
        {
            var activeButton = _activeCanvas.GetComponent<RectTransform>().GetChild(0);
            _eventSystem.SetSelectedGameObject(activeButton.gameObject);
        }
        else
        {
            _eventSystem.SetSelectedGameObject(null);
        } 
    }




    /*
    void SetActiveCanvas(Scene scene, LoadSceneMode mode)
    {
        foreach (GameObject item in _canvasList)
        {
            item.SetActive(false);
        }
        _canvasList[_gameManager.SceneLoadedIndex].SetActive(true);
        ActiveCanvas = _canvasList[_gameManager.SceneLoadedIndex];
        _gameManager.ActiveCanvas = ActiveCanvas;
    }*/





}
