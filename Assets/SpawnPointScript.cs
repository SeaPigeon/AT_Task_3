using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointScript : MonoBehaviour
{
    [SerializeField] private Vector3 _spawnPoint;
    [SerializeField] private PlayerScript _player;

    public Vector3 SpawnPoint { get { return _spawnPoint; } }

    private void Start()
    {
        /*_player = FindObjectOfType<PlayerScript>();
        _player.SpawnPoint = gameObject.transform;
        _player.SpawnPlayer();*/
        /*
        if (PlayerScript.PlayerInstance)
        {
            _player = PlayerScript.PlayerInstance;
            //_player.SpawnPoint = gameObject.transform;
            _player.SpawnPlayer();
        }
        else
        {
            _player = FindObjectOfType<PlayerScript>();
            //_player.SpawnPoint = gameObject.transform;
            _player.SpawnPlayer();
        
        */
    }
}
