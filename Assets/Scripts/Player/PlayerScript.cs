using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Variables")]
    [SerializeField] static int _MAX_HEALTH = 100;
    [SerializeField] int _currentHealth;
    [SerializeField] float _moveSpeed;
    [SerializeField] float _runSpeed;
    [SerializeField] float _fireDelay;
    [SerializeField] string _sceneToLoadOnDeath;

    [Header("Debug")]
    private static PlayerScript _playerInstance;
    [SerializeField] GameManagerScript _gameManager;
    [SerializeField] SceneManagerScript _sceneManager;
    [SerializeField] InputManagerScript _inputManager;
    private void Awake() 
    {
        PlayerSingleton();
    }
    private void Start()
    {
        SetUpReferences();
        SetUpPlayer();
        SubscribeGameInputs();
    }

    // G&S
    public int CurrentHealth { get { return _currentHealth; } set { _currentHealth = value; } }

    // Methods
    private void PlayerSingleton()
    {
        if (_playerInstance == null)
        {
            _playerInstance = this;
        }
        else if (_playerInstance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    private void SetUpReferences()
    {
        _gameManager = FindObjectOfType<GameManagerScript>();
        _inputManager = FindObjectOfType<InputManagerScript>();
        _sceneManager = FindObjectOfType<SceneManagerScript>();
    }
    private void SubscribeGameInputs()
    {
        _inputManager.InputMap.Game.Move.performed += OnMove;
        _inputManager.InputMap.Game.South.performed += OnSouth;
        _inputManager.InputMap.Game.West.performed += OnWest;
        _inputManager.InputMap.Game.North.performed += OnNorth;
        _inputManager.InputMap.Game.East.performed += OnEast;
        _inputManager.InputMap.Game.SR.performed += OnSR;
        _inputManager.InputMap.Game.SL.performed += OnSL;
        _inputManager.InputMap.Game.Start.performed += OnStart;

        //_inputManager.InputMap.Game.Move.canceled += OnMove;
        //_inputManager.InputMap.Game.South.canceled += OnSouth;
        //_inputManager.InputMap.Game.West.canceled += OnWest;
        //_inputManager.InputMap.Game.North.canceled += OnNorth;
        //_inputManager.InputMap.Game.East.canceled += OnEast;
        //_inputManager.InputMap.Game.SR.canceled += OnSR;
        //_inputManager.InputMap.Game.SL.canceled += OnSL;
        //_inputManager.InputMap.Game.Start.canceled += OnStart;
    }

    private void SetUpPlayer()
    {
        CurrentHealth = _MAX_HEALTH;
        _gameManager.ResetScore();
        /*if(_gameManager.ActiveGameState != GameState.InGame)
        {
            TogglePlayer(false);
        }*/
    }
    /*private void TogglePlayer(bool state)
    {
        gameObject.SetActive(state);
    }*/

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        
        if (CurrentHealth <= 0)
        {
            _sceneManager.LoadScene(_sceneToLoadOnDeath, 0);
        }
    }
 
    private void OnMove(InputAction.CallbackContext context) 
    {
        Debug.Log("MovePlayer");
    }
    private void OnSouth(InputAction.CallbackContext context) 
    {
        Debug.Log("SouthPlayer");
        _sceneManager.LoadScene("MainMenu", 1);
        _inputManager.ActivateInputMap(_inputManager.InputMap.UI);
    }
    private void OnWest(InputAction.CallbackContext context) 
    {
        Debug.Log("WestPlayer");
    }
    private void OnNorth(InputAction.CallbackContext context) 
    {
        Debug.Log("NorthPlayer");
    }
    private void OnEast(InputAction.CallbackContext context) 
    {
        Debug.Log("EastPlayer");
    }
    private void OnSR(InputAction.CallbackContext context) 
    {
        Debug.Log("ShoulderRPlayer");
    }
    private void OnSL(InputAction.CallbackContext context) 
    {
        Debug.Log("ShoulderLPlayer");
    }
    private void OnStart(InputAction.CallbackContext context) 
    {
        Debug.Log("StartPlayer");
    }
}
