using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    // Start is called before the first frame update
    void Start()
    {
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
        Vector2 dir = direction();
        dir.Normalize();
        rb.velocity = transform.localRotation * new Vector3(dir.x * spdMoovement, rb.velocity.y, dir.y * spdMoovement);
    }
    private Vector2 direction()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}