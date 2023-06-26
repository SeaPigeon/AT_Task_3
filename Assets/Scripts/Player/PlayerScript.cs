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
    [SerializeField] float _rotationSpeed;
    [SerializeField] float _fireDelay;
    [SerializeField] string _sceneToLoadOnDeath;
    [SerializeField] GameObject _firePoint;
    [SerializeField] GameObject _bullet;
    private float _lastBulletTime;

    [Header("PlayerInput")]
    [SerializeField] CharacterController _playerCC;
    [SerializeField] Vector2 _movementInput;
    [SerializeField] Vector2 _rotateInput;
    [SerializeField] bool _fireInput;
    private Vector3 _moveVector;
    private Vector3 _appliedMoveVector;
    

    [Header("Debug")]
    private static PlayerScript _playerInstance;
    [SerializeField] GameManagerScript _gameManager;
    [SerializeField] SceneManagerScript _sceneManager;
    [SerializeField] InputManagerScript _inputManager;
    [SerializeField] UIManagerScript _UIManager;
    [SerializeField] MeshRenderer _playerMesh;
    [SerializeField] Transform _spawnPoint;

    // G&S
    public Vector2 MovementInput { get { return _movementInput; } set { _movementInput = value; } }
    public Vector2 RotateInput { get { return _rotateInput; } set { _rotateInput = value; } }
    public bool FireInput { get { return _fireInput; } set { _fireInput = value; } }
    public float FireDelay { get { return _fireDelay; } set { _fireDelay = value; } }

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

    private void Update()
    {
        Move(MovementInput);
    }
    private void LateUpdate()
    {
        
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
        _UIManager = FindObjectOfType<UIManagerScript>();
        //_playerMesh = GetComponentInChildren<MeshRenderer>();
        _playerCC = gameObject.GetComponent<CharacterController>();
    }
    private void SubscribeGameInputs()
    {
        //_inputManager.UnsubscribeUIInputs();

        _inputManager.InputMap.Game.Move.performed += OnMove;
        _inputManager.InputMap.Game.South.started += OnSouth;
        _inputManager.InputMap.Game.West.performed += OnWest;
        _inputManager.InputMap.Game.North.performed += OnNorth;
        _inputManager.InputMap.Game.East.performed += OnEast;
        _inputManager.InputMap.Game.SR.performed += OnSR;
        _inputManager.InputMap.Game.SL.performed += OnSL;
        _inputManager.InputMap.Game.Start.performed += OnStart;

        _inputManager.InputMap.Game.Move.canceled += OnMove;
        _inputManager.InputMap.Game.South.canceled += OnSouth;
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
        /*if (_gameManager.ActiveGameState == GameState.InGame)
        {
            SpawnPlayer();
        }
        else
        {
            TogglePlayerMesh(false);
        }*/
    }

    public void TogglePlayerMesh(bool state)
    {
        _playerMesh.enabled = state;
    }
    public void MoveToSpawnPoint()
    {
        //_spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform;
        gameObject.transform.position = new Vector3(30, 
                                                    30,
                                                    30);
       /* gameObject.transform.rotation = new Quaternion(_spawnPoint.rotation.x, 
                                                       _spawnPoint.rotation.y,
                                                       _spawnPoint.rotation.z, 
                                                       _spawnPoint.rotation.w);*/
    }
    public void SpawnPlayer()
    {
        MoveToSpawnPoint();
        TogglePlayerMesh(true);
    }

    // Gameplay
    private void Move(Vector2 input)
    {
        _moveVector.z = input.y * _moveSpeed;

        _appliedMoveVector = transform.TransformDirection(_moveVector);
        _playerCC.Move(_appliedMoveVector * Time.deltaTime);
        gameObject.transform.Rotate(new Vector3(0, input.x * _rotationSpeed * Time.deltaTime, 0));

    }
    private void Fire(bool input)
    {
        if (!input && Time.time >= _lastBulletTime + _fireDelay)
        {
            _lastBulletTime = Time.time;
            Instantiate(_bullet, _firePoint.transform.position, _firePoint.transform.rotation);
            Debug.Log("pew pew");
        }
    }
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            _sceneManager.LoadScene(_sceneToLoadOnDeath);
            _UIManager.LoadCanvas(5);
            _inputManager.ActivateInputMap(_inputManager.InputMap.UI);
        }
    }

    // Inputs
    private void OnMove(InputAction.CallbackContext context) 
    {
        MovementInput = context.ReadValue<Vector2>();
        Debug.Log("MovePlayer");
    }
    private void OnSouth(InputAction.CallbackContext context) 
    {
        _fireInput = context.ReadValueAsButton();
        Fire(_fireInput);
        Debug.Log("SouthPlayer");
    }
    private void OnWest(InputAction.CallbackContext context) 
    {
        _sceneManager.OnLoadScene("MainMenu");
        _UIManager.LoadCanvas(1);
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
        FireInput = context.ReadValue<bool>();
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
