using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private Explosion _explosion;
    [SerializeField] private float forceExplosion;
    [SerializeField] private float radiusExplosion;
    [SerializeField] private float _timerExplosion;
    [SerializeField] private int damage;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine(CoroutineIsPressed());
        }
    }

    private void OnDestroy()
    {
        Explosion explosion = Instantiate(_explosion, new Vector3(transform.position.x, transform.position.y + 0.75f, transform.position.z), Quaternion.identity);
        explosion._forceExplosion = forceExplosion;
        explosion._radiusExplosion = radiusExplosion;
        explosion.damage = damage;
    }

    private IEnumerator CoroutineIsPressed()
    {
        yield return new WaitForSeconds(_timerExplosion);
        Destroy(this.gameObject);
    }
}