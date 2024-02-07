using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolExplosion : MonoBehaviour
{
    static public void BrokeObject(GameObject objetToDestroy, Transform bombTransform, float power)
    {
        Vector3 oppositeDirection = (objetToDestroy.transform.position - bombTransform.position).normalized;
        Debug.Log(objetToDestroy.name);
        Fracture fracture = objetToDestroy.GetComponent<Fracture>();
        fracture.ComputeFracture();

        fracture.fragmentRoot.GetComponentInChildren<Rigidbody>().AddForce(oppositeDirection * power, ForceMode.Impulse);
    }
}
