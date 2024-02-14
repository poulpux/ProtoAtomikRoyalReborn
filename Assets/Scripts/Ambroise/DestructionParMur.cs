using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class DestructionParMur : MonoBehaviour
{
    private Rigidbody rb;
    public List< GameObject> childs = new List<GameObject>();
    public List<Collider> childsColliders = new List<Collider>();
    private int nbChild;

    [HideInInspector]
    public UnityEvent<GameObject> DestroyColliderEvent = new UnityEvent<GameObject>();

    private bool firstUpdate;

    void Start()
    {
    }

    void Update()
    {
        if (!firstUpdate)
        {
            rb = GetComponent<Rigidbody>();
            CombineChildBoxColliders();
            nbChild = childs.Count;
            DestroyColliderEvent.AddListener((objet) => AccurencyDestroyCollider(objet));
            firstUpdate = true;
        }
    }

    private void AccurencyDestroyCollider(GameObject toDestroy)
    {
        int index = childs.FindIndex(objet => objet.gameObject == toDestroy);
        childs.RemoveAt(index);
        Destroy(childsColliders[index]);
        childsColliders.RemoveAt(index);
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
            childsColliders.Add(parentCollider);
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
