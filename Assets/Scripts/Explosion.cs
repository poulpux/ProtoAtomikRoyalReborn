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
    [HideInInspector] public int damage;
    void Start()
    {
        GameObject particule = Instantiate(_particulePrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        Explose();
        ExploRange();
    }

    private void ExploRange()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radiusExplosion);
        foreach (Collider collider in colliders)
        {
            ExploseOtherMines(collider);
            PousseObjects(collider);
        }
    }

    private void PousseObjects(Collider collider)
    {
        Rigidbody other = collider.GetComponent<Rigidbody>();
        PlayerStats playerStats = collider.GetComponent<PlayerStats>();
        if (other != null)
        {
            if(other.tag != "Mur")
                other.AddExplosionForce(_forceExplosion, transform.position, _radiusExplosion, 3f);
            if (other.gameObject.tag == "Fragment")
                other.AddExplosionForce(_forceExplosion * 5f, transform.position, _radiusExplosion, 3f);
            
        }
        if (playerStats != null)
        {
            playerStats._pv -= 20f;
        }
    }

    private void ExploseOtherMines(Collider collider)
    {
        if (collider.tag == "Mine")
            Destroy(collider.gameObject);
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
            PvEnviro pvEnviro = item.GetComponent<PvEnviro>();
            if (pvEnviro != null)
                pvEnviro.GetExplose(this);
        }
    }

    private int CompareDistance(GameObject a, GameObject b)
    {
        float distanceA = Vector3.Distance(transform.position, a.transform.position);
        float distanceB = Vector3.Distance(transform.position, b.transform.position);

        return distanceA.CompareTo(distanceB);
    }
}