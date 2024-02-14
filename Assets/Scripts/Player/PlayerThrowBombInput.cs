using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerThrowBombInput : MonoBehaviour
{
    [SerializeField] private InputActionAsset playerControls;
    [SerializeField] private Camera _Camera;
    [SerializeField] private Grenade _grenadePrefab;
    [SerializeField] private Mine _mine;
    [SerializeField] private float _throwForce;

    private InputAction shootBombeAction;
    private InputAction shootMineAction;

    void Awake()
    {
        shootBombeAction = playerControls.FindActionMap("Player").FindAction("ShootBombe");
        shootMineAction = playerControls.FindActionMap("Player").FindAction("ShootMine");
    }

    private void OnEnable()
    {
        shootBombeAction.Enable();
        shootMineAction.Enable();
    }
    private void OnDisable()
    {
        shootBombeAction.Disable();
        shootMineAction.Disable();
    }

    void Update()
    {
        if (shootBombeAction.triggered)
        {
            Grenade grenade = Instantiate(_grenadePrefab, _Camera.transform.position + transform.forward, Quaternion.identity);
            grenade.rb.AddForce(_Camera.transform.forward * _throwForce, ForceMode.Impulse);
        }
        if (shootMineAction.triggered)
        {
            Mine mine = Instantiate(_mine, _Camera.transform.position + (transform.forward * 2.5f), Quaternion.identity);
        }
    }
}
