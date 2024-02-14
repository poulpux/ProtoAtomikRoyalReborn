using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
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

    public List<GameObject> listNeightBour = new List<GameObject>();
    public bool isPassé;

    [SerializeField]
    private bool GetNeightbour;

    public DestructionParMur parent;
    private int hp;
    private Rigidbody rb;

    public FieldDestruction FieldParent;
    void Awake()
    {
        hp = maxHp;
        rb = GetComponent<Rigidbody>();
        if (GetNeightbour)
            getAllNeightbour();
    }

    public void GetExplose(Explosion explo)
    {
        hp -= explo.damage;
        if (IsDead() && isDestroyable)
        {
            if (parent != null)
                parent.DestroyColliderEvent.Invoke(gameObject);
            if (FieldParent != null)
                FieldParent.ActiveTimer();
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

    public void getAllNeightbour()
    {
        listNeightBour.Clear();
        Ray[] ray = new Ray[6];
        Vector3 right = transform.right;
        Vector3 forward = transform.forward;
        Vector3 position = transform.position;

        ray[0] = new Ray(transform.position, transform.forward);
        ray[1] = new Ray(transform.position, -transform.forward);
        ray[2] = new Ray(transform.position, transform.up);
        ray[3] = new Ray(transform.position, -transform.up);
        ray[4] = new Ray(transform.position, transform.right);
        ray[5] = new Ray(transform.position, -transform.right);

        RaycastHit hit;

        for (int i = 0; i < ray.Length; i++)
        {
            float longueur = i <= 1 ? transform.localScale.z : i <= 3 ? transform.localScale.y : transform.localScale.x;
            if (Physics.Raycast(ray[i], out hit, longueur + 0.2f, (1 << LayerMask.NameToLayer("Stuctures"))))
            {
                if (hit.collider.tag == "Mur")
                    listNeightBour.Add(hit.collider.gameObject);
            }
        }
    }
}
