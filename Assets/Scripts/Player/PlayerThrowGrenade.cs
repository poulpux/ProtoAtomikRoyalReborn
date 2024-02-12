using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThr : MonoBehaviour
{
    [SerializeField] private Camera _Camera;
    [SerializeField] private Grenade _grenadePrefab;
    [SerializeField] private Mine _mine;
    [SerializeField] private float _throwForce;
    [HideInInspector] public bool mine = true;
    [SerializeField] private LayerMask _hit;
    [SerializeField] private float _distancePauseMine;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Grenade grenade = Instantiate(_grenadePrefab, _Camera.transform.position + transform.forward, Quaternion.identity);
            grenade.rb.AddForce(_Camera.transform.forward * _throwForce, ForceMode.Impulse);
        }
        if (Input.GetMouseButtonDown(1)/* && mine*/)
        {
            Ray ray = new Ray(_Camera.transform.position, _Camera.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, _distancePauseMine, _hit))
            {
                Mine mine = Instantiate(_mine, hit.point, Quaternion.LookRotation(hit.normal));                
            }           
        }
    }
}