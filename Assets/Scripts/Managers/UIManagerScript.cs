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

        LoadCanvas(SceneManager.GetActiveScene().buildIndex); 
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
