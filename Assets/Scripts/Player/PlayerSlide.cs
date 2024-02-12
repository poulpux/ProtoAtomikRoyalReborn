using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerCrounch))]
public class PlayerSlide : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _slidingForce;
    [SerializeField] private PlayerMovementAndCameraFPS _movementAndCameraFPS;
    private float horizontalInput;
    private float verticalInput;
    private bool _sliding;
    private bool _stopSliding = false;
    [HideInInspector] public float _timerCanDash = 0f;
    public float _timerCanDashSAVE;
    private bool _canCalculTimer = false;
    private bool _canDash = true;
    private UIScript uIScript;
    private PlayerCrounch crouch;

    private void Start()
    {
        crouch = GetComponent<PlayerCrounch>();
        uIScript = FindAnyObjectByType<UIScript>();
    }
    void Update()
    {
        Crounch();
        CalculTimerCanDash();
    }

    private void Crounch()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.F) && _movementAndCameraFPS._isRunning == true && _stopSliding == false && verticalInput > 0 && _movementAndCameraFPS.controler == CONTROLER.CLAVIER)
        {
            StartSlide();
        }
        else if (_movementAndCameraFPS != null && _movementAndCameraFPS.MyControler != null && _movementAndCameraFPS.MyControler.rightStickButton.IsPressed() == true && _movementAndCameraFPS._isRunning == true && _stopSliding == false && verticalInput > 0)
        {
            StartSlide();
        }
        if (_sliding == true)
        {
            SlidingMovement();
        }
    }

    private void StartSlide()
    {
        _sliding = true;
        transform.localScale = _movementAndCameraFPS.crounchScale;
    }

    private void SlidingMovement()
    {
        Vector3 inputDirection = transform.forward * verticalInput + transform.right * horizontalInput;

        if (_canDash == true)
        {
            _movementAndCameraFPS.IncreaseSpeed();
            _rigidbody.AddForce(inputDirection.normalized * _slidingForce, ForceMode.Force);
        }
        
        if (_canDash == false)
        {
            _movementAndCameraFPS.DecreaseSpeed();
        }

        if (_stopSliding == false)
        {
            StartCoroutine(CoroutineSlide());
            _stopSliding = true;
        }
    }

    private void StopSlide()
    {
        _canCalculTimer = true;
        _stopSliding = false;
        _sliding = false;
        Ray ray = new Ray(transform.position, transform.up);
        if (!Physics.Raycast(ray, out RaycastHit hit, 0.55f))
            transform.localScale = _movementAndCameraFPS.actualScale;
        else
            crouch.needToGoUP = true;
    }

    private void CalculTimerCanDash()
    {
        if (_canCalculTimer == true)
        {
            //uIScript.UpdateViewTimerDash();
            _timerCanDash += Time.deltaTime;
            if (_timerCanDash >= _timerCanDashSAVE)
            {
                _canDash = true;
                _canCalculTimer = false;
                _timerCanDash = 0;
            }
        }
    }

    private IEnumerator CoroutineSlide()
    {
        yield return new WaitForSeconds(0.25f);
        _canDash = false;
        StopSlide();
    }
}
