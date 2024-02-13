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
    [SerializeField] private LayerMask _hit;
    [SerializeField] private float maxDistanceRaycast;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private LayerMask _hitJoint;

    private void Start()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        bool cast = Physics.Raycast(ray, out RaycastHit hit, maxDistanceRaycast);
        Vector3 hitposition = cast ? hit.point : transform.position + transform.forward * maxDistanceRaycast;

        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, hitposition);

    }

    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistanceRaycast, _hit))
        {
            if (hit.collider.gameObject.GetComponent<PlayerMovementAndCameraFPS>())
            {
                StartCoroutine(CoroutineIsPressed(_timerExplosion));
            }
        }

        Ray ray2 = new Ray(transform.position, -transform.forward);
        if (!(Physics.Raycast(ray2, out RaycastHit hit2, 1f, _hitJoint)))
        {
            StartCoroutine(CoroutineIsPressed(0f));
        }
    }

    private void OnDestroy()
    {
        Explosion explosion = Instantiate(_explosion, new Vector3(transform.position.x, transform.position.y + 0.75f, transform.position.z), Quaternion.identity);
        explosion._forceExplosion = forceExplosion;
        explosion._radiusExplosion = radiusExplosion;
        explosion.damage = damage;
    }

    private IEnumerator CoroutineIsPressed(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(this.gameObject);
    }
}