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

    public Gamepad MyControler;
    public CONTROLER controler;
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
        if (controler == CONTROLER.CLAVIER)
            rotationX -= Input.GetAxis("Mouse Y") * mouseSensiY;
        else
            rotationX -= MyControler.rightStick.ReadValue().y * 5f;

        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        cam.transform.localRotation = Quaternion.Euler(rotationX * mouseSensiY, 0f, 0f);
    }
    //Fait rotater le joueur sur l'horizontale
    private void CamRotationY()
    {
        if(controler == CONTROLER.CLAVIER)
            transform.Rotate(0f, Input.GetAxis("Mouse X") * mouseSensiX, 0f);
        else
            transform.Rotate(0f, MyControler.rightStick.ReadValue().x * 5f * mouseSensiX, 0f);

    }
    private void ZQSDMouvement()
    {
        Vector3 dir = controler == CONTROLER.CLAVIER ? Vector3.ClampMagnitude(new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")), 1f) : Vector3.ClampMagnitude(new Vector3(MyControler.leftStick.ReadValue().x, 0f, MyControler.leftStick.ReadValue().y), 1f);
        rb.velocity = transform.localRotation * new Vector3(dir.x * spdMoovement, rb.velocity.y, dir.z * spdMoovement);
    }
    private void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift) && _isCrounch == false && controler == CONTROLER.CLAVIER)
        {
            Debug.Log("cours 2 ");
            IncreaseSpeed();
            _isRunning = true;
        }
        else if (MyControler != null && MyControler.leftStickButton.IsPressed() == true && _isCrounch == false && controler == CONTROLER.MANETTE)
        {
            Debug.Log("cours");
            IncreaseSpeed();
            _isRunning = true;
        }
        else if (MyControler != null && controler == CONTROLER.MANETTE)
        {
            DecreaseSpeed();
            _isRunning = false;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && controler == CONTROLER.CLAVIER)
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