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
            rb.isKinematic = false;
            ToolExplosion.BrokeObject(gameObject, explo.transform, explo._forceExplosion, CleanFragement);
        }
        else if (IsDead())
            Destroy(gameObject);
    }

    private bool IsDead()
    {
        return hp <= 0;
    }
}
