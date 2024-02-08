using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecupParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem shooteffect;
    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > shooteffect.startLifetime)
            Destroy(this.gameObject);
    }
}