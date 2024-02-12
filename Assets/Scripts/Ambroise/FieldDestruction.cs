using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class FieldDestruction : MonoBehaviour
{
    [SerializeField]
    private GameObject parentPrefab;

    private List<GameObject> parentsField = new List<GameObject>();
    public List<GameObject> allChild = new List<GameObject>();

    [SerializeField]
    private bool OnStart;

    void Start()
    {
        if (OnStart)
        {
            RecupChild();
            List<GameObject> list = allChild;
            FirstFilonAssign(list, 0);
            //SetField();
        }
    }

    void Update()
    {
        
    }

    //public void ReFaitTout()
    //{
    //    RecupChild();
    //    List<GameObject> list = allChild;
    //    FirstFilonAssign(list, 0);
    //}

    private void FirstFilonAssign(List<GameObject> list, int nb)
    {
        GameObject a = Instantiate(parentPrefab);
        parentsField.Add(a);
        parentsField[nb].transform.SetParent(transform);
        allChild[0].transform.SetParent(parentsField[nb].transform);
        //RecupCollider(parentsField[nb], allChild[0]);
        allChild[0].GetComponent<PvEnviro>().isPassé = true;
        list.Remove(allChild[0]);
        int nbTour = 0;
        while (nbTour < 10 /*|| list.Count == 0*/)
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
                        item.transform.SetParent(parentsField[nb].transform);
                        //RecupCollider(parentsField[nb], item);
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

        List<GameObject> allChilds = new List<GameObject>();
        Transform[] allChildTrans = parentsField[nb].GetComponentsInChildren<Transform>();
        List<GameObject> ToCherchNeightBour = new List<GameObject>();
        foreach (Transform child in allChildTrans)
        {
            if (child.GetComponent<PvEnviro>() != null)
            {
                allChilds.Add(child.gameObject);
                child.GetComponent<PvEnviro>().parent = parentsField[nb].GetComponent<DestructionParMur>();
                child.GetComponent<PvEnviro>().FieldParent = this;
            }
        }
        parentsField[nb].GetComponent<DestructionParMur>().childs = allChilds;

        if (list.Count > 0)
            FirstFilonAssign(list, nb+1);
    }

    private void RecupChild()
    {
        allChild = new List<GameObject>();
        Transform[] allChildTrans = GetComponentsInChildren<Transform>();
        List<GameObject> ToCherchNeightBour = new List<GameObject>();
        foreach (Transform child in allChildTrans)
        {
            if (child.GetComponent<PvEnviro>() != null)
            {
                allChild.Add(child.gameObject);
            }
        }
        SortByDistance();
    }

    private List<GameObject> SortByDistance()
    {
        List<GameObject> sortedGameObjects = allChild.OrderBy(go => Vector3.Distance(go.transform.position, allChild[0].transform.position)).ToList();

        return sortedGameObjects;
    }
}
