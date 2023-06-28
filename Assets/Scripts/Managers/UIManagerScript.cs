using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIManagerScript : MonoBehaviour
{
    [Header("UI Variables")]
    [SerializeField] GameObject[] _canvasList;
    
    [Header("Debug")]
    [SerializeField] GameObject _activeCanvas;
    [SerializeField] private GameManagerScript _gameManager;
    [SerializeField] private EventSystem _eventSystem;

    private static UIManagerScript _UIManagerInstance = null;

    void Awake()
    {
        UIManagerSingleton();    
    }
    private void Start()
    {
        SetUpReferences();
        SetUpStartingCanvas();
        SetUpEventSystem();
    }

    // Getter & Setters
    public static UIManagerScript UIMInstance { get { return _UIManagerInstance; } }
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
        _eventSystem = gameObject.GetComponentInChildren<EventSystem>();
    }
    
    public void SetUpStartingCanvas()
    {
        foreach (GameObject item in _canvasList)
        {
            item.SetActive(false);
        }

        //LoadCanvas(SceneManager.GetActiveScene().buildIndex);
        if (SceneManager.GetActiveScene().name == "DebugScene")
        {
            LoadCanvas(0);
        }
        else if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            LoadCanvas(1);
        }
        else if (SceneManager.GetActiveScene().name == "ControlsMenu")
        {
            LoadCanvas(2);
        }
        else if (SceneManager.GetActiveScene().name == "LevelEditor")
        {
            LoadCanvas(3);
        }
        else if (SceneManager.GetActiveScene().name == "Level_1" ||
                SceneManager.GetActiveScene().name == "Level_2" || 
                SceneManager.GetActiveScene().name == "Level_3")
        {
            LoadCanvas(4);
        }
    }
    public void SetUpEventSystem()
    {
        _eventSystem.SetSelectedGameObject(ActiveCanvas.GetComponent<RectTransform>().GetChild(0).gameObject);
    }   

    // Public Methods
    public void LoadCanvas(int canvasIndex)
    {
        if (ActiveCanvas != null) 
        {
            _activeCanvas.SetActive(false);
        }
        _canvasList[canvasIndex].SetActive(true);
        ActiveCanvas = _canvasList[canvasIndex];
        _gameManager.ActiveCanvas = ActiveCanvas;

        _eventSystem.SetSelectedGameObject(ActiveCanvas.GetComponent<RectTransform>().GetChild(0).gameObject);
        
    }

}
