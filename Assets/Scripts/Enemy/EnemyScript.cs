using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] int _MAX_HEALTH = 100;
    [SerializeField] int _currentHealth;
    [SerializeField] int _damage;
    [SerializeField] int _score;
    [SerializeField] GameManagerScript _gameManager;

    void Start()
    {
        SetUpReferences();
        ResetEnemy();
    }

    void Update()
    {
        
    }

    private void SetUpReferences()
    {
        _gameManager = FindObjectOfType<GameManagerScript>();
    }
    
    public void ResetEnemy()
    {
        _currentHealth = _MAX_HEALTH;
    }
    public void TakeDamage(int dmg)
    {
        _currentHealth -= dmg;
        if (_currentHealth <= 0)
        {
            _gameManager.ChangeScore(_score);
            Destroy(gameObject);
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
