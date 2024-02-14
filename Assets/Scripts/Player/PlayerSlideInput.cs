using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class PlayerSlideInput : MonoBehaviour
{
    [SerializeField] private PlayerMovementInput _playerMovementInput;
    [SerializeField] private float _slidingForce;

    [HideInInspector] public bool _sliding;
    private bool _stopSliding = false;
    [HideInInspector] public float _timerCanDash = 0f;
    public float _timerCanDashSAVE;
    private bool _canCalculTimer = false;
    [HideInInspector] public bool _canDash = true;

    void Update()
    {
        CrounchAndSlide();
        CalculTimerCanDash();      
    }

    private void CrounchAndSlide()
    {
        if (_playerMovementInput.crounchIsTrigger == true && _playerMovementInput._isRunning == true && _stopSliding == false)
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
        transform.localScale = _playerMovementInput.crounchScale;
    }

    private void SlidingMovement()
    {
        Vector3 inputDirection = transform.forward * _playerMovementInput.moveInput.y + transform.right * _playerMovementInput.moveInput.x;

        if (_canDash == true)
        {
            _playerMovementInput.IncreaseSpeed();
            _playerMovementInput.rb.AddForce(inputDirection.normalized * _slidingForce, ForceMode.Force);
        }

        if (_canDash == false)
        {
            _playerMovementInput.DecreaseSpeed();
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
            transform.localScale = _playerMovementInput.actualScale;
        else
            _playerMovementInput.needToGoUP = true;
    }

    private void CalculTimerCanDash()
    {
        if (_canCalculTimer == true)
        {
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