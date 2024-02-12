using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldDestruction : MonoBehaviour
{
    private List<GameObject> parentsField = new List<GameObject>();
    [SerializeField]
    private GameObject sourceObject;
    void Start()
    {
        SetField();
    }

    void Update()
    {
        
    }

    private void SetField()
    {
        List<GameObject> allChild = new List<GameObject>();
        Transform[] allChildTrans = GetComponentsInChildren<Transform>();
        List<GameObject>ToCherchNeightBour = new List<GameObject>();
        foreach (Transform child in allChildTrans)
        {
            allChild.Add(child.gameObject);
        }
        parentsField.Add(new GameObject());
        parentsField[0].transform.SetParent(transform);

        //Instanciate(allChild, ToCherchNeightBour);

        //Scan(sourceObject);

        foreach (var item in allChild)
        {
            item.transform.SetParent(parentsField[0].transform);
            BoxCollider parentCollider = parentsField[0].AddComponent<BoxCollider>();
            parentCollider.size = item.transform.localScale;
            parentCollider.center = item.transform.localPosition;
        }
    }

    private void RecupCollider(GameObject parent, GameObject objet)
    {
        objet.transform.SetParent(parent.transform);
        BoxCollider parentCollider = parent.AddComponent<BoxCollider>();
        parentCollider.size = objet.transform.localScale;
        parentCollider.center = objet.transform.localPosition;
    }

    private void Instanciate(List<GameObject> allchild, List<GameObject> willScan)
    {
        RecupCollider(parentsField[0], allchild[0]);

        Scan(allchild[0], allchild, parentsField[0], willScan);
        allchild.Remove(allchild[0]);
    }

    private void Scan(GameObject objectToScan, List<GameObject> allChild, GameObject parent,List<GameObject> willScan)
    {
        Collider[] allCollider = Physics.OverlapBox(objectToScan.transform.position, objectToScan.transform.localScale);
        foreach (Collider child in allCollider)
        {
            int index = allChild.FindIndex(index => index.gameObject == child.gameObject);
            if (index != -1)
            {
                allChild[index].transform.SetParent(parent.transform);
                willScan.Add(allChild[index]);
            }
        }
    }
}
