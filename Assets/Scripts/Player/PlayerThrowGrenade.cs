using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

public class PlayerThr : MonoBehaviour
{
    [SerializeField]
    private PlayerMovementAndCameraFPS player;
    [SerializeField] private Camera _Camera;
    [SerializeField] private Grenade _grenadePrefab;
    [SerializeField] private Mine _mine;
    [SerializeField] private float _throwForce;
    [HideInInspector] public bool mine = true;
    [SerializeField] private LayerMask _hit;
    [SerializeField] private float _distancePauseMine;

    private float timer;

    void Update()
    {
        if (player.controler == CONTROLER.CLAVIER)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Grenade grenade = Instantiate(_grenadePrefab, _Camera.transform.position + transform.forward, Quaternion.identity);
                grenade.rb.AddForce(_Camera.transform.forward * _throwForce, ForceMode.Impulse);
            }
            if (Input.GetMouseButtonDown(1) && mine)
            {
                Mine mine = Instantiate(_mine, _Camera.transform.position + (transform.forward * 2.5f), Quaternion.identity);
            }
        }
        else
        {
            timer += Time.deltaTime;
            if (player.MyControler != null && player.MyControler.rightTrigger.IsPressed() == true && timer > 0.2f)
            {
                Grenade grenade = Instantiate(_grenadePrefab, _Camera.transform.position + transform.forward, Quaternion.identity);
                grenade.rb.AddForce(_Camera.transform.forward * _throwForce, ForceMode.Impulse);
                timer = 0f;
            }
            if (player.MyControler != null && player.MyControler.leftTrigger.IsPressed() == true && mine && timer > 0.2f)
            {
                Ray ray = new Ray(_Camera.transform.position, _Camera.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, _distancePauseMine, _hit))
            {
                Mine mine = Instantiate(_mine, hit.point, Quaternion.LookRotation(hit.normal));                
            }
                timer = 0f;
            }
        }
    }
}