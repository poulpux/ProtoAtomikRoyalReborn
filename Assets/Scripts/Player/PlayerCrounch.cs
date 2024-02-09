using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrounch : MonoBehaviour
{
    [SerializeField] private PlayerMovementAndCameraFPS _playerMovementAndCameraFPS;
    private bool needToGoUP;

    void Start()
    {
        _playerMovementAndCameraFPS.crounchScale = new Vector3(transform.localScale.x, transform.localScale.y / 2f, transform.localScale.z);
        _playerMovementAndCameraFPS.actualScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _playerMovementAndCameraFPS._isRunning == false)
        {
            _playerMovementAndCameraFPS._isCrounch = true;
            transform.localScale = _playerMovementAndCameraFPS.crounchScale;
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        }
        if (Input.GetKeyUp(KeyCode.F) && _playerMovementAndCameraFPS._isRunning == false)
        {
            Ray ray = new Ray(transform.position, transform.up);
            if (!Physics.Raycast(ray, out RaycastHit hit, 0.55f))
                SeReleve();
            else
                needToGoUP = true;
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
