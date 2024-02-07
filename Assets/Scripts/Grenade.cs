using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public Rigidbody rb;
    [SerializeField] private LayerMask _layer;

    private void OnDestroy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10, _layer);
        foreach (Collider collider in colliders)
        {
            Rigidbody other = collider.GetComponent<Rigidbody>();

            if (other)
            {
                other.AddExplosionForce(500, transform.position, 10, 3f);
            }
        }
    }
}