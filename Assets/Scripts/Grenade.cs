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
    [SerializeField] private int damage;

    private void OnDestroy()
    {
        Explosion explosion = Instantiate(_explosion, new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), Quaternion.identity);
        explosion._forceExplosion = forceExplosion;
        explosion._radiusExplosion = radiusExplosion;
        explosion.damage = damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision != null)
        {
            Destroy(this.gameObject);
        }
    }
}