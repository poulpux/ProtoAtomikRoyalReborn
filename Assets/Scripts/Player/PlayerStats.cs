using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float _pv;
    [HideInInspector] public float _pvSAVE;
    [SerializeField] private Transform _playerPosSpawn;


    void Start()
    {
        _pvSAVE = _pv;
    }

    void Update()
    {
        if(_pv <= 0f)
        {
            Death();
            _pv = 0f;
        }
    }

    public void Death()
    {
        transform.position = new Vector3(_playerPosSpawn.position.x, _playerPosSpawn.position.y, _playerPosSpawn.position.z);
        _pv = _pvSAVE;
    }
}