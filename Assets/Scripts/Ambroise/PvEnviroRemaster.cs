using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Fracture))]
public class PvEnviroRemaster : MonoBehaviour
{
    [SerializeField]
    private bool isDestroyable = true;
    [SerializeField]
    private bool CleanFragement = true;
    [SerializeField]
    private int maxHp;
    [SerializeField]
    private float timeToDestruct = 5f;

    private int hp;
    private Fracture fracture;
    void Start()
    {
        hp = maxHp;
        fracture = GetComponent<Fracture>(); 
        fracture.enabled = false;
    }

    public void GetExplose(Explosion explo)
    {
        hp -= explo.damage;
        if (IsDead() && isDestroyable)
        {
            fracture.enabled = false;
            Debug.Log("mort");
            ToolExplosion.BrokeObject(gameObject, explo.transform, explo._forceExplosion, CleanFragement, timeToDestruct);
        }
        else if (IsDead())
            Destroy(gameObject);
    }

    private bool IsDead()
    {
        return hp <= 0;
    }
}
