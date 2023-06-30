using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [Header("Bullet Variables")]
    [SerializeField] float _bulletLifeSpan;
    [SerializeField] float _bulletSpeed;
    [SerializeField] int _bulletDamage;
    [SerializeField] int _currentAmmo;
    [SerializeField] float _fireDelay;
    [SerializeField] Sprite _weaponSprite;

    [Header("Debug")]
    [SerializeField] Rigidbody _bulletRB;
    [SerializeField] AudioManagerScript _audioManager;

    public int BulletDamage { get { return _bulletDamage; } set { _bulletDamage = value; } }
    public int Ammo { get { return _currentAmmo; } set {_currentAmmo = value; } }
    public float FireDelay { get { return _fireDelay; } set { _fireDelay = value; } }
    public Sprite WeaponSprite { get { return _weaponSprite; } }

    private void Awake()
    {
        _bulletRB = gameObject.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _audioManager = AudioManagerScript.AMInstance;
        StartCoroutine(DestroyBullet());
        MoveBullet();
        _audioManager.PlaySFX(0);
    }

    private void MoveBullet()
    {
        _bulletRB.velocity = transform.forward * _bulletSpeed;
    }

    public void Reload(int ammo)
    {
        _currentAmmo += ammo;
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
