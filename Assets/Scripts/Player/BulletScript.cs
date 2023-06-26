using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [Header("Bullet Variables")]
    [SerializeField] float _bulletLifeSpan;
    [SerializeField] float _bulletSpeed;
    [SerializeField] int _bulletDamage;

    [Header("Debug")]
    [SerializeField] Rigidbody _bulletRB;

    public int BulletDamage { get { return _bulletDamage; } set { _bulletDamage = value; } }
    private void Awake()
    {
        _bulletRB = gameObject.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        StartCoroutine(DestroyBullet());
        MoveBullet();
    }

    private void MoveBullet()
    {
        _bulletRB.velocity = transform.forward * _bulletSpeed;
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSecondsRealtime(_bulletLifeSpan);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall") || other.GetComponent<EnemyScript>())
        {
            Destroy(gameObject);
        }
    }
   
}
