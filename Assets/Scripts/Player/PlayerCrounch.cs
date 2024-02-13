using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCrounch : MonoBehaviour
{
    [SerializeField] private PlayerMovementAndCameraFPS _playerMovementAndCameraFPS;
    [HideInInspector]
    public bool needToGoUP;
    private bool enBasLa;
    private float timer;

    void Start()
    {
        _playerMovementAndCameraFPS.crounchScale = new Vector3(transform.localScale.x, transform.localScale.y / 2f, transform.localScale.z);
        _playerMovementAndCameraFPS.actualScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.F) && _playerMovementAndCameraFPS._isRunning == false && _playerMovementAndCameraFPS.controler == CONTROLER.CLAVIER)
        {
            needToGoUP = false;
            _playerMovementAndCameraFPS._isCrounch = true;
            transform.localScale = _playerMovementAndCameraFPS.crounchScale;
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        }


        if (Input.GetKeyUp(KeyCode.F) && _playerMovementAndCameraFPS._isRunning == false && _playerMovementAndCameraFPS.controler == CONTROLER.CLAVIER)
        {
            Ray ray = new Ray(transform.position, transform.up);
            if (!Physics.Raycast(ray, out RaycastHit hit, 0.55f))
                SeReleve();
            else
                needToGoUP = true;
        }
        if(_playerMovementAndCameraFPS.controler == CONTROLER.MANETTE)
        {
            if(_playerMovementAndCameraFPS != null && _playerMovementAndCameraFPS.MyControler != null && _playerMovementAndCameraFPS.MyControler.rightStickButton.IsPressed() == true && _playerMovementAndCameraFPS._isRunning == false && timer > 0.2f)
            {
                if(enBasLa)
                {
                    needToGoUP = true;
                }
                else
                {
                    needToGoUP = false;
                    _playerMovementAndCameraFPS._isCrounch = true;
                    transform.localScale = _playerMovementAndCameraFPS.crounchScale;
                    transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
                }
                enBasLa = !enBasLa;
                timer = 0;
            }

        }

        NeedToWalk();
    }

    private void NeedToWalk()
    {
        if(needToGoUP)
        {
            Ray ray = new Ray(transform.position, transform.up);
            if (!Physics.Raycast(ray, out RaycastHit hit, 0.55f))
                SeReleve();
        }
    }

    private void SeReleve()
    {
        _playerMovementAndCameraFPS.DecreaseSpeed();
        _playerMovementAndCameraFPS._isCrounch = false;
        transform.localScale = _playerMovementAndCameraFPS.actualScale;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        needToGoUP = false;
    }
}
