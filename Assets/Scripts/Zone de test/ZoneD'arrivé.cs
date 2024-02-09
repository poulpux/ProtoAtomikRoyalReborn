using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneDarrivé : MonoBehaviour
{
    [SerializeField]
    ParcoursDuCombattantLauncher parcours;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
            parcours.LevelFinish();
    }
}
