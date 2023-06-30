using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;



public enum EnemyType
{
    BaseEnemy,
    StrongEnemy,
    Boss
}
public enum CurrentState
{
    Inactive,
    Idle,
    Chase,
    Shoot
}

public class EnemyScript : MonoBehaviour
{
    [Header("Enemy Type")]
    [SerializeField] EnemyType _enemyType;
    [SerializeField] CurrentState _currentState;
    [SerializeField] float _ACTIVATION_RANGE = 50;
    [Header("BaseEnemy")]
    [SerializeField] int _MAX_HEALTH_BASE = 40;
    [SerializeField] int _POINT_VALUE_BASE = 100;
    [SerializeField] int _M_DAMAGE_BASE = 2;
    [SerializeField] int _R_DAMAGE_BASE = 5;
    [SerializeField] GameObject _BULLET_BASE;
    [SerializeField] float _BULLET_LIFESPAN_BASE = 2;
    [SerializeField] float _BULLET_SPEED_BASE = 10;
    [SerializeField] float _ATTACK_DELAY_BASE = 2;
    [SerializeField] float _AGGRO_RANGE_BASE = 20;
    [SerializeField] float _ATTACK_RANGE_BASE = 10;
    [Header("StrongEnemy")]
    [SerializeField] int _MAX_HEALTH_STORNG = 70;
    [SerializeField] int _POINT_VALUE_STRONG = 200;
    [SerializeField] int _M_DAMAGE_STRONG = 4;
    [SerializeField] int _R_DAMAGE_STRONG = 10;
    [SerializeField] GameObject _BULLET_STRONG;
    [SerializeField] float _BULLET_LIFESPAN_STRONG = 2;
    [SerializeField] float _BULLET_SPEED_STRONG = 15;
    [SerializeField] float _ATTACK_DELAY_STRONG = 1.25f;
    [SerializeField] float _AGGRO_RANGE_STRONG = 30;
    [SerializeField] float _ATTACK_RANGE_STRONG = 15;
    [Header("Boss")]
    [SerializeField] int _MAX_HEALTH_BOSS = 250;
    [SerializeField] int _POINT_VALUE_BOSS = 1000;
    [SerializeField] int _M_DAMAGE_BOSS = 10;
    [SerializeField] int _R_DAMAGE_BOSS = 20;
    [SerializeField] GameObject _BULLET_BOSS;
    [SerializeField] float _BULLET_LIFESPAN_BOSS = 2.5f;
    [SerializeField] float _BULLET_SPEED_BOSS = 20;
    [SerializeField] float _ATTACK_DELAY_BOSS = 0.75f;
    [SerializeField] float _AGGRO_RANGE_BOSS = 40;
    [SerializeField] float _ATTACK_RANGE_BOSS = 20;
    [Header("Debug")]
    [SerializeField] GameManagerScript _gameManager;
    [SerializeField] SceneManagerScript _sceneManager;
    [SerializeField] PlayerScript _player;
    [SerializeField] GameObject _firePoint;
    [SerializeField] int _currentHealth;
    [SerializeField] int _mDamage;
    [SerializeField] int _pointValue;
    [SerializeField] float _attackDelay;
    [SerializeField] float _lastAttackTime;
    [SerializeField] GameObject _bullet;
    [SerializeField] float _attackRange;
    [SerializeField] float _aggroRange;
    [SerializeField] float _distanceToPlayer;
    [SerializeField] NavMeshAgent _navMeshAgent;
    [SerializeField] bool _isPatrolling;
    [SerializeField] Vector3 _patrolPoint;

    void Start()
    {
        SetUpReferences();
        SetUpEnemy();
    }

    void Update()
    {
        Behaviour();
    }

    // G&S
    public int MDamage { get { return _mDamage; } }
    private void SetUpReferences()
    {
        _gameManager = GameManagerScript.GMInstance;
        _sceneManager = SceneManagerScript.SMInstance;
        _player = PlayerScript.PlayerInstance;
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    
    private void SetUpEnemy()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _isPatrolling = false;

        switch (_enemyType)
        {
            case EnemyType.BaseEnemy:
                _currentHealth = _MAX_HEALTH_BASE;
                _mDamage = _M_DAMAGE_BASE;
                _pointValue = _POINT_VALUE_BASE;
                _attackDelay = _ATTACK_DELAY_BASE;
                _bullet = _BULLET_BASE;
                _bullet.GetComponent<EnemyBullet>().RDamage = _R_DAMAGE_BASE;
                _bullet.GetComponent<EnemyBullet>().BulletLifeSpan = _BULLET_LIFESPAN_BASE;
                _bullet.GetComponent<EnemyBullet>().BulletSpeed = _BULLET_SPEED_BASE;
                _aggroRange = _AGGRO_RANGE_BASE;
                _attackRange = _ATTACK_RANGE_BASE;
                break;
            case EnemyType.StrongEnemy:
                _currentHealth = _MAX_HEALTH_STORNG;
                _mDamage = _M_DAMAGE_STRONG;
                _pointValue = _POINT_VALUE_STRONG;
                _attackDelay = _ATTACK_DELAY_STRONG;
                _bullet = _BULLET_STRONG;
                _bullet.GetComponent<EnemyBullet>().RDamage = _R_DAMAGE_STRONG;
                _bullet.GetComponent<EnemyBullet>().BulletLifeSpan = _BULLET_LIFESPAN_STRONG;
                _bullet.GetComponent<EnemyBullet>().BulletSpeed = _BULLET_SPEED_STRONG;
                _aggroRange = _AGGRO_RANGE_STRONG;
                _attackRange = _ATTACK_RANGE_STRONG;
                break;
            case EnemyType.Boss:
                _currentHealth = _MAX_HEALTH_BOSS;
                _mDamage = _M_DAMAGE_BOSS;
                _pointValue = _POINT_VALUE_BOSS;
                _attackDelay = _ATTACK_DELAY_BOSS;
                _bullet = _BULLET_BOSS;
                _bullet.GetComponent<EnemyBullet>().RDamage = _R_DAMAGE_BOSS;
                _bullet.GetComponent<EnemyBullet>().BulletLifeSpan = _BULLET_LIFESPAN_BOSS;
                _bullet.GetComponent<EnemyBullet>().BulletSpeed = _BULLET_SPEED_BOSS;
                _aggroRange = _AGGRO_RANGE_BOSS;
                _attackRange = _ATTACK_RANGE_BOSS;
                break;
            default:
                _mDamage = _M_DAMAGE_BASE;
                _pointValue = _POINT_VALUE_BASE;
                _currentHealth = _MAX_HEALTH_BASE;
                _attackDelay = _ATTACK_DELAY_BASE;
                _bullet = _BULLET_BASE;
                _bullet.GetComponent<EnemyBullet>().RDamage = _R_DAMAGE_BASE;
                _bullet.GetComponent<EnemyBullet>().BulletLifeSpan = _BULLET_LIFESPAN_BASE;
                _bullet.GetComponent<EnemyBullet>().BulletSpeed = _BULLET_SPEED_BASE;
                _aggroRange = _AGGRO_RANGE_BASE;
                _attackRange = _ATTACK_RANGE_BASE;
                break;
        }
        if (PlayerInRange(_ACTIVATION_RANGE))
        {
            _currentState = CurrentState.Idle;
        }
    }
    private void Fire()
    {
        if (Time.time >= _lastAttackTime + _attackDelay && LoS())
        {
            GameObject instance;

            _navMeshAgent.SetDestination(transform.position);
            _lastAttackTime = Time.time;
            
            instance = Instantiate(_bullet, _firePoint.transform.position, _player.transform.rotation);
            instance.GetComponent<EnemyBullet>().MoveDirection = PlayerDirection();
            Debug.Log("Enemy pew pew");
        }
    }
    private void Chase()
    {
        _navMeshAgent.isStopped = false;
        _navMeshAgent.SetDestination(_player.transform.position);
    }
    private void Idle()
    {
        if (!_isPatrolling)
        {
            _patrolPoint = GenerateRandomPoint(transform.position, 2);
            _isPatrolling = true;
            StartCoroutine(WaitAfterPatrol(5f));
            Debug.Log(_patrolPoint);
        }
        _navMeshAgent.isStopped = false;
        _navMeshAgent.SetDestination(_patrolPoint);

        if (Vector3.Distance(transform.position, _patrolPoint) <= _navMeshAgent.stoppingDistance)
        {
            _navMeshAgent.isStopped = true;
        }
    }
    private Vector3 GenerateRandomPoint(Vector3 position, int range)
    {
        Vector3 randomPoint = position + Random.insideUnitSphere * range;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPoint, out hit, range, NavMesh.AllAreas);
        return hit.position;
    }
    private IEnumerator WaitAfterPatrol(float time)
    {
        Debug.Log("started CR");
        yield return new WaitForSeconds(time);
        _isPatrolling = false;
    }
    private void Behaviour()
    {
        if (PlayerInRange(_ACTIVATION_RANGE))
        {
            if (PlayerInRange(_attackRange))
            {
                if (LoS())
                {
                    _currentState = CurrentState.Shoot;
                }
                else
                {
                    _currentState = CurrentState.Chase;
                }
                
            }
            else if (PlayerInRange(_aggroRange))
            {
                _currentState = CurrentState.Chase;
            }
            else
            {
                _currentState = CurrentState.Idle;
            }
        }
        else
        {
            _currentState = CurrentState.Inactive;
        }

        switch (_currentState)
        {
            case CurrentState.Idle:
                Idle();
                //Debug.Log("Idle Enemy");
                break;
            case CurrentState.Chase:
                Chase();
                //Debug.Log("Chase Enemy");
                break;
            case CurrentState.Shoot:
                Fire();
                //Debug.Log("Fire Enemy");
                break;
            case CurrentState.Inactive:
                _navMeshAgent.isStopped = true;
                break;
            default:
                _navMeshAgent.isStopped = true;
                //Debug.Log("Inactive Enemy ERROR");
                break;
        }
    }
    private Vector3 PlayerDirection()
    {
        Vector3 targetPos;
        Vector3 moveDirection;

        targetPos = _player.transform.position;
        moveDirection = (targetPos - _firePoint.transform.position).normalized;
        return moveDirection;
    }
    private bool PlayerInRange(float range) 
    {
        _distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);

        if (_distanceToPlayer <= range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool LoS()
    {
        Ray ray = new Ray(transform.position, PlayerDirection());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _attackRange))
        {
            if (hit.collider.CompareTag("Wall") || 
                (hit.collider.CompareTag("Door") && 
                hit.collider.GetComponent<DoorScript>().CurrentDoorState == DoorScript.DoorState.Closed))
            {
                Debug.Log(hit);
                return false;
            }
        }
        return true;
    }
    public void TakeDamage(int dmg)
    {
        _currentHealth -= dmg;

        if (_currentHealth <= 0)
        {
            _gameManager.ChangeScore(_pointValue);

            if (_enemyType != EnemyType.Boss)
            {
                Destroy(gameObject);
            }
            else
            {
                _sceneManager.LoadScene("EndGameScene");
            } 
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BulletScript>())
        {
            TakeDamage(other.GetComponent<BulletScript>().BulletDamage);
        }
    }
}
