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
    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private int _activeSceneIndex;

    void Awake()
    {
        UIManagerSingleton();    
        SetUpGameManager();
    }
    private void Start()
    {
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
    private void SetUpGameManager() 
    {
        _gameManager = FindObjectOfType<GameManagerScript>();
    }
    private void SetUpStartingCanvas()
    {
        _activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        foreach (GameObject item in _canvasList)
        {
            item.SetActive(false);
        }
        _canvasList[_activeSceneIndex].SetActive(true);
        _activeCanvas = _canvasList[_activeSceneIndex];
        _gameManager.ActiveCanvas = ActiveCanvas;
    }
    private void SetUpEventSystem()
    {
        _eventSystem = gameObject.GetComponentInChildren<EventSystem>();
        _eventSystem.SetSelectedGameObject(ActiveCanvas.GetComponent<RectTransform>().GetChild(0).gameObject);
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
