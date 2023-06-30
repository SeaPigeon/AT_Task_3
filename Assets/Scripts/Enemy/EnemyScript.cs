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

public class EnemyScript : MonoBehaviour
{
    [Header("Enemy Type")]
    [SerializeField] EnemyType _enemyType;
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
                //_navMeshAgent.stoppingDistance = _ATTACK_RANGE_BASE;
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
    }
    private void Fire()
    {
        if (Time.time >= _lastAttackTime + _attackDelay)
        {
            GameObject instance;
            Vector3 targetPos;
            Vector3 moveDirection;

            _navMeshAgent.SetDestination(transform.position);
            _lastAttackTime = Time.time;

            targetPos = _player.transform.position;
            moveDirection = (targetPos - _firePoint.transform.position).normalized;
            instance = Instantiate(_bullet, _firePoint.transform.position, _player.transform.rotation);
            instance.GetComponent<EnemyBullet>().MoveDirection = moveDirection;
            Debug.Log("Enemy pew pew");
        }
    }
    private void Chase()
    {
        _navMeshAgent.SetDestination(_player.transform.position);
    }
    private void Idol()
    {

    }
    
    private void Behaviour()
    {
        if (PlayerInRange(_ACTIVATION_RANGE))
        {
            if (PlayerInRange(_attackRange))
            {
                Fire();
                return;
            }
            else if (PlayerInRange(_aggroRange))
            {
                Chase();
                return;
            }
            else
            {
                Idol();
                return;
            }
        }
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
