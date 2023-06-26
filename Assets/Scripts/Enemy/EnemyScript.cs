using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] int _MAX_HEALTH = 100;
    [SerializeField] int _currentHealth;
    [SerializeField] int _damage;

    void Start()
    {
        ResetEnemy();
    }

    void Update()
    {
        
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
