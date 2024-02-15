using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementInput : MonoBehaviour
{
    public CONTROLER controler;
    [SerializeField] private Camera _camera;
    public Rigidbody rb;
    [SerializeField] private float jumpForce;
    [SerializeField] private InputActionAsset playerControls;
    [SerializeField] private float sprintMultiplier;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float smoothInputSpeed;
    [SerializeField] private PlayerSlideInput _playerSlideInput;
    private float moveSpeedSAVE;

    private float verticalRotation;
    private float speedMultiplier;
    private Vector2 currentInputVector;
    private Vector2 smoothInputVelocity;

    private InputAction moveAction;
    private InputAction lookAction;
    [HideInInspector] public InputAction jumpAction;
    private InputAction sprintAction;
    [HideInInspector] public InputAction crounchAction;
    [HideInInspector] public Vector2 moveInput;
    private Vector2 lookInput;

    [HideInInspector] public Vector3 crounchScale;
    [HideInInspector] public Vector3 actualScale;
    [HideInInspector] public bool _isRunning = false;
    [HideInInspector] public bool _isCrounch = false;

    [HideInInspector] public bool crounchIsTrigger = false;
    private bool stopBaisser = false;
    private bool stopMonter = false;
    public bool needToGoUP;
    float timerJump;

    private void Awake()
    {
        jumpAction = playerControls.FindActionMap("Player").FindAction("Jump");
        moveAction = playerControls.FindActionMap("Player").FindAction("Movement");
        lookAction = playerControls.FindActionMap("Player").FindAction("Look");
        sprintAction = playerControls.FindActionMap("Player").FindAction("Sprint");
        crounchAction = playerControls.FindActionMap("Player").FindAction("Crounch");

        moveAction.performed += context => moveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => moveInput = Vector2.zero;

        lookAction.performed += context => lookInput = context.ReadValue<Vector2>();
        lookAction.canceled += context => lookInput = Vector2.zero;

        crounchAction.started += context => crounchIsTrigger = true;
        crounchAction.canceled += context => crounchIsTrigger = false;
    }

    private void Start()
    {
        moveSpeedSAVE = moveSpeed;
        Cursor.lockState = CursorLockMode.Locked;

        crounchScale = new Vector3(transform.localScale.x, transform.localScale.y / 2f, transform.localScale.z);
        actualScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void OnEnable()
    {
        jumpAction.Enable();
        moveAction.Enable();
        lookAction.Enable();
        sprintAction.Enable();
        crounchAction.Enable();
    }
    private void OnDisable()
    {
        jumpAction.Disable();
        moveAction.Disable();
        lookAction.Disable();
        sprintAction.Disable();
        crounchAction.Disable();
    }

    private void Update()
    {
        HandleMovement();
        Jump();
        LookRotation();
        Crounch();
        allTimer();
    }

    private void HandleMovement()
    {
        if (sprintAction.ReadValue<float>() > 0f && _isCrounch == false)
        {
            IncreaseSpeed();
            _isRunning = true;
        }
        else
        {
            DecreaseSpeed();
            _isRunning = false;
        }

        if (_isCrounch == true)
            DecreaseSpeedWhileRun();

        currentInputVector = Vector2.SmoothDamp(currentInputVector, moveInput, ref smoothInputVelocity, smoothInputSpeed);
        Vector3 dir = Vector3.ClampMagnitude(new Vector3(currentInputVector.x, 0f, currentInputVector.y), 1f);
        rb.velocity = transform.localRotation * new Vector3(dir.x * moveSpeed, rb.velocity.y, dir.z * moveSpeed);
    }

    private void Jump()
    {
        if (jumpAction.triggered && CanJump() == true)
        {
            Jumps();
        }
    }

    private void LookRotation()
    {
        verticalRotation -= lookInput.y * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -90, 90);
        _camera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        transform.Rotate(0f, lookInput.x * mouseSensitivity, 0f);
        transform.Rotate(new Vector3(0f, lookInput.x, 0f) * mouseSensitivity * Time.deltaTime);
    }

    private void Crounch()
    {
        if (crounchIsTrigger == true)
        {
            if (_playerSlideInput._sliding == false)
                BeCrounch();
        }
        if (crounchIsTrigger == false)
        {
            NotBeCrounch();
        }
    }

    private void BeCrounch()
    {
        if (_playerSlideInput._canDash == false)
        {
            _isCrounch = true;
        }
        
        stopMonter = false;
        transform.localScale = crounchScale;
        if (stopBaisser == false)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            stopBaisser = true;
        }
    }
    private void NotBeCrounch()
    {
        _isCrounch = false;
        stopBaisser = false;
        if (stopMonter == false)
        {
            needToGoUP = true;
            NeedToWalk();
            stopMonter = true;
        }
    }

    private void NeedToWalk()
    {
        if (needToGoUP)
        {
            Ray ray = new Ray(transform.position, transform.up);
            if (!Physics.Raycast(ray, out RaycastHit hit, 0.55f))
                SeReleve();
        }
    }

    private void SeReleve()
    {
        DecreaseSpeed();
        _isCrounch = false;
        transform.localScale = actualScale;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        needToGoUP = false;
    }

    public float GetIncreaseSpeed()
    {
        return moveSpeed = sprintMultiplier;
    }
    public void IncreaseSpeed()
    {
        moveSpeed = sprintMultiplier;
    }
    public void DecreaseSpeed()
    {
        moveSpeed = moveSpeedSAVE;
    }
    public void DecreaseSpeedWhileRun()
    {
        moveSpeed = moveSpeedSAVE / 2f;
    }

    private void allTimer()
    {
        timerJump += Time.deltaTime;
    }

    private bool CanJump()
    {
        return testToucheGround() == true && timerJump > 0.1f ? true : false;
    }

    private void Jumps()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        timerJump = 0;
    }

    public bool testToucheGround()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        return Physics.Raycast(ray, out RaycastHit hit, transform.localScale.y + 0.2f, ~(1 << LayerMask.NameToLayer("Player"))) ? true : false;
    }
}