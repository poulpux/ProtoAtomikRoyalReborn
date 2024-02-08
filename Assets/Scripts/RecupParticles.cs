using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecupParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem shooteffect;


    void Start()
    {
        shooteffect.startLifetime = 0;
    }


    void Update()
    {
        
    }
}