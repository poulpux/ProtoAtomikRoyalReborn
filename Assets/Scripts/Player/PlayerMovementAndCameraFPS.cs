using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementAndCameraFPS : MonoBehaviour
{
    [Header("======Player======")]
    [Space(10)]
    [SerializeField] float spdMoovement = 5f;
    private Rigidbody rb;
    [Header("======Camera======")]
    [Space(10)]
    [SerializeField] float mouseSensiX = 5f;
    [SerializeField] float mouseSensiY = 5f;
    [SerializeField] Camera cam;
    [TextArea]
    public string AboutTool = "C'est un outil qui Garenti des mouvements pour FPS cleans dont:\r\n-Horizontal / Vertical pour le player.\r\n-Rotation de cam qui marche et la camera qui se bloque.";
    float rotationX;
    [SerializeField] private float _speedRun;
    private float _speedSave;
    [HideInInspector] public bool _isRunning = false;
    [SerializeField] private PlayerCrounch _playerCrounch;
    [HideInInspector] public Vector3 crounchScale;
    [HideInInspector] public Vector3 actualScale;
    [HideInInspector] public bool _isCrounch = false;

    [SerializeField]
    private bool Player2;
    public Gamepad MyControler;
    // Start is called before the first frame update
    void Start()
    {
        _speedSave = spdMoovement;
        rb = GetComponent<Rigidbody>();
        //On rend invisible la souris
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    // Update is called once per frame
    void Update()
    {
        CamRotation();
        ZQSDMouvement();
        Run();
    }
    //Will Make all rotation of cam
    private void CamRotation()
    {
        CamRotationX();
        CamRotationY();
    }
    //On rotate la souris et on la bloque en vertical
    private void CamRotationX()
    {
        rotationX -= Input.GetAxis("Mouse Y") * mouseSensiY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        cam.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }
    //Fait rotater le joueur sur l'horizontale
    private void CamRotationY()
    {
        transform.Rotate(0f, Input.GetAxis("Mouse X") * mouseSensiX, 0f);
    }
    private void ZQSDMouvement()
    {
        Vector3 dir = Vector3.ClampMagnitude(new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")), 1f);
        rb.velocity = transform.localRotation * new Vector3(dir.x * spdMoovement, rb.velocity.y, dir.z * spdMoovement);
    }
    private void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift) && _isCrounch == false)
        {
            IncreaseSpeed();
            _isRunning = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            DecreaseSpeed();
            _isRunning = false;
        }
        if (_isCrounch == true)
            DecreaseSpeedWhileRun();

    }
    public float GetIncreaseSpeed()
    {
        return spdMoovement = _speedRun;
    }
    public void IncreaseSpeed()
    {
        spdMoovement = _speedRun;
    }
    public void DecreaseSpeed()
    {
        spdMoovement = _speedSave;
    }
    public void DecreaseSpeedWhileRun()
    {
        spdMoovement = _speedSave / 2f;
    }
}