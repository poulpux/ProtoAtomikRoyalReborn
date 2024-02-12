using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class DestructionParMur : MonoBehaviour
{
    private Rigidbody rb;
    [HideInInspector]
    public List< GameObject> childs = new List<GameObject>();
    private int nbChild;

    [HideInInspector]
    public UnityEvent<GameObject> DestroyColliderEvent = new UnityEvent<GameObject>();

    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        CombineChildBoxColliders();
        nbChild = childs.Count;
        DestroyColliderEvent.AddListener((objet) => { childs.Remove(objet); RemoveBoxCollidersFromChildren(); CombineChildBoxColliders(); });
    }

    void Update()
    {
    }

    void CombineChildBoxColliders()
    {
        if(childs.Count == 0)
            Destroy(gameObject, 15f);
        if (childs.Count < nbChild / 2 && rb.isKinematic)
        {
            rb.isKinematic = false;
        }


        foreach (var item in childs)
        {
            BoxCollider parentCollider = gameObject.AddComponent<BoxCollider>();
            parentCollider.size = item.transform.localScale;
            parentCollider.center = item.transform.localPosition;
        }
    }

    void RemoveBoxCollidersFromChildren()
    {
        // Récupérer tous les Box Colliders des enfants
        BoxCollider[] childColliders = GetComponents<BoxCollider>();

        if (childColliders.Length == 0)
        {
            return;
        }

        foreach (BoxCollider childCollider in childColliders)
        {
            Destroy(childCollider);
        }
    }
}
