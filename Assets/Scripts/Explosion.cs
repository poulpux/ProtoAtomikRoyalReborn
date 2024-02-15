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
    [SerializeField] AudioSource _AudioSource;
    [SerializeField] AudioClip _Audioexplosion;
    [SerializeField] AudioClip _Audiohit;
    public CooldownBomb owner;
    public int id;
    void Start()
    {
        _AudioSource = FindFirstObjectByType<AudioSource>();
        _AudioSource.PlayOneShot(_Audioexplosion);
        GameObject particule = Instantiate(_particulePrefab, transform.position, Quaternion.identity);
        particule.transform.localScale = new Vector3(_radiusExplosion*2f,_radiusExplosion*2f,_radiusExplosion*2f);
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
        //Manequin manequin = collider.GetComponent<Manequin>();
        if (other != null)
        {
            if(other.tag != "Mur")
                other.AddExplosionForce(_forceExplosion, transform.position, _radiusExplosion*1.3f, 3f);
            if (other.gameObject.tag == "Fragment")
                other.AddExplosionForce(_forceExplosion * 5f, transform.position, _radiusExplosion*1.3f, 3f);
            if (other.gameObject.tag == "Props")
                other.AddExplosionForce(_forceExplosion * 5f, transform.position, _radiusExplosion*1.3f, 3f);
            if(other.gameObject.tag == "Player")
            {               
                CooldownBomb life = other.GetComponent<CooldownBomb>();
                if(life != null)
                {
                    _AudioSource.PlayOneShot(_Audiohit);
                    if (life.id != owner.id)
                        owner.hitFeedback();
                    life.hp -= 20;
                }
            }
            //if (other.gameObject != manequin)
            //{
            //    other.AddExplosionForce(_forceExplosion * 5f, transform.position, _radiusExplosion, 3f);
            //    manequin.TakeDamage(50);
            //}
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