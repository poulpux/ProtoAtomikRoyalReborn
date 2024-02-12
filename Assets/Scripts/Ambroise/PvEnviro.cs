using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Fracture))]
public class PvEnviro : MonoBehaviour
{
    [SerializeField]
    private bool isDestroyable = true;
    [SerializeField]
    private bool CleanFragement = true;
    [SerializeField]
    private int maxHp;
    [SerializeField]
    private float timeToDestruct = 5f;

    [SerializeField]
    DestructionParMur parent;
    private int hp;
    private Rigidbody rb;
    void Start()
    {
        hp = maxHp;
        rb = GetComponent<Rigidbody>();
    }

    

    public void GetExplose(Explosion explo)
    {
        hp -= explo.damage;
        if (IsDead() && isDestroyable)
        {
            if(parent != null) 
                parent.DestroyColliderEvent.Invoke(gameObject);
            rb.isKinematic = false;
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
