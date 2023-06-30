using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public enum DoorType
    {
        NormalDoor,
        KeyCardDoor
    }
    public enum DoorColour 
    {
        Red,
        Blue,
        Yellow
    }
    public enum DoorState
    {
        Open,
        Closed
    }

    [Header("Door Variables")]
    [SerializeField] DoorType _doorType;
    [SerializeField] DoorColour _doorColour;
    [SerializeField] DoorState _currentDoorState;
    [SerializeField] List<Collider> _objectsInTrigger;

    void Start()
    {
        ToggleDoor();
    }
    private void ToggleDoor()
    {
        if (_currentDoorState == DoorState.Closed)
        {
            var door = gameObject.transform.GetChild(0).transform;
            door.position = new Vector3(door.position.x, door.localScale.y / 2, door.position.z);
        }
        else if (_currentDoorState == DoorState.Open)
        {
            var door = gameObject.transform.GetChild(0).transform;
            door.position = new Vector3(door.position.x, -door.localScale.y / 2, door.position.z);
        }
        
        
    }
    
    private void OpenKeyCardDoor(Collider other)
    {
        switch (_doorColour)
        {
            case DoorColour.Red:
                if (other.GetComponent<PlayerScript>().HasRedKeycard)
                {
                    _currentDoorState = DoorState.Open;
                    ToggleDoor();
                }
                break;

            case DoorColour.Blue:
                if (other.GetComponent<PlayerScript>().HasBlueKeycard)
                {
                    _currentDoorState = DoorState.Open;
                    ToggleDoor();
                }
                break;

            case DoorColour.Yellow:
                if (other.GetComponent<PlayerScript>().HasYellowKeycard)
                {
                    _currentDoorState = DoorState.Open;
                    ToggleDoor();
                }
                break;

            default:
                Debug.Log("KeyCard Door Error");
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerScript>() || other.GetComponent<EnemyScript>())
        {
            _objectsInTrigger.Add(other);

            switch (_doorType)
            {
                case DoorType.NormalDoor:
                    _currentDoorState = DoorState.Open;
                    ToggleDoor();
                    break;

                case DoorType.KeyCardDoor:
                    OpenKeyCardDoor(other);
                    break;

                default:
                    Debug.Log("Door Error");
                    break;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerScript>() || other.GetComponent<EnemyScript>())
        {
            _objectsInTrigger.Remove(other);
            if (_objectsInTrigger.Count == 0)
            {
                _currentDoorState = DoorState.Closed;
                ToggleDoor();
            }
            
        }
    }
}
