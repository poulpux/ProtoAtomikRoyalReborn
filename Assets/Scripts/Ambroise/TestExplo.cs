using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (Physics.Raycast(ray, out RaycastHit hit, 10f, (1 << LayerMask.NameToLayer("Destructible"))))
            ToolExplosion.BrokeObject(hit.collider.gameObject, transform, 100f);
    }
}
