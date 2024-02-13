using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{ 
    public float _pv;
    [HideInInspector] public float _pvSAVE;
    [SerializeField] private UIScript _uiscript;

    void Start()
    {
        _pvSAVE = _pv;
    }

    public void TakeDamage(float _damage)
    {
        _pv -= _damage;
        if(_pv <= 0)
        {
            _pv = 0;
        }
        _uiscript.UpdateViewHealthBarGreen();
    }
}