using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class TestExplo : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            tryBrockCube();
    }

    private void tryBrockCube()
    {
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
        if(Physics.Raycast(ray, out RaycastHit hit, 10f, (1 << LayerMask.NameToLayer("Destructible"))))
           BrokeCube(hit.collider.gameObject);
    }

    private void BrokeCube(GameObject objet)
    {
        Debug.Log(objet.name);
        Fracture fracture = objet.GetComponent<Fracture>();
        fracture.ComputeFracture();

        fracture.fragmentRoot.GetComponentInChildren<Rigidbody>().AddForce(Vector3.up*0.2f + transform.rotation.eulerAngles , ForceMode.Impulse);
    }
}
