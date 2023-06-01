using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManagerScript : MonoBehaviour
{
    [SerializeField] GameObject[] _canvasList;
    [SerializeField] GameObject _activeCanvas;

    private static UIManagerScript _UIManagerInstance = null;
    private GameManagerScript _gameManager;
    private EventSystem _eventSystem;

    void Awake()
    {
        UIManagerSingleton();    
    }
    private void Start()
    {
        SetUpGameManager();
        SetUpStartingCanvas();
        SetUpEventSystem();
    }

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

    public GameObject ActiveCanvas { get { return _activeCanvas; } set { _activeCanvas = value; } }

    private void SetUpStartingCanvas()
    {
        foreach (GameObject item in _canvasList)
        {
            item.SetActive(false);
        }
        Debug.Log(_gameManager.ActiveScene.buildIndex);
        _canvasList[_gameManager.ActiveScene.buildIndex].SetActive(true);
        
        _activeCanvas = _canvasList[_gameManager.ActiveScene.buildIndex];
        _gameManager.ActiveCanvas = ActiveCanvas;
        //_canvasList[1].SetActive(true);
        //_activeCanvas = _canvasList[1];
    }
    private void SetUpEventSystem()
    {
        _eventSystem = gameObject.GetComponentInChildren<EventSystem>();
        _eventSystem.SetSelectedGameObject(ActiveCanvas.GetComponent<RectTransform>().GetChild(0).gameObject);
    }
    private void SetUpGameManager() 
    {
        _gameManager = FindObjectOfType<GameManagerScript>();
    }
    public void LoadCanvas(int canvasIndex)
    {
        _activeCanvas.SetActive(false);
        _canvasList[canvasIndex].SetActive(true);
        _activeCanvas = _canvasList[canvasIndex];
        _gameManager.ActiveCanvas = ActiveCanvas;

        var activeButton = _activeCanvas.GetComponent<RectTransform>().GetChild(0);
        
        _eventSystem.SetSelectedGameObject(activeButton.gameObject);
    }
}
