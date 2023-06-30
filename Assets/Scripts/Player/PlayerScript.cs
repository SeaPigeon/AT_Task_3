using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

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
    [SerializeField] CinemachineVirtualCamera _gameCam;

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
    //[SerializeField] AudioManagerScript _audioManager;
    [SerializeField] SpriteRenderer _playerSprite;
    [SerializeField] Transform _spawnPoint;
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
    public CinemachineVirtualCamera InGameCamera { get { return _gameCam; } }

    private void Awake() 
    {
        PlayerSingleton();
    }
    private void Start()
    {
        SetUpReferences();
        SetUpEvents();
        ResetPlayer();
    }

    private void Update()
    {
        Move(MovementInput);
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
        _gameManager = GameManagerScript.GMInstance;
        _inputManager = InputManagerScript.IMInstance;
        _sceneManager = SceneManagerScript.SMInstance;
        //_audioManager = AudioManagerScript.AMInstance;
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _playerCC = gameObject.GetComponent<CharacterController>();
    }
    private void SetUpEvents()
    {
        _gameManager.OnGMSetUpComplete += SetUpPlayer;
    }
    public void SubscribeGameInputs()
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
        
        //SubscribeGameInputs();

        _activeWeapon = _weaponList[0].gameObject;
        _activeWeaponIndex = 0;
        _fireDelay = _weaponList[0].GetComponent<BulletScript>().FireDelay;
        ActivateWeapon(0);
        _isStrafing = false;
        _hasRedKeycard = false;
        _hasBlueKeycard = false;
        _hasYellowKeycard = false;

        if (_gameManager.SceneLoadedIndex == 4 ||
            _gameManager.SceneLoadedIndex == 5 ||
            _gameManager.SceneLoadedIndex == 6)
        {

            SpawnPlayer();
            Debug.Log("Player Spawned from GMEvent: " + transform.position);
        }
        else
        {
            TogglePlayerSprite(false);
            Debug.Log("Player Toggled False from GMEvent: " + _playerSprite.enabled);
        }
    }
    public void ResetPlayer() 
    {
        CurrentHealth = _MAX_HEALTH;
        _gameManager.ResetScore();
        SetUpStartingAmmo(10, 0, 0);
    }
    public void TogglePlayerSprite(bool state)
    {
        _playerSprite.enabled = state;
    }
    public void MoveToSpawnPoint()
    {
        _spawnPoint = FindObjectOfType<SpawnPointScript>().transform;

        if (_spawnPoint != null)
        {
            gameObject.transform.position = new Vector3(_spawnPoint.position.x,
                                                        _spawnPoint.position.y,
                                                        _spawnPoint.position.z);
            gameObject.transform.rotation = new Quaternion(_spawnPoint.rotation.x,
                                                           _spawnPoint.rotation.y,
                                                           _spawnPoint.rotation.z,
                                                           _spawnPoint.rotation.w);
        }
        else
        {
            Debug.Log("Spawn Error");
        }        
    }
    public void SpawnPlayer()
    {
        TogglePlayerSprite(true);
        MoveToSpawnPoint();
    }

    // Gameplay
    private void Move(Vector2 input)
    {
        if (_isStrafing && (input.x != 0 || input.y != 0))
        {
            Strafe(input);
        }
        else
        {
            _moveVector = Vector3.zero;
            _appliedMoveVector = Vector3.zero;
            _moveVector.z = input.y * _moveSpeed;

            _appliedMoveVector = transform.TransformDirection(_moveVector);
            _playerCC.Move(_appliedMoveVector * Time.deltaTime);
            gameObject.transform.Rotate(new Vector3(0, input.x * _rotationSpeed * Time.deltaTime, 0));
        }
        

    }
    private void Strafe(Vector2 input)
    {
        _moveVector.x = input.x * _moveSpeed;
        _moveVector.z = input.y * _moveSpeed;
        
        _appliedMoveVector = transform.TransformDirection(_moveVector);
        _playerCC.Move(_appliedMoveVector * Time.deltaTime);
    } 
    private void Fire(bool input)
    {
        if (_activeWeapon.GetComponent<BulletScript>().Ammo > 0)
        {
            if (input && Time.time >= _lastBulletTime + _fireDelay)
            {
                _lastBulletTime = Time.time;
                Instantiate(_activeWeapon, _firePoint.transform.position, _firePoint.transform.rotation);
                _activeWeapon.GetComponent<BulletScript>().Ammo -= 1;
                //_audioManager.PlaySFX(0);
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
        if (_weaponList[index].GetComponent<BulletScript>().Ammo > 0)
        {
            _activeWeapon = _weaponList[index];
            _activeWeaponIndex = index;
            _fireDelay = _weaponList[index].GetComponent<BulletScript>().FireDelay;
            _weaponSprite.sprite = _weaponList[index].GetComponent<BulletScript>().WeaponSprite;
        }
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
            TogglePlayerSprite(false);
            _sceneManager.LoadScene(_sceneToLoadOnDeath);
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
        if (_fireInput)
        {
            Debug.Log("SouthPlayer");
        }        
    }
    private void OnWest(InputAction.CallbackContext context) 
    {
        _westButtonInput = context.ReadValueAsButton();
        _isStrafing = _westButtonInput;
        /*if (_westButtonInput)
        {
            TakeDamage(30);
            Debug.Log("Damage Taken");
        }*/
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyScript>())
        {
            TakeDamage(other.GetComponent<EnemyScript>().MDamage);
        }
        if (other.GetComponent<EnemyBullet>())
        {
            TakeDamage(other.GetComponent<EnemyBullet>().RDamage);
        }
    }
}
