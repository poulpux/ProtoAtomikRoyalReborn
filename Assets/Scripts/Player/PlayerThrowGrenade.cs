using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThr : MonoBehaviour
{
    [SerializeField] private Camera _Camera;
    [SerializeField] private Grenade _grenadePrefab;
    [SerializeField] private Mine _mine;
    [SerializeField] private float _throwForce;
    [SerializeField] private float _timerExplosion;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Grenade grenade = Instantiate(_grenadePrefab, _Camera.transform.position + transform.forward, Quaternion.identity);
            grenade.rb.AddForce(_Camera.transform.forward * _throwForce, ForceMode.Impulse);
            Destroy(grenade.gameObject, _timerExplosion);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Mine mine = Instantiate(_mine, _Camera.transform.position + (transform.forward * 2.5f), Quaternion.identity);
        }
    }
}