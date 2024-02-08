using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public Rigidbody rb;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private Explosion _explosion;
    [SerializeField] private float forceExplosion;
    [SerializeField] private float radiusExplosion;

    private void OnDestroy()
    {
        Explosion explosion = Instantiate(_explosion, transform.position, Quaternion.identity);
        explosion._forceExplosion = forceExplosion;
        explosion._radiusExplosion = radiusExplosion;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision != null)
        {
            Destroy(this.gameObject);
        }
    }
}