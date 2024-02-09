using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestructionParMur : MonoBehaviour
{
    [SerializeField]
    private List< GameObject> childs = new List<GameObject>();

    [HideInInspector]
    public UnityEvent<GameObject> DestroyColliderEvent = new UnityEvent<GameObject>();

    void Start()
    {
        CombineChildBoxColliders();

        DestroyColliderEvent.AddListener((objet) => { childs.Remove(objet); RemoveBoxCollidersFromChildren(); CombineChildBoxColliders(); });
    }

    void Update()
    {
    }

    void CombineChildBoxColliders()
    {
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

        // Supprimer tous les Box Colliders des enfants
        foreach (BoxCollider childCollider in childColliders)
        {
            Destroy(childCollider);
        }
    }
}
