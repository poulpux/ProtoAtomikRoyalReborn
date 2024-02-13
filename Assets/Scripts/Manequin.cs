using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manequin : MonoBehaviour
{
    [SerializeField] private float _pv;
    [SerializeField] private Image _image;
    private ManequinSpawner _manequinSpawner;

    private float stockPV;
    private void Start()
    {
        _manequinSpawner = FindFirstObjectByType<ManequinSpawner>();
        stockPV = _pv;
    }

    private void Update()
    {
        _image.fillAmount = _pv / stockPV;
        Dead();
    }

    public void TakeDamage(int _damage)
    {
        _pv -= _damage;
    }

    private void Dead()
    {
        if (_pv <= 0)
        {
            ManequinSpawner.Instance.StartCoroutine(ManequinSpawner.Instance.CoroutineRespawn());
            Destroy(this.gameObject);
        }
    }
    
}
