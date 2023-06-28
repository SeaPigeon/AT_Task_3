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
    [SerializeField] GameObject _activeWeapon;
    [SerializeField] GameObject[] _weaponList;
    [SerializeField] SpriteRenderer _weaponSprite;
    [SerializeField] bool _hasRedKeycard;
    [SerializeField] bool _hasBlueKeycard;
    [SerializeField] bool _hasYellowKeycard;

    private int _activeWeaponIndex;
    private float _lastBulletTime;

    [Header("PlayerInput")]
    [SerializeField] CharacterController _playerCC;
    [SerializeField] Vector2 _movementInput;
    [SerializeField] Vector2 _rotateInput;
    [SerializeField] bool _fireInput;
    [SerializeField] bool _RSInput;
    [SerializeField] bool _LSInput;
    [SerializeField] bool _westButtonInput;
    private Vector3 _moveVector;
    private Vector3 _appliedMoveVector;
    

    [Header("Debug")]
    private static PlayerScript _playerInstance;
    [SerializeField] GameManagerScript _gameManager;
    [SerializeField] SceneManagerScript _sceneManager;
    [SerializeField] InputManagerScript _inputManager;
    [SerializeField] UIManagerScript _UIManager;
    [SerializeField] SpriteRenderer _playerSprite;
    //[SerializeField] Transform _spawnPoint;
    [SerializeField] bool _isStrafing;

    // G&S
    public static PlayerScript PlayerInstance { get { return _playerInstance; } }
    public Vector2 MovementInput { get { return _movementInput; } set { _movementInput = value; } }
    public Vector2 RotateInput { get { return _rotateInput; } set { _rotateInput = value; } }
    public bool FireInput { get { return _fireInput; } set { _fireInput = value; } }
    public float FireDelay { get { return _fireDelay; } set { _fireDelay = value; } }
    public GameObject[] WeaponList { get { return _weaponList; } }
    //public Transform SpawnPoint { get { return _spawnPoint; } set { _spawnPoint = value; } }
    public bool HasRedKeycard { get { return _hasRedKeycard; } set { _hasRedKeycard = value; } }
    public bool HasBlueKeycard { get { return _hasBlueKeycard; } set { _hasBlueKeycard = value; } }
    public bool HasYellowKeycard { get { return _hasYellowKeycard; } set { _hasYellowKeycard = value; } }

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
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _playerCC = gameObject.GetComponent<CharacterController>();
        /*if (FindObjectOfType<SpawnPointScript>())
        {
            _spawnPoint = FindObjectOfType<SpawnPointScript>().transform;
        }
        else
        {
            _spawnPoint.position = Vector3.zero;
            _spawnPoint.rotation = Quaternion.Euler(0, 0, 0);
            _spawnPoint.localScale = Vector3.zero;
        }*/
    }
    private void SubscribeGameInputs()
    {
        _inputManager.InputMap.Game.Move.performed += OnMove;
        _inputManager.InputMap.Game.South.started += OnSouth;
        _inputManager.InputMap.Game.West.started += OnWest;
        //_inputManager.InputMap.Game.North.performed += OnNorth;
        //_inputManager.InputMap.Game.East.performed += OnEast;
        _inputManager.InputMap.Game.SR.started += OnSR;
        _inputManager.InputMap.Game.SL.started += OnSL;
        //_inputManager.InputMap.Game.Start.performed += OnStart;

        _inputManager.InputMap.Game.Move.canceled += OnMove;
        _inputManager.InputMap.Game.South.canceled += OnSouth;
        _inputManager.InputMap.Game.West.canceled += OnWest;
        //_inputManager.InputMap.Game.North.canceled += OnNorth;
        //_inputManager.InputMap.Game.East.canceled += OnEast;
        _inputManager.InputMap.Game.SR.canceled += OnSR;
        _inputManager.InputMap.Game.SL.canceled += OnSL;
        //_inputManager.InputMap.Game.Start.canceled += OnStart;
    }

    private void SetUpPlayer()
    {
        CurrentHealth = _MAX_HEALTH;
        _gameManager.ResetScore();
        _activeWeapon = _weaponList[0].gameObject;
        _activeWeaponIndex = 0;
        _fireDelay = _weaponList[0].GetComponent<BulletScript>().FireDelay;
        SetUpStartingAmmo(10, 0, 0);
        ActivateWeapon(0);
        _isStrafing = false;
        _hasRedKeycard = false;
        _hasBlueKeycard = false;
        _hasYellowKeycard = false;
        if (SceneManager.GetActiveScene().buildIndex == 4 ||
            SceneManager.GetActiveScene().buildIndex == 5 ||
            SceneManager.GetActiveScene().buildIndex == 6)
        {
            Debug.Log(gameObject.transform.position);
            SpawnPlayer();
            Debug.Log("Spawn");
        }
        else
        {
            Debug.Log(gameObject.transform.position);
            TogglePlayerSprite(false);
        }
    }

    public void TogglePlayerSprite(bool state)
    {
        _playerSprite.enabled = state;
        Debug.Log("Player Sprite: " + _playerSprite.enabled);
    }
    public void MoveToSpawnPoint()
    {
        //_spawnPoint = FindObjectOfType<SpawnPointScript>().transform;

        //Debug.Log(_spawnPoint);
        /*
        gameObject.transform.position = new Vector3(_spawnPoint.position.x,
                                                    _spawnPoint.position.y,
                                                    _spawnPoint.position.z);
        gameObject.transform.rotation = new Quaternion(_spawnPoint.rotation.x, 
                                                       _spawnPoint.rotation.y,
                                                       _spawnPoint.rotation.z, 
                                                       _spawnPoint.rotation.w);*/
    
        Debug.Log("MoveStart");
        Debug.Log(gameObject.transform.position);
        gameObject.transform.position = new Vector3(20,0,20);
        gameObject.transform.rotation = new Quaternion(0,0,0,0);
        Debug.Log("MoveEnd");
        Debug.Log(gameObject.transform.position);
    }
    public void SpawnPlayer()
    {
        Debug.Log(gameObject.transform.position);
        Debug.Log("StartSpawn");
        MoveToSpawnPoint();
        TogglePlayerSprite(true);
        Debug.Log("EndSpawn");
    }

    // Gameplay
    private void Move(Vector2 input)
    {
        if (_isStrafing && Mathf.Abs(input.x)> 0.3f && Mathf.Abs(input.y) > 0.3f)
        {
            Strafe(input);
        }
        else
        {
            _moveVector.z = input.y * _moveSpeed;

            _appliedMoveVector = transform.TransformDirection(_moveVector);
            _playerCC.Move(_appliedMoveVector * Time.deltaTime);
            gameObject.transform.Rotate(new Vector3(0, input.x * _rotationSpeed * Time.deltaTime, 0));
        }
        

    } // BUG!!!
    private void Strafe(Vector2 input)
    {
        _moveVector.x = input.x * _moveSpeed;
        _moveVector.z = input.y * _moveSpeed;
        Debug.Log("Strafe)");
        _appliedMoveVector = transform.TransformDirection(_moveVector);
        _playerCC.Move(_appliedMoveVector * Time.deltaTime);
        gameObject.transform.Rotate(new Vector3(0, 0, 0));
    } // BUG!!!
    private void Fire(bool input)
    {
        if (_activeWeapon.GetComponent<BulletScript>().Ammo > 0)
        {
            if (input && Time.time >= _lastBulletTime + _fireDelay)
            {
                _lastBulletTime = Time.time;
                Instantiate(_activeWeapon, _firePoint.transform.position, _firePoint.transform.rotation);
                _activeWeapon.GetComponent<BulletScript>().Ammo -= 1;
                Debug.Log("pew pew");
            }
        }
        else
        {
            AutoSwapWeapon(); 
        }
        
    }
    private void SwapWeapon(int step)
    {
        var newWeaponIndex = _activeWeaponIndex + step;
        if (newWeaponIndex > _weaponList.Length -1)
        {
            newWeaponIndex = 0;
        }
        else if (newWeaponIndex < 0)
        {
            newWeaponIndex = _weaponList.Length -1;
        }
        ActivateWeapon(newWeaponIndex);
    }
    private void AutoSwapWeapon()
    {
        for (int i = 0; i < _weaponList.Length; i++)
        {
            if (_weaponList[i].GetComponent<BulletScript>().Ammo > 0)
            {
                ActivateWeapon(i);
                return;
            }
        }
    }
    public void ActivateWeapon(int index)
    {
        _activeWeapon = _weaponList[index];
        _activeWeaponIndex = index;
        _fireDelay = _weaponList[index].GetComponent<BulletScript>().FireDelay;
        _weaponSprite.sprite = _weaponList[index].GetComponent<BulletScript>().WeaponSprite;
    }
    private void SetUpStartingAmmo(int w1, int w2, int w3)
    {
        _weaponList[0].GetComponent<BulletScript>().Ammo = w1;
        _weaponList[1].GetComponent<BulletScript>().Ammo = w2;
        _weaponList[2].GetComponent<BulletScript>().Ammo = w3;
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
        _westButtonInput = context.ReadValueAsButton();
        _isStrafing = _westButtonInput;
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
        _RSInput = context.ReadValueAsButton();
      
        if (_RSInput)
        {
            SwapWeapon(1);
        }
        
        Debug.Log("ShoulderRPlayer");
    }
    private void OnSL(InputAction.CallbackContext context) 
    {
        _LSInput = context.ReadValueAsButton();
 
        if (_LSInput)
        {
            SwapWeapon(-1);
        }
        
        Debug.Log("ShoulderLPlayer");
    }
    private void OnStart(InputAction.CallbackContext context) 
    {
        Debug.Log("StartPlayer");
    }
}
