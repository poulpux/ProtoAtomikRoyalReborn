using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private LayerMask _layer;
    [HideInInspector] public float _radiusExplosion;
    [HideInInspector] public float _forceExplosion;
    void Start()
    {
        Debug.Log("explosion");
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radiusExplosion, _layer);
        foreach (Collider collider in colliders)
        {
            Rigidbody other = collider.GetComponent<Rigidbody>();

            if (other)
            {
                other.AddExplosionForce(_forceExplosion, transform.position, _radiusExplosion, 3f);
            }
        }
    }
}