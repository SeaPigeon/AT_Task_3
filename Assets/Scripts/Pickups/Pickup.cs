using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum PickUpType
    {
        Health,
        Ammo,
        Weapon,
        Keycard
    }

    public enum Weapons
    {
        BaseWeapon,
        Weapon1,
        Weapon2,
    }

    public enum KeycardColour
    {
        Red,
        Blue,
        Yellow
    }
    
    [Header("PickUp Variables")]
    [SerializeField] private int _healthRestored;
    [SerializeField] private int _ammoRestored;
    [SerializeField] private PickUpType _pickUpType;
    [SerializeField] private Weapons _weaponToReload;
    [SerializeField] private KeycardColour _keyCardColour;

    private void RestoreHealth(PlayerScript player)
    {
        player.TakeDamage(-_healthRestored);
    }
    private void Reload(Collider other)
    {
        switch (_weaponToReload)
        {
            case Weapons.BaseWeapon:
                other.GetComponent<PlayerScript>().WeaponList[0].GetComponent<BulletScript>().Reload(_ammoRestored);
                Debug.Log("BaseWeapon Reloaded");
                break;
            case Weapons.Weapon1:
                other.GetComponent<PlayerScript>().WeaponList[1].GetComponent<BulletScript>().Reload(_ammoRestored);
                Debug.Log("Weapon1 Reloaded");
                break;
            case Weapons.Weapon2:
                other.GetComponent<PlayerScript>().WeaponList[2].GetComponent<BulletScript>().Reload(_ammoRestored);
                Debug.Log("Weapon2 Reloaded");
                break;
            default:
                other.GetComponent<PlayerScript>().WeaponList[0].GetComponent<BulletScript>().Reload(_ammoRestored);
                Debug.Log("Default: BaseWeapon Reloaded");
                break;
        }
    }
    private void ActivateWeapon(Collider other)
    {
        switch (_weaponToReload)
        {
            case Weapons.BaseWeapon:
                other.GetComponent<PlayerScript>().ActivateWeapon(0);
                break;
            case Weapons.Weapon1:
                other.GetComponent<PlayerScript>().ActivateWeapon(1);
                break;
            case Weapons.Weapon2:
                other.GetComponent<PlayerScript>().ActivateWeapon(2);
                break;
            default:
                other.GetComponent<PlayerScript>().ActivateWeapon(0);
                break;
        }
        
    }
    private void CollectKeyCard(Collider other)
    {
        switch (_keyCardColour)
        {
            case KeycardColour.Red:
                other.GetComponent<PlayerScript>().HasRedKeycard = true;
                break;
            case KeycardColour.Blue:
                other.GetComponent<PlayerScript>().HasBlueKeycard = true;
                break;
            case KeycardColour.Yellow:
                other.GetComponent<PlayerScript>().HasYellowKeycard = true;
                break;
            default:
                Debug.Log("Error Colour KeyCards");
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerScript>())
        {
            switch (_pickUpType)
            {
                case PickUpType.Health:
                    RestoreHealth(other.GetComponent<PlayerScript>());
                    break;

                case PickUpType.Weapon:
                    Reload(other);
                    ActivateWeapon(other);
                    break;

                case PickUpType.Ammo:
                    Reload(other);
                    break;

                case PickUpType.Keycard:
                    CollectKeyCard(other);
                    break;

                default:
                    RestoreHealth(other.GetComponent<PlayerScript>());
                    Debug.Log("PickUp Error");
                    break;
            }

            Destroy(gameObject);
        }
    }
}
