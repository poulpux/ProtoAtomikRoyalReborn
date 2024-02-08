using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private GameObject _particulePrefab;
    [SerializeField] private LayerMask _layer;
    [HideInInspector] public float _radiusExplosion;
    [HideInInspector] public float _forceExplosion;
    void Start()
    {
        GameObject particule = Instantiate(_particulePrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        PousseExplo();
        Explose();
    }

    private void PousseExplo()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radiusExplosion);
        Debug.Log(colliders.Length);
        foreach (Collider collider in colliders)
        {
            Rigidbody other = collider.GetComponent<Rigidbody>();

            if (other != null)
                other.AddExplosionForce(_forceExplosion,transform.position, _radiusExplosion,3f);
        }
    }

    private void Explose()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radiusExplosion);
        GameObject[] nearbyObjects = new GameObject[colliders.Length];

        for (int i = 0; i < colliders.Length; i++)
            nearbyObjects[i] = colliders[i].gameObject;
        Array.Sort(nearbyObjects, CompareDistance);

        foreach (var item in nearbyObjects)
        {
            ToolExplosion.BrokeObject(item, transform, _forceExplosion);
        }
    }

    private int CompareDistance(GameObject a, GameObject b)
    {
        float distanceA = Vector3.Distance(transform.position, a.transform.position);
        float distanceB = Vector3.Distance(transform.position, b.transform.position);

        return distanceA.CompareTo(distanceB);
    }
}