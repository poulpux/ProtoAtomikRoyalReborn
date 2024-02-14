using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CooldownBomb : MonoBehaviour
{
    [HideInInspector]
    public float timerBomb, timerMine;
    [HideInInspector]
    public int nbBomb, nbMine;

    public float cooldownBomb, cooldownMine;

    [SerializeField]
    private int nbMaxMine;
    [SerializeField] private int nbMaxBomb;

    public int id;

    [SerializeField]
    private GameObject Enviro;

    [SerializeField]
    private PlayerMovementAndCameraFPS player;
    [SerializeField] private Camera _Camera;
    [SerializeField] private Grenade _grenadePrefab;
    [SerializeField] private Mine _mine;
    [SerializeField] private float _throwForce;
    [HideInInspector]
    public bool mine = true;

    private float timer;
    
    public int maxHp = 100;


    [HideInInspector]
    public int hp;

    private bool doOneTime;

    void Start()
    {
        hp = maxHp;
        Tools.TimeScale(1f);
    }

    void Update()
    {
        timerBomb += Time.deltaTime;
        timerMine += Time.deltaTime;

        if(timerBomb > cooldownBomb && nbBomb < nbMaxBomb && hp > 0)
        {
            timerBomb = 0f;
            nbBomb++;
        }
        
        if(timerMine > cooldownMine && nbMine < nbMaxMine && hp > 0)
        {
            timerMine = 0f;
            nbMine++;
        }
        ThowNadeAndCo();

        if (hp <= 0 && !doOneTime)
        {
            _Camera.gameObject.transform.SetParent(Enviro.transform);
            Destroy(gameObject);
            doOneTime = true;   
        }
    }

    private void ThowNadeAndCo()
    {
        if (player.controler == CONTROLER.CLAVIER)
        {
            if (Input.GetMouseButtonDown(0) && nbBomb >0)
            {
                Grenade grenade = Instantiate(_grenadePrefab, _Camera.transform.position + transform.forward, Quaternion.identity);
                grenade.rb.AddForce(_Camera.transform.forward * _throwForce, ForceMode.Impulse);
                nbBomb--;
            }
            if (Input.GetMouseButtonDown(1) && mine && nbMine > 0)
            {
                Mine mine = Instantiate(_mine, _Camera.transform.position + (transform.forward * 2.5f), Quaternion.identity);
                mine.id = id;
                nbMine--;
            }
        }
        else
        {
            timer += Time.deltaTime;
            if (player.MyControler != null && player.MyControler.rightTrigger.IsPressed() == true && timer > 0.2f && nbBomb > 0)
            {
                Grenade grenade = Instantiate(_grenadePrefab, _Camera.transform.position + transform.forward, Quaternion.identity);
                grenade.rb.AddForce(_Camera.transform.forward * _throwForce, ForceMode.Impulse);
                timer = 0f;
                nbBomb--;
            }
            if (player.MyControler != null && player.MyControler.leftTrigger.IsPressed() == true && mine && timer > 0.2f && nbMine > 0)
            {
                Mine mine = Instantiate(_mine, _Camera.transform.position + (transform.forward * 2.5f), Quaternion.identity);
                timer = 0f;
                mine.id = id;
                nbMine--;
            }
        }


    }

    private void OnDestroy()
    {
        Explose();
        ExploRange();
    }

    private void ExploRange()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2f);
        foreach (Collider collider in colliders)
        {
            PousseObjects(collider);
        }
    }

    private void PousseObjects(Collider collider)
    {
        Rigidbody other = collider.GetComponent<Rigidbody>();
        //Manequin manequin = collider.GetComponent<Manequin>();
        if (other != null)
        {
            if (other.gameObject.tag == "Fragment")
                other.AddExplosionForce(3000 * 5f, transform.position, 2f, 3f);
            
        }
    }

    private void Explose()
    {
        ToolExplosion.BrokeObject(gameObject, transform, 3000, true, 0.7f);
    }

}
