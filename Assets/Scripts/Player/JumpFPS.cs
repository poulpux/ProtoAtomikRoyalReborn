using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class JumpFPS : MonoBehaviour
{
    [Header("======Jump======")]
    [Space(10)]
    [SerializeField] private float jumpPow = 5f;
    Rigidbody rb;
    [TextArea]
    public string AboutTool = "Attention : Mettre la layer (Player) au Player. \nFonction de saut opti, detection du sol grace a un raycasting aux pied du personnage. ";
    float timerJump;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        allTimer();
        if (Input.GetButtonDown("Jump") && CanJump() == true)
            Jump();
    }
    private void allTimer()
    {
        timerJump += Time.deltaTime;
    }
    private bool CanJump()
    {
        return testToucheGround() == true && timerJump > 0.1f ? true : false;
    }
    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpPow, ForceMode.Impulse);
        timerJump = 0;
    }
    private bool testToucheGround()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        return Physics.Raycast(ray, out RaycastHit hit, transform.localScale.y + 0.2f, ~(1 << LayerMask.NameToLayer("Player"))) ? true : false;
    }
}
