using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FieldDestruction : MonoBehaviour
{
    private List<GameObject> parentsField = new List<GameObject>();
    [SerializeField]
    private GameObject sourceObject;
    List<GameObject> allChild = new List<GameObject>();

    void Start()
    {
        RecupChild();
        FirstFilonAssign();
        //SetField();
    }

    void Update()
    {
        
    }
    private void FirstFilonAssign()
    {
        List<GameObject> list = allChild;
        allChild[0].transform.SetParent(parentsField[0].transform);
        allChild[0].GetComponent<PvEnviro>().isPassé = true; 
        int nbTour = 0;
        while (nbTour < 0 /*|| list.Count == 0*/)
        {
            List<GameObject> toRemove = new List<GameObject>();

            foreach (var item in list)
            {
                PvEnviro connect = item.GetComponent<PvEnviro>();
                foreach (var voisin in connect.listNeightBour)
                {
                    PvEnviro poto = voisin.GetComponent<PvEnviro>();
                    if (poto.isPassé)
                    {
                        toRemove.Add(item);
                        item.transform.SetParent(parentsField[0].transform);
                        connect.isPassé = true;
                    }
                }
            }
            nbTour++;
            foreach (var item in toRemove)
            {
                list.Remove(item);
            }
        }
    }

    private void SetField()
    {
        allChild = new List<GameObject>();
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

    private void RecupChild()
    {
        allChild = new List<GameObject>();
        Transform[] allChildTrans = GetComponentsInChildren<Transform>();
        List<GameObject> ToCherchNeightBour = new List<GameObject>();
        foreach (Transform child in allChildTrans)
        {
            if(child.GetComponent<PvEnviro>()!=null)
                allChild.Add(child.gameObject);
        }
        parentsField.Add(new GameObject());
        parentsField[0].transform.SetParent(transform);
        SortByDistance();
    }

    private void RecupCollider(GameObject parent, GameObject objet)
    {
        objet.transform.SetParent(parent.transform);
        BoxCollider parentCollider = parent.AddComponent<BoxCollider>();
        parentCollider.size = objet.transform.localScale;
        parentCollider.center = objet.transform.localPosition;
    }

    private List<GameObject> SortByDistance()
    {
        List<GameObject> sortedGameObjects = allChild.OrderBy(go => Vector3.Distance(go.transform.position, allChild[0].transform.position)).ToList();

        return sortedGameObjects;
    }
}
